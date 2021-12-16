using System.Globalization;
using System.Text;

namespace GeoPlaceIp.Infras.Converters
{
    public static class ConvertSbytes
    {
        public static sbyte[] StrToSbytes(this string str)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(str);

            return Array.ConvertAll(bytes, q => Convert.ToSByte(q));
        }
        public static string SbytesToStr(this sbyte[] array)
        {
            unsafe 
            { 
                fixed (sbyte* namePtr = array)
                {
                    return new string(namePtr, 0, array.Length, Encoding.ASCII);
                }
            }
    }
    }
}
