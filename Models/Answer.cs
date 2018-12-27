using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBCWebAPI.Models
{
    public class Answer
    {
        public string AnswerID { get; set; }
        public string Content { get; set; }
        public string QuestionID { get; set; }
        public string Correct { get; set; }
    }
}
