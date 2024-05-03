using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aTframework3demo.TestEntities
{
    public class Validator
    {
        public Validator(int questionsNumber = 6)
        {
            QuestionsNumber = questionsNumber;

            for (int i = 0; i < questionsNumber; i++)
            {
                Questions.Add(i, "Вопрос " + DateTime.Now.Ticks);
            }
        }
        public string Xss = "<script>alert(1)</script>";
        public int QuestionsNumber { get; set; }
        public Dictionary<int, string> Questions { get; set; } = new Dictionary<int, string>();
    }
}