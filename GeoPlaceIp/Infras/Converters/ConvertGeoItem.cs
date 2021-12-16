namespace GeoPlaceIp.Infras.Converters
{
    public static class ConvertGeoItem
    {
        public static GeoItemOut Convert(this GeoItem gi)
        {
            return new GeoItemOut() { city = gi.city.SbytesToStr(),
                                      country = gi.country.SbytesToStr(),
                                      region = gi.region.SbytesToStr(),
                                      postal = gi.region.SbytesToStr(),
                                      organization = gi.region.SbytesToStr(),
                                      latitude = gi.latitude,
                                      longitude = gi.longitude };
        }
    }
}
