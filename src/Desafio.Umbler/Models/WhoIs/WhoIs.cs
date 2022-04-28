using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Whois.NET;

namespace Desafio.Umbler.Models.WhoIs;

public class WhoIs : IWhoIs
{
    public async Task<WhoisResponse> QueryAsync(string query, string server = null, int port = 43, Encoding encoding = null, int timeout = 600, int retries = 10, CancellationToken token = default)
    {
        return await WhoisClient.QueryAsync(query);
    }
}