using DnsClient;

public class DnsHelper
{
    private readonly LookupClient _lookup = new LookupClient();

    public async Task<IEnumerable<string>> GetARecordsAsync(string name)
    {
        var res = await _lookup.QueryAsync(name, QueryType.A);
        return res.Answers.ARecords().Select(r => r.Address.ToString());
    }

    public async Task<IEnumerable<string>> GetMxRecordsAsync(string name)
    {
        var res = await _lookup.QueryAsync(name, QueryType.MX);
        return res.Answers.MxRecords().Select(r => $"{r.Exchange.Value} (priority {r.Preference})");
    }

    public async Task<IEnumerable<string>> ReverseLookupAsync(string ip)
    {
        var res = await _lookup.QueryReverseAsync(System.Net.IPAddress.Parse(ip));
        return res.Answers.PtrRecords().Select(r => r.PtrDomainName.Value);
    }
}
