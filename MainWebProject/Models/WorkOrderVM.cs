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
        public List<BusinessVm>? PickupBusinessDetail { get; set;}
        public List<BusinessVm>? DestinationBusinessDetail { get; set;}

    }

    public class BusinessVm
    {
        public string? PickupBusinessName { get; set; }
        public string? PickupBusinessNameTwo { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Notes { get; set; }
    }
}
