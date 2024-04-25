using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.PageObjects;
using atFrameWork2.TestEntities;

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

            homePage
                .LeftMenu
                // Открыть формы
                .OpenForms()
                // Создать 2 формы

                // Проверить, что кнопка 'Удалить' неактивка

                // Выбрать в таблице созданные формы

                // Нажать на кнопку 'Удалить'

                // Проверить, что выбранные формы удалены

                ;
                
        }
    }
}