using atFrameWork2.BaseFramework;
using atFrameWork2.SeleniumFramework;
using aTframework3demo.PageObjects.Tasks;
using aTframework3demo.TestEntities;


namespace aTframework3demo.PageObjects.Forms
{
    /// <summary>
    /// Страница раздела форм
    /// </summary>
    public class FormsMainPage
    {
        public FormsMainPage CreateForm(Form Form)
        {
            FormQuestionsFrame Frame = OpenCreateFormSlider();
            Frame.ChangeFormTitle(Form.Title);
            Frame.AddQuestion(Form);
            Frame.SaveForm();

            return this;
        }
    
        public NewTaskFrame CreateTask(Form Form)
        {
            string Title = Form.Title;

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

        public FormsMainPage DeleteForm(Form Form)
        {
            string Title = Form.Title;
            new WebItem($"//a[text()='{Title}']/parent::span/parent::div/parent::td/parent::tr//a", $"Контексное меню формы {Title}")
                .Click();
            new WebItem("//div[@class='popup-window']//span[text()='Удалить']", "Опция 'Удалить'")
                .Click();

            return this;
        }

         public FormQuestionsFrame EditForm(Form Form)
        {
            string Title = Form.Title;
            new WebItem($"//a[text()='{Title}']/parent::span/parent::div/parent::td/parent::tr//a", $"Контексное меню формы {Title}")
                .Click();
            new WebItem("//div[@class='popup-window']//span[text()='Редактировать']", "Опция 'Редактировать'")
                .Click();

            new WebItem("//iframe[@class='side-panel-iframe']", $"Фрейм редактирования формы {Title}")
                .SwitchToFrame();

            return new FormQuestionsFrame();
        }

        public bool IsFormPresent(Form Form)
        {
            string Title = Form.Title;
            WebItem NextButton = new WebItem("//a[text()='Следующая']", "Кнопка 'Следующая'");
            WebItem CreatedForm = new WebItem($"//a[text()='{Title}']", "Созданная форма");

            if (CreatedForm.WaitElementDisplayed())
            {
                return true;
            }

            while (NextButton.WaitElementDisplayed())
            {
                NextButton.Click();
                if (CreatedForm.WaitElementDisplayed())
                {
                    return true;
                }
            }

            return false;
        }

        public OpenedFormFrame OpenForm(Form Form)
        {
            string Title = Form.Title;
            new WebItem($"//a[text()='{Title}']/parent::span/parent::div/parent::td/parent::tr//a", $"Контексное меню формы {Title}")
                .Click();
            new WebItem("//div[@class='popup-window']//span[text()='Открыть']", "Опция 'Открыть'")
                .Click();

            new WebItem("//iframe[@class='side-panel-iframe']", $"Фрейм создания формы {Title}")
                .SwitchToFrame();

            return new OpenedFormFrame();
        }

        public FormQuestionsFrame OpenCreateFormSlider()
        {
            new WebItem("//button[@class='ui-btn ui-btn-success']", "Кнопка 'Создать'")
                .Click();
            new WebItem("//iframe[@class='side-panel-iframe']", "Фрейм создания формы")
                .SwitchToFrame();

            return new FormQuestionsFrame();
        }

        public FormResultPage OpenResults(Form Form)
        {
            string Title = Form.Title;
            new WebItem($"//a[text()='{Title}']/parent::span/parent::div/parent::td/parent::tr//a", $"Контексное меню формы {Title}")
                .Click();
            new WebItem("//div[@class='popup-window']//span[text()='Результаты']", "Опция 'Результаты'")
                .Click();

            new WebItem("//iframe[@class='side-panel-iframe']", $"Фрейм результатов формы {Title}")
                .SwitchToFrame();

            return new FormResultPage();
        }


        public FormsMainPage SelectForm(Form Form)
        {
            string Title = Form.Title;
            new WebItem($"//a[text()='{Title}']/parent::span/parent::div/parent::td/parent::tr//td[@class='main-grid-cell main-grid-cell-checkbox']/span",
                $"Чекбокс формы {Title}").Click();

            return this;
        }

    }
}