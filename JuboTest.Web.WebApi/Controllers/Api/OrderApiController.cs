using JuboTest.Service.Management;
using Microsoft.AspNetCore.Mvc;

namespace JuboTest.Web.WebApi.Controllers.Api
{
    [Route("api/order")]
    public class OrderApiController : ApiControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderApiController(IOrderService orderService) : base()
        {
            _orderService = orderService;
        }

        [HttpGet("list/{patientNo}")]
        public ApiResult<List<OrderListViewModel>> ListByPatient(string patientNo)
        {
            return base.GetResult<List<OrderListViewModel>>(() =>
            {
                return _orderService.ListByPatient(patientNo);
            });
        }

        [HttpGet("list/history/{orderId}")]
        public ApiResult<List<OrderHistoryListViewModel>> ListHistory(string orderId)
        {
            return base.GetResult<List<OrderHistoryListViewModel>>(() =>
            {
                return _orderService.ListHistory(orderId);
            });
        }

        [HttpPost("create")]
        public ApiResult Create(OrderCreateViewModel input)
        {
            return base.GetResult(() =>
            {
                _orderService.Create(input);
            });
        }

        [HttpPost("edit")]
        public ApiResult Edit(OrderEditViewModel input)
        {
            return base.GetResult(() =>
            {
                _orderService.Edit(input);
            });
        }
    }
}