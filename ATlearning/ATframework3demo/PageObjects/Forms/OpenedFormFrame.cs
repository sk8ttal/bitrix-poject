using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.SeleniumFramework;
using aTframework3demo.TestEntities;

namespace aTframework3demo.PageObjects.Forms
{
    /// <summary>
    /// Страница слайдера для прохождения формы. Открывается через контексное меню.
    /// </summary>
    public class OpenedFormFrame
    {
        public OpenedFormFrame IsQuestionBlocksPresent(Form Form)
        {
            foreach (var Question in Form.Questions)
            {
                WebItem Element = new WebItem($"//label[text()='{Question}']", $"Элемент c названием {Question}");
                if (Element.WaitElementDisplayed())
                {
                    Log.Info($"Элемент c названием {Question} отображен");
                }
                else
                {
                    throw new Exception($"Элемент c названием {Question} не отображен");
                }
            }

            return this;
        }

        public FormsMainPage CloseForm()
        {
            WebDriverActions.SwitchToDefaultContent();
            new WebItem("//div[@class='side-panel-label-icon side-panel-label-icon-close']", "Кнопка закрытия слайдера")
                .Click();

            return new FormsMainPage();
        }

        public OpenedFormFrame StartForm()
        {
            new WebItem("//button[text()='Начать']", "Кнопка 'Начать'")
                .Click();

            return this;
        }
    }
}