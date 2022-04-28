using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Desafio.Umbler.Models;
using Microsoft.EntityFrameworkCore;
using Desafio.Umbler.Controllers.Views;
using Desafio.Umbler.Models.WhoIs;
using Desafio.Umbler.Models.Lookup;

namespace Desafio.Umbler.Controllers
{
    [Route("api")]
    public class DomainController : Controller
    {
        private readonly DatabaseContext _db;
        private readonly IWhoIs _whoIs;
        private readonly ILookup _lookup;

        public DomainController(DatabaseContext db, IWhoIs whoIs, ILookup lookup)
        {
            _db = db;
            _whoIs = whoIs;
            _lookup = lookup;
        }

        [HttpGet, Route("domain/{domainName}")]
        public async Task<IActionResult> Get(string domainName)
        {
            var domain = await _db.Domains.FirstOrDefaultAsync(d => d.Name == domainName);

            if (domain == null)
            {
                domain = new Domain(_whoIs, _lookup, domainName);
                _db.Domains.Add(domain);
            }

            if (!domain.Validate()) 
            {
                domain.Update(domainName);
            }

            await _db.SaveChangesAsync();

            var view = new DomainView 
            {
                Name = domain.Name,
                Ip = domain.Ip,
                WhoIs = domain.WhoIs
            };

            return Ok(view);
        }
    }
}
