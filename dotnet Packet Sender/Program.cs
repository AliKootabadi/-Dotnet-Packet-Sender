using System.Net;
using System.Net.Sockets;
using System.Text;

#region [ Variables ]
string hostName;
IPHostEntry IpList;
List<IPAddress> IPv4list = new List<IPAddress>();
Socket socket = null;
string Message = "";
int Count = 0;
#endregion

#region [ SELECT IP ]
hostName = Dns.GetHostName();
IpList = Dns.GetHostEntry(hostName);

Console.WriteLine("Please Select one of IP Addresses below.");
foreach (var Ip in IpList.AddressList)
{
    if (Ip.AddressFamily == AddressFamily.InterNetwork)
    {
        Console.WriteLine($"[{Count++}]. {Ip}");
        IPv4list.Add(Ip);
    }
}
int SelectedIp = int.Parse(Console.ReadLine()!);
#endregion

#region [ WRITE MESSAGE ]
Console.WriteLine("Please write a message to send.");
Message = Console.ReadLine()!;
byte[] data = Encoding.UTF8.GetBytes(Message!);
#endregion

#region [ Create Socket ]
socket = new(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
socket.Bind(new IPEndPoint(IPv4list[SelectedIp], 0));
#endregion

#region [ Send ]
socket.SendTo(data, new IPEndPoint(IPAddress.Parse("192.168.1.1"), 0));
#endregion