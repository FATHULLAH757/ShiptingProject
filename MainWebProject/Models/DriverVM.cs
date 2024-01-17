namespace MainWebProject.Models
{
    public class DriverVM
    {
        public int Id { get; set; }
        public int DriverRoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EnterDate { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
