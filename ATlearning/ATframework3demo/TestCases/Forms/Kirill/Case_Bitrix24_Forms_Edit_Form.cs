using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.PageObjects;
using atFrameWork2.SeleniumFramework;
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
            string formTitle = "testForm" + DateTime.Now.Ticks;
            var testForm = new Form(
                formTitle, 
                1, 
                1, 
                1, 
                3
            );

            //подготовка формы для редактирования
            var formsMainPage = homePage
                .LeftMenu
                //открыть формы
                .OpenForms()
                //нажать на 'создать'
                .OpenCreateFormSlider()
                //изменить название формы
                .ChangeFormTitle(testForm)
                //добавить вопросы
                .CreateQuestionsWithParameters(testForm)
                //сохранить форму
                .SaveForm();

            //ассерт отображения формы в списке
            bool isFormPresent = formsMainPage.IsFormPresent(testForm);
            if (!isFormPresent)
            {
                Log.Error($"Созданная форма с названием '{formTitle}' не отображается");
            }

            string questionTitle = "testQuestion " + DateTime.Now.Ticks;
            formsMainPage
                //открыть фрейм редактирования формы
                .EditForm(testForm)
                //удалить вопрос 2
                .DeleteQuestionByName(testForm.Questions[2])
                //добавить новый вопрос
                .AddQuestion()
                //переименовать созданный вопрос
                .RenameOneQuestion("Название", questionTitle)
                //сохранить форму
                .SaveForm();

            var openedFormFrame = formsMainPage
                //открыть форму
                .OpenForm(testForm.Title)
                //нажать 'начать'
                .StartForm();

            //ассерт порядка вопросов в форме - проверка первого вопроса в списке
            bool isInOrder = openedFormFrame
                .IsFirstQuestionNamed(testForm.Questions[0]);
            if (!isInOrder)
            {
                Log.Error($"Неверный порядок вопросов.");
            }

            //ассерт удаления вопроса в форме
             bool isQuestionPresent = openedFormFrame.IsQuestionWithNamePresent(testForm.Questions[1]);
             if (isQuestionPresent)
             {
                Log.Error($"Вопрос '{testForm.Questions[1]}' не удален");
             }

            formsMainPage = openedFormFrame
                //закрыть форму
                .CloseForm();

            var formEditPage = formsMainPage
                //открыть фрейм редактирования формы
                .EditForm(testForm)
                //удалить все вопросы
                .DeleteAllQuestions(testForm)
                //попытаться сохранить пустую форму
                .SaveFormWithErrors();

            //ассерт ограничения в создании формы без вопросов
            bool isEmptyFormAlertPresent = formEditPage
                .IsEmptyFormAlertPresent();
            if (!isEmptyFormAlertPresent)
            {
                Log.Error($"Создалась форма '{testForm.Title}' без вопросов");
            }

            formTitle = "testForm" + DateTime.Now.Ticks;
            var editedTestForm = new Form(
                formTitle, 
                0,
                1,
                1,
                3
            );

            formsMainPage = formEditPage
                //переименовать форму
                .RenameForm(testForm.Title, editedTestForm.Title)
                //добавить вопросы
                .CreateQuestionsWithParameters(editedTestForm)
                //сохранить форму
                .SaveForm();

            //ассерт отображения формы после переименования
            isFormPresent = formsMainPage.IsFormPresent(editedTestForm);
            if (!isFormPresent)
            {
                Log.Error($"Созданная форма с названием '{formTitle}' не отображается");
            }

            WebDriverActions.Refresh();
            //ассерт наличия блоков вопросов в отредактированной форме
            openedFormFrame = formsMainPage
                //открыть форму
                .OpenForm(editedTestForm.Title)
                //проверить есть ли нужные блоки вопросов
                .IsQuestionBlocksPresent(editedTestForm);
        }
    }
}