using atFrameWork2.SeleniumFramework;

namespace aTframework3demo.PageObjects.Forms
{
    public class OpenedProfileFrame
    {
        public string GetUserName()
        {
            var userNameField = new WebItem("//div[@data-cid='NAME']//div[@class='ui-entity-editor-content-block-text']", "Поле имени профиля");

            return userNameField.InnerText();
        }
    }
}