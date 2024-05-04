using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.SeleniumFramework;
using aTframework3demo.TestEntities;

namespace aTframework3demo.PageObjects.Forms
{
    /// <summary>
    /// Сущность слайдера для прохождения формы. Открывается через контексное меню.
    /// </summary>
    public class OpenedFormFrame
    {
        public OpenedFormFrame IsQuestionBlocksPresent(Form Data)
        {
            foreach (var Question in Data.Questions)
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

        public bool IsFirstQuestionNamed(string referenceName)
        {
            var firstEncounteredQuestionBlock = new WebItem($"//div[@class='mb-3']//label", "Первый вопрос в форме");
            string actualName = firstEncounteredQuestionBlock.InnerText();
            if (actualName == referenceName)
            {
                return true;
            }

            else
            {
                Log.Error($"Ожидался вопрос '{referenceName}', фактический - '{actualName}'");
                return false;
            }
        }

        public bool IsQuestionWithNamePresent(string referenceName)
        {
            var formQuestion = new WebItem($"//label[@class='form-label' and text()='{referenceName}']", "Название вопроса");
            bool isQuestionPresent = Waiters.WaitForCondition(() => formQuestion.WaitElementDisplayed(), 2, 6, "Ожидание появления вопроса");

            return isQuestionPresent;
        }

        public OpenedFormFrame ChooseOptionInQuestion(string optionName, string questionName)
        {
            new WebItem($"//label[text()='{optionName}']/ancestor::div[@class='form-check']/input[contains(@name,'{questionName}')]", $"Опция '{optionName}' вопроса '{questionName}'")
                .Click();

            return this;
        }

        public OpenedFormFrame SendKeysToTextQuestion(string inputValue, string questionName)
        {
            var inputField = new WebItem($"//label[@class='form-label' and text()='{questionName}']/ancestor::div[@class='mb-3']//input[@class='form-control']", $"Поле ввода текста вопроса '{questionName}'");
            inputField.Click();
            inputField.SendKeys(inputValue);

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

        public FormsMainPage FinishForm()
        {
            new WebItem("//button[@class='btn btn-primary']", "Кнопка 'Подтвердить'")
                .Click();

            //без этого костыля селениум кликает на элементы слишком быстро
            Waiters.StaticWait_s(2);
            WebDriverActions.SwitchToDefaultContent();

            return new FormsMainPage();
        }
    }
}