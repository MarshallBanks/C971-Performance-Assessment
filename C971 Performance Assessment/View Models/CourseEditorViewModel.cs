using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace C971_Performance_Assessment.View_Models
{
    class CourseEditorViewModel
    {
        public ICommand DoneTappedCommand { get; }

        public CourseEditorViewModel()
        {
            DoneTappedCommand = new Command(OnDoneTapped);
        }

        private void OnDoneTapped()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }

}
