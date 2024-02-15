namespace Task5.Data
{
    public static class RegionsDataLoader
    {
        private const string filepath="RegionsData/regions.xml";
        public static RegionData[] Load()
        {
            XMLDataLoader<RegionData> xMLDataLoader = new XMLDataLoader<RegionData>();
            RegionData[] regions = xMLDataLoader.DeserializeData(filepath);
            return regions;
        }

        public static void Save(RegionData[] regionsData) 
        {
            XMLDataLoader<RegionData> xMLDataLoader = new XMLDataLoader<RegionData>();
            xMLDataLoader.SerializeData(regionsData, filepath);
        }
    }
}
