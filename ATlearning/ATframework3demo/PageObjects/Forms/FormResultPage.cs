using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.SeleniumFramework;
using aTframework3demo.TestEntities;

namespace aTframework3demo.PageObjects.Forms
{
    /// <summary>
    /// Страница слайдера результатов формы
    /// </summary>
    public class FormResultPage
    {
        WebItem AnswerOfQuestion(string questionName, string optionName) => new WebItem($"//span[text()='{questionName}']/ancestor::table//span[text()='{optionName}']", $"Ответ '{optionName}' на вопрос {questionName}");
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

        public bool CheckTestScore(Form testForm)
        {
            Waiters.StaticWait_s(3);

            int maxScore = testForm.CorrectAnswers.Count;
            int actualScore = 0;

            foreach (string questionName in testForm.Questions)
            {
                int maxRightOptions = testForm.CorrectAnswers[questionName].Count;
                int rightOptions = 0;
                bool allOptionsCorrect = true;

                foreach (string optionName in testForm.Answers[questionName])
                {
                    var answer = AnswerOfQuestion(questionName, optionName);

                    if (!answer.WaitElementDisplayed())
                    {
                        Log.Error($"Ответ {optionName} на вопрос {questionName} не найден");
                    }

                    if (testForm.CorrectAnswers[questionName].Contains(optionName))
                    {
                        rightOptions++;
                    }

                    else
                    {
                        allOptionsCorrect = false;
                    }
                }

                if (rightOptions != maxRightOptions)
                {
                    allOptionsCorrect = false;
                }

                if (allOptionsCorrect)
                {
                    actualScore++;
                }
            }

            //в кейсе предполагается что только один результат
            var resultInput = new WebItem($"//span[text()='Количество правильных ответов']/ancestor::table//span[text()='{actualScore} из {maxScore}']", "Результат прохождения тестовой формы");
            
            if (!resultInput.WaitElementDisplayed())
            {
                Log.Error($"Неверно подсчитаны ответы, ожидалось {actualScore} из {maxScore}");
                return false;
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