using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using atFrameWork2.PageObjects;
using atFrameWork2.SeleniumFramework;

namespace ATframework3demo.PageObjects
{
    public class TopMenu
    {
        public LoginPage LogOut()
        {
            new WebItem("//span[@class='user-name']", "Меню пользователя")
                .Click();
            new WebItem("(//div[@class='ui-popupcomponentmaker__content--section-item'])[last()]", "Кнопка выхода из аккаунта")
                .Click();

            return new LoginPage();
        }
    }
}