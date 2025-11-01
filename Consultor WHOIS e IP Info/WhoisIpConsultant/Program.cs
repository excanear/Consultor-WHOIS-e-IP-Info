using System.Text.Json;

var whois = new WhoisClient();
var ipinfo = new IpInfoClient();
var dns = new DnsHelper();

if (args.Length == 0)
{
    Console.WriteLine("Consultor WHOIS e IP Info - uso:\n  whois <dominio>\n  ip <endereco-ip>\n  dns <dominio>\n  rdap <endereco-ip>");
    while (true)
    {
        Console.Write('\n');
        Console.Write("comando> ");
        var line = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(line)) continue;
        if (line.Trim().ToLower() == "exit") break;
        var parts = line.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0) continue;
        var cmd = parts[0].ToLower();
        var arg = parts.Length > 1 ? parts[1].Trim() : string.Empty;
        await Execute(cmd, arg);
    }
}
else
{
    var cmd = args[0].ToLower();
    var arg = args.Length > 1 ? args[1] : string.Empty;
    await Execute(cmd, arg);
}

async Task Execute(string cmd, string arg)
{
    try
    {
        switch (cmd)
        {
            case "whois":
                if (string.IsNullOrWhiteSpace(arg)) { Console.WriteLine("Use: whois <dominio>"); break; }
                Console.WriteLine(await whois.QueryAsync(arg));
                break;
            case "ip":
                if (string.IsNullOrWhiteSpace(arg)) { Console.WriteLine("Use: ip <endereco-ip>"); break; }
                var geo = await ipinfo.GetGeoAsync(arg);
                Console.WriteLine(JsonSerializer.Serialize(geo, new JsonSerializerOptions { WriteIndented = true }));
                break;
            case "rdap":
                if (string.IsNullOrWhiteSpace(arg)) { Console.WriteLine("Use: rdap <endereco-ip>"); break; }
                var rd = await ipinfo.GetRdapAsync(arg);
                Console.WriteLine(rd);
                break;
            case "dns":
                if (string.IsNullOrWhiteSpace(arg)) { Console.WriteLine("Use: dns <dominio>"); break; }
                var a = await dns.GetARecordsAsync(arg);
                var mx = await dns.GetMxRecordsAsync(arg);
                Console.WriteLine("A records:\n" + string.Join('\n', a));
                Console.WriteLine('\n' + "MX records:\n" + string.Join('\n', mx));
                break;
            default:
                Console.WriteLine("Comando desconhecido: " + cmd);
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erro: " + ex.Message);
    }
}
