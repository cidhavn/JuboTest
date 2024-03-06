using MongoDB.Driver;

namespace JuboTest.Repository.Jubo
{
    public class PatientRepository : RepositoryBase, IPatientRepository
    {
        private readonly IMongoCollection<Patient> _collection;

        public PatientRepository(JuboRepositorySetting setting) : base(setting)
        {
            _collection = base.GetCollection<Patient>("Patient");
        }

        public List<Patient> ListAll()
        {
            return _collection.Find(x => true).ToList();
        }

        public Patient FindByNo(string no)
        {
            return _collection.Find(x => x.No == no).FirstOrDefault();
        }

        public void Insert(PatientInsert data)
        {
            _collection.InsertOne(new Patient()
            {
                No = data.No,
                Name = data.Name,
                Gender = data.Gender,
                CreateTime = DateTimeOffset.UtcNow
            });
        }

        public void Update(PatientUpdate data)
        {
            var filter = Builders<Patient>.Filter.Eq(x => x.No, data.No);
            var update = Builders<Patient>.Update.Set(x => x.Name, data.Name)
                                                 .Set(x => x.Gender, data.Gender);

            _collection.UpdateOne(filter, update);
        }
    }
}