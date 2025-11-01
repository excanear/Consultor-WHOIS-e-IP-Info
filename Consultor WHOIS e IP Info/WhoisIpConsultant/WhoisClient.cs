using System.Net.Sockets;
using System.Text;

public class WhoisClient
{
    private static readonly Dictionary<string, string> _tldServers = new()
    {
        {"com", "whois.verisign-grs.com"},
        {"net", "whois.verisign-grs.com"},
        {"org", "whois.pir.org"},
        {"br", "whois.registro.br"},
        {"io", "whois.nic.io"}
    };

    public async Task<string> QueryAsync(string domainOrIp)
    {
        var server = PickWhoisServer(domainOrIp);
        using var tcp = new TcpClient();
        await tcp.ConnectAsync(server, 43);
        using var stream = tcp.GetStream();
        var query = domainOrIp + "\r\n";
        var data = Encoding.ASCII.GetBytes(query);
        await stream.WriteAsync(data, 0, data.Length);
        using var ms = new MemoryStream();
        var buffer = new byte[4096];
        int read;
        while ((read = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
        {
            ms.Write(buffer, 0, read);
            // small delay to allow remote to close nicely
            if (!stream.DataAvailable) break;
        }
        return Encoding.UTF8.GetString(ms.ToArray());
    }

    private string PickWhoisServer(string input)
    {
        // if looks like IP, use whois.arin.net (IPv4/6 depending)
        if (System.Net.IPAddress.TryParse(input, out var ip))
        {
            return ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork ? "whois.arin.net" : "whois.iana.org";
        }
        // extract TLD
        var parts = input.Split('.', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length < 2) return "whois.iana.org";
        var tld = parts[^1].ToLowerInvariant();
        if (_tldServers.TryGetValue(tld, out var server)) return server;
        return "whois.iana.org";
    }
}
