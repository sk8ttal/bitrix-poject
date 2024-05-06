using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.PageObjects;
using atFrameWork2.TestEntities;
using aTframework3demo.PageObjects;
using aTframework3demo.PageObjects.Forms;
using aTframework3demo.TestEntities;

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
            Form Form_1 = new Form(
                "Test1" + DateTime.Now.Ticks
            );
            Form Form_2 = new Form(
                "Test2" + DateTime.Now.Ticks
            );

            var Case = homePage
                .LeftMenu
                // Открыть формы
                .OpenForms()
                // Создать 2 формы
                .CreateForm(Form_1)
                .CreateForm(Form_2)
                // Выбрать в таблице созданные формы
                .SelectForm(Form_1)
                .SelectForm(Form_2)
                // Нажать на кнопку 'Удалить'
                .DeleteSelectedForms();

            // Ждем обновление таблицы
                Waiters.StaticWait_s(4);
            
            bool Result_1 = Case
                // Проверить, что выбранные формы удалены
                .IsFormPresent(Form_1);

            if (!Result_1)
            {
                Log.Info($"Форма {Form_1.Title} удалена");
            }
            else 
            {
                Log.Error($"Форма {Form_1.Title} не удалена");
            }

            bool Result_2 = new FormsMainPage()
                .IsFormPresent(Form_2);

            if (!Result_2)
            {
                Log.Info($"Форма {Form_2.Title} удалена");
            }
            else 
            {
                 Log.Error($"Форма {Form_2.Title} не удалена");
            }
        }
    }
}