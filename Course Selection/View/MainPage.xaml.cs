using Course_Selection.Model;
using Course_Selection.ViewModel;
using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;

namespace Course_Selection;

public partial class MainPage : ContentPage
{

	
    public MainPage()
	{
        InitializeComponent();

        BindingContext = new StudentViewModel();

    }

   
}

