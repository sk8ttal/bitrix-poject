using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aTframework3demo.TestEntities
{
    public class Form
    {
        static string Name() => "Вопрос " + DateTime.Now.Ticks;
        static string Answer(string Text = "") => $"Ответ {Text} " + DateTime.Now.Ticks;
        public Form(string title, int TextQuestions = 1, int OneFromListQuestions = 0, int ManyFromListQuestions = 0, int OptionsNumber = 0)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            QuestionsNumber = TextQuestions + OneFromListQuestions + ManyFromListQuestions;
            Random Random = new Random();

            if (TextQuestions > 0)
            {
                for (int i = 0; i < TextQuestions; i++)
                {
                    string name = Name();
                    string answer = Answer();

                    Questions.Add(name);
                    CorrectAnswers.Add(name, new List<string> { answer });
                    Answers.Add(name, new List<string> { answer });
                    QuestionTypes.Add(name, TypeNames[QuestionType.Text]);
                }
            }
            if (OneFromListQuestions > 0)
            {
                for (int i = 0; i < OneFromListQuestions; i++)
                {
                    string name = Name();
                    List<string> OptionsList = new();

                    for (int j = 0; j < OptionsNumber+1; j++)
                    {
                        string Option = Answer($"{j + 1}");
                        OptionsList.Add(Option);
                    }
                    Questions.Add(name);
                    Options.Add(name, OptionsList);
                    CorrectAnswers.Add(name, new List<string> { Options[name][0] });
                    QuestionTypes.Add(name, TypeNames[QuestionType.One_from_list]);

                    int Index = Random.Next(Options[name].Count);
                    Answers.Add(name, new List<string> { Options[name][Index] });
                }
            }
            if (ManyFromListQuestions > 0)
            {
                for (int i = 0; i < ManyFromListQuestions; i++)
                {
                    string name = Name();

                    List<string> OptionsList = new();
                    for (int j = 0; j < OptionsNumber+1; j++)
                    {
                        string Option = Answer($"{j + 1}");
                        OptionsList.Add(Option);
                    }
                    Questions.Add(name);
                    Options.Add(name, OptionsList);
                    CorrectAnswers.Add(name, new List<string> { Options[name][0], Options[name][1] });
                    QuestionTypes.Add(name, TypeNames[QuestionType.Many_from_list]);

                    int Index = Random.Next(Options[name].Count);
                    Answers.Add(name, new List<string> { Options[name][Index] });
                }
            }
        }

        public string Title { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Timer { get; set; }
        public string Attempts { get; set; }
        public int QuestionsNumber { get; set; }
        public List<string> Questions { get; set; } = new();
        public Dictionary<string, string> QuestionTypes { get; set; } = new();
        public Dictionary<string, List<string>> Options { get; set; } = new();
        public Dictionary<string, List<string>> Answers { get; set; } = new();
        public Dictionary<string, List<string>> CorrectAnswers { get; set; } = new();

        public enum QuestionType
        {
            Text,
            One_from_list,
            Many_from_list
        }

        public static Dictionary<QuestionType, string> TypeNames = new()
        {
            {QuestionType.Text, "Текстовый ввод"},
            {QuestionType.One_from_list, "Один из списка"},
            {QuestionType.Many_from_list, "Несколько из списка"}
        };

    }
}