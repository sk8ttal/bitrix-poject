using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.SeleniumFramework;
using aTframework3demo.TestEntities;

namespace aTframework3demo.PageObjects.Forms
{
    /// <summary>
    /// Страница слайдера редактора форма. Раздел с вопросами
    /// </summary>
    public class FormQuestionsFrame : FormBaseitem
    {
        WebItem NewQuestion = new WebItem("//h3[text()='Название']", "Поле названия вопроса");

        WebItem NewQuestionField = new WebItem("//input[@value='Название']", "Поле для ввода названия вопроса");
        WebItem Container = new WebItem($"(//label[text()='Обязательный вопрос'])[last()]", "Блок вопроса");
        WebItem Selector(string questionName) => new WebItem($"//h3[text()='{questionName}']/ancestor::div[@class='row']//select",
        $"Список типов вопроса для блока {questionName}");
        WebItem Question(string questionName) => new WebItem($"//h3[text()='{questionName}']", "Поле названия вопроса");
        WebItem Field(string questionName) => new WebItem($"//input[@value='{questionName}']", "Поле для ввода названия вопроса");
        WebItem ButtonByText(string Text) => new WebItem($"//button[text()='{Text}']", $"Кнопка '{Text}'");
        WebItem SetAnswerButton(string questionName) => new WebItem($"//h3[text()='{questionName}']//ancestor::div[@class='card mb-3 mt-3']//input[@value]", $"Кнопка установки парвильного ответа для вопроса {questionName}");
        WebItem OptionButton(string QuestionName) => new WebItem($"//h3[text()='{QuestionName}']/ancestor::div[@class='card mb-3 mt-3']//button[text()='+']",
                $"Кнопка добавления опции к вопросу {QuestionName}");
        WebItem Option(string QuestionName) => new WebItem($"//h3[text()='{QuestionName}']/ancestor::div[@class='card mb-3 mt-3']/div[@class='card-body']//label[text()='Новая опция']",
                $"Поле названия опции для вопроса {QuestionName}");
        WebItem OptionField(string QuestionName) => new WebItem($"//h3[text()='{QuestionName}']/ancestor::div[@class='card mb-3 mt-3']/div[@class='card-body']//input",
            "Поле для ввода названия опции");
        WebItem OptionCheckBox(string optionName) => new WebItem($"//label[text()='{optionName}']/ancestor::div[@class='form-check inner-question-options-wrap']//input",
            $"Чекбокс опции с именем {optionName}");
        WebItem DeleteQuestionButton(string questionName) => new WebItem($"//h3[text()='{questionName}']/ancestor::div[@class='card mb-3 mt-3']//button[@class='btn-close']", $"Конпка удаления вопроса {questionName}");

        public FormQuestionsFrame AddQuestion()
        {
            ButtonByText("+ Добавить вопрос").Hover(0);
            ButtonByText("+ Добавить вопрос").Click(0);

            return this;
        }

        public FormQuestionsFrame ChangeFormTitle(Form Form)
        {
            new WebItem("//h1[text()='Новая форма']", "Название формы")
                .Click();
            new WebItem("//input[@type='text' and @value='Новая форма']", "Полее ввода названия формы")
                .ReplaceText(Form.Title);
            new WebItem("//body", "Фрейм создания формы")
                .Click();

            return this;
        }

        public FormQuestionsFrame ChangeOptionName(string QuestionName, string OptionName)
        {
            WebItem Option = new WebItem($"//h3[text()='{QuestionName}']/ancestor::div[@class='card mb-3 mt-3']/div[@class='card-body']//label[text()='Новая опция']",
                $"Поле названия опции для вопроса {QuestionName}");
            WebItem Field = new WebItem($"//h3[text()='{QuestionName}']/ancestor::div[@class='card mb-3 mt-3']/div[@class='card-body']//input[@type='text']",
                "Поле для ввода названия опции");


            Option.Hover(0);
            Option.Click(0);
            Field.ReplaceText(OptionName, 0);
            Container.Hover(0);
            Container.Click(0);

            return this;
        }

