using System.Net;

namespace GeoPlaceIp.Infras.Converters
{
    public static class ConvertIp
    {
        public static uint IpStringToUint(this string ipString)
        {
            try
            {
                var ipAddress = IPAddress.Parse(ipString);
                var ipBytes = ipAddress.GetAddressBytes();
                var ip = (uint)ipBytes[0] << 24;
                ip += (uint)ipBytes[1] << 16;
                ip += (uint)ipBytes[2] << 8;
                ip += (uint)ipBytes[3];
                return ip;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("The provided data is not an IP address", ex);
            }
        }

        public static string IpUintToString(this uint ipUint)
        {
            var ipBytes = BitConverter.GetBytes(ipUint);
            var ipBytesRevert = new byte[4];
            ipBytesRevert[0] = ipBytes[3];
            ipBytesRevert[1] = ipBytes[2];
            ipBytesRevert[2] = ipBytes[1];
            ipBytesRevert[3] = ipBytes[0];
            return new IPAddress(ipBytesRevert).ToString();
        }
    }
}
