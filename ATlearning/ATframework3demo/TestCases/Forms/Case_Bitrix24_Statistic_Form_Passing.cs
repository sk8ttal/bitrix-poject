using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.PageObjects;
using atFrameWork2.TestEntities;
using ATframework3demo.PageObjects;
using aTframework3demo.TestEntities;
using ATframework3demo.PageObjects.Forms;
using ATframework3demo.TestEntities;
using atFrameWork2.SeleniumFramework;

namespace aTframework3demo.TestCases.Forms
{
    public class Case_Bitrix24_Statistic_Form_Passing : CaseCollectionBuilder
    {
        protected override List<TestCase> GetCases()
        {
            var caseCollection = new List<TestCase>();
            caseCollection.Add(new TestCase("FORMS: Прохождение оппроса сотрудником", homePage => FormPassing(homePage)));
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
                "Test" + DateTime.Now.Ticks.ToString(),
                3
            );
               
            FormSettings Settings = new FormSettings()
            {
                StartDate = Date.ToString(),
                EndDate = Date.ToString(),
                StartTime = Time.AddMinutes(4).ToString(),
                EndTime = Time.AddMinutes(5).ToString(),
                Timer = "0001",
                Attempts = "1"
            };

            var Case = homePage
                .LeftMenu
                // Открыть формы
                .OpenForms()
                // Создать форму
                .OpenCreateFormSlider()


            //     // Проверить, что настройки открываются с перовго раза
            //     .SwitchToSettings();

            // bool IsSetingOpened = Case
            //     .IsSettingsOpened();

            // if (IsSetingOpened)
            // {
            //     Log.Info("Панель настроек формы открыта");
            // }
            // else
            // {
            //     Log.Error("Панель настроек формы не открыта");
            // }

            // Case


                // Переключиться на вопросы
                .SwitchToQuestions()
                // Изменить название формы
                .ChangeFormTitle(Form.Title)
                // Добавить вопрос
                .AddQuestion(Form.QuestionsNumber) 
            
            .CreateHighLoadedQuestions(Form.Questions, Form.Type[2],  Form.QuestionsNumber, 1);

            // Переименовать опции
            for (int i = 1; i <= Form.QuestionsNumber; i++)
            {
                Case.ChangeOptionName(Form.Questions[i]);
            }
            
            Case
                // Переключится на настройик
                .SwitchToSettings()
                // Установить настройки для формы
                .SetFormProperties(Settings)
                // Сделать форму анонимной
                .SetAnon()
                // Сделать форму активной
                .SetActive()
                // Сохранить форму
                .SaveForm()
                .CreateTask(Form.Title)
                .SetContractor(Participants.Contractor)
                .SetDirector(Participants.Director)
                .SetWatcher(Participants.Watcher)
                .CreateTask();
                
            Waiters.StaticWait_s(8);

            new TopMenu()
                .LogOut()
                .Login(User.Login, User.Password)
                .OpenTasks()
                .OpenTask(Form.Title)
                .OpenForm();

            Waiters.StaticWait_s(4);

        }
    }
}