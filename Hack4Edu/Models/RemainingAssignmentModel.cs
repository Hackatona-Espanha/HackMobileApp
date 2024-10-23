namespace Hack4Edu.Models
{
    public class RemainingAssignmentModel
    {
        public int Id { get; set; }

        public bool IsRead { get; set; }
        public string AssignmentName { get; set; }

        public string AssignmentImage { get; set; }

        public List<AIGeneratedworkModel> AIGeneratedWorks { get; set; }

    }
}
