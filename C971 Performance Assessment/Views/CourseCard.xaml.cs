using C971_Performance_Assessment.Data;
using C971_Performance_Assessment.Pages;
using C971_Performance_Assessment.View_Models;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971_Performance_Assessment.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class CourseCard : ContentView
    {

        public static readonly BindableProperty CourseProperty = BindableProperty.Create(
            nameof(Course),
            typeof(Course),
            typeof(CourseCard),
            null);

        public Course Course
        {
            get { return (Course)GetValue(CourseProperty); }
            set { SetValue(CourseProperty, value); }
        }

        public CourseCard()
        {
            InitializeComponent();
        }


        private async Task OpenActionSheet()
        {
            // Show the action sheet to the user
            string action = await Application.Current.MainPage.DisplayActionSheet("Options", "Cancel", null, "Add New Course", "Edit", "Delete", "View Details");

            // Handle the user's choice
            switch (action)
            {
                case "Add New Course":
                    AddNewCourse();
                    break;
                case "Edit":
                    OpenCourseEditor();
                    break;
                case "Delete":
                    DeleteCourse();
                    break;
                case "View Details":
                    OpenCourseDetails();
                    break;
                case "Cancel":
                    // Do nothing
                    break;
            }
        }

        private void CardTapped(object sender, System.EventArgs e)
        {
            Debug.WriteLine("OnCardTapped Executed");

            OpenCourseDetails();
        }

        private void OpenCourseDetails()
        {
            _ = Application.Current.MainPage.Navigation.PushAsync(new CourseDetailsPage(Course));
        }

        private void OpenCourseEditor()
        {
            _ = Application.Current.MainPage.Navigation.PushAsync(new CourseEditorPage(Course));
        }

        private void AddNewCourse()
        {
            Debug.WriteLine("Message Sent");
            // Send the message to trigger the method in the MainViewModel
            MessagingCenter.Send(this, "AddNewCourseMessage");
        }

        private void DeleteCourse()
        {
            MessagingCenter.Send(this, "DeleteCourseMessage", Course);
        }

        private void EllipsesTapped(object sender, System.EventArgs e)
        {
            _ = OpenActionSheet();
        }
    }
}