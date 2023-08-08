using Course_Selection.Model;
using Course_Selection.View;
using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Maui.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course_Selection.ViewModel;


internal class AddCourseViewModel : INotifyPropertyChanged
{
    FirebaseClient firebaseClient = new Firebase.Database.FirebaseClient("https://applied-5-course-selection-default-rtdb.firebaseio.com/");
    private INavigation _navigation;

    private string code;

    private string name;
    private string description;

    public event PropertyChangedEventHandler PropertyChanged;

    public Command AddCourse { get; }


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

    public AddCourseViewModel(INavigation navigation)
    {

        this._navigation = navigation;


        AddCourse = new Command(AddCourseBtnTappedAsync);
    }

    private async void AddCourseBtnTappedAsync(object obj)
    {

        try
        {
            
            var response = await firebaseClient.Child("Courses").PostAsync(new Courses
            { 
                Code = Code,
                Name = Name,
                Description = Description
            });
            await firebaseClient.Child("Courses").Child(response.Key).PutAsync(new Courses
            {
                Key = response.Key,
                Code = Code,
                Name = Name,
                Description = Description
            });

            Console.WriteLine("Response "+response.Key);

            // Check the status code to determine if the data was saved successfully
            if (response !=null)
            {
                await this._navigation.PushAsync(new CoursesPage());
            }
            else
            {
                Console.WriteLine("Failed to save data" );
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