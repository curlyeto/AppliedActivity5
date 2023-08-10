using Course_Selection.Model;
using Course_Selection.ViewModel;
namespace Course_Selection.View.Student;

public partial class EditStudentPage : ContentPage
{
    private Model.Student selectedCourse;

    public EditStudentPage(Model.Student student)
	{
		InitializeComponent();
        BindingContext = new EditStudentViewModel(Navigation, student);
    }

  
}