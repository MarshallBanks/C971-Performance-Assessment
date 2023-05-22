using C971_Performance_Assessment.Pages;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971_Performance_Assessment.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class CourseCard : ContentView
    {
        public CourseCard()
        {
            InitializeComponent();
        }

        private void OpenCourseDetails()
        {
            _ = Application.Current.MainPage.Navigation.PushAsync(new CourseDetailsPage());
        }

        private void CardTapped(object sender, System.EventArgs e)
        {
            Debug.WriteLine("OnCardTapped Executed");

            OpenCourseDetails();
        }

        private async Task OpenActionSheet()
        {
            // Show the action sheet to the user
            string action = await Application.Current.MainPage.DisplayActionSheet("Options", "Cancel", null, "Add New Course", "Edit", "Delete", "View Details");

            // Handle the user's choice
            switch (action)
            {
                case "Add New Course":
                    // Add a new course
                    break;
                case "Edit":
                    _ = Application.Current.MainPage.Navigation.PushAsync(new CourseEditorPage());
                    break;
                case "Delete":
                    // Delete the current course
                    break;
                case "View Details":
                    OpenCourseDetails();
                    break;
                case "Cancel":
                    // Do nothing
                    break;
            }
        }

        private void EllipsesTapped(object sender, System.EventArgs e)
        {
            _ = OpenActionSheet();
        }
    }
}