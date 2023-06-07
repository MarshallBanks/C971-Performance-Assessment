using C971_Performance_Assessment.Data;
using C971_Performance_Assessment.Views;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace C971_Performance_Assessment.View_Models
{
    class CourseDetailsViewModel : INotifyPropertyChanged
    {
        private readonly AssessmentRepository _assessmentRepository;
        public ICommand BackArrowTappedCommand { get; }
        public ICommand EllipsesTappedCommand { get; }
        public ICommand ShareTappedCommand { get; }
        public Course Course { get; set; }
        public Assessment PerfAssessment { get; set; }
        public Assessment ObjAssessment { get; set; }

        private bool _perfAlertOn;
        public bool PerfAlertOn
        {
            get => _perfAlertOn;
            set { _perfAlertOn = value; OnPropertyChanged(); }
        }

        private bool _perfAlertOff; 
        public bool PerfAlertOff
        {
            get => _perfAlertOff;
            set { _perfAlertOff = value; OnPropertyChanged(); }
        }

        private bool _objAlertOn;
        public bool ObjAlertOn
        {
            get => _objAlertOn;
            set { _objAlertOn = value; OnPropertyChanged(); }
        }

        private bool _objAlertOff;
        public bool ObjAlertOff
        {
            get => _objAlertOff;
            set { _objAlertOff = value; OnPropertyChanged(); }
        }

        public string AlertStatusLabel => Course.IsDateAlertsActive ? "ON" : "OFF";

        private string _formattedStatus;
        public string FormattedStatus
        {
            get => _formattedStatus;
            set { _formattedStatus = value; OnPropertyChanged(); }
        }

        public CourseDetailsViewModel(Course course, Assessment perfAssessment, Assessment objAssessment)
        {
            Course = course;
            PerfAssessment = perfAssessment;
            ObjAssessment = objAssessment;
            FormattedStatus = Course.GetFormattedStatus();

            // Initialize the AssessmentRepository object
            _assessmentRepository = new AssessmentRepository(Database.GetInstance().GetConnection());

            SetAssessmentAlertIcons();

            BackArrowTappedCommand = new Command(OnBackArrowTapped);
            EllipsesTappedCommand = new Command(OnEllipsesTapped);
            ShareTappedCommand = new Command(OnShareTapped);

            // Message subscriber to get updated Course and assessments from Course Editor
            MessagingCenter.Subscribe<CourseEditorViewModel, (Course, Assessment, Assessment)>(this, "CourseUpdatedMessage", (sender, updateData) =>
            {
                (Course updatedCourse, Assessment updatedPerfAssessment, Assessment updatedObjAssessment) = updateData;
                Course = updatedCourse;
                PerfAssessment = updatedPerfAssessment;
                ObjAssessment = updatedObjAssessment;
                FormattedStatus = Course.GetFormattedStatus();
                SetAssessmentAlertIcons();
                OnPropertyChanged(nameof(Course));
                OnPropertyChanged(nameof(PerfAssessment));
                OnPropertyChanged(nameof(ObjAssessment));
            });
        }

        // Swaps visibility of bell on and bell off icon in xaml
        // For both obj and perf assessment
        private void SetAssessmentAlertIcons()
        {
            if (PerfAssessment.DateAlertIsOn)
            {
                PerfAlertOn = true;
                PerfAlertOff = false;
            }
            else if (!PerfAssessment.DateAlertIsOn)
            {
                PerfAlertOn = false;
                PerfAlertOff = true;
            }

            if (ObjAssessment.DateAlertIsOn)
            {
                ObjAlertOn = true;
                ObjAlertOff = false;
            }
            else if (!ObjAssessment.DateAlertIsOn)
            {
                ObjAlertOn = false;
                ObjAlertOff = true;
            }
        }

        private void OnBackArrowTapped()
        {
            // Send a message to Main Page to refresh Course Data
            MessagingCenter.Send(this, "RefreshCoursesMessage");

            BackToMainPage();
        }

        private void BackToMainPage()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        private void OnShareTapped()
        {
            Share.RequestAsync(new ShareTextRequest
            {
                Text = $"{Course.Title} Notes: {Course.Notes}",
                Title = "Share Notes"
            }); ;
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
                    DeleteCourse();
                    BackToMainPage();
                    break;
                case "Cancel":
                    // Do nothing
                    break;
            }
        }

        private void OpenCourseEditor()
        {
            Application.Current.MainPage.Navigation.PushAsync(new CourseEditorPage(Course, PerfAssessment, ObjAssessment));
        }

        private void DeleteCourse()
        {
            MessagingCenter.Send(this, "DeleteCourseMessage", Course);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
