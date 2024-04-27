using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.SeleniumFramework;
using atFrameWork2.TestEntities;
using aTframework3demo.TestEntities;

namespace ATframework3demo.PageObjects.Forms
{
    public class OpenedFormFrame
    {
        public OpenedFormFrame IsQuestionBlockPresent(AllQuestionTypesForm Data)
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

        public FormsMainPage CloseForm()
        {
            WebDriverActions.SwitchToDefaultContent();
            new WebItem("//div[@class='side-panel-label-icon side-panel-label-icon-close']", "Кнопка закрытия слайдера")
                .Click();

            return new FormsMainPage();
        }
    }
}