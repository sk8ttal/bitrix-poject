using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using atFrameWork2.SeleniumFramework;
using aTframework3demo.TestEntities;

namespace ATframework3demo.PageObjects.Forms
{
    public class ResultsFrame
    {
        public bool CkeckAnswers(Form Form)
        {
            Dictionary<int, string> Questions = Form.Questions;
            Dictionary<string, string> Answers = Form.Answers;

            for (int i = 0; i < Form.QuestionsNumber; i++)
            {
                string QuestionName = Questions[i];
                string AnswerName = Answers[QuestionName];
                WebItem Question = new WebItem($"//span[text()='{QuestionName}']", "span");
                WebItem Answer = new WebItem($"//span[text()='{AnswerName}']", "span");

                if (!Question.WaitElementDisplayed() || !Answer.WaitElementDisplayed())
                {
                    return false;
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
    }
}