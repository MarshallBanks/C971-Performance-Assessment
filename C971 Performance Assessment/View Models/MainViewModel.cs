using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using FontAwesome;
using System.Threading.Tasks;
using C971_Performance_Assessment.Data;
using System.Collections.ObjectModel;
using C971_Performance_Assessment.Views;
using C971_Performance_Assessment.Pages;

namespace C971_Performance_Assessment.View_Models
{
    public class MainViewModel : INotifyPropertyChanged
    {
        // PROPERTIES AND PRIVATE MEMBERS

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

        private bool _isDateLabelVisible = false;
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

        private bool _isIconsVisible = false;

        public bool IsIconsVisible
        {
            get => _isIconsVisible;
            set { _isIconsVisible = value; OnPropertyChanged(); }
        }

        private Term _selectedTerm = new Term();
        public Term SelectedTerm
        {
            get { return _selectedTerm; }
            set
            {
                if (_selectedTerm != value)
                {
                    _selectedTerm = value;
                    OnPropertyChanged(nameof(SelectedTerm));
                }

                // Update the global variable
                Globals.SelectedTerm = value;
            }
        }

        private string _termName = "Select or add a term above to get started.";
        public string TermName
        {
            get { return _termName; }
            set
            {
                if (_termName != value)
                {
                    _termName = value;
                    OnPropertyChanged(nameof(TermName));
                }
            }
        }

        private DateTime _termStartDate;
        public DateTime TermStartDate
        {
            get { return _termStartDate; }
            set
            {
                if (_termStartDate != value)
                {
                    _termStartDate = value;
                    OnPropertyChanged(nameof(TermStartDate));
                }
            }
        }
        
        private DateTime _termEndDate;
        public DateTime TermEndDate
        {
            get { return _termEndDate; }
            set
            {
                if (_termEndDate != value)
                {
                    _termEndDate = value;
                    OnPropertyChanged(nameof(TermEndDate));
                }
            }
        }

        // Property to hold the collection of terms
        public ObservableCollection<Term> Terms { get; set; }

        // Property to hold the collection of Courses

        private ObservableCollection<Course> _courses;
        public ObservableCollection<Course> Courses
        {
            get { return _courses; }
            set
            {
                if (_courses != value)
                {
                    _courses = value;
                    OnPropertyChanged(nameof(Courses));
                }
            }
        }

        public int SelectedTermIndex { get; set; } = 0;
        public bool appLaunch { get; set; } = true;
        public bool SkipOnTermSelected { get; set; } = false;

        private readonly TermRepository _termRepository;
        private readonly CourseRepository _courseRepository;

        public ICommand PenTappedCommand { get; }
        public ICommand DateIconTappedCommand { get; }
        public ICommand EntryCompletedCommand { get; }
        public ICommand TermSelectedCommand { get; }


        // CONSTRUCTOR
        public MainViewModel()
        {
            // Initialize the TermRepository object
            _termRepository = new TermRepository(Database.GetInstance().GetConnection());

            // Initialize the TermRepository object
            _courseRepository = new CourseRepository(Database.GetInstance().GetConnection());

            // Add New Course
            MessagingCenter.Subscribe<CourseCard>(this, "AddNewCourseMessage", (sender) =>
            {
                // Handle the message, e.g., add the new course
                Debug.WriteLine("Add Course Message Received");
                _ = AddNewCourseAsync();
            });

            // Delete Course
            MessagingCenter.Subscribe<CourseCard, Course>(this, "DeleteCourseMessage", (sender, course) =>
            {
                Debug.WriteLine("Delete Course Message Received");
                _ = DeleteCourse(course);
            });

            // Load the terms
            _ = LoadTermsAsync();

            //Load the courses
            _ = LoadCoursesAsync();

            PenTappedCommand = new Command(OnPenTapped);
            DateIconTappedCommand = new Command(OnDateIconTapped);
            EntryCompletedCommand = new Command(OnEntryCompleted);
            TermSelectedCommand = new Command(OnTermSelected);
        }


        // METHODS
        private async Task LoadTermsAsync()
        {
            Debug.WriteLine($"Load Terms Async Executed and Selected Term is {SelectedTerm.Name}");

            Terms = new ObservableCollection<Term>
            {
                new Term { Name = "Select Term" },
                new Term { Name = "Add New Term" }
            };

            List<Term> terms = await _termRepository.GetTermsAsync();

            // If the database is not empty
            if (terms != null)
            {
                int insertIndex = 1; // Inserts actual terms after the "Select Term" Dummy Term
                foreach (var term in terms)
                {
                    
                    Terms.Insert(insertIndex, term); 
                    insertIndex++;
                   
                }
            }

            if (appLaunch)
            {
                Debug.WriteLine($"app Launch was true so SelectedTerm set to Terms[0]");
                SelectedTerm = Terms[0];
                appLaunch = false;
            }

            OnPropertyChanged(nameof(Terms));

            Debug.WriteLine($"LoadTermsAsync Finished");
        }

        private void OnPenTapped()
        {
            Debug.WriteLine("OnPenTapped Reached.");

            IsTitleLabelVisible = false;
            IsTitleEntryVisible = true;

            MessagingCenter.Send(this, "PenTapped");

        }

        private async Task LoadCoursesAsync()
        {
            Courses = new ObservableCollection<Course>();

            List<Course> courses = await _courseRepository.GetCoursesAsync();

            foreach (Course course in courses)
            {
                if (course.TermId == SelectedTerm.Id)
                {
                    Courses.Add(course);
                }
            }
        }

