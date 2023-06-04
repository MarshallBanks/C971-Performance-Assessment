using C971_Performance_Assessment.Data;
using C971_Performance_Assessment.View_Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971_Performance_Assessment.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseDetailsPage : ContentPage
    {
        public CourseDetailsPage(Course course, Assessment perfAssessment, Assessment objAssessment)
        {
            InitializeComponent();

            var viewModel = new CourseDetailsViewModel(course, perfAssessment, objAssessment);
            this.BindingContext = viewModel;

            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}