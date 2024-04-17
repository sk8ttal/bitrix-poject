using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.PageObjects;
using atFrameWork2.TestEntities;

namespace ATframework3demo.TestCases
{
    public class Case_Bitrix24_Create_Form : CaseCollectionBuilder
    {
        protected override List<TestCase> GetCases()
        {
            var caseCollection = new List<TestCase>();
            caseCollection.Add(new TestCase("FORMS: Создание формы", homePage => CreateForm(homePage)));
            return caseCollection;
        }

        void CreateForm(PortalHomePage homePage)
        {
            string formTitle = "testForm" + DateTime.Now.Ticks;
            var testForm = new Form(formTitle);

            var isFormPresent = homePage
                .LeftMenu
                //открыть формы
                .OpenForms()
                //создать форму
                .CreateNewForm(testForm)
                //проверить появилась ли созданная форма в списке
                .IsFormPresent(testForm);

            if (!isFormPresent)
            {
                Log.Error($"Созданная форма с названием '{formTitle}Новая форма' не отображается");
            }
        }
    }
}
