using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.PageObjects;
using atFrameWork2.TestEntities;
using ATframework3demo.PageObjects;
using aTframework3demo.TestEntities;
using atFrameWork2.SeleniumFramework;

namespace ATframework3demo.TestCases.Forms
{
    public class Case_Bitrix24_Form_Validation : CaseCollectionBuilder
    {
        protected override List<TestCase> GetCases()
        {
            var caseCollection = new List<TestCase>();
            caseCollection.Add(new TestCase("FORMS: Валидация формы", homePage => FormValidation(homePage)));
            return caseCollection;
        }

        public void FormValidation(PortalHomePage homePage)
        {
            string XSS = "<script>alert(1)</script>";

            Form Form = new Form
            (
                "Test" + DateTime.Now.Ticks,
                6
            );

            DateOnly Date = DateOnly.Parse(DateTime.Now.ToShortDateString());
            TimeOnly Time = TimeOnly.Parse(DateTime.Now.ToShortTimeString());

            FormSettings Settings = new FormSettings()
            {
                EndDate = Date.ToString(),
                EndTime = Time.ToString(),
                Timer = "0000",
                Attempts = "0"
            };

            var Case = homePage
                .LeftMenu
                .OpenForms()
                .OpenCreateFormSlider()
                .ChangeFormTitle(" ")
                // Создать 6 пустых вопроса всех типов
                .AddQuestion(Form.QuestionsNumber)
                .SetQuestionsName(Form.QuestionsNumber, Form)
                .ChangeQuestionType(Form.Questions[1], Form.Type[2])
                .ChangeQuestionType(Form.Questions[2], Form.Type[3])
                .ChangeQuestionType(Form.Questions[4], Form.Type[2])
                .ChangeQuestionType(Form.Questions[5], Form.Type[3])
                .SetQuestionToTestType(Form.Questions[3])
                .SetQuestionToTestType(Form.Questions[4])
                .SetQuestionToTestType(Form.Questions[5]);

            bool SelectedAnswerError = Case
                .SaveFormWithErrors()
                .IsNotSelectedCorrectAnswerErrorPresent();

            if (!SelectedAnswerError)
            {
                Log.Info("Предупреждение отображено");
            }

            Case
                .SetAnswer(Form.Questions[4])
                .SetAnswer(Form.Questions[5])
                .ChangeOptionName(Form.Questions[1], " ")
                .ChangeOptionName(Form.Questions[2], " ")
                .ChangeOptionName(Form.Questions[4], " ")
                .ChangeOptionName(Form.Questions[5], " ")
                ;

           

            // Очистить содержимое всех полей
            for (int i = 0; i < Form.QuestionsNumber; i++)
            {
                Case
                .RenameOneQuestion(Form.Questions[i], " ");
            }

            bool IsAllQuestionsNamed = Case
                .SaveForm()
                .EditForm("Новая форма")
                .IsAllQuestionsNamed(Form);

            if (!IsAllQuestionsNamed)
            {
                Log.Error("Не все вопросы были установлены по умолчанию");
            }

            Case
                .ChangeOptionName(Form.Questions[1], XSS)
                .ChangeOptionName(Form.Questions[2], XSS)
                .ChangeOptionName(Form.Questions[4], XSS)
                .ChangeOptionName(Form.Questions[5], XSS);

            for (int i = 0; i < Form.QuestionsNumber; i++)
            {
                Case
                .RenameOneQuestion(Form.Questions[i], XSS);
            }

            Case
                .RenameForm("Новая форма", XSS)
                .SwitchToSettings()
                .SwitchToQuestions()
                .SaveForm()
                .OpenForm(XSS)
                .StartForm()
                .CloseForm()
                .EditForm(XSS)
                .SwitchToSettings()
                .SetFormProperties(Settings)
                .SwitchToQuestions();

            WebDriverActions.Refresh();

            IsAllQuestionsNamed = Case
                .IsAllQuestionsNamed(Form);

            if (!IsAllQuestionsNamed)
            {
                Log.Error("Не все вопросы отображены");
            }

            Case
                .CloseForm()
                .DeleteForm(XSS);
        }
    }
}