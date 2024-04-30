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
            string formTitle = "testForm" + DateTime.Now.Ticks;
            var testForm = new Form(formTitle, 3);

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
                .SetQuestionsName(testForm.Questions)
                //сохранить форму
                .SaveForm();

            //ассерт отображения формы в списке
            bool isFormPresent = formsMainPage.IsFormPresent(testForm.Title);
            if (!isFormPresent)
            {
                Log.Error($"Созданная форма с названием '{formTitle}' не отображается");
            }

            string questionTitle = "testQuestion " + DateTime.Now.Ticks;
            formsMainPage
                //открыть фрейм редактирования формы
                .EditForm(testForm.Title)
                //удалить вопрос 2
                .DeleteQuestionByName("Вопрос 2")
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
                .IsFirstQuestionNamed(testForm.Questions[1]);
            if (!isInOrder)
            {
                Log.Error($"Неверный порядок вопросов. Ожидался вопрос '{testForm.Questions[1]}', фактический - '{questionTitle}'");
            }


            formsMainPage = openedFormFrame
                //закрыть форму
                .CloseForm();

            var formEditPage = formsMainPage
                //открыть фрейм редактирования формы
                .EditForm(testForm.Title)
                //удалить все вопросы
                .DeleteQuestionsFromTop(testForm.QuestionsNumber)
                //попытаться сохранить пустую форму
                .SaveEmptyForm();

            //ассерт ограничения в создании формы без вопросов
            bool isEmptyFormAlertPresent = formEditPage.IsEmptyFormAlertPresent();
            if (!isEmptyFormAlertPresent)
            {
                Log.Error($"Создалась форма '{testForm.Title}' без вопросов");
            }

            formTitle = "testForm" + DateTime.Now.Ticks;
            var editedTestForm = new Form(formTitle, 2);

            formsMainPage = formEditPage
                //переименовать форму
                .RenameForm(testForm.Title, editedTestForm.Title)
                //добавить вопросы
                .AddQuestion(editedTestForm.QuestionsNumber)
                //задать имя вопросам
                .SetQuestionsName(editedTestForm.Questions)
                //поменять тип вопроса 1 на один из списка 
                .ChangeQuestionType(editedTestForm.Questions[1], editedTestForm.Type[2])
                //поменять тип вопроса 2 на несколько из списка
                .ChangeQuestionType(editedTestForm.Questions[2], editedTestForm.Type[3])
                //создать 2 опции в вопросе 1
                .AddNewOption(editedTestForm.Questions[1], 2)
                //изменить название опций в вопросе 1
                .ChangeOptionName(editedTestForm.Questions[1])
                //создать 3 опции в вопросе 2
                .AddNewOption(editedTestForm.Questions[2], 3)
                //изменить название опций в вопросе 2
                .ChangeOptionName(editedTestForm.Questions[2])
                //сохранить форму
                .SaveForm();

            //ассерт отображения формы после переименования
            isFormPresent = formsMainPage.IsFormPresent(editedTestForm.Title);
            if (!isFormPresent)
            {
                Log.Error($"Созданная форма с названием '{formTitle}' не отображается");
            }

            //ассерт наличия блоков вопросов в отредактированной форме
            openedFormFrame = formsMainPage
                //открыть форму
                .OpenForm(editedTestForm.Title)
                //нажать 'начать'
                .StartForm()
                //проверить есть ли нужные блоки вопросов
                .IsQuestionBlockPresent(editedTestForm);
        }
    }
}