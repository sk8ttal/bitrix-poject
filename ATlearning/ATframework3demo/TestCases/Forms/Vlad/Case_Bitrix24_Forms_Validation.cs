// using atFrameWork2.BaseFramework;
// using atFrameWork2.BaseFramework.LogTools;
// using atFrameWork2.PageObjects;
// using aTframework3demo.TestEntities;
// using atFrameWork2.SeleniumFramework;

// namespace ATframework3demo.TestCases.Forms
// {
//     public class Case_Bitrix24_Form_Validation : CaseCollectionBuilder
//     {
//         protected override List<TestCase> GetCases()
//         {
//             var caseCollection = new List<TestCase>();
//             caseCollection.Add(new TestCase("FORMS: Валидация формы", homePage => FormValidation(homePage)));
//             return caseCollection;
//         }

//         public void FormValidation(PortalHomePage homePage)
//         {
//             DateOnly Date = DateOnly.Parse(DateTime.Now.ToShortDateString());
//             TimeOnly Time = TimeOnly.Parse(DateTime.Now.ToShortTimeString());

//             Form Form = new Form
//             (
//                 "Test" + DateTime.Now.Ticks,
//                 1,
//                 2,
//                 2,
//                 2
//             )
//             {
//                 EndDate = Date.ToString(),
//                 EndTime = Time.ToString(),
//                 Timer = "0000",
//                 Attempts = "0"
//             };

//             Validator validator = new (3);


//             var Case = homePage
//                 .LeftMenu
//                 .OpenForms()
//                 .OpenCreateFormSlider()
//                 .ChangeFormTitle(Form)
//                 .RenameForm(Form.Title, validator.EmptyString)
//                 // Создать 3 пустых вопроса всех типов
//                 .CreateQuestionsWithParameters(Form)
//                 .SetFormToTestType(Form)
//                 .ChangeOptionsName(Form, validator.EmptyString)
//                 .ChangeQuestionsName(Form, validator.EmptyString);

//             bool SelectedAnswerError = Case
//                 .SaveFormWithErrors()
//                 .IsNotSelectedCorrectAnswerErrorPresent();

//             if (!SelectedAnswerError)
//             {
//                 Log.Info("Предупреждение отображено");
//             }

//             Case
//                 .SetFormRightAnswers(Form)
//                 .ChangeOptionsName(Form, " ");


//             // Очистить содержимое всех полей
//             for (int i = 0; i < Form.QuestionsNumber; i++)
//             {
//                 Case
//                 .RenameOneQuestion(Form.Questions[i], " ");
//             }

//             bool IsAllQuestionsNamed = Case
//                 .SaveForm()
//                 .EditForm(Form.Title)
//                 .IsAllQuestionsNamed(Form);

//             if (!IsAllQuestionsNamed)
//             {
//                 Log.Error("Не все вопросы были установлены по умолчанию");
//             }

//             Case
//                 .ChangeOptionsName(Form, validator.XSS)
//                 .ChangeQuestionsName(Form, validator.XSS);

//             Case
//                 .RenameForm(Form.Title, validator.XSS)
//                 .SwitchToSettings()
//                 .SwitchToQuestions()
//                 .SaveForm()
//                 .OpenForm(validator.XSS)
//                 .StartForm()
//                 .CloseForm()
//                 .EditForm(validator.XSS)
//                 .SwitchToSettings()
//                 .SetFormProperties(Form)
//                 .SwitchToQuestions();

//             WebDriverActions.Refresh();

//             Case
//                 .CloseForm()
//                 .DeleteForm(validator.XSS);
//         }
//     }
// }