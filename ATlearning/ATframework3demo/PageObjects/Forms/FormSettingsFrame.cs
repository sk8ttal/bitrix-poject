using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.SeleniumFramework;
using aTframework3demo.TestEntities;

namespace aTframework3demo.PageObjects.Forms
{
    /// <summary>
    /// Страница слайдера редактора форма. Раздел с настройками
    /// </summary>
    public class FormSettingsFrame : FormBaseitem
    {
        public bool IsSettingsOpened()
        {
            WebItem Field = new WebItem("//div[@class='mb-3']//label[text()='Время начала доступа к тесту']", "Описание поля даты и времени появления теста");

            if (Field.WaitElementDisplayed())
            {
                return true;
            }
            return false;
        }

        public FormsMainPage SaveForm()
        {
            WebItem Button = new WebItem("//button[text()='СОХРАНИТЬ']", "Кнопка 'Сохранить'");
            Button.Hover();
            Button.Click();

            Waiters.StaticWait_s(2);

            return new FormsMainPage();
        }

        public FormSettingsFrame SetStartDate(string Date, string Time)
        {
            WebItem StartField = new WebItem("//div[@class='mb-3']//label[text()='Доступна с']/parent::div/child::input", "Поле ввода даты и времени появления теста");

            StartField.SendKeys(Date);
            StartField.NotTextKey("ArrowRight");
            StartField.SendKeys(Time);

            return this;
        }

        public FormSettingsFrame SetEndDate(string Date, string Time)
        {
            WebItem EndField = new WebItem("//div[@class='mb-3']//label[text()='Доступна до']/parent::div/child::input", "Поле ввода даты и времени закрытия теста");

            EndField.SendKeys(Date);
            EndField.NotTextKey("ArrowRight");
            EndField.SendKeys(Time);

            return this;
        }

        public FormSettingsFrame SetTimer(string Time)
        {
            new WebItem("//div[@class='mb-3']//label[text()='Таймер']/parent::div/child::input", "Поле таймера")
                .SendKeys(Time);

            return this;

        }

        public FormSettingsFrame SetAttempts(string Attempts)
        {
            new WebItem("//div[@class='mb-3']//label[text()='Количество попыток']/parent::div/child::input", "Поле количества попыток")
                .SendKeys(Attempts);

            return this;
        }

        public FormSettingsFrame SetAnon()
        {
            new WebItem("//div[@class='mb-3']//label[text()='Анонимная']/parent::div/child::input", "Пункт 'Анонимная'")
                .Click();

            return this;
        }

        public FormSettingsFrame SetActive()
        {
            new WebItem("//div[@class='mb-3']//label[text()='Активная']/parent::div/child::input", "Пункт 'Активная'")
                .Click();

            return this;
        }

        public FormSettingsFrame SetFormProperties(Form Settings)
        {

            if (Settings.StartDate != null && Settings.StartTime != null)
            {
                SetStartDate(Settings.StartDate, Settings.StartTime);
            }

            if (Settings.EndDate != null && Settings.EndTime != null)
            {
                SetEndDate(Settings.EndDate, Settings.EndTime);

            }

            if (Settings.Timer != null)
            {
                SetTimer(Settings.Timer);
            }

            if (Settings.Attempts != null)
            {
                SetAttempts(Settings.Attempts);
            }

            return this;
        }

        public FormQuestionsFrame SwitchToQuestions()
        {
            new WebItem("//button[text()='Вопросы']", "Панель вопросов")
                .Click();

            return new FormQuestionsFrame();
        }


    }
}