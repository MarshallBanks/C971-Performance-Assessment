using C971_Performance_Assessment.Data;
using C971_Performance_Assessment.View_Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971_Performance_Assessment.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseEditorPage : ContentPage
    {
        public CourseEditorPage(Course course)
        {
            InitializeComponent();

            var viewModel = new CourseEditorViewModel(course);
            this.BindingContext = viewModel;

            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}