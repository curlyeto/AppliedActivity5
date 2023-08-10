using CommunityToolkit.Mvvm.ComponentModel;
using Course_Selection.Model;
using Course_Selection.View.Course;
using Course_Selection.View.Student;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Course_Selection.ViewModel;

public partial class StudentViewModel : INotifyPropertyChanged
{
    FirebaseClient firebaseClient = new Firebase.Database.FirebaseClient("https://applied-5-course-selection-default-rtdb.firebaseio.com/");
    public ObservableCollection<Student> StudentList { get; set; } = new ObservableCollection<Student>();
    private INavigation _navigation;

    // Handler when input change in the xmal file
    public event PropertyChangedEventHandler PropertyChanged;
    // RegisterBtn called we denifed before xmal file 
    public Command AddStudentPage { get; }


    public ICommand EditStudentPageCommand => new Command<Student>(GoToEditStudentPage);
    public ICommand DeleteStudentCommand => new Command<Student>(DeleteStudentPage);


    public StudentViewModel(INavigation navigation)
    {
        // assgined navigation
        this._navigation = navigation;
        // when register button click we define new method
        AddStudentPage = new Command(GoToAddStudentPage);
        //EditCoursePage = new Command(GoToEditCoursePage);
        //DeleteCourse = new Command(DeleteCoursePage);
        var collection = firebaseClient.Child("Students").AsObservable<Student>().Subscribe((dbevent) =>
        {
            if (dbevent.Object != null)
            {
                StudentList.Add(dbevent.Object);
            }
        }
       );

    }

    private async void GoToAddStudentPage(object obj)
    {
        await this._navigation.PushAsync(new AddStudent());

    }
    private async void GoToEditStudentPage(Student selectedCourse)
    {
        await this._navigation.PushAsync(new EditStudentPage(selectedCourse));

    }
    private async void DeleteStudentPage(Student selectedCourse)
    {
        StudentList.Remove(selectedCourse);
        await firebaseClient.Child("Students").Child(selectedCourse.Key).DeleteAsync();
        StudentList.Remove(selectedCourse);






    }




    private void RaisePropertyChanged(string v)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
    }

}
