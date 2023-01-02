using ConsoleTables;
using ProLinkLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pioneer_CLI
{
    public class NetworkManagementController
    {
       public NetworkManagementController()
       {

       }

        public void InitCLI(ProLinkController plc)
        {
            bool first_step_completed = false;
            bool ip_config_step_completed = false;
            NetworkInterface networkInt = null;

            Console.WriteLine("In order to make this tool working, you need to change the IP Address to be on the same network as Pioneer Devices");
            Console.WriteLine("This first setup guides you to select the Network interface and automatically change the IP Address, you might be disconnected from Internet");
            Console.WriteLine("IMPORTANT: While closing the program make sure to change the Ethernet settings again to have Internet Connection");

            while(!first_step_completed)
            {
                var table = new ConsoleTable("ID", "NetworkInterfaceName", "Description", "IPAddress", "MACAddress");
                var nw_list = Utils.GetNetworkInterfaces();
                foreach (int id in nw_list.Keys)
                {
                    table.AddRow(id,
                        nw_list[id].Name,
                        nw_list[id].Description,
                        nw_list[id].GetIPProperties().UnicastAddresses.FirstOrDefault(ip => ip.Address.AddressFamily == AddressFamily.InterNetwork).Address.ToString(), 
                        nw_list[id].GetPhysicalAddress());
                }
                table.Write();
                Console.WriteLine();
                Console.Write("Select the network ID option: ");
                try
                {
                    int option = Int32.Parse(Console.ReadLine());
                    nw_list.TryGetValue(option, out networkInt);
                    if (networkInt != null)
                    {
                        first_step_completed = true;
                    }
                    else
                    {
                        Console.WriteLine("Error while selecting the network interface, please provide a correct value of ID");
                    }
                }
                catch
                {
                    Console.WriteLine("Error while selecting the network interface, please provide a correct value of ID");
                }
            }


            while (!ip_config_step_completed)
            {
                Console.Write("Is the CDJ and Mixer connected to your home network or not? [Y|N]: ");
                string q_1 = Console.ReadLine().ToLower();
                if (q_1 == "y")
                {
                    Console.Write("Please provide your computer IP Address [xxx.xxx.xxx.xxx]: ");
                    string ip_address = Console.ReadLine();
                    try
                    {
                        plc.GetVirtualCDJ().IPaddress = IPAddress.Parse(ip_address).GetAddressBytes();
                        plc.GetVirtualCDJ().MacAddress = Utils.GetMacAddress(networkInt.Name).GetAddressBytes();
                        var mask = networkInt.GetIPProperties().UnicastAddresses.FirstOrDefault(ip => ip.Address.AddressFamily == AddressFamily.InterNetwork).IPv4Mask.ToString();
                        plc.GetVirtualCDJ().BroadcastAddress = Utils.GetBroadcastAddress(IPAddress.Parse(ip_address), IPAddress.Parse(mask)).ToString();

                        ip_config_step_completed = true;
                    }
                    catch
                    {
                        Console.WriteLine("Error While parsing IP");
                    }
                }
                else if(q_1 == "n")
                {
                    Console.WriteLine("Changing the IP Address to: 169.254.45.12");
                    Console.WriteLine("Changing the Broadcast IP Address to: 169.254.255.255");
                    Utils.SetIP(networkInt.Name, "169.254.45.12", "255.255.0.0");
                    plc.GetVirtualCDJ().IPaddress = IPAddress.Parse("169.254.45.12").GetAddressBytes();
                    plc.GetVirtualCDJ().MacAddress = Utils.GetMacAddress(networkInt.Name).GetAddressBytes();
                    plc.GetVirtualCDJ().BroadcastAddress = "169.254.255.255";

                    ip_config_step_completed = true;
                }
                else
                {
                    Console.WriteLine("Invalid Option!");
                }
            }
            Console.WriteLine("Finishing the setup....");
            Thread.Sleep(4000);
            Console.WriteLine("All steps done! Please enjoy and be responsible");
            Console.WriteLine();
        }
    
    }
}
