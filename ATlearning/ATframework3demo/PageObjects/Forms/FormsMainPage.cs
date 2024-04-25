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
            WebDriverActions.Refresh();
            WebItem NextButton = new WebItem("//a[text()='Следующая']", "Кнопка 'Следующая'");
            WebItem Form = new WebItem($"//span[text()='{Title}']", "Созданная форма");

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
                
            return new OpenedFormFrame();
        }


        // public FormsMainPage CreateNewForm(Form testForm)
        // {
        //     //нажать на кнопку создать форму
        //     var createFormButton = new WebItem("//button[@class='ui-btn ui-btn-success']", "Кнопка 'Добавить форму' в верхней панели");
        //     createFormButton.Click();

        //     //переключиться на фрейм создания формы
        //     new WebItem("//iframe[@class='side-panel-iframe']", "Фрейм создания формы").SwitchToFrame();

        //     //нажать на хэдер формы
        //     var formHeader = new WebItem("//h1[text()='Новая форма']", "Хэдер названия формы в окне создания сверху");
        //     formHeader.Click();

        //     //заполнить название формы
        //     var formNameInput = new WebItem("//input[@type='text' and @value='Новая форма']", "Поле ввода имени формы");
        //     formNameInput.SendKeys(testForm.Title);

        //     //сохранить созданную форму
        //     var saveWholeFormButton = new WebItem("//button[@class='btn btn-primary']", "Кнопка 'Сохранить' под меню создания формы");
        //     saveWholeFormButton.Click();

        //     saveWholeFormButton.Click();

        //     //вернуться на изначальный фрейм
        //     WebDriverActions.SwitchToDefaultContent();
        //     return this;
        // }

        // public bool IsFormPresent(Form testForm)
        // {
        //     //ожидание появления формы в списке форм
        //     var taskTitleInGrid = new WebItem($"//span[@class='main-grid-cell-content' and text()='{testForm.Title}Новая форма']", "Заголовок формы в общем списке форм");
        //     bool isFormPresent = Waiters.WaitForCondition(() => taskTitleInGrid.WaitElementDisplayed(), 2, 6, $"Ожидание появления формы '{testForm.Title}Новая форма' в списке форм");

        //     return isFormPresent;
        // }

    }
}