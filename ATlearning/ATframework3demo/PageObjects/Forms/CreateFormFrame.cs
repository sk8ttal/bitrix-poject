using atFrameWork2.BaseFramework;
using atFrameWork2.BaseFramework.LogTools;
using atFrameWork2.SeleniumFramework;
using aTframework3demo.TestEntities;
using ATframework3demo.TestEntities;

namespace ATframework3demo.PageObjects
{
    public class CreateFormFrame
    {
        WebItem Question = new WebItem("//h3[text()='Название']", "Поле названия вопроса");
        WebItem Field = new WebItem("//input[@value='Название']", "Поле для ввода названия вопроса");
        WebItem Container = new WebItem("(//p[text()='Здесь будет поле для ввода ответа'])[last()]", "Блок вопроса");
        WebItem Button = new WebItem("//button[text()='СОХРАНИТЬ']", "Кнопка 'Сохранить'");

        public CreateFormFrame AddQuestion(int QuestionsNumber = 1)
        {
            for (int i = 0; i < QuestionsNumber; i++)
            {
                WebItem AddButton = new WebItem("//button[text()='+ Добавить вопрос']", "Кнопка добавления вопроса");

                AddButton.Hover(0);
                AddButton.Click(0);
            }

            return this;
        }

        public CreateFormFrame AddNewOption(string QuestionName, int OptionsNumber = 1)
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

        public CreateFormFrame ChangeFormTitle(string Title)
        {
            new WebItem("//h1[text()='Новая форма']", "Название формы")
                .Click();
            new WebItem("//input[@type='text' and @value='Новая форма']", "Полее ввода названия формы")
                .ReplaceText(Title);
            new WebItem("//body", "Фрейм создания формы")
                .Click();

            return this;
        }

        public CreateFormFrame ChangeOptionName(string QuestionName, string OptionName)
        {
            WebItem Option = new WebItem($"//h3[text()='{QuestionName}']/ancestor::div[@class='card mb-3 mt-3']/div[@class='card-body']//label[text()='Новая опция']",
                $"Поле названия опции для вопроса {QuestionName}");
            WebItem Field = new WebItem($"//h3[text()='{QuestionName}']/ancestor::div[@class='card mb-3 mt-3']/div[@class='card-body']//input",
                "Поле для ввода названия опции");
            WebItem Container = new WebItem($"//h3[text()='{QuestionName}']/ancestor::div[@class='card mb-3 mt-3']//input[@class='form-check-input']",
                "Блок вопроса");

            Option.Hover(0);
            Option.Click(0);
            Field.ReplaceText(OptionName, 0);
            Container.Click(0);

            return this;
        }

        public CreateFormFrame ChangeOptionsName(string QuestionName, Dictionary<string, List<string>> Options)
        {
            WebItem Option = new WebItem($"//h3[text()='{QuestionName}']/ancestor::div[@class='card mb-3 mt-3']/div[@class='card-body']//label[text()='Новая опция']",
                $"Поле названия опции для вопроса {QuestionName}");
            WebItem Field = new WebItem($"//h3[text()='{QuestionName}']/ancestor::div[@class='card mb-3 mt-3']/div[@class='card-body']//input",
                "Поле для ввода названия опции");
            WebItem Container = new WebItem($"//h3[text()='{QuestionName}']/ancestor::div[@class='card mb-3 mt-3']//input[@class='form-check-input']",
                "Блок вопроса");

            List<string> OptionNames = new List<string>();
            while (Option.WaitElementDisplayed())
            {
                string Name = $"Ответ {DateTime.Now.Ticks}";

                OptionNames.Add(Name);
                Option.Hover(0);
                Option.Click(0);
                Field.ReplaceText(Name, 0);
                Container.Click(0);
            }
            Options.Add(QuestionName, OptionNames);

            return this;
        }

