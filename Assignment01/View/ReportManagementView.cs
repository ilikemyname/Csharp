using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assignment01.Controller;

namespace Assignment01.View
{
    class ReportManagementView
    {
        private ReportController reportController;
        private CourseController courseController;
        private ClassController classController;

        public ReportManagementView(CourseController courseController, ClassController classController)
        {
            // TODO: Complete member initialization
            this.courseController = courseController;
            this.classController = classController;
            reportController = new ReportController(courseController, classController);
        }

        internal void PrintEnrolReport()
        {
            reportController.PrintEnrolReport();
        }
    }
}
