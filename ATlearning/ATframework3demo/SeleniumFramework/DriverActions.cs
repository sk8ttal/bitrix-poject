using atFrameWork2.BaseFramework.LogTools;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Text;

namespace atFrameWork2.SeleniumFramework
{
    class DriverActions
    {
        public static IWebDriver GetNewDriver()
        {
            IWebDriver driver;
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            return driver;
        }

        public static void Refresh(IWebDriver driver = default)
        {
            Log.Info($"{nameof(Refresh)}");
            driver ??= WebItem.DefaultDriver;
            driver.Navigate().Refresh();
        }

        public static void OpenUri(Uri uri, IWebDriver driver = default)
        {
            Log.Info($"{nameof(OpenUri)}: {uri}");
            driver ??= WebItem.DefaultDriver;
            driver.Navigate().GoToUrl(uri);
        }

        public static void BrowserAlert(bool accept, IWebDriver driver = default)
        {
            driver ??= WebItem.DefaultDriver;
            IAlert alert = driver.SwitchTo().Alert();
            string alertText = alert.Text;
            string result = $"Алерт браузера '{alertText}': нажата кнопка ";

            if (accept)
            {
                alert.Accept();
                result += "ОK";
            }
            else
            {
                alert.Dismiss();
                result += "Отмена";
            }

            Log.Info(result);
        }

        public static void SwitchToDefaultContent(IWebDriver driver = default)
        {
            Log.Info($"{nameof(SwitchToDefaultContent)}");
            driver ??= WebItem.DefaultDriver;
            driver.SwitchTo().DefaultContent();
        }
    }
}
