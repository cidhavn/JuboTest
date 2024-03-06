using MongoDB.Driver;

namespace JuboTest.Repository.Jubo
{
    public class OrderHistoryRepository : RepositoryBase, IOrderHistoryRepository
    {
        private readonly IMongoCollection<OrderHistory> _collection;

        public OrderHistoryRepository(JuboRepositorySetting setting) : base(setting)
        {
            _collection = base.GetCollection<OrderHistory>("OrderHistory");
        }

        public List<OrderHistory> ListByOrderId(string orderId)
        {
            return _collection.Find(x => x.OrderId == orderId).ToList();
        }

        public void Insert(OrderHistoryInser data)
        {
            _collection.InsertOne(new OrderHistory()
            {
                OrderId = data.OrderId,
                Message = data.Message,
                CreateTime = DateTimeOffset.UtcNow
            });
        }
    }
}