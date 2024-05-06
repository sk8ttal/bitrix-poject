using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.PageObjects;
using aTframework3demo.TestEntities;
using atFrameWork2.TestEntities;
using ATframework3demo.PageObjects;
using aTframework3demo.PageObjects.Forms;

namespace aTframework3demo.TestCases.Forms
{
    public class Case_Bitrix24_Test_Form_Passing : CaseCollectionBuilder
    {
        protected override List<TestCase> GetCases()
        {
            var caseCollection = new List<TestCase>();
            caseCollection.Add(new TestCase("FORMS: Прохождение теста сотрудником", homePage => TestFormPassing(homePage)));
            return caseCollection;
        }

        public void TestFormPassing(PortalHomePage homePage)
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

            string formTitle = "testForm" + DateTime.Now.Ticks;
            var testForm = new Form(
                formTitle, 
                1, 
                2, 
                1, 
                3
            );
            
            string textAnswerValue = "testAnswer" + DateTime.Now.Ticks;

            //создать форму для прохождения
            var formsMainPage = homePage
                .LeftMenu
                .OpenForms()
                .OpenCreateFormSlider()
                .ChangeFormTitle(testForm)
                .CreateQuestionsWithParameters(testForm)
                .SetFormToTestType(testForm)
                .SetFormRightAnswers(testForm)
                .SaveForm()
                .CreateTask(testForm)
                .SetContractor(Participants.Contractor)
                .SetDirector(Participants.Director)
                .SetWatcher(Participants.Watcher)
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
                .OpenTask(testForm.Title)
                // Открыть форму
                .OpenForm()
                // Начать прохождение
                .StartForm()
                .AnswerTheQuestions(testForm)
                .SubmitForm();

            Waiters.StaticWait_s(3);

            var formResultPage = homePage
                .LeftMenu
                .OpenForms()
                .OpenResults(testForm.Title);
                
            bool answersDisplayResult = formResultPage
                .CheckAnswers(testForm);

            if (answersDisplayResult)
            {
                Log.Info("Все ответы и вопросы отображены");
            }
            else
            {
                Log.Error("Не все ответы и вопросы отображены");
            }

            bool isScoreCorrect = formResultPage
                .CheckTestScore(testForm);

            if (answersDisplayResult)
            {
                Log.Info("Все тестовые ответы засчитаны");
            }
            else
            {
                Log.Error("Не все тестовые ответы засчитаны");
            }
        }
    }
}