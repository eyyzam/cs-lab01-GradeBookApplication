using System;
using System.Collections.Generic;
using System.Linq;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
	public class RankedGradeBook : BaseGradeBook
	{
		public RankedGradeBook(string name, bool isWeighted) : base(name, isWeighted)
		{
			Type = GradeBookType.Ranked;
		}

		public override char GetLetterGrade(double averageGrade)
		{
			if (Students.Count < 5) throw new InvalidOperationException("At least 5 students needed to perform ranked grading!");

			// Using LINQ to sort the list by descending on "AverageGrade"
			// and making a list containing only AverageGrades of all students
			var studentGrades = Students.OrderByDescending(x => x.AverageGrade).Select(x => x.AverageGrade).ToList();

			// Obtaining one fifth of the students count
			var oneFifth = (int)Math.Ceiling(Students.Count * 0.2);

			// List of obtainable grades
			var obtainableGrades = new List<char>() {'A', 'B', 'C', 'D', 'F'};

			for (var i = 1; i <= 4; i++)
			{
				// Each 20% gets set the specific grade
				if (studentGrades[(oneFifth * i) - 1] <= averageGrade)
				{
					return obtainableGrades.ElementAt(i- 1);
				}
			}
			return obtainableGrades.Last();
		}

		public override void CalculateStatistics()
		{
			if (Students.Count < 5)
			{
				Console.WriteLine("Ranked grading requires at least 5 students.");
				return;
			}

			base.CalculateStatistics();
		}

		public override void CalculateStudentStatistics(string name)
		{
			if (Students.Count < 5)
			{
				Console.WriteLine("Ranked grading requires at least 5 students.");
				return;
			}

			base.CalculateStudentStatistics(name);
		}
	}
}
