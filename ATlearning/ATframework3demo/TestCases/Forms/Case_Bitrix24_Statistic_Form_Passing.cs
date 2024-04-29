using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.PageObjects;
using atFrameWork2.TestEntities;
using ATframework3demo.PageObjects;
using aTframework3demo.TestEntities;
using ATframework3demo.PageObjects.Forms;

namespace aTframework3demo.TestCases.Forms
{
    public class Case_Bitrix24_Statistic_Form_Passing : CaseCollectionBuilder
    {
        protected override List<TestCase> GetCases()
        {
            var caseCollection = new List<TestCase>();
            caseCollection.Add(new TestCase("FORMS: Прохождение оппроса сотрудником", homePage => FormPassing(homePage)));
            return caseCollection;
        }

        public void FormPassing(PortalHomePage homePage)
        {


            homePage
                .LeftMenu
                .OpenForms()
                // .CreateFormWithProperties()
                ;
        }
    }
}