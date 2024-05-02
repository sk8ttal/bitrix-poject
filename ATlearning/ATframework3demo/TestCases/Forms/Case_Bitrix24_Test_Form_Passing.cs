using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.PageObjects;
using aTframework3demo.TestEntities;

namespace aTframework3demo.TestCases.Forms
{
    public class Case_Bitrix24_Test_Form_Passing : CaseCollectionBuilder
    {
        protected override List<TestCase> GetCases()
        {
            var caseCollection = new List<TestCase>();
            caseCollection.Add(new TestCase("FORMS: Прохождение теста сотрудником", homePage => FormPassing(homePage)));
            return caseCollection;
        }

        public void FormPassing(PortalHomePage homePage)
        {
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
                .SetQuestionsName(testForm.Questions)
                // Изменить типы вопросов для вопросов 2, 3 и 4
                //один из списка
                .ChangeQuestionType(testForm.Questions[2], testForm.Type[2])
                //один из списка
                .ChangeQuestionType(testForm.Questions[3], testForm.Type[2])
                //несколько из списка
                .ChangeQuestionType(testForm.Questions[4], testForm.Type[3])
                // Добавить для 2 вопроса 2 опции
                .AddNewOption(testForm.Questions[2], 2)
                // Изменить названия опций для 2 вопроса
                .ChangeOptionName(testForm.Questions[2])
                // Добавить для 3 вопроса 2 опции
                .AddNewOption(testForm.Questions[3], 2)
                // Изменить названия опций для 3 вопроса
                .ChangeOptionName(testForm.Questions[3])
                // Добавить для 4 вопроса 3 опции
                .AddNewOption(testForm.Questions[4], 3)
                // Изменить названия опций для 4 вопроса
                .ChangeOptionName(testForm.Questions[4])
                // Сохранить форму
                .SaveForm();

            formsMainPage = formsMainPage
                //открыть форму
                .OpenForm(testForm.Title)
                //начать выполнение формы
                .StartForm()
                //в текстовом вопросе 1 написать строку
                .SendKeysToTextQuestion(textAnswerValue, testForm.Questions[1])
                //выбрать опцию 2 в вопросе 2 (один из списка)
                .ChooseOptionInQuestion("Ответ 2", testForm.Questions[2])
                //выбрать опцию 1 в вопросе 2 (один из списка)
                .ChooseOptionInQuestion("Ответ 1", testForm.Questions[2])
                //выбрать опцию 1 в вопросе 3 (несколько из списка)
                .ChooseOptionInQuestion("Ответ 2", testForm.Questions[3])
                //выбрать опцию 3 в вопросе 3 (несколько из списка)
                .ChooseOptionInQuestion("Ответ 1", testForm.Questions[4])
                //выбрать опцию 2 в вопросе 4 (один из списка)
                .ChooseOptionInQuestion("Ответ 3", testForm.Questions[4])
                //завершить прохождение формы
                .FinishForm();

            var expectedRow = new Dictionary<int, string>()
            {
                [1] = textAnswerValue,
                [2] = "Ответ 1",
                [3] = "Ответ 2",
                [4] = "Ответ 3\r\nОтвет 1",
            };


            bool isResultRowAsExpected = formsMainPage
                //открыть результаты
                .OpenResults(testForm.Title)
                //просмотреть соответствует ли строка таблицы результатов ожидаемой expectedRow
                .IsResultRowAsExpected(expectedRow);

            if (!isResultRowAsExpected)
            {
                Log.Error("Фактическая строка с ответами не совпадает с ожидаемой");
            }
        }
    }
}