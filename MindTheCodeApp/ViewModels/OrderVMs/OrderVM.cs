using AppCore.Models.AuthModels;


namespace MindTheCodeApp.ViewModels.OrderVMs
{
    public class OrderVM
    {
        public bool Fulfilled { get; set; }
        public bool Active { get; set; }
        public bool Canceled { get; set; }
        public decimal Cost { get; set; }
        public DateTime DateCreated { get; set; }
        public int BookId { get; set; }
        public decimal Unitcost { get; set; }
        public decimal TotalCost { get; set; }
        public int Count { get; set; }
        public string StreetAddress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}