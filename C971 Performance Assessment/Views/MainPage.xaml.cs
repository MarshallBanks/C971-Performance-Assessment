using C971_Performance_Assessment.View_Models;
using C971_Performance_Assessment.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace C971_Performance_Assessment
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            // Subscribe to the PenTapped message and set the focus to the TitleEntry element
            MessagingCenter.Subscribe<MainViewModel>(this, "PenTapped", OnPenTapped);

        }

        private void TitleEntry_Completed(object sender, EventArgs e)
        {
            MainViewModel mainViewModel = (MainViewModel)BindingContext;
            mainViewModel.OnEntryCompleted();
        }

        private void OnPenTapped(MainViewModel sender)
        {
            Label titleLabel = this.FindByName<Label>("TitleLabel");
            Entry titleEntry = this.FindByName<Entry>("TitleEntry");
            if (titleLabel.IsVisible == false)
            {
                titleEntry?.Focus();
            }
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            MainViewModel mainViewModel = (MainViewModel)BindingContext;
            mainViewModel.OnTermSelected();
        }

        private void TitleEntry_Focused(object sender, FocusEventArgs e)
        {
            var entry = (Entry)sender;
            entry.CursorPosition = entry.Text?.Length ?? 0;
        }
    }
}
