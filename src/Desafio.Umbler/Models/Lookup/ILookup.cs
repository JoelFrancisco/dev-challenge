using System.Threading;
using System.Threading.Tasks;
using DnsClient;

namespace Desafio.Umbler.Models.Lookup;

public interface ILookup
{
    Task<IDnsQueryResponse> QueryAsync(string query, QueryType queryType, QueryClass queryClass = QueryClass.IN, CancellationToken cancellationToken = default);
}