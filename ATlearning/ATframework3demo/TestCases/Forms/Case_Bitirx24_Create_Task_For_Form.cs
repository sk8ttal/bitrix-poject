using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.PageObjects;
using atFrameWork2.TestEntities;
using ATframework3demo.PageObjects;

namespace ATframework3demo.TestCases
{
    public class Case_Bitirx24_Create_Task_For_Form : CaseCollectionBuilder
    {
        protected override List<TestCase> GetCases()
        {
            var caseCollection = new List<TestCase>();
            caseCollection.Add(new TestCase("FORMS: Интеграция с задачами", homePage => TaskFromForm(homePage)));
            return caseCollection;
        }

        void TaskFromForm(PortalHomePage homePage)
        {
            homePage
                .LeftMenu
                .OpenForms()
                // 
                
                ;
        }

    }
}