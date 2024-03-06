using JuboTest.Repository.Jubo;

namespace JuboTest.Service.Management
{
    public class PatientService : ServiceBase, IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository) : base()
        {
            _patientRepository = patientRepository;
        }

        public List<PatientListViewModel> ListAll()
        {
            var result = new List<PatientListViewModel>();

            var data = _patientRepository.ListAll();
            if (data.IsNullOrEmpty() == false)
            {
                foreach (var item in data)
                {
                    result.Add(new PatientListViewModel()
                    {
                        No = item.No,
                        Name = item.Name,
                        Gender = item.Gender,
                        CreateTime = item.CreateTime
                    });
                }
            }

            return result;
        }

        public void Create(PatientCreateViewModel data)
        {
            var patient = _patientRepository.FindByNo(data.No);

            if (patient != null) throw new Exception("已有相同患者編號");

            _patientRepository.Insert(new PatientInsert()
            {
                No = data.No,
                Name = data.Name,
                Gender = data.Gender
            });
        }

        public void Edit(PatientEditViewModel data)
        {
            var patient = _patientRepository.FindByNo(data.No);

            if (patient == null) throw new Exception("無此患者編號");

            _patientRepository.Update(new PatientUpdate()
            {
                No = data.No,
                Name = data.Name,
                Gender = data.Gender
            });
        }
    }
}