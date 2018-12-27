using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBCWebAPI.Models
{
    public class Lesson
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string IDTP { get; set; }
        public string FileURLOnline { get; set; }
        public string FileURLDowload { get; set; }
        public string ImageURL { get; set; }
        public string Transcript { get; set; }
        public string Actor { get; set; }
        public string Sumary { get; set; }
        public string Vocabulary { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
    }
}
