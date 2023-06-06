using C971_Performance_Assessment.Data;
using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace C971_Performance_Assessment
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Create a new instance of your main page
            MainPage mainPage = new MainPage();

            // Create a new instance of a navigation page with your main page as the root page
            NavigationPage navigationPage = new NavigationPage(mainPage);

            // Set the main page of your application to the navigation page
            MainPage = navigationPage;

            // Hide the navigation bar on the MainPage
            NavigationPage.SetHasNavigationBar(mainPage, false);

        }

        protected override async void OnStart()
        {
            await AddDummyDataAsync();
            CheckUpcomingDates();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private async Task AddDummyDataAsync()
        {
            TermRepository termRepository = new TermRepository(Database.GetInstance().GetConnection());
            CourseRepository courseRepository = new CourseRepository(Database.GetInstance().GetConnection());
            AssessmentRepository assessmentRepository = new AssessmentRepository(Database.GetInstance().GetConnection());

            // Check if the Term table is empty
            if (termRepository.GetTermsAsync().Result.Count == 0)
            {
                // Create a new term
                Term term = new Term
                {
                    Name = "Term 1",
                    StartDate = DateTime.Now.AddDays(1),
                    EndDate = DateTime.Now.AddDays(30)
                };

                // Add the term to the database and get the generated ID
                int termId = await termRepository.SaveTermAsync(term);

                // Create a new course
                Course course = new Course
                {
                    TermId = termId,
                    Title = "Mobile Development",
                    Code = "C971",
                    StartDate = DateTime.Now.AddDays(1),
                    EndDate = DateTime.Now.AddDays(15),
                    InstructorName = "Marshall Banks",
                    InstructorNumber = "253.884.7763",
                    InstructorEmail = "mbank21@wgu.edu",
                    Notes = "This course is real tough."
                };

                // Add the course to the database
                int courseId = await courseRepository.SaveCourseAsync(course);

                // Create two assessments for the course
                Assessment assessment1 = new Assessment
                {
                    CourseId = courseId,
                    Title = "Performance Assessment - C197",
                    DueDate = DateTime.Now.AddDays(5),
                    Type = AssessmentType.Performance
                };

                Assessment assessment2 = new Assessment
                {
                    CourseId = courseId,
                    Title = "Objective Assessment - D192",
                    DueDate = DateTime.Now.AddDays(10),
                    Type = AssessmentType.Objective
                };

                // Add the assessments to the database
                _ = assessmentRepository.SaveAssessmentAsync(assessment1);
                _ = assessmentRepository.SaveAssessmentAsync(assessment2);
            }
        }

        private void CheckUpcomingDates()
        {
            Debug.WriteLine("Check Upcoming Dates reached");

            // Get the courses and assessments from your repository
            CourseRepository courseRepository = new CourseRepository(Database.GetInstance().GetConnection());
            List<Course> courses = courseRepository.GetCoursesAsync().Result;

            foreach (Course course in courses)
            {
                if (course.IsDateAlertsActive == false)
                {
                    continue;
                }
                else
                {
                    // Calculate the number of days until the course starts or ends
                    int daysUntilStart = (course.StartDate - DateTime.Now).Days;
                    int daysUntilEnd = (course.EndDate - DateTime.Now).Days;

                    // Check if the course's start or end date is within the next 2 weeks
                    if (daysUntilStart <= 14)
                    {
                        string startMessage = daysUntilStart > 0 ? $"in {daysUntilStart} days" : "today";

                        // Construct the notification message
                        string notificationMessage = $"Course '{course.Title}' is starting {startMessage} on {course.StartDate.ToShortDateString()}.";

                        // Construct unique notification ID for courses
                        int courseNotificationId = 1000 + course.Id;

                        // Trigger notificatio to tray to alert the user about the upcoming course dates
                        CrossLocalNotifications.Current.Show("Course Alert", notificationMessage, courseNotificationId);
                    }

                    // Check if the course's end date is within the next 2 weeks
                    if (daysUntilEnd <= 14)
                    {
                        string endMessage = daysUntilEnd > 0 ? $"in {daysUntilEnd} days" : "today";

                        // Construct the notification message
                        string notificationMessage = $"Course '{course.Title}' is starting {endMessage} on {course.EndDate.ToShortDateString()}.";

                        // Construct unique notification ID for courses
                        int courseNotificationId = 1000 + course.Id;

                        // Trigger notificatio to tray to alert the user about the upcoming course dates
                        CrossLocalNotifications.Current.Show("Course Alert", notificationMessage, courseNotificationId);
                    }
                }
            }

            // Get the courses and assessments from your repository
            AssessmentRepository assessmentRepository = new AssessmentRepository(Database.GetInstance().GetConnection());
            List<Assessment> assessments = assessmentRepository.GetAssessmentsAsync().Result;

            foreach (Assessment assessment in assessments)
            {
                if (assessment.DateAlertIsOn == false)
                {
                    continue;
                }
                else
                {
                    // Calculate the number of days until the course starts or ends
                    int daysUntilDue = (assessment.DueDate - DateTime.Now).Days;

                    // Check if the course's start or end date is within the next 2 weeks
                    if (daysUntilDue <= 14)
                    {
                        string startMessage = daysUntilDue > 0 ? $"in {daysUntilDue} days" : "today";

                        // Construct the notification message
                        string notificationMessage = $"Assessment '{assessment.Title}' is due {startMessage} on {assessment.DueDate.ToShortDateString()}.";

                        int assessmentNotificationID = 2000 + assessment.Id;

                        // Trigger a local notification to alert the user about the upcoming course dates
                        CrossLocalNotifications.Current.Show("Assessment Alert", notificationMessage, assessmentNotificationID);
                    }
                }
            }
        }
    }
}
