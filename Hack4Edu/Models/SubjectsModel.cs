namespace Hack4Edu.Models
{
    public class SubjectsModel
    {
        public string Name { get; set; }
        public string Image { get; set; }

        public List<RemainingAssignmentModel> RemainingAssignments { get; set; }
    }
}
