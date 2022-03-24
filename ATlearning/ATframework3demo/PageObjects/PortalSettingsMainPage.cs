using atFrameWork2.SeleniumFramework;

namespace ATframework3demo.PageObjects
{
    public class PortalSettingsMainPage
    {
        internal PortalSettingsMainPage DisableDefaultSendToAll()
        {
            ChangeDefaultSendToAllState(false);
            return new PortalSettingsMainPage();
        }

        internal PortalSettingsMainPage EnableDefaultSendToAll()
        {
            ChangeDefaultSendToAllState(true);
            return new PortalSettingsMainPage();
        }

        internal PortalSettingsMainPage ChangeDefaultSendToAllState(bool mustBeChecked)
        {
            //снять галочку
            var checkboxSendToAllByDefault = new WebItem("//input[@id='default_livefeed_toall']", "Чекбокс настройки Адресация всем по умолчанию");
            bool isChecked = checkboxSendToAllByDefault.Checked();
            if(isChecked != mustBeChecked)
                checkboxSendToAllByDefault.Click();
            return new PortalSettingsMainPage();
        }

        internal PortalSettingsMainPage Save()
        {
            //ткнуть в кнопку сохранить
            var btnSave = new WebItem("//span[contains(text(), 'Сохранить настройки')]", "Кнопка Сохранить настройки");
            btnSave.Click();
            return new PortalSettingsMainPage();
        }

    }
}
