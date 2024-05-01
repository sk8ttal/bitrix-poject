using atFrameWork2.SeleniumFramework;
using aTframework3demo.PageObjects.Forms;

namespace atFrameWork2.PageObjects
{
    public class PortalHomePage
    {
        public PortalLeftMenu LeftMenu => new PortalLeftMenu();

        public PortalHomePage OpenProfileMiniWindow()
        {
            new WebItem("//div[@class='user-block']", "Блок профиля в верхней панели сайта")
                .Click();

            return this;
        }

        public OpenedProfileFrame OpenProfileSlider()
        {
            new WebItem("//div[@class='system-auth-form__item system-auth-form__scope --clickable']", "Кнопка открытия слайдера профиля")
                .Click();

            new WebItem("//iframe[@class='side-panel-iframe']", "Фрейм профиля пользователя")
                .SwitchToFrame();

            return new OpenedProfileFrame();
        }
    }
}
