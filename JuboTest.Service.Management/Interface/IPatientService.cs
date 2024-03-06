namespace JuboTest.Service.Management
{
    public interface IPatientService
    {
        List<PatientListViewModel> ListAll();

        void Create(PatientCreateViewModel data);

        void Edit(PatientEditViewModel data);
    }
}