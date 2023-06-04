﻿using C971_Performance_Assessment.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace C971_Performance_Assessment.View_Models
{
    class CourseEditorViewModel
    {
        private readonly CourseRepository _courseRepository;
        private readonly AssessmentRepository _assessmentRepository;

        private string selectedStatus;

        public string SelectedStatus
        {
            get { return selectedStatus; }
            set
            {
                selectedStatus = value;
                OnPropertyChanged();
            }
        }

        public Course Course { get; set; }
        public Assessment PerfAssessment { get; set; }
        public Assessment ObjAssessment { get; set; }

        public ICommand DoneTappedCommand { get; }

        public CourseEditorViewModel(Course course, Assessment perfAssessment, Assessment objAssessment)
        {
            Course = course;
            PerfAssessment = perfAssessment;
            ObjAssessment = objAssessment;

            SetSelectedStatus();

            DoneTappedCommand = new Command(OnDoneTapped);

            // Initialize the CourseRepository object
            _courseRepository = new CourseRepository(Database.GetInstance().GetConnection());
            // Initialize the AssessmentRepository object
            _assessmentRepository = new AssessmentRepository(Database.GetInstance().GetConnection());
        }

        private void SetSelectedStatus()
        {
            if (Course != null)
            {
                SelectedStatus = Course.GetFormattedStatus();
            }
        }

        private void OnDoneTapped()
        {
            // Update the database with the modified objects
            _ = _courseRepository.SaveCourseAsync(Course);
            _ = _assessmentRepository.SaveAssessmentAsync(PerfAssessment);
            _ = _assessmentRepository.SaveAssessmentAsync(ObjAssessment);

            //Send updated Course and assessments back to Course Details
            var updateData = (Course, PerfAssessment, ObjAssessment);
            MessagingCenter.Send(this, "CourseUpdatedMessage", updateData);

            // Navigate back to the previous page
            _ = Application.Current.MainPage.Navigation.PopAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
