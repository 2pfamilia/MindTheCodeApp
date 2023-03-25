namespace MindTheCodeApp.ViewModels.OrderVMs
{
    public class IndexOrderVM
    {
        public int IndexOrderId { get; set; }
        public string CustomerEmail { get; set; } = string.Empty;
        public bool Fulfilled { get; set; }
        public bool Active { get; set; }
        public bool Cancelled { get; set; }
        public string Address { get; set; } = string.Empty;
        public decimal Cost { get; set; }
        public DateTime OrderCreated { get; set; }
    }
}