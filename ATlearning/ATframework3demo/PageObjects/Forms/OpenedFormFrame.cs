using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.SeleniumFramework;
using aTframework3demo.PageObjects.Forms;
using aTframework3demo.TestEntities;

namespace ATframework3demo.PageObjects.Forms
{
    public class OpenedFormFrame : FormPage
    {
        /// <summary>
        /// Отвечает на тестовые вопросы формы по ключу, записанному в объекте Form
        /// </summary>
        /// <param name="answersDict"></param>
        /// <returns></returns>
        public OpenedFormFrame AnswerTest(Form form)
        {

            //рассматриваем каждую ключевую пару
            foreach (var keyPair in form.Answers)
            {
                //рассматриваем весь список ответов
                foreach (string answerValue in keyPair.Value)
                {
                    //если тип вопроса текстовый то применяем один метод
                    if (form.QuestionTypes[keyPair.Key] == form.Type[1])
                    {
                        SendKeysToTextQuestion(answerValue, keyPair.Key);
                    }

                    //иначе применяем другой (если будут другие типы вопросов это нужно будет дополнить)
                    else
                    {
                        ChooseOptionInQuestion(answerValue, keyPair.Key);
                    }
                }
            }

            return this;
        }
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
            var submitButton = new WebItem("//button[@class='btn btn-primary submit-button']", "Кнопка 'ОТПРАВИТЬ'");
            submitButton.Hover();
            submitButton.Click();

            //без этого костыля селениум кликает на элементы слишком быстро
            Waiters.StaticWait_s(2);
            WebDriverActions.SwitchToDefaultContent();

            return new FormsMainPage();
        }
    }
}