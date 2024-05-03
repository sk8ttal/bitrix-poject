using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.PageObjects;
using atFrameWork2.TestEntities;
using ATframework3demo.PageObjects;
using aTframework3demo.TestEntities;
using ATframework3demo.PageObjects.Forms;

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
            Form Data = new Form(
                "Test" + DateTime.Now.Ticks.ToString(),
                55
            );

            var Case = homePage
                .LeftMenu
                .OpenForms()
                .OpenCreateFormSlider()
                .ChangeFormTitle(Data.Title);
                // .AddQuestion(Data.QuestionsNumber)

            for (int i = 0; i < Data.QuestionsNumber; i++)
            {
                Case.CreateSingleQuestionBlock(Data, Data.Type[2], 5);
            }
            Case
                .SaveForm()
                .OpenForm(Data.Title)
                .StartForm();
        }
    }
}