using JuboTest.Repository.Jubo;

namespace JuboTest.Service.Management
{
    public class OrderService : ServiceBase, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderHistoryRepository _orderHistoryRepository;

        public OrderService(
            IOrderRepository orderRepository,
            IOrderHistoryRepository orderHistoryRepository)
            : base()
        {
            _orderRepository = orderRepository;
            _orderHistoryRepository = orderHistoryRepository;
        }

        public List<OrderListViewModel> ListByPatient(string patientNo)
        {
            var result = new List<OrderListViewModel>();
            var data = _orderRepository.ListByPatientNo(patientNo);

            if (data.IsNullOrEmpty() == false)
            {
                foreach (var item in data)
                {
                    result.Add(new OrderListViewModel()
                    {
                        Id = item.Id,
                        Message = item.Message,
                        CreateTime = item.CreateTime,
                        UpdateTime = item.UpdateTime
                    });
                }
            }

            return result;
        }

        public List<OrderHistoryListViewModel> ListHistory(string orderId)
        {
            var result = new List<OrderHistoryListViewModel>();
            var data = _orderHistoryRepository.ListByOrderId(orderId);

            if (data.IsNullOrEmpty() == false)
            {
                foreach (var item in data)
                {
                    result.Add(new OrderHistoryListViewModel()
                    {
                        Message = item.Message,
                        CreateTime = item.CreateTime
                    });
                }
            }

            return result;
        }

        public void Create(OrderCreateViewModel data)
        {
            _orderRepository.Insert(new OrderInser()
            {
                PatientNo = data.PatientNo,
                Message = data.Message
            });
        }

        public void Edit(OrderEditViewModel data)
        {
            var order = _orderRepository.FindById(data.Id);

            if (order == null) throw new Exception("無此醫囑資訊");

            _orderRepository.Update(new OrderUpdate()
            {
                Id = data.Id,
                Message = data.Message
            });

            _orderHistoryRepository.Insert(new OrderHistoryInser()
            {
                OrderId = order.Id,
                Message = order.Message
            });
        }
    }
}