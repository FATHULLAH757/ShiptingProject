namespace MainWebProject.Models
{
    public class WorkOrderVM
    {
        public int Id { get; set; }
        public bool IsDrayage { get; set; }
        public int FirstDriverId { get; set; }
        public int SecondDriverId { get; set; }
        public int TruckId { get; set; }
        public string? Status { get; set; }
        public DateTime? PickupDate { get; set; }
        public DateTime? ETADate { get; set; }
        public string? FBN { get; set; }
        public float TripMiles { get; set; }
        public float DHMiles { get; set; }
        public float FirstDriverMiles { get; set; }
        public float SecondDriverMiles { get; set; }
        public bool IsTarp { get; set; }
        public bool IsPartialLoad { get; set; }
        public float Amount { get; set; }
        public float TotalAmount { get; set; }
        public float DispatchPay { get; set; }
        public float FirstDriverPay { get; set; }
        public float SecondDriverPay { get; set; }
        public int BillToId { get; set; }
        public string? BusinessName { get; set; }
        public string? AddressOne { get; set; }
        public string? AddressTwo { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Phone { get; set; }
        public string? Ext { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public string? Notes { get; set; }
        public string? OrderId { get; set; }
        public List<WorkOrderPickupVm>? PickupBusinessDetail { get; set;}
        public List<WorkOrderDestinationVm>? DestinationBusinessDetail { get; set;}
        public AdditionalChargesVM WorkOrderAdditionalCharges { get; set; }
        public WorkOrderDrayageVM WorkOrderDrayage { get; set; }

    }

    public class WorkOrderPickupVm
    {
        public int Id { get; set; }
        public string? PickupBusinessName1 { get; set; }
        public string? PickupBusinessName2 { get; set; }
        public string? PickupCity { get; set; }
        public string? PickupState { get; set; }
        public string? PickupZipCode { get; set; }
        public string? PickupNotes { get; set; }
        public int WorkOrderId { get; set; }
    }
    public class WorkOrderDestinationVm
    {
        public int Id { get; set; }
        public string? DestinationBusinessName1 { get; set; }
        public string? DestinationBusinessName2 { get; set; }
        public string? DestinationCity { get; set; }
        public string? DestinationState { get; set; }
        public string? DestinationZipCode { get; set; }
        public string? DestinationNotes { get; set; }
        public int WorkOrderId { get; set; }
    }
    public class AdditionalChargesVM
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
       
    }
    public class WorkOrderDrayageVM {
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

    }
}
