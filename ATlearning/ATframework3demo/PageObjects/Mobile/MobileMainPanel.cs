namespace ATframework3demo.PageObjects.Mobile;

/// <summary>
/// Главная панель приложения
/// </summary>
public class MobileMainPanel
{
    public MobileTasksListPage SelectTasks()
    {
        return new MobileTasksListPage(); 
    }
}