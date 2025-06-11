using System.Collections.Generic;
using System.Linq;

namespace Model.Custom.EvaluacionGridView
{
    public class ExampleViewModel
    {
        public ExampleViewModel()
        {
            Answers = new Dictionary<string, string>();
            Questions = new List<KeyValuePair<string, List<string>>>();
            questionss = new List<string>();
            
        }
        public string Identificacion { get; set; }
        public Dictionary<string, string> Answers { get; set; }
        public List<KeyValuePair<string, List<string>>> Questions { get; set; }
        public List<string> questionss { get; set; }
        public ExampleViewModel Add(string key, string[] value,string enun)
        {
            Questions.Add(new KeyValuePair<string, List<string>>(key, value.ToList()));
            questionss.Add(enun);
            return this;
        }
    }
}