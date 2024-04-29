using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.PageObjects;
using aTframework3demo.TestEntities;

namespace ATframework3demo.TestCases.Forms
{
    public class Case_Bitrix24_Edit_Form : CaseCollectionBuilder
    {
        protected override List<TestCase> GetCases()
        {
            var caseCollection = new List<TestCase>();
            caseCollection.Add(new TestCase("FORMS: Редактирование формы", homePage => EditForm(homePage)));
            return caseCollection;
        }

        void EditForm(PortalHomePage homePage)
        {
            string formTitle = "testForm" + DateTime.Now.Ticks.ToString();
            var testForm = new AllQuestionTypesForm(formTitle, 1);

            //подготовка формы для редактирования
            var formsMainPage = homePage
                .LeftMenu
                //открыть формы
                .OpenForms()
                //нажать на 'создать'
                .OpenCreateFormSlider()
                //изменить название формы
                .ChangeFormTitle(testForm.Title)
                //добавить вопрос
                .AddQuestion(testForm.QuestionsNumber)
                //задать имя вопросам
                .RenameOneQuestion("Название", "testQuestion1")
                //сохранить форму
                .SaveForm();

            bool isFormPresent = formsMainPage.IsFormPresent(testForm.Title);
            if (!isFormPresent)
            {
                Log.Error($"Созданная форма с названием '{formTitle}' не отображается");
            }

            var formEditPage = formsMainPage
                //открыть фрейм редактирования формы
                .EditForm(testForm.Title)
                //удалить все вопросы
                .DeleteQuestionsFromTop(testForm.QuestionsNumber)
                .SaveEmptyForm();

            bool isEmptyFormAlertPresent = formEditPage.IsEmptyFormAlertPresent();
            if (!isEmptyFormAlertPresent)
            {
                Log.Error($"Создалась форма '{testForm.Title}' без вопросов");
            }
        }
    }
}