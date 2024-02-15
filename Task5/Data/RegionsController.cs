namespace Task5.Data
{
    public class RegionsController
    {
        public RegionData[] Regions;
        public RegionsController()
        {
            Regions = RegionsDataLoader.Load();
        }

        public RegionData GetDefaultRegion()
        {
            if (Regions.Length > 0)
                return Regions[0];
            return null;
        }
    }
}
