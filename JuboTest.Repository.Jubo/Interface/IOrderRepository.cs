namespace JuboTest.Repository.Jubo
{
    public interface IOrderRepository
    {
        List<Order> ListByPatientNo(string patientNo);

        Order FindById(string Id);

        void Insert(OrderInser data);

        void Update(OrderUpdate data);
    }
}