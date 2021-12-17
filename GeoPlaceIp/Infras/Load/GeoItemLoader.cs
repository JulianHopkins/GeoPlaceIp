using GeoPlaceIp.Infras.Converters;
using System.IO.MemoryMappedFiles;

namespace GeoPlaceIp.Infras.Load
{
    public class GeoItemLoader
    {
        public GeoItemLoader()
        {
            mmva = DataLoader.mmf.CreateViewAccessor();
        }
        public GeoItemLoader(MemoryMappedViewAccessor mmva)
        {
            this.mmva = mmva;
        }

        private MemoryMappedViewAccessor mmva;

        public GeoItem GetGeoItem(long address, string city = null)
        {
            var gi = new GeoItem();



            sbyte[] _country = new sbyte[8];
            sbyte[] _region = new sbyte[12];
            sbyte[] _postal = new sbyte[12];

            sbyte[] _organization = new sbyte[32];
            mmva.ReadArray(address, _country, 0, 8);
            mmva.ReadArray(address + 8, _region, 0, 12);
            mmva.ReadArray(address + 20, _postal, 0, 12);

            if (string.IsNullOrEmpty(city))
            {
                sbyte[] _city = new sbyte[24];
                mmva.ReadArray(address + 32, _city, 0, 24);
                gi.city = _city.SbytesToStr();
            }
            else gi.city = city;

            mmva.ReadArray(address + 56, _organization, 0, 32);
            gi.country = _country.SbytesToStr();
            gi.region = _region.SbytesToStr();
            gi.postal = _postal.SbytesToStr();

            gi.organization = _organization.SbytesToStr();
            gi.latitude = mmva.ReadSingle(address + 88).ToString();
            gi.longitude = mmva.ReadSingle(address + 92).ToString();
            return gi;
        }

    }
}
