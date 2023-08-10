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

class AddStudentViewModel : INotifyPropertyChanged
{
    FirebaseClient firebaseClient = new Firebase.Database.FirebaseClient("https://applied-5-course-selection-default-rtdb.firebaseio.com/");

    public ObservableCollection<Courses> CoursesList { get; set; } = new ObservableCollection<Courses>();
    private INavigation _navigation;

    private string studentId;

    private string name;
    private Courses course;


    public event PropertyChangedEventHandler PropertyChanged;

    public Command AddStudent { get; }


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


    public AddStudentViewModel(INavigation navigation)
    {

        this._navigation = navigation;


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
            Console.WriteLine("***********************");
            Console.WriteLine(StudentId);
            Console.WriteLine(Name);
            Console.WriteLine(Course.Name);
            Console.WriteLine("***********************");

            var response = await firebaseClient.Child("Students").PostAsync(new Student
                {
                    StudentId = StudentId,
                    Name = Name,
                    CourseName = Course.Name
                }
             );
            await firebaseClient.Child("Students").Child(response.Key).PutAsync(new Student
            {
                Key = response.Key,
                StudentId = StudentId,
                Name = Name,
                CourseName = Course.Name
            });

            Console.WriteLine("Response " + response.Key);

            // Check the status code to determine if the data was saved successfully
            if (response != null)
            {
                await this._navigation.PushAsync(new MainPage());
            }
            else
            {
                Console.WriteLine("Failed to save data");
            }
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
