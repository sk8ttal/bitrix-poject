using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aTframework3demo.TestEntities
{
    public class TaskFromForm
    {
        public TaskFromForm(string Contractor, string Watcher, string Director)
        {
            this.Contractor = Contractor ?? throw new Exception(nameof(Contractor)); 
            this.Watcher = Watcher ?? throw new Exception(nameof(Watcher)); 
            this.Director = Director ?? throw new Exception(nameof(Director)); 
        }

        public string Contractor { get; set; }
        public string Watcher { get; set; }
        public string Director { get; set; }
    }
}