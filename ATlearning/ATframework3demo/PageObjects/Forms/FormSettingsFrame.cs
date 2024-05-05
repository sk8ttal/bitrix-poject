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

        public FormSettingsFrame SetStartDate(Form Form)
        {
            WebItem StartField = new WebItem("//div[@class='mb-3']//label[text()='Доступна с']/parent::div/child::input", "Поле ввода даты и времени появления теста");

            StartField.SendKeys(Form.StartDate);
            StartField.NotTextKey("ArrowRight");
            StartField.SendKeys(Form.StartTime);

            return this;
        }

        public FormSettingsFrame SetEndDate(Form Form)
        {
            WebItem EndField = new WebItem("//div[@class='mb-3']//label[text()='Доступна до']/parent::div/child::input", "Поле ввода даты и времени закрытия теста");

            EndField.SendKeys(Form.EndDate);
            EndField.NotTextKey("ArrowRight");
            EndField.SendKeys(Form.EndTime);

            return this;
        }

        public FormSettingsFrame SetTimer(Form Form)
        {
            new WebItem("//div[@class='mb-3']//label[text()='Таймер']/parent::div/child::input", "Поле таймера")
                .SendKeys(Form.Timer);

            return this;

        }

        public FormSettingsFrame SetAttempts(Form Form)
        {
            new WebItem("//div[@class='mb-3']//label[text()='Количество попыток']/parent::div/child::input", "Поле количества попыток")
                .SendKeys(Form.Attempts);

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
                SetStartDate(Settings);
            }

            if (Settings.EndDate != null && Settings.EndTime != null)
            {
                SetEndDate(Settings);

            }

            if (Settings.Timer != null)
            {
                SetTimer(Settings);
            }

            if (Settings.Attempts != null)
            {
                SetAttempts(Settings);
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