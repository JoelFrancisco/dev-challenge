using System.Threading;
using System.Threading.Tasks;
using DnsClient;

namespace Desafio.Umbler.Models.Lookup;

public class Lookup : ILookup
{
    public async Task<IDnsQueryResponse> QueryAsync(string query, QueryType queryType, QueryClass queryClass = QueryClass.IN, CancellationToken cancellationToken = default)
    {
        return await new LookupClient().QueryAsync(query, queryType);
    }
}