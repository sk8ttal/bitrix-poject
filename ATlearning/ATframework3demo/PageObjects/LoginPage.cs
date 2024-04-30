using atFrameWork2.PageObjects;
using atFrameWork2.SeleniumFramework;
using atFrameWork2.TestEntities;
using ATframework3demo.PageObjects;
using OpenQA.Selenium;

namespace ATframework3demo.PageObjects
{
    public class LoginPage
    {

        public PortalLeftMenu Login(string Login, string Password)
        {
            var loginField = new WebItem("//input[@name='USER_LOGIN']", "Поле для ввода логина");
            var pwdField = new WebItem("//input[@name='USER_PASSWORD']", "Поле для ввода пароля");
            loginField.ClearField();
            loginField.SendKeys(Login);
            loginField.SendKeys(Keys.Enter);
            pwdField.SendKeys(Password, logInputtedText: true);
            pwdField.SendKeys(Keys.Enter);
            
            return new PortalLeftMenu();
        }
    }
}