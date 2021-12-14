using System;

public struct IpRange : IComparable<IpRange>
{
    public int CompareTo(IpRange range)
    {

            return this.ip_from.CompareTo(range.ip_from);

    }
    public uint ip_from;           // начало диапазона IP адресов
    public int ip_to;             // конец диапазона IP адресов
    public uint location_index;    // индекс записи о местоположении
}
