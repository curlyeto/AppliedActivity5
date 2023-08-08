using Course_Selection.Model;
using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;

namespace Course_Selection;

public partial class TestPage : ContentPage
{
    FirebaseClient firebaseClient = new Firebase.Database.FirebaseClient("https://applied-5-course-selection-default-rtdb.firebaseio.com/");
    public ObservableCollection<MyDatabaseRecord> DatabaseItems { get; set; } = new ObservableCollection<MyDatabaseRecord>();
    public TestPage()
	{
		InitializeComponent();
        BindingContext = this;
        var collection = firebaseClient.Child("Records").AsObservable<MyDatabaseRecord>().Subscribe((dbevent) =>
        {
            if (dbevent.Object != null)
            {
                DatabaseItems.Add(dbevent.Object);
            }
        }
        );
    }
    private void OnCounterClicked(object sender, EventArgs e)
    {

        firebaseClient.Child("Records").PostAsync(new MyDatabaseRecord
        {
            MyProperty = recordData.Text
        });
        recordData.Text = "";
    }
}