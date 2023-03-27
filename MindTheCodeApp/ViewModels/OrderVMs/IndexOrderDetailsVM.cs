namespace MindTheCodeApp.ViewModels.OrderVMs
{
    public class IndexOrderDetailsVM
    {
        public int IndexOrderDetailsId { get; set; }
        public string OrderEmail { get; set; } = string.Empty;
        public string BookTitle { get; set; } = string.Empty;
        public decimal UnitCost { get; set; }
        public decimal TotalCost { get; set; }
        public int Count { get; set; }
    }
}