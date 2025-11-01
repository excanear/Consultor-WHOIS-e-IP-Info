using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

public class IpGeoInfo
{
    public string? status { get; set; }
    public string? message { get; set; }
    public string? country { get; set; }
    public string? regionName { get; set; }
    public string? city { get; set; }
    public string? isp { get; set; }
    public string? org { get; set; }
    public string? query { get; set; }
    public double? lat { get; set; }
    public double? lon { get; set; }
    public string? timezone { get; set; }
}

public class IpInfoClient
{
    private readonly HttpClient _http = new();

    public async Task<IpGeoInfo?> GetGeoAsync(string ip)
    {
        var url = $"http://ip-api.com/json/{ip}?fields=status,message,country,regionName,city,isp,org,query,lat,lon,timezone";
        try
        {
            var resp = await _http.GetFromJsonAsync<IpGeoInfo>(url);
            return resp;
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao consultar geolocalização: " + ex.Message, ex);
        }
    }

    public async Task<string> GetRdapAsync(string ip)
    {
        var url = $"https://rdap.org/ip/{ip}";
        try
        {
            var s = await _http.GetStringAsync(url);
            // pretty print JSON if possible
            try
            {
                using var doc = JsonDocument.Parse(s);
                return JsonSerializer.Serialize(doc.RootElement, new JsonSerializerOptions { WriteIndented = true });
            }
            catch { return s; }
        }
        catch (HttpRequestException ex)
        {
            return "RDAP request failed: " + ex.Message;
        }
    }
}
