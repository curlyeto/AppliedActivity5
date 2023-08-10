using Course_Selection.Model;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_Selection.ViewModel;

class EditStudentViewModel : INotifyPropertyChanged
{
    FirebaseClient firebaseClient = new Firebase.Database.FirebaseClient("https://applied-5-course-selection-default-rtdb.firebaseio.com/");

    public ObservableCollection<Courses> CoursesList { get; set; } = new ObservableCollection<Courses>();
    private INavigation _navigation;

    private string studentId;

    private string name;
    private Courses course;


    public event PropertyChangedEventHandler PropertyChanged;

    public Command AddStudent { get; }

    public Student student { get; set; }
    public string StudentId
    {
        get => studentId; set
        {
            studentId = value;
            RaisePropertyChanged("StudentId");
        }
    }
    public string Name
    {
        get => name; set
        {
            name = value;
            RaisePropertyChanged("Name");
        }
    }
    public Courses Course
    {
        get => course; set
        {
            course = value;
            RaisePropertyChanged("Course");
        }
    }


    public EditStudentViewModel(INavigation navigation,Student student)
    {

        this._navigation = navigation;

        this.student = student;
        this.Name = student.Name;
        this.StudentId = student.StudentId;


        AddStudent = new Command(AddStudentBtnTappedAsync);

        var collection = firebaseClient.Child("Courses").AsObservable<Courses>().Subscribe((dbevent) =>
        {
            if (dbevent.Object != null)
            {
                CoursesList.Add(dbevent.Object);
            }
        }
    );
    }

    private async void AddStudentBtnTappedAsync(object obj)
    {

        try
        {
   

           
            await firebaseClient.Child("Students").Child(this.student.Key).PutAsync(new Student
            {
                Key = this.student.Key,
                StudentId = StudentId,
                Name = Name,
                CourseName = Course == null ? this.student.CourseName : Course.Name,
            });



            await this._navigation.PushAsync(new MainPage());
            
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }

    }



    private void RaisePropertyChanged(string v)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
    }
}
