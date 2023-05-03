using C971_Performance_Assessment.View_Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace C971_Performance_Assessment.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    
    public partial class CourseCard : ContentView
    {
        public CourseCard()
        {
            InitializeComponent();

            BindingContext = new CourseCardViewModel();
        }
    }
}