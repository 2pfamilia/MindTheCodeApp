namespace MindTheCodeApp.ViewModels.OrderVMs
{
    public class CreateOrderDetailsVM
    {
        public int CreateOrderDetailsId { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public decimal UnitCost { get; set; }
        public decimal TotalCost { get; set; }
        public int Count { get; set; }
    }
}