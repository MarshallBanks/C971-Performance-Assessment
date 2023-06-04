using C971_Performance_Assessment.Data;
using C971_Performance_Assessment.View_Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971_Performance_Assessment.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseEditorPage : ContentPage
    {
        public CourseEditorPage(Course course, Assessment perfAssessment, Assessment objAsessment)
        {
            InitializeComponent();

            var viewModel = new CourseEditorViewModel(course, perfAssessment, objAsessment);
            this.BindingContext = viewModel;

            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}