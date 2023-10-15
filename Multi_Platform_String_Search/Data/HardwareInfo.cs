using Hardware.Info;
using System.Net.NetworkInformation;

public class CpuInfoService
{
    static readonly IHardwareInfo hardwareInfo = new HardwareInfo();

    public string CPUinfo () //Returns CPU model
    {
        string CPU = "";
       bool includePercentProcessorTime = false;
        var hardwareInfo = new HardwareInfo(includePercentProcessorTime);
        hardwareInfo.RefreshCPUList();
        foreach (var cpu in hardwareInfo.CpuList)
        {
            CPU = cpu.Name.ToString();
        }
        return CPU;
    }
    public string CPUusage() //Returns CPU processor time in percentage
    {
        string CPUusage = "";
        var hardwareInfo = new HardwareInfo();
        hardwareInfo.RefreshCPUList();
        foreach (var cpu in hardwareInfo.CpuList)
        {
            CPUusage = cpu.PercentProcessorTime.ToString();
        }
        return CPUusage;
    }
    public List<string> RAMtype() //Returns RAM FormFactor and speed
    {
        var RAMtype = new List<string>();
        var hardwareInfo = new HardwareInfo();
        hardwareInfo.RefreshMemoryList();
        foreach (var hardware in hardwareInfo.MemoryList)
        {
            RAMtype.Add(hardware.FormFactor.ToString());
            RAMtype.Add(hardware.Speed.ToString());
        }
        return RAMtype;
    }

    public class DISKinfo
    {
        public string Disk { get; set; }
        public string Size { get; set; }
        public string FreeSpace { get; set; }
    }
    public List<DISKinfo> DISKtype() //Returns Disk names and the free space available
    {
        double SingleVolumeFreeSpace = 0;
        double TotalPartitionFreeSpace = 0;
        List<DISKinfo> Disk = new List<DISKinfo>();

        var HardwareInfo = new HardwareInfo();
        HardwareInfo.RefreshDriveList();
        foreach (var drive in HardwareInfo.DriveList)
        {
            double DISKsize = Convert.ToDouble(drive.Size);
            Disk.Add(new DISKinfo {Disk = drive.Model.ToString(), Size = Math.Round(DISKsize / Math.Pow(10, 9), 2).ToString()});
            foreach (var partition in drive.PartitionList)
            {
                foreach (var volume in partition.VolumeList)
                {
                    SingleVolumeFreeSpace += Convert.ToDouble(volume.FreeSpace);
                    TotalPartitionFreeSpace += SingleVolumeFreeSpace;
                }
                //FullInfo.Add(partition.Name.ToString()); //For later use in other UI elements
                //FullInfo.Add(Math.Round(TotalPartitionFreeSpace / Math.Pow(10, 9), 1).ToString()); //For later use in other UI elements
                SingleVolumeFreeSpace = 0;
            }
            if (Disk.Count > 0)
            {
                Disk[Disk.Count - 1].FreeSpace = Math.Round(TotalPartitionFreeSpace / Math.Pow(10, 9), 1).ToString();
            }
            else
            {
                Console.WriteLine("List is empty");
            }
            //Disk.Add(new DISKinfo { FreeSpace = Math.Round(TotalPartitionFreeSpace / Math.Pow(10, 9), 1).ToString() }); //Adds the total free space of the Disk to the list
            TotalPartitionFreeSpace = 0;
        }
        return Disk;
    }

    public string GPUusage() //Returns GPU usage in percentage
    {
        string GPUusage = "";
        var hardwareInfo = new HardwareInfo();
        hardwareInfo.RefreshCPUList();
        foreach (var cpu in hardwareInfo.CpuList)
        {
            GPUusage = cpu.PercentProcessorTime.ToString();
        }
        return GPUusage;
    }

    public string RAMusage() //Returns RAM usage
    {
        string RAMusage;
        double RAMtotal;
        double RAMavailable;
        double RAMmath;

        var hardwareInfo = new HardwareInfo();
        hardwareInfo.RefreshMemoryStatus();
        RAMtotal = hardwareInfo.MemoryStatus.TotalPhysical;
        RAMavailable = hardwareInfo.MemoryStatus.AvailablePhysical;
        RAMmath = RAMavailable / RAMtotal * 100;
        RAMusage = Math.Round(RAMmath).ToString();

        return RAMusage;
    }
}
