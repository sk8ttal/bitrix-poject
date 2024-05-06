using atFrameWork2.SeleniumFramework;
using aTframework3demo.TestEntities;

namespace aTframework3demo.PageObjects.Forms
{
    /// <summary>
    /// Страница для прохождения формы. Открывается при переходе по ссылки на форму
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
            foreach (string Name in Form.Questions)
            {
                if (Form.QuestionTypes[Name] == Form.TypeNames[Form.QuestionType.Text])
                {
                    new WebItem($"//label[text()='{Name}']/parent::div/input", $"Поле ввода ответа для вопроса {Name}")
                        .SendKeys(Form.Answers[Name][0]);
                }
                if (Form.QuestionTypes[Name] == Form.TypeNames[Form.QuestionType.One_from_list])
                {
                    string OptionName = Form.Answers[Name][0];

                    new WebItem($"//label[text()='{Name}']/parent::div//label[text()='{OptionName}']/parent::div/input", $"Опция {OptionName}")
                        .Click();
                }
                if (Form.QuestionTypes[Name] == Form.TypeNames[Form.QuestionType.Many_from_list])
                {
                    foreach (string OptionName in Form.Answers[Name])
                    {
                        new WebItem($"//label[text()='{Name}']/parent::div//label[text()='{OptionName}']/parent::div/input", $"Опция {OptionName}")
                        .Click();
                    }
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