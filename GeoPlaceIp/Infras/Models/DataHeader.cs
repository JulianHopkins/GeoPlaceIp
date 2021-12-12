using System;
using System.IO.MemoryMappedFiles;

public class DataHeader
{

    public int version;           // версия база данных
    public sbyte[] name = new sbyte[32];          // название/префикс для базы данных
    public ulong timestamp;         // время создания базы данных
    public int records;           // общее количество записей
    public uint offset_ranges;     // смещение относительно начала файла до начала списка записей с геоинформацией
    public uint offset_cities;     // смещение относительно начала файла до начала индекса с сортировкой по названию городов
    public uint offset_locations;  // смещение относительно начала файла до начала списка записей о местоположении

    public DataHeader(MemoryMappedViewAccessor mmva)
    {
        mmva.Read(0, out version);
        mmva.ReadArray(4, name, 0, 32);
        mmva.Read(36, out timestamp);
        mmva.Read(44, out records);
        mmva.Read(48, out offset_ranges);
        mmva.Read(52, out offset_cities);
        mmva.Read(56, out offset_locations);

    }
}
