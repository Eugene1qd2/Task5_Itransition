using System.ComponentModel.DataAnnotations;

namespace Task5.Data
{
    public class ChaosGenerator
    {
        int seed;
        Random random;
        RegionsController _regions;
        string _regionId;
        string currentAlhp;

        const int CHAOS_VARS = 3;

        public int GetMistakesCount(double mistCount)
        {
            int mistRoundCount = (int)mistCount;
            double mistChance = mistCount - mistRoundCount;
            return mistRoundCount + (random.NextDouble() <= mistChance ? 1 : 0);
        }
        public void SetCurrentRegion(string regionId)
        {
            _regionId = regionId;
            currentAlhp = _regions.Regions.Where(x => x.RegionId == regionId).First().RegionAlphabet;
        }

        public ChaosGenerator(int seed, RegionsController regions)
        {
            Init(seed);
            _regions = regions;
        }

        public void Init(int seed)
        {
            this.seed = seed;
            random = new Random(seed);
        }

        public async Task<RecordData> GenerateChaos(RecordData data,double mistCount)
        {
            int _mistCount = GetMistakesCount(mistCount);
            char[][] dataStr = new char[][] { data.FullName.ToCharArray(), data.PhoneNumber.ToCharArray(), data.Address.ToCharArray() };
            int[] startLens = new int[] { dataStr[0].Length, dataStr[1].Length, dataStr[2].Length };
            for (int i = 0; i < _mistCount; i++)
            {
                int index = random.Next(0, 3);
                int r = random.Next(0, 3);
                r = startLens[index] - dataStr[index].Length < -2 ? 1 : (startLens[index] - dataStr[index].Length > 2) ? 0 :r ;
                switch (r)
                {
                    case 0:
                        dataStr[index] = AddChaosLetter(dataStr[index]);
                        break;
                    case 1:
                        dataStr[index] = DeleteChaosLetter(dataStr[index]);
                        break;
                    case 2:
                        dataStr[index] = SwapChaosLetter(dataStr[index]);
                        break;
                }
            }

            data.FullName = string.Join("", dataStr[0]);
            data.PhoneNumber = string.Join("", dataStr[1]);
            data.Address = string.Join("", dataStr[2]);
            Init(seed + 1);
            return data;
        }

        private char[] SwapChaosLetter(char[] v)
        {
            int id1 = random.Next(0, v.Length);
            int id2 = random.Next(0, v.Length);
            char save = v[id1];
            v[id1] = v[id2];
            v[id2] = save;
            return v;
        }

        private char[] DeleteChaosLetter(char[] v) //не воркает
        {
            random.Next();
            int id = random.Next(0, v.Length);
            string str = string.Join("", v).Remove(id,1);
            return str.ToCharArray();
        }

        private char[] AddChaosLetter(char[] v) //не воркает
        {
            int id = random.Next(0, v.Length);
            int idLetter = random.Next(0, currentAlhp.Length);
            string str = string.Join("", v).Insert(id, currentAlhp[idLetter].ToString());
            return str.ToCharArray();
        }
    }
}
