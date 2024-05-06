using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aTframework3demo.TestEntities
{
    public class Validator
    {
        public Validator(int questionsNumber = 1)
        {
            QuestionsNumber = questionsNumber;

            for (int i = 1; i <= questionsNumber; i++)
            {
                XssQuestions.Add(i, "<script>alert(1)</script>");
            }
        }
        public string XSS = "<script>alert(1)</script>";
        public string EmptyString = " ";
        public int QuestionsNumber { get; set; }
        public Dictionary<int, string> XssQuestions { get; set; } = new Dictionary<int, string>();
    }
}