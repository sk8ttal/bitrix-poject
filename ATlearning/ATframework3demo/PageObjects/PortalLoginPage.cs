using atFrameWork2.SeleniumFramework;
using atFrameWork2.TestEntities;
using ATframework3demo.PageObjects;
using OpenQA.Selenium;

namespace atFrameWork2.PageObjects
{
    class PortalLoginPage : BaseLoginPage
    {
        public PortalLoginPage(PortalInfo portal) : base(portal)
        {
        }

        public PortalHomePage Login(User admin)
        {
            WebDriverActions.OpenUri(portalInfo.PortalUri);
            var loginField = new WebItem("//input[@name='USER_LOGIN']", "Поле для ввода логина");
            var pwdField = new WebItem("//input[@name='USER_PASSWORD']", "Поле для ввода пароля");
            loginField.SendKeys(admin.Login);
            loginField.SendKeys(Keys.Enter);
            pwdField.SendKeys(admin.Password, logInputtedText: false);
            pwdField.SendKeys(Keys.Enter);
            return new PortalHomePage();
        }
    }
}
