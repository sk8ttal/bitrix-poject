using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aTframework3demo.TestEntities
{
    public class Form
    {
        public Form(string title, int questionsNumber = 1)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            QuestionsNumber = questionsNumber;
        }

        public string Title { get; set; }
        public int QuestionsNumber { get; set; }
        public List<string> Questions { get; set; } = new List<string>();
        public Dictionary<string, string> QuestionTypes { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, List<string>> Options { get; set; } = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> Answers { get; set; } = new Dictionary<string, List<string>>();
        public Dictionary<int, string> Type = new Dictionary<int, string>()
        {
            [1] = "Текстоввый ввод",
            [2] = "Один из списка",
            [3] = "Несколько из списка",
        };
    }
}