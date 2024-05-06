using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.PageObjects;
using atFrameWork2.SeleniumFramework;
using aTframework3demo.TestEntities;


namespace aTframework3demo.TestCases.Forms
{
    public class Case_Bitrix24_Question_Blocks_Spam : CaseCollectionBuilder
    {
        protected override List<TestCase> GetCases()
        {
            var caseCollection = new List<TestCase>();
            caseCollection.Add(new TestCase("FORMS: Спам блоками вопросов со сменой типа", homePage => BlocksSpam(homePage)));
            return caseCollection;
        }

        public void BlocksSpam(PortalHomePage homePage)
        {
            Form Form = new Form(
                "Test" + DateTime.Now.Ticks.ToString(),
                5,
                5,
                5,
                10
            );

            var Case = homePage
                .LeftMenu
                .OpenForms()
                .OpenCreateFormSlider()
                .ChangeFormTitle(Form)
                .CreateQuestionsWithParameters(Form)
                .SaveForm();

            Waiters.StaticWait_s(4);
            
            Case
                .OpenForm(Form.Title)
                .StartForm()
                .CloseForm()
                .DeleteForm(Form.Title);
        }
    }
}