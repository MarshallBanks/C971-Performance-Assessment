using C971_Performance_Assessment.Data;
using C971_Performance_Assessment.Pages;
using C971_Performance_Assessment.View_Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971_Performance_Assessment.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class CourseCard : ContentView
    {
        private readonly AssessmentRepository _assessmentRepository;
        public Assessment PerfAssessment { get; set; }
        public Assessment ObjAssessment { get; set; }

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

            // Initialize the TermRepository object
            _assessmentRepository = new AssessmentRepository(Database.GetInstance().GetConnection());

            _ = LoadAssessments();
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
            if(ObjAssessment == null)
            {
                Debug.Write($"Objective Assessment is NULL");
            }
            else
            {
                Debug.Write($"Objective Assessment is called {ObjAssessment.Title}");
                _ = Application.Current.MainPage.Navigation.PushAsync(new CourseDetailsPage(Course, PerfAssessment, ObjAssessment));
            }
        }

        private void OpenCourseEditor()
        {
            Debug.Write($"Objective Assessment is called {ObjAssessment.Title}");
            _ = Application.Current.MainPage.Navigation.PushAsync(new CourseEditorPage(Course, PerfAssessment, ObjAssessment));
        }

        private async Task LoadAssessments()
        {
            // Had to add a while loop to better time assigning
            // Obj and Perf with saving assessments in database
            // otherwise would get a null object reference
            int maxIterations = 10; 
            int iterationCount = 0;

            while ((PerfAssessment == null || ObjAssessment == null) && iterationCount < maxIterations)
            {
                ++iterationCount;

                List<Assessment> assessments = await _assessmentRepository.GetAssessmentsAsync();

                foreach (Assessment assessment in assessments)
                {
                    if (assessment.CourseId == Course.Id && assessment.Type == AssessmentType.Performance)
                    {
                        PerfAssessment = assessment;
                        OnPropertyChanged(nameof(PerfAssessment));
                    }
                    if (assessment.CourseId == Course.Id && assessment.Type == AssessmentType.Objective)
                    {
                        ObjAssessment = assessment;
                        Debug.WriteLine($"ObjAssessment is called {ObjAssessment.Title}");
                        OnPropertyChanged(nameof(ObjAssessment));
                    }
                }
            }
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