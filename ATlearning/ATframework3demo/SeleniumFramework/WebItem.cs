using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using ATframework3demo.BaseFramework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

namespace atFrameWork2.SeleniumFramework
{
    class WebItem
    {
        public static IWebDriver _defaultDriver = default;
        public static IWebDriver DefaultDriver
        {
            get
            {
                if (_defaultDriver == default)
                    _defaultDriver = DriverActions.GetNewDriver();
                return _defaultDriver;
            }

            set => _defaultDriver = value;
        }

        List<string> XPathLocators { get; set; } = new List<string>();
        public string Description { get; set; }
        public string DescriptionFull { get => $"'{Description}' локаторы: {string.Join(", ", XPathLocators)}"; }

        public WebItem(string xpathLocator, string description) : this(new List<string> { xpathLocator }, description)
        {
        }

        public WebItem(List<string> xpathLocators, string description)
        {
            XPathLocators = xpathLocators;
            Description = description;
        }

        public void Click(IWebDriver driver = default)
        {
            WaitElementDisplayed(driver: driver);
            PrintActionInfo(nameof(Click));

            Execute((button, drv) =>
            {
                button.Click();
            }, driver);
        }

        public void SendKeys(string textToInput, IWebDriver driver = default, bool logInputtedText = true)
        {
            WaitElementDisplayed(driver: driver);
            string textToLog = $"'{textToInput}'";
            if (!logInputtedText)
                textToLog = "[логирование отключено]";
            PrintActionInfo($"Ввод текста {textToLog} в элемент");

            Execute((input, drv) =>
            {
                input.SendKeys(textToInput);
            }, driver);
        }

        public void SwitchToFrame(IWebDriver driver = default)
        {
            PrintActionInfo(nameof(SwitchToFrame));
            Execute((frame, drv) =>
            {
                drv.SwitchTo().Frame(frame);
            }, driver);
        }

        public void SelectListItemByText(string listItemToSelect, IWebDriver driver = default)
        {
            WaitElementDisplayed(driver: driver);
            PrintActionInfo($"Выбор пункта списка '{listItemToSelect}' в списке");

            Execute((select, drv) =>
            {
                var selEl = new SelectElement(select);
                string itemToSelectResultText = default;
                bool optionExists = selEl.Options.ToList().Find(x => x.Text == listItemToSelect) != null;

                if (!optionExists)
                    itemToSelectResultText = selEl.Options.ToList().Find(x => x.Text.Contains(listItemToSelect))?.Text;
                else
                    itemToSelectResultText = listItemToSelect;

                if (itemToSelectResultText != null)
                    selEl.SelectByText(itemToSelectResultText);
                else
                    throw new Exception($"Пункт списка '{listItemToSelect}' не найден в списке {DescriptionFull}");
            }, driver);
        }

        public void AssertTextContains(string expectedText, string failMessage, IWebDriver driver = default)
        {
            PrintActionInfo(nameof(AssertTextContains));

            Execute((targetElement, drv) =>
            {
                string factText = targetElement.Text;

                if (string.IsNullOrEmpty(factText) || !factText.Contains(expectedText))
                {
                    Log.Error(failMessage + Environment.NewLine +
                        $"Ожидалось наличие подстроки: {expectedText}, но было:{Environment.NewLine}{factText}");
                }
            }, driver);
        }

        public bool WaitElementDisplayed(int maxWait_s = 5, IWebDriver driver = default)
        {
            return WaitDisplayedCommon(driver, maxWait_s, true, "Ожидание отображения элемента " + DescriptionFull);
        }

        public bool WaitWhileElementDisplayed(int maxWait_s = 5, IWebDriver driver = default)
        {
            return WaitDisplayedCommon(driver, maxWait_s, false, "Ожидание пропадания элемента " + DescriptionFull);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="maxWait_s"></param>
        /// <param name="waitDirection">Если true то будет ждать пока элемент не станет отображаться, иначе будет ждать пока элемент отображается</param>
        /// <param name="waitDescription"></param>
        /// <returns></returns>
        bool WaitDisplayedCommon(IWebDriver driver, int maxWait_s, bool waitDirection, string waitDescription)
        {
            driver ??= DefaultDriver;
            var impWait = driver.Manage().Timeouts().ImplicitWait;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            bool result = Waiters.WaitForCondition(() =>
            {
                bool expectedState = false;

                Execute((el, drv) =>
                {
                    expectedState = el.Displayed == waitDirection;
                }, driver, true);

                return expectedState;
            }, 1, maxWait_s, waitDescription);

            driver.Manage().Timeouts().ImplicitWait = impWait;
            return result;
        }

        void Execute(Action<IWebElement, IWebDriver> seleniumCode, IWebDriver driver, bool throwAtDebug = false)
        {
            driver ??= DefaultDriver;

            try
            {
                foreach (var locator in XPathLocators)
                {
                    IWebElement targetElement = default;
                    int staleRetryCount = 3;
                    bool interceptedHandlerFirstTry = true;

                    for (int i = 0; i < staleRetryCount; i++)
                    {
                        try
                        {
                            targetElement = driver.FindElement(By.XPath(locator));
                            seleniumCode.Invoke(targetElement, driver);
                            break;
                        }
                        catch (WebDriverException ex)
                        {
                            if (ex is NoSuchElementException)
                            {
                                if (locator == XPathLocators.Last())
                                    throw;
                            }
                            else if (ex is StaleElementReferenceException)
                            {
                                if (i == staleRetryCount - 1)
                                    throw;
                                Thread.Sleep(2000);
                                continue;
                            }
                            else if (ex is ElementClickInterceptedException)
                            {
                                if (ex.Message.Contains("helpdesk-notification-popup"))
                                {
                                    new WebItem("//div[contains(@class, 'popup-close-btn')]", "Кнопка закрытия баннера").Click(driver);
                                    if (interceptedHandlerFirstTry)
                                        i++;
                                    interceptedHandlerFirstTry = false;
                                    continue;
                                }
                                else
                                    throw;
                            }
                            else
                                throw;
                        }

                        break;
                    }

                    if (targetElement != default)
                        break;
                }
            }
            catch (Exception e)
            {
                if(throwAtDebug || !EnvironmentSettings.IsDebug)
                    throw;
                Debug.Fail(e.ToString());
            }
        }

        private void PrintActionInfo(string actionTitle)
        {
            Log.Info($"{actionTitle}: " + DescriptionFull);
        }
    }
}
