using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using atFrameWork2.SeleniumFramework;
using aTframework3demo.TestEntities;

namespace aTframework3demo.PageObjects.Forms
{
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
            for (int i = 1; i <= Form.QuestionsNumber; i++)
            {
                var random = new Random();
                string QuestionName = Form.Questions[i];
                int Index = random.Next(Form.Options[QuestionName].Count);
                string OptionName = Form.Options[QuestionName][Index];

                new WebItem($"//label[text()='{QuestionName}']/parent::div//label[text()='{OptionName}']/parent::div/input", $"Опция {OptionName}")
                    .Click();
                Form.Answers.Add(QuestionName,OptionName);
            }

            return this;
        }

        public FormPage SubmitForm()
        {
            new WebItem("//button[text()='Подтвердить']", "Кнопка 'Подтвердить'")
                .Click();

            return this;
        }
    }
}