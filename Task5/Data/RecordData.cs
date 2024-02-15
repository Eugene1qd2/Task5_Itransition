
namespace Task5.Data
{
    public class RecordData
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public override string ToString()
        {
            return $"{Id}\n{FullName}\n{Address}\n{PhoneNumber}";
        }
    }
}
