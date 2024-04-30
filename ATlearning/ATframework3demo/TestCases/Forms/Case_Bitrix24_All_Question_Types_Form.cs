using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.PageObjects;
using aTframework3demo.TestEntities;
using ATframework3demo.PageObjects;

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
            Form Data = new Form(
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
                .ChangeFormTitle(Data.Title)
                // Добавить 3 блока вопросов
                .AddQuestion(Data.QuestionsNumber)
                // Изменить названия блоков
                .SetQuestionsName(Data.Questions)
                // Изменить типы вопросов для вопросов 2 и 3
                // на один из списка и несколько из списка
                .ChangeQuestionType(Data.Questions[2], Data.Type[2])
                .ChangeQuestionType(Data.Questions[3], Data.Type[3])
                // Добавить для 2 вопроса 2 опции
                .AddNewOption(Data.Questions[2], 2)
                // Изменить названия опций для 2 вопроса
                .ChangeOptionName(Data.Questions[2])
                // Добавить для 3 вопроса 3 опции
                .AddNewOption(Data.Questions[3], 3)
                // Изменить названия опций для 3 вопроса
                .ChangeOptionName(Data.Questions[3])
                // Сохранить форму
                .SaveForm()
                // Проверить, что форма отображается в таблице
                .IsFormPresent(Data.Title);

            if (Result)
            {
                Log.Info($"Форма {Data.Title} создана");
            }
            else
            {
                throw new Exception($"Форма {Data.Title} не создана");
            }

                new FormsMainPage()
                // Открыть форму
                .OpenForm(Data.Title)
                .StartForm()
                // Проверить, что все 3 блока отображаются
                .IsQuestionBlocksPresent(Data)
                // Закрыть форму
                .CloseForm()
                // Удалить форму
                .DeleteForm(Data.Title);
        }
    }
}