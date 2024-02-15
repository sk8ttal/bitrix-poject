using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;


namespace atFrameWork2.SeleniumFramework
{
    class MobileDriverActions
    {
        /// <summary>
        /// Создает настроенный объект мобильного драйвера
        /// </summary>
        /// <returns></returns>
        public static AppiumDriver GetNewMobileDriver()
        {
            var appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalOption(MobileCapabilityType.PlatformName, "Android");
            appiumOptions.AddAdditionalOption(MobileCapabilityType.Udid, "your_device_udid"); // Уникальный идентификатор устройства
            appiumOptions.AddAdditionalOption(MobileCapabilityType.AutomationName, "UiAutomator2"); // Драйвер для автоматизации
            appiumOptions.AddAdditionalOption(MobileCapabilityType.App, "/path/to/your/bitrix24.apk"); // Путь к приложению
            appiumOptions.AddAdditionalOption(MobileCapabilityType.NoReset, true); // Не сбрасывать состояние приложения
            appiumOptions.AddAdditionalOption(MobileCapabilityType.FullReset, false); // Выполнить полный сброс состояния
            appiumOptions.AddAdditionalOption(MobileCapabilityType.NewCommandTimeout, 60); // Таймаут для новой команды
            var appiumHost = "http://127.0.0.1:4723";
            AppiumDriver driver = new AndroidDriver(new Uri($"{appiumHost}/wd/hub"), appiumOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            return driver;
        }
    }
}
