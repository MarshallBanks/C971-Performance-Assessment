using C971_Performance_Assessment.Data;
using C971_Performance_Assessment.Pages;
using C971_Performance_Assessment.Views;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace C971_Performance_Assessment.View_Models
{
    public class CourseCardViewModel : INotifyPropertyChanged
    {
        private Course _course;

        public Course Course
        {
            get { return _course; }
            set
            {
                _course = value;
                OnPropertyChanged();
            }
        }

        public ICommand EllipsesTappedCommand { get; }
        public ICommand CardTappedCommand { get; }

        public CourseCardViewModel(Course course)
        {
            // Initialize the EllipsesTappedCommand Property with a new Command
            EllipsesTappedCommand = new Command(OnEllipsesTapped);

            CardTappedCommand = new Command(OnCardTapped);

            Course = course;
        }

        private async void OnEllipsesTapped()
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

        private void OnCardTapped()
        {
            Debug.WriteLine("OnCardTapped Executed");

            // Hide the navigation bar on the MainPage

            OpenCourseDetails();

        }

        private void OpenCourseDetails()
        {
            Application.Current.MainPage.Navigation.PushAsync(new CourseDetailsPage());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.WriteLine("CourseCard On Property Changed Reached");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}


