using Pioneer_CLI.Devices;
using Pioneer_CLI.NFSMode;
using ProLinkLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pioneer_CLI.Commands
{
    public class NFSCommand : ICommand
    {
        public void BuildCustom(ProLinkController plc, CommandLineController clc, string args)
        {
            throw new NotImplementedException();
        }

        public void Run(ProLinkController plc, CommandLineController clc, string args)
        {
            try
            {
               if(clc.GetSelectedDevice() != null)
               {
                    var device = clc.GetSelectedDevice();
                    if(device is CDJ)
                    {
                        var cdj = (CDJ)device;
                        
                        if(cdj.UsbLocalStatus == 0x00 || cdj.SdLocalStatus == 0x00)
                        {
                            NFSCommandLineController nfs = new NFSCommandLineController();
                            nfs.InitShell(cdj.IpAddress, false);
                        }
                        else
                        {
                            Console.WriteLine("No USB or media mounted on that device");
                            return;
                        }
                    }
                    else if(device is Mixer && device.GetDeviceName().Contains("rekordbox"))
                    {
                        NFSCommandLineController nfs = new NFSCommandLineController();
                        nfs.InitShell(device.GetIPAddress(), true);
                    }
               }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error while processing command. Usage nfs");
                Console.WriteLine(e);
            }
        }
    }
}
