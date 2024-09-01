namespace Epro.Models
{
	public class indexmodel
	{
		public IEnumerable<Epro.Models.Recipe> Recipe { get; set; }
        public int TotalRecipeCount { get; set; }
        public IEnumerable<Epro.Models.UserRecipe> UserRecipe { get; set; }
		public IEnumerable<Epro.Models.UserRecord> UserRecord { get; set; }
        public int TotalUserCount { get; set; }
        public IEnumerable<Epro.Models.Order> Order { get; set; }
        public int TotalOrderCount { get; set; }
        public IEnumerable<Epro.Models.Contact> Contact { get; set; }
        public int TotalContactCount { get; set; }
        public IEnumerable<Epro.Models.Feedback> Feedback { get; set; }
        public int TotalFeedbackCount { get; set; }
    }
}
