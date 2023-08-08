using Course_Selection.Model;
using Course_Selection.View.Course;
using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Maui.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Threading;
namespace Course_Selection.ViewModel;

internal class CourseViewModel : INotifyPropertyChanged
{
    FirebaseClient firebaseClient = new Firebase.Database.FirebaseClient("https://applied-5-course-selection-default-rtdb.firebaseio.com/");
    public ObservableCollection<Courses> CoursesList { get; set; } = new ObservableCollection<Courses>();
    private INavigation _navigation;
 
    // Handler when input change in the xmal file
    public event PropertyChangedEventHandler PropertyChanged;
    // RegisterBtn called we denifed before xmal file 
    public Command AddCoursePage { get; }
    public Command EditCoursePage { get; }
    public Command DeleteCourse { get; }

    public ICommand EditCoursePageCommand => new Command<Courses>(GoToEditCoursePage);
    public ICommand DeleteCourseCommand => new Command<Courses>(DeleteCoursePage);


    public CourseViewModel(INavigation navigation)
    {
        // assgined navigation
        this._navigation = navigation;
        // when register button click we define new method
        AddCoursePage = new Command(GoToAddCoursePage);
        //EditCoursePage = new Command(GoToEditCoursePage);
        //DeleteCourse = new Command(DeleteCoursePage);
        var collection = firebaseClient.Child("Courses").AsObservable<Courses>().Subscribe((dbevent) =>
        {
            if (dbevent.Object != null)
            {
                CoursesList.Add(dbevent.Object);
            }
        }
       );

    }

    private async void GoToAddCoursePage(object obj)
    {
        await this._navigation.PushAsync(new AddCoursePage());

    }
    private async void GoToEditCoursePage(Courses selectedCourse)
    {
        await this._navigation.PushAsync(new EditCoursePage(selectedCourse));

    }
    private async void DeleteCoursePage(Courses selectedCourse)
    {
        CoursesList.Remove(selectedCourse);
        await firebaseClient.Child("Courses").Child(selectedCourse.Key).DeleteAsync();
        CoursesList.Remove(selectedCourse);






    }




    private void RaisePropertyChanged(string v)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
    }
}
