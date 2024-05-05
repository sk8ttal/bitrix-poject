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
        public List<string> Questions { get; set; } = new ();
        public Dictionary<string, string> QuestionTypes { get; set; } = new ();
        public Dictionary<string, List<string>> Options { get; set; } = new ();
        public Dictionary<string, List<string>> Answers { get; set; } = new ();
        public string StartDate {get; set;}
        public string EndDate {get; set;}
        public string StartTime {get; set;}
        public string EndTime {get; set;}
        public string Timer {get; set;}
        public string Attempts {get; set;}
        // public Dictionary<int, string> Type = new Dictionary<int, string>()
        // {
        //     [1] = "Текстоввый ввод",
        //     [2] = "Один из списка",
        //     [3] = "Несколько из списка",
        // };

        public enum Type
        {
            Text,
            One_from_list,
            Many_from_list
        }

        public static Dictionary<Type, string> TypeNames = new ()
        {
            {Type.Text, "Текстовый ввод"},
            {Type.One_from_list, "Один из списка"},
            {Type.Many_from_list, "Несколько из списка"}
        };
    }
}