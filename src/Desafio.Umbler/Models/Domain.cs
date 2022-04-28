using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Desafio.Umbler.Models.Lookup;
using Desafio.Umbler.Models.WhoIs;
using DnsClient;

namespace Desafio.Umbler.Models
{
    public class Domain
    {        
        private readonly IWhoIs _whoIs;
        private readonly ILookup _lookup;

        public Domain() 
        { 

        }

        public Domain(IWhoIs whoIs, ILookup lookup, string domainName)
        {
            _whoIs = whoIs;
            _lookup = lookup;
            Create(domainName);
        }

        public async void Create(string domainName) 
        {
            var response = await _whoIs.QueryAsync(domainName);
            var result = await _lookup.QueryAsync(domainName, QueryType.ANY);
            var record = result.Answers.ARecords().FirstOrDefault();
            var address = record?.Address;
            var ip = address?.ToString();

            var hostResponse = await _whoIs.QueryAsync(ip);

            Name = domainName;
            Ip = ip;
            UpdatedAt = DateTime.Now;
            WhoIs = response.Raw;
            Ttl = record?.TimeToLive ?? 0;
            HostedAt = hostResponse.OrganizationName;
        }

        public bool Validate() 
        {
            if (DateTime.Now.Subtract(UpdatedAt).TotalMinutes > Ttl)
            {
                return false;
            }

            return true;
        }

        public void Update(string domainName) 
        {
            Create(domainName);
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string WhoIs { get; set; }
        public int Ttl { get; set; }
        public string HostedAt { get; set; }
    }
}