        private async void OnDateIconTapped()
        {
            Debug.WriteLine("OnDateIconTapped Reached.");

            IsDatePickerVisible = !IsDatePickerVisible;
            IsDateLabelVisible = !IsDateLabelVisible;

            if(DateIcon == CALENDAR_ICON)
            {
                DateIcon = CHECK_ICON;
            }
            else
            {
                SkipOnTermSelected = true;

                DateIcon = CALENDAR_ICON;

                if (SelectedTerm != null)
                {
                    SelectedTerm.StartDate = TermStartDate;
                    SelectedTerm.EndDate = TermEndDate;

                    _ = await _termRepository.SaveTermAsync(SelectedTerm);
                }

                int selectedTermId = SelectedTerm.Id;

                await LoadTermsAsync();

                foreach (Term term in Terms)
                {
                    if (term.Id == selectedTermId)
                    {
                        SelectedTerm = term;
                        break;
                    }
                }

                OnPropertyChanged(nameof(Terms));
                SkipOnTermSelected = false;
            }
        }

        public async void OnEntryCompleted()
        {
            SkipOnTermSelected = true;

            Debug.WriteLine("OnEntryCompleted executed.");

            IsTitleLabelVisible = true;
            IsTitleEntryVisible = false;

            if (SelectedTerm != null)
            {
                SelectedTerm.Name = TermName;
                _ = await _termRepository.SaveTermAsync(SelectedTerm);
            }

            int selectedTermId = SelectedTerm.Id;

            await LoadTermsAsync();

            foreach (Term term in Terms)
            {
                if (term.Id == selectedTermId)
                {
                    SelectedTerm = term;
                    break;
                }
            }



            OnPropertyChanged(nameof(Terms));
            SkipOnTermSelected = false;
        }

        private async Task AddNewCourseAsync()
        {
            Debug.WriteLine("AddNewCourseAsync Executed");

            // Create a new course
            Course newCourse = new Course
            {
                Title = "New Course",
                TermId = SelectedTerm.Id
            };

            // Save the new course to the database
            _ = await _courseRepository.SaveCourseAsync(newCourse);

            await LoadCoursesAsync();
        }

        private async Task DeleteCourse(Course course)
        {
            _ = Courses.Remove(course);

            _ = _courseRepository.DeleteCourseAsync(course);

            if (Courses.Count == 0)
            {
                _ = AddNewCourseAsync();
            }
        }

        public async void OnTermSelected()
        {
            if (SkipOnTermSelected == true)
            {
                Debug.WriteLine("OnTermSelected() skipped");
                return;
            }
            else
            {
                Debug.WriteLine("OnTermSelected executed.");
                Debug.WriteLine($"SelectedTerm.name is {SelectedTerm.Name}");

                // "Add New Term" Selected
                if (SelectedTerm.Name == "Add New Term")
                {
                    SkipOnTermSelected = true;
                    Debug.WriteLine($"Add New Term executed");
                    int newTermNumber = 1;

                    foreach (Term term in Terms)
                    {
                        if (term.Name.StartsWith("New Term"))
                        {
                            Debug.WriteLine($"New Term Number is {newTermNumber}");
                            newTermNumber++;
                        }
                    }

                    Term newTerm = new Term { Name = $"New Term {newTermNumber}" };
                    Debug.WriteLine($"newTerm name is {newTerm.Name}");

                    _ = await _termRepository.SaveTermAsync(newTerm);

                    await LoadTermsAsync();
                    

                    await Task.Delay(TimeSpan.FromSeconds(0.2));
                    Debug.WriteLine($"Task Delay executed");

                    foreach (Term term in Terms)
                    {
                        if (term.Name == $"New Term {newTermNumber}")
                        {
                            SkipOnTermSelected = false;
                            SelectedTerm = term;
                            SkipOnTermSelected = true;

                            AddNewCourseAsync();

                            break;
                        }
                    }
                    SkipOnTermSelected = false;
                }
                // "Select Term" Selected
                else if (SelectedTerm.Name == "Select Term")
                {
                    TermName = "Select or add a term above to get started.";
                    //IsCourseCardVisible = false;
                    IsIconsVisible = false;
                    IsTitleLabelVisible = true;
                    IsDateLabelVisible = false;

                    _ = LoadCoursesAsync();
                }
                // Actual Term Selected
                else
                {
                    Debug.WriteLine("Actual Term Selected");
                    try
                    {
                        if (SelectedTerm == null)
                        {
                            Debug.WriteLine($"SelectedTerm is null");
                        }
                        else
                        {
                            Debug.WriteLine($"SelectedTerm: {SelectedTerm.Name}");
                            Debug.WriteLine($"SelectedTerm: {SelectedTerm.StartDate}");
                            //IsCourseCardVisible = true;
                            IsIconsVisible = true;
                            IsTitleLabelVisible = true;
                            IsDateLabelVisible = true;
                            TermName = SelectedTerm.Name;
                            TermStartDate = SelectedTerm.StartDate;
                            TermEndDate = SelectedTerm.EndDate;
                            Debug.WriteLine($"Selected Term End Date is {SelectedTerm.EndDate}");

                            _ = LoadCoursesAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"An error occurred: {ex.Message}");
                        Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
                    }

                    
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.WriteLine("MainViewModel On Property Changed Reached");

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}







