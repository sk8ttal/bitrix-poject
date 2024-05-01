using atFrameWork2.BaseFramework;
using atFrameWork2.SeleniumFramework;

namespace ATframework3demo.PageObjects
{
    public class CreateFormFrame
    {
        WebItem Question = new WebItem("//h3[text()='Название']", "Поле названия вопроса");
        WebItem Field = new WebItem("//input[@value='Название']", "Поле для ввода названия вопроса");
        WebItem Container = new WebItem($"//p[text()='Короткий ответ']", "Блок вопроса");
        WebItem Button = new WebItem("//button[text()='Сохранить']", "Кнопка 'Сохранить'");

        public CreateFormFrame ChangeFormTitle(string Title)
        {
            new WebItem("//h1[text()='Новая форма']", "Название формы")
                .Click();
            new WebItem("//input[@type='text' and @value='Новая форма']", "Полее ввода названия формы")
                .ReplaceText(Title);
            //пока нужно выходить из поля названия формы таким костылем
            new WebItem("//body", "Фрейм создания формы")
                .Click();

            return this;
        }

        public CreateFormFrame RenameForm(string formNameBefore, string formNameAfter)
        {
            new WebItem($"//h1[text()='{formNameBefore}']", "Название формы")
                .Click();
            new WebItem($"//input[@type='text' and @value='{formNameBefore}']", "Полее ввода названия формы")
                .ReplaceText(formNameAfter);
            //пока нужно выходить из поля названия формы таким костылем
            new WebItem("//body", "Фрейм создания формы")
                .Click();

            return this;
        }

        public CreateFormFrame AddQuestion(int QuestionsNumber = 1)
        {
            for (int i = QuestionsNumber; i > 0; i--)
            {
                new WebItem("//button[text()='+']", "Кнопка добавления вопроса")
                    .Click(0);
            }

            return this;
        }

        public CreateFormFrame DeleteQuestionByName(string questionName)
        {
            new WebItem($"//button[@class='btn btn-danger' and ./parent::div/parent::div//div[@class='col text-left' and .//h3[text()='{questionName}']]]", $"Кнопка удалить вопроса с названием {questionName}")
                .Click();

            return this;
        }

        public CreateFormFrame DeleteQuestionsFromTop(int questionNumber)
        {
            for (int i = 0; i < questionNumber; i++)
            {
                new WebItem("//button[@class='btn btn-danger']", "Кнопка удаления вопроса")
                    .Click();
            }

            return this;
        }

        public CreateFormFrame RenameOneQuestion(string questionNameBefore, string questionNameAfter)
        {
            WebItem Question = new WebItem($"//h3[text()='{questionNameBefore}']", "Поле названия вопроса");
            WebItem Field = new WebItem($"//input[@value='{questionNameBefore}']", "Поле для ввода названия вопроса");
            Question.Click();
            Field.ReplaceText(questionNameAfter);
            //костыль чтобы выйти из инпута имени вопроса
            new WebItem("//body", "Верхняя панель в блоке вопроса").Click();

            return this;
        }

        public CreateFormFrame SetQuestionsName(Dictionary<int, string> Names)
        {


            foreach (var questionName in Names)
            {
                Question.Hover();
                Question.Click();
                Field.ReplaceText(questionName.Value);
            }

            new WebItem("//body", "Фрейм слайдера создания формы").Click();

            return this;
        }

        public CreateFormFrame ChangeQuestionType(string QuestionName, string QuestionType)
        {
            WebItem Selector = new WebItem($"//h3[text()='{QuestionName}']/ancestor::div[@class='row']//select", $"Список типов вопроса для блока {QuestionName}");
            // ancestor и многое другое можно найти на https://msiter.ru/tutorials/xpath/axes (не реклама)
            Selector
                .Hover();
            Selector
                .Click();
            new WebItem($"//h3[text()='{QuestionName}']/ancestor::div[@class='row']//option[text()='{QuestionType}']", $"Тип вопроса {QuestionType}")
                .Click();

            return this;
        }

        public CreateFormFrame AddNewOption(string QuestionName, int OptionsNumber = 1)
        {
            WebItem Button = new WebItem($"//h3[text()='{QuestionName}']/ancestor::div[@class='card mb-3 mt-3']//button[text()='+']",
                $"Кнопка добавления опции к вопросу {QuestionName}");

            for (int i = OptionsNumber; i > 0; i--)
            {
                Button.Hover();
                Button.Click();
            }

            return this;
        }

        public CreateFormFrame ChangeOptionName(string QuestionName)
        {
            WebItem Option = new WebItem($"//h3[text()='{QuestionName}']/ancestor::div[@class='card mb-3 mt-3']/div[@class='card-body']//label[text()='Новая опция']",
                $"Поле названия опции для вопроса {QuestionName}");
            WebItem Field = new WebItem($"//h3[text()='{QuestionName}']/ancestor::div[@class='card mb-3 mt-3']/div[@class='card-body']//input",
                "Поле для ввода названия опции");
            WebItem Container = new WebItem($"//h3[text()='{QuestionName}']/ancestor::div[@class='card mb-3 mt-3']//input[@class='form-check-input']",
                "Блок вопроса");

            int i = 1;
            while (Option.WaitElementDisplayed())
            {
                Option.Click();
                Field.ReplaceText($"Ответ {i}");
                Container.Click();
                i++;
            }

            return this;
        }

        public FormsMainPage SaveForm()
        {
            Button.Hover();
            Button.Click();

            Waiters.StaticWait_s(2);
            WebDriverActions.Refresh();

            return new FormsMainPage();
        }

        public CreateFormFrame CreateHighLoadedQuestion(Dictionary<int, string> Names, string QuestionType, int QuestionsNumber, int OptionsNumber = 5)
        {
            for (int i = 1; i <= QuestionsNumber; i++)
            {
                string Name = Names[i];
                WebItem Selector = new WebItem($"//h3[text()='{Name}']/ancestor::div[@class='row']//select", $"Список типов вопроса для блока {Name}");
                WebItem Button = new WebItem($"//h3[text()='{Name}']/ancestor::div[@class='card mb-3 mt-3']//button[text()='+']",
                    $"Кнопка добавления опции к вопросу {Name}");

                Container.Hover(0);
                Question.Click(0);
                Field.ReplaceText(Name);
                Container.Click(0);
                Selector.Click(0);
                new WebItem($"//h3[text()='{Name}']/ancestor::div[@class='row']//option[text()='{QuestionType}']", $"Тип вопроса {QuestionType}")
                    .Click(0);

                for (int j = OptionsNumber; j > 0; j--)
                {
                    Button.Click(0);
                }
            }

            return this;
        }
        public CreateFormFrame SaveEmptyForm()
        {
            WebItem Button = new WebItem("//button[text()='Сохранить']", "Кнопка 'Сохранить'");
            Button.Hover();
            Button.Click();

            return this;
        }

        public bool IsEmptyFormAlertPresent()
        {
            var alertMessage = new WebItem("//div[@class='alert alert-danger' and text()='Нельзя создать форму без вопросов']", "Предупреждение после попытки форму без вопросов");
            bool isAlertPresent = Waiters.WaitForCondition(() => alertMessage.WaitElementDisplayed(), 2, 6, "Ожидание появления сообщения об ошибке");

            return isAlertPresent;
        }
    }
}