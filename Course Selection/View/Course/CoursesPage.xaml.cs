using Course_Selection.ViewModel;
using Firebase.Database;

namespace Course_Selection.View;

public partial class CoursesPage : ContentPage
{
	public CoursesPage()
	{
		InitializeComponent();
        BindingContext = new CourseViewModel(Navigation);
    }
   
}