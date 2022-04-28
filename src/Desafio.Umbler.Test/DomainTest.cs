using Desafio.Umbler.Models;
using Desafio.Umbler.Models.Lookup;
using Desafio.Umbler.Models.WhoIs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Desafio.Umbler.Test;

[TestClass]
public class DomainTest 
{
    [TestMethod]
    public void Domain_is_created()
    {
        var domainName = "";

        var whoIs = new Mock<IWhoIs>();
        whoIs.Setup(x => x.QueryAsync(domainName)).Returns();

        var lookup = new Mock<ILookup>();

        var domain = new Domain();
    }
}