using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.PageObjects;
using atFrameWork2.TestEntities;
using ATframework3demo.PageObjects;
using aTframework3demo.TestEntities;

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
                .SetQuestionToTestType(Form.Questions[3])
                .ChangeQuestionType(Form.Questions[4], Form.Type[2])
                .SetQuestionToTestType(Form.Questions[4])
                .ChangeQuestionType(Form.Questions[5], Form.Type[3])
                .SetQuestionToTestType(Form.Questions[5]);

            Case
                .ChangeOptionName(Form.Questions[1], " ")
                .ChangeOptionName(Form.Questions[2], " ")
                .ChangeOptionName(Form.Questions[4], " ")
                .ChangeOptionName(Form.Questions[5], " ");

            // Очистить содержимое всех полей
            for (int i = 0; i < Form.QuestionsNumber; i++)
            {
                Case
                .RenameOneQuestion(Form.Questions[i], " ");
            }

            bool IsAllQuestionsNamed = Case
                .SaveForm()
                .EditForm("Новая форма")
                .IsAllQuestionsNamed(Form.QuestionsNumber);

            if (!IsAllQuestionsNamed)
            {
                throw new Exception("Не все вопросы были установлены по умолчанию");
            }

            for (int i = 0; i < Form.QuestionsNumber; i++)
            {
                Case
                .RenameOneQuestion(Form.Questions[i], XSS);
            }

            Case
                .ChangeOptionName(Form.Questions[1], XSS)
                .ChangeOptionName(Form.Questions[2], XSS)
                .ChangeOptionName(Form.Questions[4], XSS)
                .ChangeOptionName(Form.Questions[5], XSS)
                .RenameForm("Новая форма", XSS)
                .SwitchToSettings()
                .SwitchToQuestions()
                .SaveForm()
                .OpenForm(XSS)
                ;
        }
    }
}