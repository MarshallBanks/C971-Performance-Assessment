using C971_Performance_Assessment.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace C971_Performance_Assessment.View_Models
{
    public class CourseCardViewModel
    {
        public ICommand EllipsesTappedCommand { get; }
        public ICommand CardTappedCommand { get; }

        public CourseCardViewModel()
        {
            // Initialize the EllipsesTappedCommand Property with a new Command
            EllipsesTappedCommand = new Command(OnEllipsesTapped);

            CardTappedCommand = new Command(OnCardTapped);
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
                    // Edit the current course
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

    }
}


