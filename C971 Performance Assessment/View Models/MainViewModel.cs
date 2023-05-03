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

        private bool _isTitleLabelVisible;

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


        public ICommand PenTappedCommand { get; }
        public ICommand EntryCompletedCommand { get; }

        public MainViewModel()
        {
            PenTappedCommand = new Command(OnPenTapped);
            EntryCompletedCommand = new Command(OnEntryCompleted);

            IsTitleLabelVisible = true;
        }

        private void OnPenTapped()
        {
            Debug.WriteLine("OnPenTappedReached");

            IsTitleLabelVisible = !IsTitleLabelVisible;
            IsTitleEntryVisible = !IsTitleEntryVisible;

            MessagingCenter.Send(this, "PenTapped");

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
