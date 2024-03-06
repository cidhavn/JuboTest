namespace JuboTest.Service.Management
{
    public interface IOrderService
    {
        public List<OrderListViewModel> ListByPatient(string patientNo);

        public List<OrderHistoryListViewModel> ListHistory(string orderId);

        public void Create(OrderCreateViewModel data);

        public void Edit(OrderEditViewModel data);
    }
}