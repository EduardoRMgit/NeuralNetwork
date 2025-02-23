using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.StudyCase.Model
{
    internal class StudyData(double studyHours, double sleepHours, double expected)
    {
        public double StudyHours => studyHours;

        public double SleepHours => sleepHours;

        public double Expected => expected;
    }
}
