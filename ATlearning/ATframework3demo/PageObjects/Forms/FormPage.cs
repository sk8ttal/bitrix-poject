using atFrameWork2.SeleniumFramework;
using aTframework3demo.TestEntities;

namespace aTframework3demo.PageObjects.Forms
{
    /// <summary>
    /// Страница для прохождения формы. Открывается при переходе по ссылки на форму
    /// </summary>
    public class FormPage
    {
        WebItem Option(string questionName, string optionName) =>
            new WebItem($"//label[text()='{questionName}']/parent::div//label[text()='{optionName}']/parent::div/input", $"Опция {optionName}");

        WebItem TextField(string questionName) => new WebItem($"//label[text()='{questionName}']/parent::div/input", $"Поле ввода ответа для вопроса {questionName}");

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
                    TextField(Name).Hover();
                    TextField(Name).SendKeys(Form.Answers[Name][0]);
                }
                if (Form.QuestionTypes[Name] == Form.TypeNames[Form.QuestionType.One_from_list])
                {
                    string OptionName = Form.Answers[Name][0];
                    Option(Name, OptionName).Hover();
                    Option(Name, OptionName).Click();
                    
                }
                if (Form.QuestionTypes[Name] == Form.TypeNames[Form.QuestionType.Many_from_list])
                {
                    foreach (string OptionName in Form.Answers[Name])
                    {
                        Option(Name, OptionName).Hover();
                        Option(Name, OptionName).Click();
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