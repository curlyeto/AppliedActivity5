using Course_Selection.Model;
using Course_Selection.View;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_Selection.ViewModel;



internal class EditCoursesViewModel : INotifyPropertyChanged
{
    FirebaseClient firebaseClient = new Firebase.Database.FirebaseClient("https://applied-5-course-selection-default-rtdb.firebaseio.com/");
    private INavigation _navigation;

    private string code;

    private string name;
    private string description;

    public event PropertyChangedEventHandler PropertyChanged;

    public Command AddCourse { get; }

    public Courses courses { get; set; }


    public string Code
    {
        get => code; set
        {
            code = value;
            RaisePropertyChanged("Code");
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
    public string Description
    {
        get => description; set
        {
            description = value;
            RaisePropertyChanged("Description");
        }
    }

    public EditCoursesViewModel(INavigation navigation, Courses resonseCourses)
    {

        this._navigation = navigation;
        this.courses=resonseCourses;
        Code = resonseCourses.Code;
        Name = resonseCourses.Name;
        Description = resonseCourses.Description;

        AddCourse = new Command(AddCourseBtnTappedAsync);
    }

    private async void AddCourseBtnTappedAsync(object obj)
    {

        try
        {
           await firebaseClient.Child("Courses").Child(this.courses.Key).PutAsync(new Courses
            {
                Key = this.courses.Key,
                Code = Code,
                Name = Name,
                Description = Description
            });

            await this._navigation.PushAsync(new CoursesPage());

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
