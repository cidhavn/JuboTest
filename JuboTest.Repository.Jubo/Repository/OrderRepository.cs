using MongoDB.Driver;

namespace JuboTest.Repository.Jubo
{
    public class OrderRepository : RepositoryBase, IOrderRepository
    {
        private readonly IMongoCollection<Order> _collection;

        public OrderRepository(JuboRepositorySetting setting) : base(setting)
        {
            _collection = base.GetCollection<Order>("Order");
        }

        public List<Order> ListByPatientNo(string patientNo)
        {
            return _collection.Find(x => x.PatientNo == patientNo).ToList();
        }

        public Order FindById(string Id)
        {
            return _collection.Find(x => x.Id == Id).FirstOrDefault();
        }

        public void Insert(OrderInser data)
        {
            _collection.InsertOne(new Order()
            {
                PatientNo = data.PatientNo,
                Message = data.Message,
                CreateTime = DateTimeOffset.UtcNow
            });
        }

        public void Update(OrderUpdate data)
        {
            var filter = Builders<Order>.Filter.Eq(x => x.Id, data.Id);
            var update = Builders<Order>.Update.Set(x => x.Message, data.Message)
                                               .Set(x => x.UpdateTime, DateTimeOffset.UtcNow);

            _collection.UpdateOne(filter, update);
        }
    }
}