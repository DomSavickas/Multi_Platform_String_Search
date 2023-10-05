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
}
