using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
        Console.WriteLine("1. Server ");
        Console.WriteLine("2. Client ");
        Console.Write("Nhập dữ liệu: ");
        string num = Console.ReadLine(); // Nhập số từ người dùng

        switch (num) // So sánh chuỗi
        {
            case "1": // Trường hợp Server
                // Yêu cầu người dùng nhập địa chỉ IP
                Console.Write("Nhập địa chỉ IP để khởi động server: ");
                string ipAddressServer = Console.ReadLine();

                // Yêu cầu người dùng nhập cổng
                Console.Write("Nhập cổng để khởi động server: ");
                int portServer = int.Parse(Console.ReadLine());

                // Tạo TcpListener với địa chỉ IP và cổng người dùng nhập
                TcpListener server = new TcpListener(IPAddress.Parse(ipAddressServer), portServer);
                server.Start();
                Console.WriteLine($"Server started on {ipAddressServer}:{portServer}...");

                while (true)
                {
                    // Chấp nhận kết nối từ client
                    TcpClient client = server.AcceptTcpClient();
                    NetworkStream stream = client.GetStream();

                    // Gửi thông điệp đến client
                    byte[] buffer = Encoding.ASCII.GetBytes("Hello, Client!");
                    stream.Write(buffer, 0, buffer.Length);

                    // Đóng kết nối sau khi gửi tin nhắn
                    client.Close();
                }

            case "2": // Trường hợp Client
                // Yêu cầu người dùng nhập địa chỉ IP
                Console.Write("Nhập địa chỉ IP để kết nối client: ");
                string ipAddressClient = Console.ReadLine();

                // Yêu cầu người dùng nhập cổng
                Console.Write("Nhập cổng để kết nối client: ");
                int portClient = int.Parse(Console.ReadLine());

                // Tạo TcpClient với địa chỉ IP và cổng người dùng nhập
                TcpClient tcpClient = new TcpClient(ipAddressClient, portClient);
                NetworkStream clientStream = tcpClient.GetStream();

                // Đọc dữ liệu từ server
                byte[] bufferClient = new byte[1024];
                int bytesRead = clientStream.Read(bufferClient, 0, bufferClient.Length);
                Console.WriteLine(Encoding.ASCII.GetString(bufferClient, 0, bytesRead));

                // Đóng kết nối client
                tcpClient.Close();
                break;

            default:
                Console.WriteLine("Lựa chọn không hợp lệ, vui lòng nhập 1 hoặc 2.");
                break;
        }
    }
}
