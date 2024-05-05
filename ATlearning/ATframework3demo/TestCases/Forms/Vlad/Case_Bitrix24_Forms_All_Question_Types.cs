using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.PageObjects;
using aTframework3demo.TestEntities;
using aTframework3demo.PageObjects.Forms;

namespace ATframework3demo.TestCases.Forms
{
    public class Case_Bitrix24_All_Question_Types_Form : CaseCollectionBuilder
    {
        protected override List<TestCase> GetCases()
        {
            var caseCollection = new List<TestCase>();
            caseCollection.Add(new TestCase("FORMS: Создание формы с вопросами всех типов", homePage => CreateAllQuestionTypesForm(homePage)));
            return caseCollection;
        }

        void CreateAllQuestionTypesForm(PortalHomePage homePage)
        {
            Form Form = new Form(
                "Test" + DateTime.Now.Ticks.ToString(),
                3
            );

            bool Result = homePage
                .LeftMenu
                // Открыть формы
                .OpenForms()
                // Нажать на кнопку 'Создать'
                .OpenCreateFormSlider()
                // Изменить название формы
                .ChangeFormTitle(Form.Title)
                // Добавить 3 блока вопросов
                .AddQuestion(Form.QuestionsNumber)
                // Изменить названия блоков
                .SetQuestionsName(Form.QuestionsNumber, Form)
                // Изменить типы вопросов для вопросов 2 и 3
                // на один из списка и несколько из списка
                .ChangeQuestionType(Form.Questions[1], Form.Type[2])
                .ChangeQuestionType(Form.Questions[2], Form.Type[3])
                // Добавить для 2 вопроса 2 опции
                .AddNewOption(Form.Questions[1], 2)
                // Изменить названия опций для 2 вопроса
                .ChangeOptionsName(Form.Questions[1], Form.Options)
                // Добавить для 3 вопроса 3 опции
                .AddNewOption(Form.Questions[2], 3)
                // Изменить названия опций для 3 вопроса
                .ChangeOptionsName(Form.Questions[2], Form.Options)
                // Сохранить форму
                .SaveForm()
                // Проверить, что форма отображается в таблице
                .IsFormPresent(Form.Title);

            if (Result)
            {
                Log.Info($"Форма {Form.Title} создана");
            }
            else
            {
                throw new Exception($"Форма {Form.Title} не создана");
            }

                new FormsMainPage()
                // Открыть форму
                .OpenForm(Form.Title)
                .StartForm()
                // Проверить, что все 3 блока отображаются
                .IsQuestionBlocksPresent(Form)
                // Закрыть форму
                .CloseForm()
                // Удалить форму
                .DeleteForm(Form.Title);
        }
    }
}