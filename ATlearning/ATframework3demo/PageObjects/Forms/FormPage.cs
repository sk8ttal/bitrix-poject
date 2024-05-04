using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using atFrameWork2.SeleniumFramework;
using aTframework3demo.TestEntities;

namespace aTframework3demo.PageObjects.Forms
{
    /// <summary>
    /// Сущность страницы для прохождения формы. Открывается при переходе по ссылки на форму
    /// </summary>
    public class FormPage
    {
        public bool IsFormNameCorrect(string FormName)
        {
            WebItem Title = new WebItem($"//h1[text()='{FormName}']", "Название формы");

            if (Title.WaitElementDisplayed())
            {
                return true;
            }
            return false;
        }

        public FormPage StartForm()
        {
            new WebItem("//button[text()='Начать']", "Кнопка 'Начать'")
                .Click();

            return this;
        }

        public FormPage AnswerTheQuestions(Form Form)
        {
            for (int i = 0; i < Form.QuestionsNumber; i++)
            {
                Random Random = new Random();
                string Question = Form.Questions[i];
                string QuestionName = Form.Questions[i];

                if (Form.QuestionTypes[Question] == Form.Type[1])
                {
                    string Answer = "Ответ" + DateTime.Now.Ticks;

                    new WebItem($"//label[text()='{QuestionName}']/parent::div/input", $"Поле ввода ответа для вопроса {QuestionName}")
                        .SendKeys(Answer);

                    Form.Answers.Add(QuestionName, new List<string> { Answer });

                }
                else if (Form.QuestionTypes[Question] == Form.Type[2])
                {
                    int Index = Random.Next(Form.Options[QuestionName].Count);
                    string OptionName = Form.Options[QuestionName][Index];

                    new WebItem($"//label[text()='{QuestionName}']/parent::div//label[text()='{OptionName}']/parent::div/input", $"Опция {OptionName}")
                        .Click();

                    Form.Answers.Add(QuestionName, new List<string> { OptionName });
                }
                else
                {
                    List<string> Answers = new List<string>();
                    int AnswersNumber = Random.Next(2,Form.Options[QuestionName].Count);
                    for (int j = 0; j < AnswersNumber; j++)
                    {
                        string OptionName = Form.Options[QuestionName][j];

                        new WebItem($"//label[text()='{QuestionName}']/parent::div//label[text()='{OptionName}']/parent::div/input", $"Опция {OptionName}")
                        .Click();

                        Answers.Add(OptionName);
                    }
                    Form.Answers.Add(QuestionName, Answers);
                }
            }

            return this;
        }

        public FormPage SubmitForm()
        {
            WebItem SubmitButton = new WebItem("//button[text()='ОТПРАВИТЬ']", "Кнопка 'ОТПРАВИТЬ'");
            SubmitButton.Hover();
            SubmitButton.Click();

            return this;
        }
    }
}