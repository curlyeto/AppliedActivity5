using Course_Selection.Model;
using Course_Selection.ViewModel;

namespace Course_Selection.View.Course;

public partial class EditCoursePage : ContentPage
{
	Courses courses = new Courses();
	public EditCoursePage(Courses resonseCourses)
	{
		InitializeComponent();
		courses=resonseCourses;
		Console.WriteLine(courses.Name);
        BindingContext = new EditCoursesViewModel(Navigation,resonseCourses);
    }
}