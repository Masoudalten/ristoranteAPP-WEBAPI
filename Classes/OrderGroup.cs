namespace apiTest.Classes
{
    public class OrderGroup
    {
        public int OrderId { get; set; }
        public List<Order> OrderItems { get; set; }
        public int total { get; set; }
        public string time { get; set; }
    }
}
