using Course_Selection.ViewModel;

namespace Course_Selection.View.Course;

public partial class AddCoursePage : ContentPage
{
	public AddCoursePage()
	{
		InitializeComponent();
        BindingContext = new AddCourseViewModel(Navigation);
    }
}