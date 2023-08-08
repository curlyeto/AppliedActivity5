using CommunityToolkit.Mvvm.ComponentModel;
using Course_Selection.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_Selection.ViewModel
{
    public partial class StudentViewModel : ObservableObject
    {
        public ObservableCollection<Student> Students { get; } = new ObservableCollection<Student>();

        [ObservableProperty]
        string title;

        public StudentViewModel()
        {
            Title = "Students";

            // Add example recipes to the collection
            Students.Add(new Student
            {
               StudentId = 1,
               Name = "Ertugrul",
                CourseList = new List<int>(new int[3])

        });

            Students.Add(new Student
            {
                StudentId = 2,
                Name = "Ahmet",
                CourseList = new List<int>(new int[2])
            });

            Students.Add(new Student
            {
                StudentId = 3,
                Name = "Mehmet",
                CourseList = new List<int>(new int[5])
            });
        }

    }
}