        public CreateFormFrame ChangeQuestionType(string QuestionName, string QuestionType)
        {
            // ancestor и многое другое можно найти на  (не реклама)
            WebItem Selector = new WebItem($"//h3[text()='{QuestionName}']/ancestor::div[@class='row']//select", $"Список типов вопроса для блока {QuestionName}");

            Selector.Hover(0);
            Selector.Click(0);
            new WebItem($"//h3[text()='{QuestionName}']/ancestor::div[@class='row']//option[text()='{QuestionType}']", $"Тип вопроса {QuestionType}")
                .Click(0);

            return this;
        }

        public CreateFormFrame CreateSingleQuestionBlock(Form Form, string QuestionType, int OptionsNumber = 1)
        {
            WebItem NextButton = new WebItem("//button[text()='»']", "Кнопка следующего раздела");

            if (NextButton.WaitElementDisplayed(1))
            {
                NextButton.Hover(0);
                NextButton.Click(0);
            }

            AddQuestion();

            string Name = "Вопрос " + DateTime.Now.Ticks;
            Form.Questions.Add(Name);
            Form.QuestionTypes.Add(Name, QuestionType);

            Container.Hover(0);
            Question.Click(0);
            Field.ReplaceText(Name, 0);

            if (QuestionType == Form.Type[2] || QuestionType == Form.Type[3])
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

        public CreateFormFrame DeleteQuestionByName(string questionName)
        {
            new WebItem($"//h3[text()='{questionName}']//ancestor::div[@class='header-question-title']//button", $"Кнопка удалить вопроса с названием {questionName}")
                .Click();

            return this;
        }

        public CreateFormFrame DeleteQuestionsFromTop(int questionNumber)
        {
            for (int i = 0; i < questionNumber; i++)
            {
                new WebItem("//button[@class='btn-close']", "Кнопка удаления вопроса")
                    .Click();
            }

            return this;
        }

        public bool IsAllQuestionsNamed(int QuestionsNumber)
        {
            

            for (int i = 0; i < QuestionsNumber; i++)
            {
                if (!new WebItem($"(//h3[text()='Название'])[{i}]", "Блок вопроса номер " + i).WaitElementDisplayed())
                {
                    Log.Error("Блок вопроса номер " + i + " не найден");
                    return false;
                }
                Log.Info("Блок вопроса номер " + i + " найден");
            }

            return true;
        }

        public bool IsEmptyFormAlertPresent()
        {
            var alertMessage = new WebItem("//div[@class='alert alert-danger' and text()='Нельзя создать форму без вопросов']", "Предупреждение после попытки форму без вопросов");
            bool isAlertPresent = Waiters.WaitForCondition(() => alertMessage.WaitElementDisplayed(), 2, 6, "Ожидание появления сообщения об ошибке");

            return isAlertPresent;
        }

        public bool IsSettingsOpened()
        {
            WebItem Field = new WebItem("//div[@class='mb-3']//label[text()='Время начала доступа к тесту']", "Описание поля даты и времени появления теста");

            if (Field.WaitElementDisplayed())
            {
                return true;
            }
            return false;
        }

        public CreateFormFrame SetQuestionsName(int QuestionsNumber, Form Form)
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

        public CreateFormFrame SetStartDate(string Date, string Time)
        {
            WebItem StartField = new WebItem("//div[@class='mb-3']//label[text()='Время начала доступа к тесту']/parent::div/child::input", "Поле ввода даты и времени появления теста");

            StartField.SendKeys(Date);
            StartField.NotTextKey("ArrowRight");
            StartField.SendKeys(Time);

            return this;
        }

        public CreateFormFrame SetEndDate(string Date, string Time)
        {
            WebItem EndField = new WebItem("//div[@class='mb-3']//label[text()='Время конца доступа к тесту']/parent::div/child::input", "Поле ввода даты и времени закрытия теста");

            EndField.SendKeys(Date);
            EndField.NotTextKey("ArrowRight");
            EndField.SendKeys(Time);

            return this;
        }

        public CreateFormFrame SetTimer(string Time)
        {
            new WebItem("//div[@class='mb-3']//label[text()='Таймер на прохождение']/parent::div/child::input", "Поле ввода даты и времени")
                .SendKeys(Time);

            return this;

        }

        public CreateFormFrame SetAttempts(string Attempts)
        {
            new WebItem("//div[@class='mb-3']//label[text()='Количество попыток']/parent::div/child::input", "Поле ввода даты и времени")
                .SendKeys(Attempts);

            return this;
        }

        public CreateFormFrame SetAnon()
        {
            new WebItem("//div[@class='mb-3']//label[text()='Анонимная форма']/parent::div/child::input", "Пункт 'Анонимная форма'")
                .Click();

            return this;
        }

        public CreateFormFrame SetActive()
        {
            new WebItem("//div[@class='mb-3']//label[text()='Форма активна']/parent::div/child::input", "Пункт 'Форма активна'")
                .Click();

            return this;
        }

        public CreateFormFrame SetQuestionToTestType(string questionName)
        {
            WebItem Button = 
                new WebItem($"(//h3[text()='{questionName}']//ancestor::div[@class='row']//div[@class='form-check']/input)[last()]", "Радиокнопка переключения вопроса в тестовый тип");
            
            Button.Hover(0);
            Button.Click(0);

            return this;
        }

        public CreateFormFrame SetQuestionToNonTestType(string questionName)
        {
            WebItem Button = 
                new WebItem($"(//h3[text()='{questionName}']//ancestor::div[@class='row']//div[@class='form-check']/input)[1]", "Радиокнопка переключения вопроса в тип без проверки");

            Button.Hover(0);    
            Button.Click(0);

            return this;
        }

        public CreateFormFrame SetFormRightAnswers(Form form)
        {

            return this;
        }

        public CreateFormFrame SetFormProperties(FormSettings Settings)
        {
            CreateFormFrame Form = new CreateFormFrame();

            if (Settings.StartDate != null && Settings.StartTime != null)
            {
                Form.SetStartDate(Settings.StartDate, Settings.StartTime);
            }

            if (Settings.EndDate != null && Settings.EndTime != null)
            {
                Form.SetEndDate(Settings.EndDate, Settings.EndTime);

            }

            if (Settings.Timer != null)
            {
                Form.SetTimer(Settings.Timer);
            }

            if (Settings.Attempts != null)
            {
                Form.SetAttempts(Settings.Attempts);

            }

            return this;
        }
        public FormsMainPage SaveForm()
        {
            Button.Hover();
            Button.Click();

            Waiters.StaticWait_s(2);

            return new FormsMainPage();
        }

        public CreateFormFrame SaveFormWithErrors()
        {
            //WebItem Button = new WebItem("//button[text()='Сохранить']", "Кнопка 'Сохранить'");
            Button.Hover();
            Button.Click();

            return this;
        }

        public CreateFormFrame SwitchToSettings()
        {
            WebItem Settings = new WebItem("//button[text()='Настройки']", "Панель настроек");

            Settings.Hover();
            Settings.Click();

            return this;
        }

        public CreateFormFrame SwitchToQuestions()
        {
            new WebItem("//button[text()='Вопросы']", "Панель вопросов")
                .Click();

            return this;
        }

        public CreateFormFrame RenameForm(string formNameBefore, string formNameAfter)
        {
            new WebItem($"//h1[text()='{formNameBefore}']", "Название формы")
                .Click();
            new WebItem($"//input[@type='text' and @value='{formNameBefore}']", "Полее ввода названия формы")
                .ReplaceText(formNameAfter);
            new WebItem("//body", "Фрейм создания формы")
                .Click();

            return this;
        }

        public CreateFormFrame RenameOneQuestion(string questionNameBefore, string questionNameAfter)
        {
            WebItem Question = new WebItem($"//h3[text()='{questionNameBefore}']", "Поле названия вопроса");
            WebItem Field = new WebItem($"//input[@value='{questionNameBefore}']", "Поле для ввода названия вопроса");
            Question.Hover(0);
            Question.Click(0);
            Field.ReplaceText(questionNameAfter);
            //костыль чтобы выйти из инпута имени вопроса
            new WebItem("//body", "Верхняя панель в блоке вопроса").Click();

            return this;
        }
    }


}