        public FormQuestionsFrame SetOptionsName(Form Form, string questionName)
        {

            if (Form.QuestionTypes[questionName] == Form.TypeNames[Form.QuestionType.One_from_list]
                || Form.QuestionTypes[questionName] == Form.TypeNames[Form.QuestionType.Many_from_list])
            {
                foreach (var optionName in Form.Options[questionName])
                {
                    Option(questionName).Hover(0);
                    Option(questionName).Click(0);
                    OptionField(questionName).ReplaceText(optionName, 0);
                    Container.Hover(0);
                    Container.Click(0);
                }

            }

            return this;
        }

        public FormQuestionsFrame ChangeOptionsName(Form Form, string NewName)
        {
            foreach (string Name in Form.Questions)
            {
                if (Form.QuestionTypes[Name] == Form.TypeNames[Form.QuestionType.One_from_list]
                    || Form.QuestionTypes[Name] == Form.TypeNames[Form.QuestionType.Many_from_list])
                    foreach (var optionName in Form.Options[Name])
                    {
                        Option(Name).Hover(0);
                        Option(Name).Click(0);
                        OptionField(Name).ReplaceText(NewName, 0);
                        Container.Hover(0);
                        Container.Click(0);
                    }
            }

            return this;
        }



        public FormQuestionsFrame ChangeQuestionsName(Form Form, string NewName)
        {
            foreach (var Name in Form.Questions)
            {
                Question(Name).Hover(0);
                Question(Name).Click(0);
                Field(Name).ReplaceText(NewName, 0);
            }

            return this;
        }

        public FormQuestionsFrame ChangeQuestionType(Form Form)
        {
            WebItem Selector(string QuestionName) => new WebItem($"//h3[text()='{QuestionName}']/ancestor::div[@class='row']//select", $"Список типов вопроса для блока {QuestionName}");

            foreach (string Name in Form.Questions)
            {
                string QuestionType = Form.QuestionTypes[Name];
                Selector(Name).Hover(0);
                Selector(Name).Click(0);
                new WebItem($"//h3[text()='{Name}']/ancestor::div[@class='row']//option[text()='{QuestionType}']", $"Тип вопроса {QuestionType}")
                    .Click(0);
            }

            return this;
        }

        public FormQuestionsFrame CreateQuestionsWithParameters(Form Form)
        {
            WebItem NextButton = ButtonByText("»");
            foreach (string Name in Form.Questions)
            {
                if (NextButton.WaitElementDisplayed(1))
                {
                    NextButton.Hover(0);
                    NextButton.Click(0);
                    Waiters.StaticWait_s(4);
                }

                AddQuestion();

                NewQuestion.Hover(0);
                NewQuestion.Click(0);
                NewQuestionField.ReplaceText(Name, 0);

                string Type = Form.QuestionTypes[Name];
                if (Type == Form.TypeNames[Form.QuestionType.One_from_list] || Type == Form.TypeNames[Form.QuestionType.Many_from_list])
                {

                    Container.Hover(0);
                    Container.Click(0);
                    Selector(Name).Click(0);
                    new WebItem($"//h3[text()='{Name}']/ancestor::div[@class='row']//option[text()='{Type}']", $"Тип вопроса {Type}")
                        .Click(0);

                    for (int j = Form.Options[Name].Count; j > 1; j--)
                    {
                        OptionButton(Name).Hover(0);
                        OptionButton(Name).Click(0);
                    }
                    SetOptionsName(Form, Name);
                    Container.Hover(0);
                    Container.Click(0);
                }
            }



            return this;
        }

        /// <summary>
        /// Меняет тип всех вопросов в форме на 'Тестовый'
        /// </summary>
        /// <param name="testForm"></param>
        /// <returns></returns>
        public FormQuestionsFrame SetFormToTestType(Form testForm)
        {
            foreach (string questionName in testForm.Questions)
            {
                WebItem Button = new WebItem($"(//h3[text()='{questionName}']//ancestor::div[@class='row']//div[@class='form-check']/input)[last()]", "Радиокнопка переключения вопроса в тестовый тип");

                Button.Hover(0);
                Button.Click(0);
            }

            return this;
        }

        /// <summary>
        /// Меняет тип всех вопросов в форме на 'Без проверки'
        /// </summary>
        /// <param name="testForm"></param>
        /// <returns></returns>
        public FormQuestionsFrame SetFormToNonTestType(Form testForm)
        {
            foreach (string questionName in testForm.Questions)
            {
                WebItem radioButton = new WebItem($"(//h3[text()='{questionName}']//ancestor::div[@class='row']//div[@class='form-check']/input)[1]", "Радиокнопка переключения вопроса в тип без проверки");

                Button.Hover(0);
                Button.Click(0);
            }

            return this;
        }

