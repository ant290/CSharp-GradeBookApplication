using System;
using System.Linq;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name): base(name)
        {
            Type = GradeBookType.Ranked;
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count() < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStudentStatistics(name);
        }

        public override void CalculateStatistics()
        {
            if (Students.Count() < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStatistics();
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
            {
                throw new InvalidOperationException("Not enough students!");
            }

            //work out how many students cause a grade drop
            var threshold = (int)Math.Ceiling(Students.Count * 0.2);
            //get the average grades in descending order
            var grades = Students.OrderByDescending(e => e.AverageGrade).Select(e => e.AverageGrade).ToList();

            if (grades[threshold - 1] <= averageGrade)
            {
                return 'A';
            }
            if (grades[(threshold * 2) - 1] <= averageGrade)
            {
                return 'B';
            }
            if (grades[(threshold * 3) - 1] <= averageGrade)
            {
                return 'C';
            }
            if (grades[(threshold * 4) - 1] <= averageGrade)
            {
                return 'D';
            }
            return 'F';
        }
    }
}
