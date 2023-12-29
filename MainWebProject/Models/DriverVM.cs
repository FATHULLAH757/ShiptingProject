namespace MainWebProject.Models
{
    public class DriverVM
    {
        public int Id { get; set; }
        public int DriverRoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
