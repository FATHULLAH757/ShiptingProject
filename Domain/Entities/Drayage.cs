using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Drayage")]
    public class Drayage
    {
        public int Id { get; set; }
        public string? DrayageAccountManager { get; set; }
        public string? DrayageSeal { get; set; }
        public DateTime? DrayageLFD { get; set; }
        public DateTime? DrayageVesselETA { get; set; }
        public string? DrayageContainer { get; set; }
        public string? DrayageSizedrayageSize { get; set; }
        public string? DrayageOW { get; set; }
        public string? DrayageHazmat { get; set; }
        public string? DrayageBKG { get; set; }
        public string? DrayageChassis { get; set; }
        public DateTime? DrayageOutGateDate { get; set; }
        public int DrayageOutGateDriver { get; set; }
        public DateTime? DrayageInGateDate { get; set; }
        public int DrayageInGateDriver { get; set; }
        public DateTime? DrayageStateDate { get; set; }
        public string? DrayageLocation { get; set; }
        public int WorkOrderId { get; set; }
        public WorkOrder WorkOrder { get; set; }
    }
}
