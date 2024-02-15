using atFrameWork2.SeleniumFramework;
using atFrameWork2.TestEntities;

namespace ATframework3demo.PageObjects.Mobile;

public class MobileTasksListPage
{
    public MobileTasksListPage CreateTask(Bitrix24Task task)
    {
        var createNewTaskBtn = new MobileItem("", "");
        return this;
    }

    public void IsTaskPresent(Bitrix24Task task)
    {
        throw new NotImplementedException();
    }
    
}