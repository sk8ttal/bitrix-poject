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
                1,
                1,
                1,
                2
            );

            bool Result = homePage
                .LeftMenu
                // Открыть формы
                .OpenForms()
                // Нажать на кнопку 'Создать'
                .OpenCreateFormSlider()
                // Изменить название формы
                .ChangeFormTitle(Form)
                .CreateQuestionsWithParameters(Form)
                .SaveForm()
                // Проверить, что форма отображается в таблице
                .IsFormPresent(Form);

            if (Result)
            {
                Log.Info($"Форма {Form.Title} создана");
            }
            else
            {
                throw new Exception($"Форма {Form.Title} не создана");
            }

                bool IsFormPresent = new FormsMainPage()
                // Открыть форму
                .OpenForm(Form.Title)
                .StartForm()
                // Проверить, что все 3 блока отображаются
                .IsQuestionBlocksPresent(Form)
                // Закрыть форму
                .CloseForm()
                // Удалить форму
                .DeleteForm(Form.Title)
                .IsFormPresent(Form);

                if (IsFormPresent){
                    Log.Info("Форма успешно удалена");
                }
                else{
                    Log.Error("Форма не была удалена");
                }
        }
    }
}