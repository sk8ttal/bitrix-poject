using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.SeleniumFramework;
using aTframework3demo.TestEntities;

namespace ATframework3demo.PageObjects.Forms
{
    public class OpenedFormFrame
    {
        public OpenedFormFrame IsQuestionBlockPresent(Form Data)
        {
            foreach (var Question in Data.Questions.Values)
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

        public OpenedFormFrame StartForm()
        {
            new WebItem("//button[@class='btn btn-primary']", "Кнопка 'Начать' в фрейме формы")
                .Click();

            return this;
        }

        public bool IsFirstQuestionNamed(string referenceName)
        {
            var firstEncounteredQuestionBlock = new WebItem($"//div[@class='mb-3']//label", "Первый вопрос в форме");
            bool isQustionAsReference = firstEncounteredQuestionBlock.AssertTextContains(referenceName, "Wrong text");

            return isQustionAsReference;

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