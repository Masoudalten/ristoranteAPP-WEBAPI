using apiTest.Model;

namespace apiTest.Interface
{
    public interface IOrderRepository
    {
        IEnumerable<OrderGroup> GetOrderList();
        void CreateNewOrder(Order[] orderItems, int total);
        void DeleteOrder(int id);
    }
}
