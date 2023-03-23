namespace MindTheCodeApp.ViewModels.OrderVMs
{
    public class CreateOrderVM
    {
        public int CreateOrderId { get; set; }
        public int UserId { get; set; }
        public bool Fulfilled { get; set; }
        public bool Active { get; set; }
        public bool Canceled { get; set; }
        public int AddressInformationId { get; set; }
        public decimal Cost { get; set; }

    }
}
