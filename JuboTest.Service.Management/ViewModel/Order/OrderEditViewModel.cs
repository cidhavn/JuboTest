using System.ComponentModel.DataAnnotations;

namespace JuboTest.Service.Management
{
    public class OrderEditViewModel
    {
        [Required(ErrorMessage = "未指定醫囑 ID")]
        public string Id { get; set; }

        [Required(ErrorMessage = "未輸入醫囑內容")]
        public string Message { get; set; }
    }
}