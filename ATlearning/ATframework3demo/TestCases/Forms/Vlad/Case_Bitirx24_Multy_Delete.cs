using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.PageObjects;
using atFrameWork2.TestEntities;
using ATframework3demo.PageObjects;

namespace ATframework3demo.TestCases
{
    public class Case_Bitirx24_Multy_Delete : CaseCollectionBuilder
    {
        protected override List<TestCase> GetCases()
        {
            var caseCollection = new List<TestCase>();
            caseCollection.Add(new TestCase("FORMS: Множественное удаление форм", homePage => MultyDelete(homePage)));
            return caseCollection;
        }

        void MultyDelete(PortalHomePage homePage)
        {
            string Title1 = "Test1" + DateTime.Now.Ticks;
            string Title2 = "Test2" + DateTime.Now.Ticks;

            bool Result_1 = homePage
                .LeftMenu
                // Открыть формы
                .OpenForms()
                // Создать 2 формы
                .CreateForm(Title1)
                .CreateForm(Title2)
                // Выбрать в таблице созданные формы
                .SelectForm(Title1)
                .SelectForm(Title2)
                // Нажать на кнопку 'Удалить'
                .DeleteSelectedForms()
                // Проверить, что выбранные формы удалены
                .IsFormPresent(Title1);

            if (Result_1)
            {
                Log.Info($"Форма {Title1} удалена");
            }
            else 
            {
                throw new Exception($"Форма {Title1} не удалена");
            }

            bool Result_2 = new FormsMainPage()
                .IsFormPresent(Title2);

            if (Result_2)
            {
                Log.Info($"Форма {Title1} удалена");
            }
            else 
            {
                throw new Exception($"Форма {Title1} не удалена");
            }
        }
    }
}