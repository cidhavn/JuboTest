using System.ComponentModel.DataAnnotations;

namespace JuboTest.Service.Management
{
    public class OrderCreateViewModel
    {
        [Required(ErrorMessage = "未指定患者編號")]
        public string PatientNo { get; set; }

        [Required(ErrorMessage = "未輸入醫囑內容")]
        public string Message { get; set; }
    }
}