using Bogus;
using Bogus.DataSets;
using System.Security.Cryptography;
using System.Text;
namespace Task5.Data
{
    public class TableController
    {
        public int Seed { get; set; }

        ChaosGenerator chaosGenerator;

        public string CurrentRegionId { get; set; }
        public RegionsController RegionsController;

        Faker<RecordData> faker;


        public TableController(int Seed)
        {
            RegionsController = new RegionsController(); //сделать больше
            CurrentRegionId = RegionsController.GetDefaultRegion().RegionId;
            chaosGenerator=new ChaosGenerator(Seed,RegionsController);
            Init(Seed);
        }

        public void Init(int Seed)
        {
            this.Seed = Seed;
            faker = new Faker<RecordData>(CurrentRegionId).StrictMode(true)
                .RuleFor(x => x.FullName, f => f.Name.FullName())
                .RuleFor(x => x.Address, f => f.Person.Address.City + ", " + f.Person.Address.Street + ", " + f.Person.Address.Suite)
                .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumberFormat())
                .RuleFor(x => x.Id, f => f.IndexFaker.ToString());
            faker.UseSeed(Seed);

            InitChaos();
        }
        public void InitChaos()
        {
            chaosGenerator.Init(Seed);
            chaosGenerator.SetCurrentRegion(CurrentRegionId);
        }

        private async Task<List<RecordData>> GenerateNextRecordsAsync(int amount, double mistakes)
        {
            List<RecordData> records = new List<RecordData>();

            InitChaos();

            for (int i = 0; i < amount; i++)
            {
                RecordData record = faker.Generate();
                using (MD5 md5 = MD5.Create())
                {
                    byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(record.Id + CurrentRegionId + Seed));
                    Guid result = new Guid(hash);
                    record.Id = result.ToString();
                }
                record= await chaosGenerator.GenerateChaos(record,mistakes);

                records.Add(record);
            }

            return records;
        }
        public Task<List<RecordData>> GetRecordsAsync(int amount,double mistakes)
        {
            return GenerateNextRecordsAsync(amount, mistakes); //добавить ошибки
        }

        public void SetCurrentRegion(string regionId)
        {
            CurrentRegionId = regionId;
            Init(Seed);
        }

    }
}
