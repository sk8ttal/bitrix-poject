using atFrameWork2.SeleniumFramework;
using ATframework3demo.PageObjects;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace atFrameWork2.PageObjects
{
    public class PortalLeftMenu
    {
        public TasksListPage OpenTasks()
        {
            ClickMenuItem(new WebItem("//li[@id='bx_left_menu_menu_tasks']", "Пункт левого меню 'Задачи'"));
            return new TasksListPage();
        }

        public static SiteListPage OpenSites()
        {
            ClickMenuItem(new WebItem("//li[@id='bx_left_menu_menu_sites']", "Пункт левого меню 'Сайты'"));
            return new SiteListPage();
        }

        internal PortalSettingsMainPage OpenSettings()
        {
            var btnSettings = new WebItem("//li[@id='bx_left_menu_menu_configs_sect']", "Пункт левого меню настройки");
            ClickMenuItem(btnSettings);
            return new PortalSettingsMainPage();
        }

        private static void ClickMenuItem(WebItem menuItem)
        {
            if (menuItem.WaitElementDisplayed() == false)
            {
                //развернуть меню
                var btnMore = new WebItem("//span[@id='menu-more-btn-text']", "Кнопка Ещё левого меню");
                btnMore.Click();
            }
            //клик в пункт меню
            menuItem.Click();
        }

        internal NewsPage OpenNews()
        {
            //клик в пункт меню Новости
            var btnNews = new WebItem("//li[@id='bx_left_menu_menu_live_feed']", "Пункт левого меню Новости");
            ClickMenuItem(btnNews);
            return new NewsPage();
        }
    }
}
