using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.PageObjects;
using atFrameWork2.TestEntities;
using ATframework3demo.PageObjects;
using aTframework3demo.TestEntities;
using aTframework3demo.PageObjects.Forms;


namespace aTframework3demo.TestCases.Forms
{
    public class Case_Bitrix24_Statistic_Form_Passing : CaseCollectionBuilder
    {
        protected override List<TestCase> GetCases()
        {
            var caseCollection = new List<TestCase>();
            caseCollection.Add(new TestCase("FORMS: Прохождение опроса сотрудником", homePage => FormPassing(homePage)));
            return caseCollection;
        }
        public void FormPassing(PortalHomePage homePage)
        {
            UserForTests User = new UserForTests()
            {
                Login = "Oleg",
                Password = "Qwerty123&",
                Uri = new Uri("http://dev.bx/")
            };

            TaskFromForm Participants = new TaskFromForm(
                "Олег Иванов",
                "Михаил Виктор",
                "Vlad Zorin"
            );

            DateOnly Date = DateOnly.Parse(DateTime.Now.ToShortDateString());
            TimeOnly Time = TimeOnly.Parse(DateTime.Now.ToShortTimeString());

            Form Form = new Form(
                "Test" + DateTime.Now.Ticks,
                3
            )
            {
                StartDate = Date.ToString(),
                EndDate = Date.ToString(),
                StartTime = Time.AddMinutes(2).ToString(),
                EndTime = Time.AddMinutes(5).ToString(),
                Attempts = "1"
            };
   

            var Case = homePage
                .LeftMenu
                // Открыть формы
                .OpenForms()
                // Создать форму
                .OpenCreateFormSlider()
                // Изменить название формы
                .ChangeFormTitle(Form.Title)
                // Создать вопросы для опроса
                .CreateSingleQuestionBlock(Form, Form.Type.Text)
                .CreateSingleQuestionBlock(Form, Form.Type.One_from_list, 1)
                .CreateSingleQuestionBlock(Form, Form.Type.Many_from_list, 3)
                // Переключится на настройик
                .SwitchToSettings()
                // Установить настройки для формы
                .SetFormProperties(Form)
                // Сделать форму анонимной
                .SetAnon()
                // Сохранить форму
                .SaveForm()
                // Открыть создание задачи
                .CreateTask(Form.Title)
                // Назначить ответстветнного
                .SetContractor(Participants.Contractor)
                // Назначить поставщика
                .SetDirector(Participants.Director)
                // Назначить смотрящего
                .SetWatcher(Participants.Watcher)
                // Создать задачу
                .CreateTask();

            Waiters.StaticWait_s(8);

            var ContinueCase = new TopMenu()
                // Выйти из аккаунта
                .LogOut()
                // Войти в аккаунт
                .Login(User.Login, User.Password);

            Waiters.StaticWait_s(3);

            ContinueCase
                // Открыть задачи
                .OpenTasks()
                // Открыть задачу
                .OpenTask(Form.Title)
                // Открыть форму
                .OpenForm()
                // Начать прохождение
                .StartForm()
                .AnswerTheQuestions(Form)
                .SubmitForm();

            Waiters.StaticWait_s(3);

            bool Result = homePage
                .LeftMenu
                .OpenForms()
                .OpenResults(Form.Title)
                .CheckAnswers(Form);

            if (Result)
            {
                Log.Info("Все ответы и вопросы отображены");
            }
            else
            {
                Log.Error("Не все ответы и вопросы отображены");
            }

            new FormResultPage()
                .CloseFrame()
                .DeleteForm(Form.Title);
        }
    }
}