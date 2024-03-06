namespace JuboTest.Repository.Jubo
{
    public interface IPatientRepository
    {
        List<Patient> ListAll();

        Patient FindByNo(string no);

        void Insert(PatientInsert data);

        void Update(PatientUpdate data);
    }
}