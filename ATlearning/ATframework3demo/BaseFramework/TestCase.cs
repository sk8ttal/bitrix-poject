using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.PageObjects;
using atFrameWork2.SeleniumFramework;
using atFrameWork2.TestEntities;
using ATframework3demo.BaseFramework;
using ATframework3demo.PageObjects.Mobile;

namespace atFrameWork2.BaseFramework
{
    public class TestCase
    {
        public static TestCase RunningTestCase { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title">Название тесткейса</param>
        /// <param name="body">Ссылка на метод тела кейса</param>
        /// <exception cref="ArgumentNullException"></exception>
        public TestCase(string title, Action<PortalHomePage> body)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Body = body ?? throw new ArgumentNullException(nameof(body));
            Node = new TestCaseTreeNode(title);
            EnvType = TestCaseEnvType.Web;
        }

        public TestCase(string title, Action<MobileHomePage> body)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            MobileBody = body ?? throw new ArgumentNullException(nameof(body));
            Node = new TestCaseTreeNode(title);
            EnvType = TestCaseEnvType.Mobile;
        }

        int logCounter = 0;

        public void Execute(PortalInfo testPortal, Action uiRefresher)
        {
            Status = TestCaseStatus.running;
            uiRefresher.Invoke();
            RunningTestCase = this;
            logCounter++;
            CaseLogPath = Path.Combine(Environment.CurrentDirectory, $"caselog{DateTime.Now:ddMMyyyyHHmmss}{logCounter}.html");
            Log.WriteHtmlHeader(CaseLogPath);
            uiRefresher.Invoke();

            try
            {
                Log.Info($"---------------Запуск кейса '{Title}'---------------");
                if (EnvType == TestCaseEnvType.Web)
                {
                    var portalLoginPage = new PortalLoginPage(testPortal);
                    var homePage = portalLoginPage.Login(testPortal.PortalAdmin);
                    Body.Invoke(homePage);
                }
                else
                {
                    var loginPage = new MobileLoginPage(testPortal);
                    var homePage = loginPage.Login(testPortal.PortalAdmin);
                    MobileBody.Invoke(homePage);
                }

            }
            catch (Exception e)
            {
                Log.Error($"Кейс не пройден, причина:{Environment.NewLine}{e}");
            }

            Log.Info($"---------------Кейс '{Title}' завершён---------------");

            try
            {
                if (BaseItem._defaultDriver != default)
                {
                    BaseItem.DefaultDriver.Quit();
                    BaseItem.DefaultDriver = default;
                }
            }
            catch (Exception) { }

            if (CaseLog.Any(x => x is LogMessageError))
                Status = TestCaseStatus.failed;
            else
                Status = TestCaseStatus.passed;

            RunningTestCase = default;
            uiRefresher.Invoke();
        }

        public string Title { get; set; }
        Action<PortalHomePage> Body { get; set; }
        Action<MobileHomePage> MobileBody { get; set; }
        public TestCaseTreeNode Node { get; set; }
        public string CaseLogPath { get; set; }
        public List<LogMessage> CaseLog { get; } = new List<LogMessage>();
        public TestCaseStatus Status { get; set; }
        public TestCaseEnvType EnvType { get; set; }
    }

    public enum TestCaseEnvType
    {
        Web,
        Mobile
    }
}
