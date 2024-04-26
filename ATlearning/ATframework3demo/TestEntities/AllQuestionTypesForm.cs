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
            var Questions = new Dictionary<int, string>() ?? throw new ArgumentNullException(nameof(questionsNumber));;

            for (int i = questionsNumber; i > 0; i--)
            {
                Questions.Add(i, $"Вопрос {i}");
            }
        }

        public string Title { get; set; }
        public Dictionary<int, string> Type = new Dictionary<int, string>()
        {
            [1] = "Текстоввый ввод",
            [2] = "Один из списка",
            [3] = "Несколько из списка",
        };
    }
}