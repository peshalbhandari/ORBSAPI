namespace ORBS.Model.Models
{
    public class Table
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        public bool IsOccupied { get; set; }

        public bool IsReserved { get; set; }

        public bool IsActive { get; set; }
    }
}
