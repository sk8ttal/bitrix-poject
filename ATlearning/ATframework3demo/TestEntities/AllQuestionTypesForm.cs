using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aTframework3demo.TestEntities
{
    public class AllQuestionTypesForm
    {
        public AllQuestionTypesForm(string title, int questionsNumber)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            var questions = new Dictionary<int, string>();
            QuestionsNumber = questionsNumber;

            for (int i = 1; i <= questionsNumber; i++)
            {
                questions.Add(i, $"Вопрос {i}");
            }

            Questions = questions;
        }

        public string Title { get; set; }
        public int QuestionsNumber { get; set; }
        public  Dictionary<int, string> Questions { get; set; }
        public Dictionary<int, string> Type = new Dictionary<int, string>()
        {
            [1] = "Текстоввый ввод",
            [2] = "Один из списка",
            [3] = "Несколько из списка",
        };
    }
}