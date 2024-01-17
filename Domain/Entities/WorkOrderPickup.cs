using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class WorkOrderPickup
    {
        public int Id { get; set; }
        public string? PickupBusinessName1 { get; set; }    
        public string? PickupBusinessName2 { get; set; }    
        public string? PickupCity { get; set; }    
        public string? PickupState { get; set; }    
        public string? PickupZipCode { get; set; }    
        public string? PickupNotes { get; set; }

        public int WorkOrderId { get; set; }
        public WorkOrder WorkOrder { get; set; }
    }
    public class WorkOrderDestination
    {
        public int Id { get; set; }
        public string? DestinationBusinessName1 { get; set; }
        public string? DestinationBusinessName2 { get; set; }
        public string? DestinationCity { get; set; }
        public string? DestinationState { get; set; }
        public string? DestinationZipCode { get; set; }
        public string? DestinationNotes { get; set; }
        public int WorkOrderId { get; set; }
        public WorkOrder WorkOrder { get; set; }
    }
    public class WorkOrderAdditionalCharges
    {
        public int Id { get; set; }
        public string? FSCDetail { get; set; }
        public float FSCDetailAmount { get; set; }
        public string? ChasisRentDetail { get; set; }
        public float ChasisRentDetailAmount { get; set; }
        public string? PREFullDetail { get; set; }
        public float PREFullDetailAmount { get; set; }
        public string? StorageDetail { get; set; }
        public float StorageDetailAmount { get; set; }
        public string? DetentionDetail { get; set; }
        public float DetentionDetailAmount { get; set; }
        public string? LayOverDetail { get; set; }
        public float LayOverDetailAmount { get; set; }
        public string? PortCngestionDetail { get; set; }
        public float PortCngestionDetailAmount { get; set; }
        public string? OverWeightFeeDetail { get; set; }
        public float OverWeightFeeDetailAmount { get; set; }
        public string? ReeferFeeDetail { get; set; }
        public float ReeferFeeDetailAmount { get; set; }
        public string? TruckOrderNotUsedDetail { get; set; }
        public float TruckOrderNotUsedDetailAmount { get; set; }
        public int WorkOrderId { get; set; }
        public WorkOrder WorkOrder { get; set; }
    }
}