        /// <summary>
        /// Ставит правильные ответы для тестовой формы
        /// </summary>
        /// <param name="testForm"></param>
        /// <returns></returns>
        public FormQuestionsFrame SetFormRightAnswers(Form testForm)
        {
            foreach (string questionName in testForm.Questions)
            {
                if (testForm.QuestionTypes[questionName] == Form.TypeNames[Form.QuestionType.Text])
                {
                    var textInput = new WebItem($"//h3[text()='{questionName}']/ancestor::div[@class='card mb-3 mt-3']//input[@class='form-control']", "Поле ввода текста для текстового тестового вопроса формы");

                    textInput.Hover();
                    textInput.Click();
                    textInput.SendKeys(testForm.CorrectAnswers[questionName][0]);
                }

                else if (testForm.QuestionTypes[questionName] == Form.TypeNames[Form.QuestionType.One_from_list])
                {
                    var optionName = testForm.CorrectAnswers[questionName][0];
                    OptionCheckBox(optionName).Hover();
                    OptionCheckBox(optionName).Click();
                }

                else if (testForm.QuestionTypes[questionName] == Form.TypeNames[Form.QuestionType.Many_from_list])
                {
                    foreach (string optionName in testForm.CorrectAnswers[questionName])
                    {
                        OptionCheckBox(optionName).Hover();
                        OptionCheckBox(optionName).Click();
                    }
                }
            }

            return this;
        }

        public FormQuestionsFrame DeleteAllQuestions(Form testForm)
        {
            foreach (string questionName in testForm.Questions)
            {
                DeleteQuestionButton(questionName).Hover();
                DeleteQuestionButton(questionName).Click();
            }

            return this;
        }

        public FormQuestionsFrame SetAnswer(string questionName)
        {
            SetAnswerButton(questionName).Hover(0);
            SetAnswerButton(questionName).Click(0);

            return this;
        }

        public FormsMainPage SaveForm()
        {
            Button.Hover();
            Button.Click();

            Waiters.StaticWait_s(2);

            return new FormsMainPage();
        }

        public FormQuestionsFrame SaveFormWithErrors()
        {
            Button.Hover();
            Button.Click();

            return this;
        }

        public FormQuestionsFrame RenameForm(string formNameBefore, string formNameAfter)
        {
            new WebItem($"//h1[text()='{formNameBefore}']", "Название формы")
                .Hover();
            new WebItem($"//h1[text()='{formNameBefore}']", "Название формы")
                .Click();
            new WebItem($"//input[@type='text' and @value='{formNameBefore}']", "Полее ввода названия формы")
                .ReplaceText(formNameAfter);
            new WebItem("//body", "Фрейм создания формы")
                .Click();

            return this;
        }

        public FormQuestionsFrame RenameOneQuestion(string questionNameBefore, string questionNameAfter)
        {
            WebItem Question = new WebItem($"//h3[text()='{questionNameBefore}']", "Поле названия вопроса");
            WebItem Field = new WebItem($"//input[@value='{questionNameBefore}']", "Поле для ввода названия вопроса");
            Question.Hover(0);
            Question.Click(0);
            Field.ReplaceText(questionNameAfter, 0);
            //костыль чтобы выйти из инпута имени вопроса
            new WebItem("//body", "Верхняя панель в блоке вопроса").Click(0);

            return this;
        }

        public FormSettingsFrame SwitchToSettings()
        {
            WebItem Settings = ButtonByText("Настройки");

            Settings.Hover();
            Settings.Click();

            return new FormSettingsFrame();
        }

        public FormQuestionsFrame DeleteQuestionByName(string questionName)
        {
            DeleteQuestionButton(questionName).Hover();
            DeleteQuestionButton(questionName).Click();

            return this;
        }

        public bool IsEmptyFormAlertPresent()
        {
            
            var alert = new WebItem("//div[@class='alert alert-danger' and text()='Нельзя создать форму без вопросов']", "Алерт при попытке создать пустую форму");

            if(alert.WaitElementDisplayed())
            {
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}