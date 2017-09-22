using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace nZadarma.Tests
{
    [TestFixture]
    public class Tests
    {
        
        [Test]
        public async Task BalanceTest()
        {
            var zadarmaApi = new ZadarmaApi(PrivateConstants.Key, PrivateConstants.Secret);
            var balance = await zadarmaApi.Balance();
            
            Assert.That(balance, Is.Not.Null);
            Assert.That(balance.Balance, Is.GreaterThan(1));
        }
        
        [Test]
        public async Task SipsTest()
        {
            var zadarmaApi = new ZadarmaApi(PrivateConstants.Key, PrivateConstants.Secret);
            var balance = await zadarmaApi.Sips();
            
            Assert.That(balance, Is.Not.Null);
            Assert.That(balance.Sips,  Is.Not.Null);
            Assert.That(balance.Sips.Any(), Is.True);
        }
        
        [Test]
        public async Task CallbackTest()
        {
            var zadarmaApi = new ZadarmaApi(PrivateConstants.Key, PrivateConstants.Secret);
            var callback = await zadarmaApi.Callback(PrivateConstants.NumberFrom, PrivateConstants.NumberTo);
            
            Assert.That(callback, Is.Not.Null);
            Assert.That(callback.Message, Is.Null);
        }
    }
}