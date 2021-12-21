using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using System.Collections.Generic;

public class DataLoader
{
    private readonly string DataPath = ".\\Data\\geobase.dat";
    public static MemoryMappedFile mmf;

    public DataLoader()
    {
        mmf = MemoryMappedFile.CreateFromFile(DataPath, FileMode.Open, "geodata", 0, MemoryMappedFileAccess.Read);
    }



}
