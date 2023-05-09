using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using FontAwesome;
using System.Threading.Tasks;
using System.Linq;

namespace C971_Performance_Assessment.View_Models
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public const string CALENDAR_ICON = FontAwesomeIcons.Calendar;
        public const string CHECK_ICON = FontAwesomeIcons.Check;

        private bool _isTitleLabelVisible = true;

        public bool IsTitleLabelVisible
        {
            get => _isTitleLabelVisible;
            set { _isTitleLabelVisible = value; OnPropertyChanged(); }
        }

        private bool _isTitleEntryVisible;
        public bool IsTitleEntryVisible
        {
            get => _isTitleEntryVisible;
            set { _isTitleEntryVisible = value; OnPropertyChanged(); }
        }

        private bool _isDatePickerVisible;
        public bool IsDatePickerVisible
        {
            get => _isDatePickerVisible;
            set { _isDatePickerVisible = value; OnPropertyChanged(); }
        }

        private bool _isDateLabelVisible = true;
        public bool IsDateLabelVisible
        {
            get => _isDateLabelVisible;
            set { _isDateLabelVisible = value; OnPropertyChanged(); }
        }

        private string _dateIcon = CALENDAR_ICON;
        public string DateIcon
        {
            get => _dateIcon;
            set { _dateIcon = value; OnPropertyChanged(); }
        }



        public ICommand PenTappedCommand { get; }
        public ICommand DateIconTappedCommand { get; }
        public ICommand EntryCompletedCommand { get; }
 
        public MainViewModel()
        {
            PenTappedCommand = new Command(OnPenTapped);
            DateIconTappedCommand = new Command(OnDateIconTapped);
            EntryCompletedCommand = new Command(OnEntryCompleted);
        }

        private void OnPenTapped()
        {
            Debug.WriteLine("OnPenTapped Reached.");

            IsTitleLabelVisible = false;
            IsTitleEntryVisible = true;

            MessagingCenter.Send(this, "PenTapped");

        }

        private void OnDateIconTapped()
        {
            Debug.WriteLine("OnDateIconTapped Reached.");

            IsDatePickerVisible = !IsDatePickerVisible;
            IsDateLabelVisible = !IsDateLabelVisible;

            DateIcon = (DateIcon == CALENDAR_ICON) ? CHECK_ICON : CALENDAR_ICON;

        }

        public void OnEntryCompleted()
        {
            Debug.WriteLine("OnEntryCompleted executed.");

            IsTitleLabelVisible = true;
            IsTitleEntryVisible = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
