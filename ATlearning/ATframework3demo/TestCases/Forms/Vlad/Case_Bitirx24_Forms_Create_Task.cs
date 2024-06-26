using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.PageObjects;
using atFrameWork2.TestEntities;
using ATframework3demo.PageObjects;
using aTframework3demo.TestEntities;
using aTframework3demo.PageObjects.Tasks;

namespace ATframework3demo.TestCases
{
    public class Case_Bitirx24_Create_Task_For_Form : CaseCollectionBuilder
    {
        protected override List<TestCase> GetCases()
        {
            var caseCollection = new List<TestCase>();
            caseCollection.Add(new TestCase("FORMS: Создание формы с задачей на прохождение", homePage => TaskFromForm(homePage)));
            return caseCollection;
        }

        void TaskFromForm(PortalHomePage homePage)
        {
            string Title = "Test" + DateTime.Now.Ticks;

            Form Form = new (
                Title
            );

            TaskFromForm Participants = new TaskFromForm(
                "Олег Иванов",
                "Михаил Виктор",
                "Vlad Zorin"
            );

            bool TaskNameExists = homePage
                .LeftMenu
                .OpenForms()
                // Создать форму
                .CreateForm(Form)
                // Выбрать опцию 'Создать задачу'
                .CreateTask(Form)
                // Проверить, что название задачи соответствует названию формы
                .CheckTaskTitle(Title);

            if (TaskNameExists)
            {
                Log.Info($"Поле названия задачи сожержит название формы {Title}");
            }
            else
            {
                throw new Exception($"Поле названия задачи не сожержит название формы {Title}");
            }

            Waiters.StaticWait_s(3);

            // Задаем постановщика, ответсвенного и смотрящего
            new NewTaskFrame()
                .SetContractor(Participants.Contractor)
                .SetWatcher(Participants.Watcher)
                .SetDirector(Participants.Director)
                .CreateTask();

            // Проверяем, что открывается созданная форма
            bool IsFormNameCorrect = homePage.LeftMenu
            .OpenTasks()
            .OpenTask(Title)
            .OpenForm()
            .IsFormNameCorrect(Title);

            if (IsFormNameCorrect)
            {
                Log.Info($"Открыта форма {Title}");
            }
            else
            {
                throw new Exception($"Форма {Title} не была открыта");
            }
        }

    }
}