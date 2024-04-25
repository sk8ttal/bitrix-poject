using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATframework3demo.TestEntities
{
    public class AllQuestionTypesForm
    {   
        public AllQuestionTypesForm(string title)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
        }

        public string Title { get; set; }
        public string Description { get; set; }
    }
}