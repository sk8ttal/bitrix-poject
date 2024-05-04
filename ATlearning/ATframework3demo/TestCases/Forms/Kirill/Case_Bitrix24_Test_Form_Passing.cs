using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.PageObjects;
using atFrameWork2.TestEntities;
using aTframework3demo.TestEntities;
using ATframework3demo.PageObjects;

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
            UserForTests testUser = new UserForTests()
            {
                Login = "Oleg",
                Password = "Qwerty123&",
                Uri = new Uri("http://dev.bx/")
            };

            TaskFromForm Participants = new TaskFromForm(
                "Олег Иванов",
                "Кирилл Винников",
                "Кирилл Винников"
            );

            string formTitle = "testForm" + DateTime.Now.Ticks;
            var testForm = new Form(formTitle, 4);
            string textAnswerValue = "testAnswer" + DateTime.Now.Ticks;

            //создать форму для прохождения
            var formsMainPage = homePage
                .LeftMenu
                // Открыть формы
                .OpenForms()
                // Нажать на кнопку 'Создать'
                .OpenCreateFormSlider()
                // Изменить название формы
                .ChangeFormTitle(testForm.Title)
                // Добавить 3 блока вопросов
                .AddQuestion(testForm.QuestionsNumber)
                // Изменить названия блоков
                .SetQuestionsName(testForm.QuestionsNumber, testForm)
                // Изменить типы вопросов для вопросов 2, 3 и 4
                //один из списка
                .ChangeQuestionTypeWithFormSave(testForm.Questions[1], testForm.Type[2], testForm.QuestionTypes)
                //один из списка
                .ChangeQuestionTypeWithFormSave(testForm.Questions[2], testForm.Type[2], testForm.QuestionTypes)
                //несколько из списка
                .ChangeQuestionTypeWithFormSave(testForm.Questions[3], testForm.Type[3], testForm.QuestionTypes)
                // Добавить для 2 вопроса 2 опции
                .AddNewOption(testForm.Questions[1], 2)
                // Изменить названия опций для 2 вопроса
                .ChangeOptionsName(testForm.Questions[1], testForm.Options)
                // Добавить для 3 вопроса 2 опции
                .AddNewOption(testForm.Questions[2], 2)
                // Изменить названия опций для 3 вопроса
                .ChangeOptionsName(testForm.Questions[2], testForm.Options)
                // Добавить для 4 вопроса 3 опции
                .AddNewOption(testForm.Questions[3], 3)
                // Изменить названия опций для 4 вопроса
                .ChangeOptionsName(testForm.Questions[3], testForm.Options)
                //сменить тип вопроса на тестовый
                .SetQuestionToTestType(testForm.Questions[0])
                //поставить в вопросе 1 (текстовый вопрос) правильный ответ
                .SetTextQuestionRightAnswer(testForm.Questions[0], textAnswerValue, testForm.RightAnswers)
                //сменить тип вопроса на тестовый
                .SetQuestionToTestType(testForm.Questions[1])
                //поставить в вопросе 2 (один из списка) правильный ответ
                .SetOptionQuestionRightAnswer(testForm.Questions[1], testForm.Options[testForm.Questions[1]][0], testForm.RightAnswers)
                //сменить тип вопроса на тестовый
                .SetQuestionToTestType(testForm.Questions[2])
                //поставить в вопросе 3 (один из списка) правильный ответ
                .SetOptionQuestionRightAnswer(testForm.Questions[2], testForm.Options[testForm.Questions[2]][1], testForm.RightAnswers)
                //сменить тип вопроса на тестовый
                .SetQuestionToTestType(testForm.Questions[3])
                //поставить в вопросе 4 (несколько из списка) 2 правильных ответа
                .SetOptionQuestionRightAnswer(testForm.Questions[3], testForm.Options[testForm.Questions[3]][2], testForm.RightAnswers)
                .SetOptionQuestionRightAnswer(testForm.Questions[3], testForm.Options[testForm.Questions[3]][3], testForm.RightAnswers)
                //сохранить форму
                .SaveForm()
                .CreateTask(testForm.Title)
                // Назначить ответстветнного
                .SetContractor(Participants.Contractor)
                // Назначить поставщика
                .SetDirector(Participants.Director)
                // Создать задачу
                .CreateTask();

            Waiters.StaticWait_s(8);

            var ContinueCase = new TopMenu()
                // Выйти из аккаунта
                .LogOut()
                // Войти в аккаунт
                .Login(testUser.Login, testUser.Password);

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

            bool isResultRowAsExpected = homePage
                .LeftMenu
                .OpenForms()
                //открыть результаты
                .OpenResults(testForm.Title)
                //просмотреть соответствует ли строка таблицы результатов ожидаемой expectedRow
                .AreTestAnswersAsExpected(testForm);

            if (!isResultRowAsExpected)
            {
                Log.Error("Фактическая строка с ответами не совпадает с ожидаемой");
            }
        }
    }
}