using C971_Performance_Assessment.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace C971_Performance_Assessment.View_Models
{
    class CourseEditorViewModel
    {
        public Course Course { get; set; }

        public ICommand DoneTappedCommand { get; }

        public CourseEditorViewModel(Course course)
        {
            Course = course;

            DoneTappedCommand = new Command(OnDoneTapped);
        }

        private void OnDoneTapped()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }

}
