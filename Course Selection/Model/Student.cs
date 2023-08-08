using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_Selection.Model
{
    public class Student
    {
        public string Key;
        public int StudentId { get; set; }
        public string Name { get; set; }
        public List<int> CourseList { get; set; }
    }
}
