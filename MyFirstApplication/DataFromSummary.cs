using System;
using System.Collections.Generic;

namespace MyFirstApplication
{
	public class DataFromSummary
	{
		public string Name { get; set;}
		public string DOB { get; set;}
		public string Gender { get; set;}
		public string WorkingPosition { get; set;}
		public string Salary { get; set;}
		public string Telephone { get; set;}
		public string Email { get; set;}
		public bool ResponseState { get; set;}
		public string Response { get; set;}

		private static DataFromSummary instanse;

		public static DataFromSummary Instance()
		{
			if (instanse==null)
			{
				instanse = new DataFromSummary();
			}
			return instanse;
		}

		public DataFromSummary ()
		{
			this.Clear ();
		}

		public void Clear()
		{
			Name = string.Empty;
			DOB = string.Empty;
			Gender = "Мужчина";
			WorkingPosition = string.Empty;
			Salary = string.Empty;
			Telephone = string.Empty;
			Email = string.Empty;
			ResponseState = false;
			Response = string.Empty;
		}
	}
}

