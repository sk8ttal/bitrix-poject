using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aTframework3demo.TestEntities
{
    public class Form
    {
        public Form(string title, int questionsNumber)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            var questions = new Dictionary<int, string>();
            Options = new Dictionary<string, List<string>>();
            Answers = new Dictionary<string, string>();
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
        public  Dictionary<string, List<string>> Options { get; set; }
        public  Dictionary<string, string> Answers { get; set; }
        public Dictionary<int, string> Type = new Dictionary<int, string>()
        {
            [1] = "Текстоввый ввод",
            [2] = "Один из списка",
            [3] = "Несколько из списка",
        };
    }
}