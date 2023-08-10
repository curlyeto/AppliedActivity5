using Course_Selection.ViewModel;

namespace Course_Selection.View.Student;

public partial class AddStudent : ContentPage
{
	public AddStudent()
	{
		InitializeComponent();
        BindingContext = new AddStudentViewModel(Navigation);
    }
}