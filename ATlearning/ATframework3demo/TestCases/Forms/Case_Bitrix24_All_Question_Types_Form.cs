using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.PageObjects;
using aTframework3demo.TestEntities;

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
            
            string Title = "Test" + DateTime.Now.Ticks.ToString();
            string SecondQuestion = "Вопрос 2";
            string ThirdQuestion = "Вопрос 3";
            string OneFromType = "Один из списка";
            string SomeFromType = "Несколько из списка";

            // AllQuestionTypesForm Data = new AllQuestionTypesForm(
            //     "Test" + DateTime.Now.Ticks.ToString(),
            //     3
            // );

            homePage
                .LeftMenu
                // Открыть формы
                .OpenForms()
                // Нажать на кнопку 'Создать'
                .OpenCreateFormSlider()
                // Изменить название формы
                .ChangeFormTitle(Title)
                // Добавить 3 блока вопросов
                .AddQuestion(3)
                // Изменить названия блоков
                .SetNumberQuestion()
                // Изменить типы вопросов для вопросов 2 и 3
                // на один из списка и несколько из списка
                .ChangeQuestionType(SecondQuestion, OneFromType)
                .ChangeQuestionType(ThirdQuestion, SomeFromType)
                // Добавить для 2 вопроса 2 опции
                .AddNewOption(SecondQuestion, 2)
                // Изменить названия опций для 2 вопроса
                .ChangeOptionName(SecondQuestion)
                // Добавить для 3 вопроса 3 опции
                .AddNewOption(ThirdQuestion, 3)
                // Изменить названия опций для 3 вопроса
                .ChangeOptionName(ThirdQuestion)
                // Сохранить форму
                .SaveForm()
                // Проверить, что форма отображается в таблице
                .IsFormPresent(Title)
                // Открыть форму
                .OpenForm(Title)
                // Проверит, что сожержимое формы отображается корректно
                
                // Удалить форму
                //.DeleteForm(Title)
                ;
                
        }
    }
}