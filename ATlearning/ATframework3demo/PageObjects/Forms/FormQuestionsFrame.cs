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
        WebItem Question = new WebItem("//h3[text()='Название']", "Поле названия вопроса");
        WebItem Field = new WebItem("//input[@value='Название']", "Поле для ввода названия вопроса");
        WebItem Container = new WebItem("(//p[text()='Здесь будет поле для ввода ответа'])[last()]", "Блок вопроса");
        WebItem ButtonByText(string Text) => new WebItem($"//button[text()='{Text}']", $"Кнопка '{Text}'");

        public FormQuestionsFrame AddQuestion(int QuestionsNumber = 1)
        {
            for (int i = 0; i < QuestionsNumber; i++)
            {
                ButtonByText("+ Добавить вопрос").Hover(0);
                ButtonByText("+ Добавить вопрос").Click(0);
            }

            return this;
        }

        public FormQuestionsFrame AddNewOption(string QuestionName, int OptionsNumber = 1)
        {
            WebItem Button = new WebItem($"//h3[text()='{QuestionName}']/ancestor::div[@class='card mb-3 mt-3']//button[text()='+']",
                $"Кнопка добавления опции к вопросу {QuestionName}");

            for (int i = OptionsNumber; i > 0; i--)
            {
                Button.Hover(0);
                Button.Click(0);
            }

            return this;
        }

        public FormQuestionsFrame ChangeFormTitle(string Title)
        {
            new WebItem("//h1[text()='Новая форма']", "Название формы")
                .Click();
            new WebItem("//input[@type='text' and @value='Новая форма']", "Полее ввода названия формы")
                .ReplaceText(Title);
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
            WebItem Container = new WebItem($"//body", "Блок вопроса");

            Option.Hover(0);
            Option.Click(0);
            Field.ReplaceText(OptionName, 0);
            Container.Hover(0);
            Container.Click(0);

            return this;
        }

        public FormQuestionsFrame ChangeOptionsName(string QuestionName, Dictionary<string, List<string>> Options)
        {
            WebItem Option = new WebItem($"//h3[text()='{QuestionName}']/ancestor::div[@class='card mb-3 mt-3']/div[@class='card-body']//label[text()='Новая опция']",
                $"Поле названия опции для вопроса {QuestionName}");
            WebItem Field = new WebItem($"//h3[text()='{QuestionName}']/ancestor::div[@class='card mb-3 mt-3']/div[@class='card-body']//input",
                "Поле для ввода названия опции");
            WebItem Container = new WebItem($"//h3[text()='{QuestionName}']/ancestor::div[@class='card mb-3 mt-3']//input[@class='form-check-input']",
                "Блок вопроса");

            List<string> OptionNames = new List<string>();
            int i = 0;
            while (Option.WaitElementDisplayed())
            {
                string Name = $"Ответ {DateTime.Now.Ticks}";

                OptionNames.Add(Name);
                Option.Hover(0);
                Option.Click(0);
                Field.ReplaceText(Name, 0);
                Container.Click(0);
                if (i == 500)
                {
                    break;
                }
            }
            Options.Add(QuestionName, OptionNames);

            return this;
        }

        public FormQuestionsFrame ChangeQuestionType(string QuestionName, string QuestionType)
        {
            // ancestor и многое другое можно найти на  (не реклама)
            WebItem Selector = new WebItem($"//h3[text()='{QuestionName}']/ancestor::div[@class='row']//select", $"Список типов вопроса для блока {QuestionName}");

            Selector.Hover(0);
            Selector.Click(0);
            new WebItem($"//h3[text()='{QuestionName}']/ancestor::div[@class='row']//option[text()='{QuestionType}']", $"Тип вопроса {QuestionType}")
                .Click(0);

            return this;
        }

        public FormQuestionsFrame CreateSingleQuestionBlock(Form Form, Form.Type QuestionType, int OptionsNumber = 1)
        {
            WebItem NextButton = ButtonByText("»");

            if (NextButton.WaitElementDisplayed(1))
            {
                NextButton.Hover(0);
                NextButton.Click(0);
                Waiters.StaticWait_s(4);
            }

            AddQuestion();

            string Name = "Вопрос " + DateTime.Now.Ticks;
            Form.Questions.Add(Name);
            Form.QuestionTypes.Add(Name, QuestionType);

            Container.Hover(0);
            Question.Click(0);
            Field.ReplaceText(Name, 0);

            if (Form.Type.One_from_list.Equals(QuestionType) || Form.Type.Many_from_list.Equals(QuestionType))
            {
                WebItem Selector = new WebItem($"//h3[text()='{Name}']/ancestor::div[@class='row']//select", $"Список типов вопроса для блока {Name}");
                WebItem Button = new WebItem($"//h3[text()='{Name}']/ancestor::div[@class='card mb-3 mt-3']//button[text()='+']",
                $"Кнопка добавления опции к вопросу {Name}");

                Container.Click(0);
                Selector.Click(0);
                new WebItem($"//h3[text()='{Name}']/ancestor::div[@class='row']//option[text()='{QuestionType}']", $"Тип вопроса {QuestionType}")
                .Click(0);

                for (int j = OptionsNumber; j > 0; j--)
                {
                    Button.Click(0);
                }

                ChangeOptionsName(Form.Questions.Last(), Form.Options);
            }

            return this;
        }

        public FormQuestionsFrame DeleteQuestionByName(string questionName)
        {
            new WebItem($"//h3[text()='{questionName}']//ancestor::div[@class='header-question-title']//button", $"Кнопка удалить вопроса с названием {questionName}")
                .Click();

            return this;
        }

        public FormQuestionsFrame DeleteQuestionsFromTop(int questionNumber)
        {
            for (int i = 0; i < questionNumber; i++)
            {
                new WebItem("//button[@class='btn-close']", "Кнопка удаления вопроса")
                    .Click();
            }

            return this;
        }

        public bool IsAllQuestionsNamed(Form Form)
        {


            for (int i = 0; i < Form.QuestionsNumber; i++)
            {
                if (!new WebItem($"//h3[text()='{Form.Questions[i]}']", $"Блок вопроса {Form.Questions[i]}").WaitElementDisplayed())
                {
                    Log.Error($"Блок вопроса {Form.Questions[i]} не найден");
                    return false;
                }
                Log.Info($"Блок вопроса {Form.Questions[i]} найден");
            }

            return true;
        }

        public bool IsEmptyFormAlertPresent()
        {
            WebItem alertMessage = new WebItem("//div[@class='alert alert-danger' and text()='Нельзя создать форму без вопросов']", "Предупреждение после попытки форму без вопросов");
            bool isAlertPresent = Waiters.WaitForCondition(() => alertMessage.WaitElementDisplayed(), 2, 6, "Ожидание появления сообщения об ошибке");

            return isAlertPresent;
        }

        public bool IsNotSelectedCorrectAnswerErrorPresent()
        {
            WebItem Message = new WebItem("//div[@class='alert alert-danger' and text()='Для тестовых вопросов хотя бы один вариант должен быть правильным']", 
                "Предупреждение после попытки создать форму без указания правильных вариантов ответов");
            bool IsAlertPresent = Waiters.WaitForCondition(() => Message.WaitElementDisplayed(), 2, 6, "Ожидание появления сообщения об ошибке");

            return IsAlertPresent;
        }

        public FormQuestionsFrame SetQuestionsName(int QuestionsNumber, Form Form)
        {

            for (int i = 0; i < QuestionsNumber; i++)
            {
                string Name = "Вопрос " + DateTime.Now.Ticks;
                Question.Hover(0);
                Question.Click(0);
                Field.ReplaceText(Name, 0);
                Form.Questions.Add(Name);
            }

            return this;
        }

        public FormQuestionsFrame SetQuestionToTestType(string questionName)
        {
            WebItem Button =
                new WebItem($"(//h3[text()='{questionName}']//ancestor::div[@class='row']//div[@class='form-check']/input)[last()]", "Радиокнопка переключения вопроса в тестовый тип");

            Button.Hover();
            Button.Click();

            return this;
        }

        public FormQuestionsFrame SetQuestionToNonTestType(string questionName)
        {
            WebItem Button =
                new WebItem($"(//h3[text()='{questionName}']//ancestor::div[@class='row']//div[@class='form-check']/input)[1]", "Радиокнопка переключения вопроса в тип без проверки");

            Button.Hover(0);
            Button.Click(0);

            return this;
        }

        public FormQuestionsFrame SetFormRightAnswers(Form Form)
        {
            

            return this;
        }

        public FormQuestionsFrame SetAnswer(string QuestionName)
        {
            WebItem Button =
                new WebItem($"//h3[text()='{QuestionName}']//ancestor::div[@class='card mb-3 mt-3']//input[@value]", "Радиокнопка переключения вопроса в тип без проверки");

            Button.Hover(0);
            Button.Click(0);

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


    }
}