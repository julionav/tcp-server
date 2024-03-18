// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Net.Sockets;
using System.Text;

var port = 8888;
var server = new TcpListener(IPAddress.Any, port);
server.Start();

Console.WriteLine("Server started. Listening on port " + port);

while (true)
{
    TcpClient client = await server.AcceptTcpClientAsync();
    Console.WriteLine("New client connected.");

    // Handle the client connection in a separate task
    _ = HandleClientAsync(client);
}

async Task HandleClientAsync(TcpClient client)
{
    NetworkStream stream = client.GetStream();

    byte[] buffer = new byte[1024];
    int bytesRead;

    while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
    {
        string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
        Console.WriteLine("Received message: " + message);

        if (message.ToLower().Trim() == "ping")
        {
            byte[] response = Encoding.ASCII.GetBytes("pong");
            await stream.WriteAsync(response, 0, response.Length);
            Console.WriteLine("Sent response: pong");
        }
    }

    client.Close();
    Console.WriteLine("Client disconnected.");
}