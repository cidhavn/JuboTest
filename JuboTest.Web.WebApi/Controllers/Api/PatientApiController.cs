using JuboTest.Service.Management;
using Microsoft.AspNetCore.Mvc;

namespace JuboTest.Web.WebApi.Controllers.Api
{
    [Route("api/patient")]
    public class PatientApiController : ApiControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientApiController(IPatientService patientService) : base()
        {
            _patientService = patientService;
        }

        [HttpGet("list")]
        public ApiResult<List<PatientListViewModel>> ListAll()
        {
            return base.GetResult<List<PatientListViewModel>>(() =>
            {
                return _patientService.ListAll();
            });
        }

        [HttpPost("create")]
        public ApiResult Create(PatientCreateViewModel input)
        {
            return base.GetResult(() =>
            {
                _patientService.Create(input);
            });
        }

        [HttpPost("edit")]
        public ApiResult Edit(PatientEditViewModel input)
        {
            return base.GetResult(() =>
            {
                _patientService.Edit(input);
            });
        }
    }
}