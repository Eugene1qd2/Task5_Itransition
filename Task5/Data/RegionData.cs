namespace Task5.Data
{
    public class RegionData
    {
        public string RegionId { get; set; }
        public string RegionName { get; set; }
        public string RegionAlphabet { get; set; }

        public RegionData(string regionId, string regionName, string regionAlphabet)
        {
            RegionId = regionId;
            RegionName = regionName;
            RegionAlphabet = regionAlphabet;
        }
        public RegionData()
        {

        }
    }
}
