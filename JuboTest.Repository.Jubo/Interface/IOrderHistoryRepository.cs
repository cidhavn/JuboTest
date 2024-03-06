namespace JuboTest.Repository.Jubo
{
    public interface IOrderHistoryRepository
    {
        List<OrderHistory> ListByOrderId(string orderId);

        void Insert(OrderHistoryInser data);
    }
}