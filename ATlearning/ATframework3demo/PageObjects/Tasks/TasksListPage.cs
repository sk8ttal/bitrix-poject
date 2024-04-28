using atFrameWork2.SeleniumFramework;
using aTframework3demo.PageObjects.Tasks;

namespace atFrameWork2.PageObjects
{
    public class TasksListPage
    {
        public OpenedTaskFrame OpenTask(string TaskName)
        {
            new WebItem($"//a[contains(text(), '{TaskName}')]", $"Задача для формы {TaskName}")
                .Click();
            new WebItem("//iframe[@class='side-panel-iframe']", $"Фрейм созданной задачи {TaskName}")
                .SwitchToFrame();

            return new OpenedTaskFrame();
        }
    }
}