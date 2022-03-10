using atFrameWork2.SeleniumFramework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace atFrameWork2.PageObjects
{
    class PortalLeftMenu
    {
        public TasksListPage OpenTasks()
        {
            new WebItem("//li[@id='bx_left_menu_menu_tasks']", "Пункт левого меню 'Задачи'").Click();
            return new TasksListPage();
        }

        public static SiteListPage OpenSites()
        {
            new WebItem("//li[@id='bx_left_menu_menu_sites']", "Пункт левого меню 'Сайты'").Click();
            return new SiteListPage();
        }
    }
}
