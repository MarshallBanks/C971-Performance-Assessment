using C971_Performance_Assessment.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace C971_Performance_Assessment.View_Models
{
    class CourseDetailsViewModel : INotifyPropertyChanged
    {
        public ICommand BackArrowTappedCommand { get; }
        public ICommand EllipsesTappedCommand { get; }

        public CourseDetailsViewModel()
        {
            BackArrowTappedCommand = new Command(OnBackArrowTapped);
            EllipsesTappedCommand = new Command(OnEllipsesTapped);
        }

        private void OnBackArrowTapped()
        {
            BackToMainPage();
        }

        private void BackToMainPage()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        private async void OnEllipsesTapped()
        {
            // Show the action sheet to the user
            string action = await Application.Current.MainPage.DisplayActionSheet("Options", "Cancel", null, "Edit", "Delete");

            // Handle the user's choice
            switch (action)
            {
                case "Edit":
                    OpenCourseEditor();
                    break;
                case "Delete":
                    // Edit the current course
                    break;
                case "Cancel":
                    // Do nothing
                    break;
            }
        }

        private void OpenCourseEditor()
        {
            Application.Current.MainPage.Navigation.PushAsync(new CourseEditorPage());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
