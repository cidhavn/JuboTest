using System.ComponentModel.DataAnnotations;

namespace JuboTest.Service.Management
{
    public class PatientEditViewModel
    {
        [Required(ErrorMessage = "未指定患者編號")]
        public string No { get; set; }

        [Required(ErrorMessage = "未輸入患者姓名")]
        public string Name { get; set; }

        [Required(ErrorMessage = "未輸入患者性別")]
        public string Gender { get; set; }
    }
}