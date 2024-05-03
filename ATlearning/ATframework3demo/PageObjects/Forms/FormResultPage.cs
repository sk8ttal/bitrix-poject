using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.SeleniumFramework;
using aTframework3demo.TestEntities;

namespace ATframework3demo.PageObjects.Forms
{
    public class FormResultPage
    {

        public bool CheckAnswers(Form Form)
        {
            Waiters.StaticWait_s(3);

            List<string> Questions = Form.Questions;

            for (int i = 0; i < Form.QuestionsNumber; i++)
            {
                string QuestionName = Questions[i];
                List<string> AnswerName = Form.Answers[QuestionName];

                for (int j = 0; j < AnswerName.Count; j++)
                {
                    WebItem AnswerOnQuestion = new WebItem($"//span[text()='{QuestionName}']/ancestor::table//span[text()='{AnswerName[j]}']",
                        $"Ответ {AnswerName[j]} на вопрос {QuestionName}");

                    if (!AnswerOnQuestion.WaitElementDisplayed())
                    {
                        Log.Error($"Ответ {AnswerName[j]} на вопрос {QuestionName} не найден");

                        return false;
                    }
                    Log.Info($"Ответ {AnswerName[j]} на вопрос {QuestionName} найден");
                } 
            }

            return true;
        }

        public FormsMainPage CloseFrame()
        {
            WebDriverActions.SwitchToDefaultContent();
            new WebItem("//div[@class='side-panel-label-icon side-panel-label-icon-close']", "Кнопка закрытия слайдера")
                .Click();

            return new FormsMainPage();
        }


        public bool IsResultRowAsExpected(Dictionary<int, string> ChosenOptions)
        {
            string xPath = "//tr[@class='main-grid-row main-grid-row-body']//td/following-sibling::td/following-sibling::td/following-sibling::td/following-sibling::td/following-sibling::td";

            foreach (var optionId in ChosenOptions)
            {
                xPath += "/following-sibling::td";
                var encounteredCellInRow = new WebItem(xPath, "Очередная ячейка в строке результата");
                string encounteredAnswer = encounteredCellInRow.InnerText();

                if (encounteredAnswer != optionId.Value)
                {
                    Log.Error($"Очередное полученное значение '{encounteredAnswer}' не совпадает с ожидаемым'{optionId.Value}'");
                    return false;
                }
            }

            return true;
        }

        public FormResultPage OpenDisplayRowSettings()
        {
            new WebItem("//div[@class='main-grid-container']//th[@class='main-grid-cell-head main-grid-cell-static main-grid-cell-action']", "Контексное меню отображений в таблице форм")
                .Click();

            return this;
        }

        public FormResultPage CheckFormStartInput()
        {
            new WebItem("//input[@id='START_TIME-checkbox']", "Чекбокс начала прохождения формы")
                .Click();

            return this;
        }

        public FormResultPage CheckFormEndInput()
        {
            new WebItem("//input[@id='COMPLETED_TIME-checkbox']", "Чекбокс конца прохождения формы")
                .Click();

            return this;
        }

        public FormResultPage AcceptDisplaySettings()
        {
            new WebItem("//span[@class='ui-btn ui-btn-success main-grid-settings-window-actions-item-button']", "Кнопка 'Применить'")
                .Click();

            return this;
        }
    }
}