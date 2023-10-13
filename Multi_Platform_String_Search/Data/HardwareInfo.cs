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

    public List<string> DISKtype() //Returns DISK usage in percentage
    {
        double SinglePartitionFreeSpace = 0;
        double TotalPartitionFreeSpace = 0;
        var FullInfo = new List<string>();

        var hardwareInfo = new HardwareInfo();
        hardwareInfo.RefreshDriveList();
        foreach (var drive in hardwareInfo.DriveList)
        {
            string TempDname = drive.Model.ToString();
            double DISKsize = Convert.ToDouble(drive.Size);
            FullInfo.Add(TempDname);
            FullInfo.Add(Math.Round(DISKsize / Math.Pow(10, 9), 2).ToString());
            foreach (var partition in drive.PartitionList)
            {
                string TempPname = partition.Name.ToString();
                foreach (var volume in partition.VolumeList)
                {
                    SinglePartitionFreeSpace += Convert.ToDouble(volume.FreeSpace);
                    TotalPartitionFreeSpace += SinglePartitionFreeSpace;
                }
                FullInfo.Add(TempPname);
                FullInfo.Add(Math.Round(TotalPartitionFreeSpace / Math.Pow(10, 9), 1).ToString());
                SinglePartitionFreeSpace = 0;
            }
            FullInfo.Add(Math.Round(TotalPartitionFreeSpace / Math.Pow(10, 9), 1).ToString());
            TotalPartitionFreeSpace = 0;
        }

        return FullInfo;
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
