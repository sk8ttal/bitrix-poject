using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.SeleniumFramework;
using aTframework3demo.TestEntities;

namespace ATframework3demo.PageObjects.Forms
{
    public class FormResultPage
    {
        //public static bool ScrambledEquals<T>(IEnumerable<T> list1, IEnumerable<T> list2)
        //{
        //    var cnt = new Dictionary<T, int>();
        //    foreach (T s in list1)
        //    {
        //        if (cnt.ContainsKey(s))
        //        {
        //            cnt[s]++;
        //        }
        //        else
        //        {
        //            cnt.Add(s, 1);
        //        }
        //    }
        //    foreach (T s in list2)
        //    {
        //        if (cnt.ContainsKey(s))
        //        {
        //            cnt[s]--;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    return cnt.Values.All(c => c == 0);
        //}
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

        /// <summary>
        /// Сверяет полученные ответы в форме с ключом, проверяет количество баллов за правильные ответы
        /// </summary>
        /// <param name="chosenOptions"></param>
        /// <param name="rightOptions"></param>
        /// <returns></returns>
        public bool AreTestAnswersAsExpected(Form testForm)
        {
            Waiters.StaticWait_s(3);
            int maxScore = testForm.RightAnswers.Count;
            int actualScore = 0;

            //каждая пара вопрос ответ - 
            foreach (var questionAnswersPair in testForm.Answers)
            {
                string question = questionAnswersPair.Key;
                int rightOptionsQuantity = testForm.RightAnswers[question].Count;
                int actualRightOptionsQuantity = 0;
                bool allCorrectAnswers = true;
                //каждый ответ в списке ответов в исходной паре
                foreach (string answer in questionAnswersPair.Value)
                {
                    //если в правильных ответах нет хотя бы одного нужного элемента весь ответ считается неправильным
                    if (!testForm.RightAnswers[question].Contains(answer))
                    {
                        allCorrectAnswers = false;
                    }

                    else
                    {
                        actualRightOptionsQuantity++;
                    }

                    var spanAnswer = new WebItem($"//span[text()='{questionAnswersPair.Key}']/ancestor::table//span[text()='{answer}']", $"Ответ {answer} на вопрос {questionAnswersPair.Key}");
                    bool isSpanAnswerPresent = Waiters.WaitForCondition(() => spanAnswer.WaitElementDisplayed(), 2, 6, "Ожидание отображения ответа на вопрос");

                    if (!isSpanAnswerPresent)
                    {
                        Log.Error($"Ответ {answer} на вопрос {questionAnswersPair.Key} не найден");
                        return false;
                    }
                }

                if (actualRightOptionsQuantity != rightOptionsQuantity)
                {
                    allCorrectAnswers = false;
                }

                if (allCorrectAnswers)
                {
                    actualScore++;
                }
            }

            var expectedScoreCell = new WebItem($"//span[text()='Количество правильных ответов']/ancestor::table//span[contains(text(), '{actualScore} из {maxScore}')]", "Ожидаемое количество отвеченных правильно вопросов");
            bool isScoreAsExpected = Waiters.WaitForCondition(() => expectedScoreCell.WaitElementDisplayed(), 2, 6, "Ожидание отображения результата теста");

            if (!isScoreAsExpected)
            {
                var actualScoreCell = new WebItem($"//span[text()='Количество правильных ответов']/ancestor::table//span[contains(text(), 'из')]", "Фактическое количество отвеченных правильно вопросов");
                Log.Error($"Фактически засчитано {actualScoreCell.InnerText()} вопросов, ожидалось {actualScore}");
                return false;
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