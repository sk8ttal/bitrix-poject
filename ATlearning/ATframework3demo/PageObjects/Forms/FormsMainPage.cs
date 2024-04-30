using atFrameWork2.SeleniumFramework;
using ATframework3demo.PageObjects.Forms;


namespace ATframework3demo.PageObjects
{
    public class FormsMainPage
    {
        public FormsMainPage CreateForm(string Title)
        {
            CreateFormFrame Form = OpenCreateFormSlider();
            Form.ChangeFormTitle(Title);
            Form.AddQuestion();
            Form.SaveForm();

            return this;
        }
    
        public NewTaskFrame CreateTask(string Title)
        {
            new WebItem($"//a[text()='{Title}']/parent::span/parent::div/parent::td/parent::tr//a", $"Контексное меню формы {Title}")
                .Click();
            new WebItem("//div[@class='popup-window']//span[text()='Создать задачу']", "Опция 'Создать задачу'")
                .Click();

            new WebItem("//iframe[@class='side-panel-iframe']", $"Фрейм создания задачи для {Title}")
                .SwitchToFrame();

            return new NewTaskFrame();
        }

        public FormsMainPage DeleteSelectedForms()
        {
            new WebItem("//span[text()='Удалить']", "Кнопка множественного действия 'Удалить'")
                .Click();
            new WebItem("//span[text()='Подтвердить']", "Кнопка 'Потвердить' всплывающего окна")
                .Click();

            return this;
        }

        public FormsMainPage DeleteForm(string Title)
        {
            new WebItem($"//a[text()='{Title}']/parent::span/parent::div/parent::td/parent::tr//a", $"Контексное меню формы {Title}")
                .Click();
            new WebItem("//div[@class='popup-window']//span[text()='Удалить']", "Опция 'Удалить'")
                .Click();

            return this;
        }

         public CreateFormFrame EditForm(string Title)
        {
            new WebItem($"//a[text()='{Title}']/parent::span/parent::div/parent::td/parent::tr//a", $"Контексное меню формы {Title}")
                .Click();
            new WebItem("//div[@class='popup-window']//span[text()='Редактировать']", "Опция 'Редактировать'")
                .Click();

            new WebItem("//iframe[@class='side-panel-iframe']", $"Фрейм редактирования формы {Title}")
                .SwitchToFrame();

            return new CreateFormFrame();
        }

        public bool IsFormPresent(string Title)
        {
            WebItem NextButton = new WebItem("//a[text()='Следующая']", "Кнопка 'Следующая'");
            WebItem Form = new WebItem($"//a[text()='{Title}']", "Созданная форма");

            if (Form.WaitElementDisplayed())
            {
                return true;
            }

            while (NextButton.WaitElementDisplayed())
            {
                NextButton.Click();
                if (Form.WaitElementDisplayed())
                {
                    return true;
                }
            }

            return false;
        }

        public OpenedFormFrame OpenForm(string Title)
        {
            new WebItem($"//a[text()='{Title}']/parent::span/parent::div/parent::td/parent::tr//a", $"Контексное меню формы {Title}")
                .Click();
            new WebItem("//div[@class='popup-window']//span[text()='Открыть']", "Опция 'Открыть'")
                .Click();

            new WebItem("//iframe[@class='side-panel-iframe']", $"Фрейм создания формы {Title}")
                .SwitchToFrame();

            return new OpenedFormFrame();
        }

        public CreateFormFrame OpenCreateFormSlider()
        {
            new WebItem("//button[@class='ui-btn ui-btn-success']", "Кнопка 'Создать'")
                .Click();
            new WebItem("//iframe[@class='side-panel-iframe']", "Фрейм создания формы")
                .SwitchToFrame();

            return new CreateFormFrame();
        }

        public FormsMainPage SelectForm(string Title)
        {
            new WebItem($"//a[text()='{Title}']/parent::span/parent::div/parent::td/parent::tr//td[@class='main-grid-cell main-grid-cell-checkbox']/span",
                $"Чекбокс формы {Title}").Click();

            return this;
        }

    }
}