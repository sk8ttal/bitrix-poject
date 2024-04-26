using atFrameWork2.BaseFramework;
using atFrameWork2.SeleniumFramework;
using atFrameWork2.TestEntities;
using ATframework3demo.PageObjects.Forms;

namespace ATframework3demo.PageObjects
{
    public class FormsMainPage
    {
        public CreateFormFrame OpenCreateFormSlider()
        {
            new WebItem("//button[@class='ui-btn ui-btn-success']", "Кнопка 'Создать'")
                .Click();
            new WebItem("//iframe[@class='side-panel-iframe']", "Фрейм создания формы")
                .SwitchToFrame();

            return new CreateFormFrame();
        }

        public FormsMainPage IsFormPresent(string Title)
        {
            WebItem NextButton = new WebItem("//a[text()='Следующая']", "Кнопка 'Следующая'");
            WebItem Form = new WebItem($"//span[text()='{Title}']", "Созданная форма");

            if (Form.WaitElementDisplayed())
                {
                    return this;
                }

            while (NextButton.WaitElementDisplayed())
            {
                NextButton.Click();
                if (Form.WaitElementDisplayed())
                {
                    return this;
                }
            }

            throw new Exception("Созданная форма не отображена в таблице форм");
        }

        public OpenedFormFrame OpenForm(string Title)
        {
            new WebItem($"//span[text()='{Title}']", "Созданная форма")
                .DoubleClick();

            new WebItem("//iframe[@class='side-panel-iframe']", $"Фрейм созданияформы {Title}")
                .SwitchToFrame();
                
            return new OpenedFormFrame();
        }

        public FormsMainPage DeleteForm(string Title)
        {
            new WebItem($"//span[text()='{Title}']/parent::div/parent::td/parent::tr//a", $"Контексное меню формы {Title}")
                .Click();
            new WebItem("//div[@class='popup-window']//span[text()='Удалить']", "Опция 'Удалить'")
                .Click();

            return this; 
        }
    }
}