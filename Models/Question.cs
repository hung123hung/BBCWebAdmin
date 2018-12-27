using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBCWebAPI.Models
{
    public class Question
    {
        public string QuestionID { get; set; }
        public string Content { get; set; }
        public string TypeQuestion { get; set; }
        public string LessonID { get; set; }
    }
}
