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
            
            Console.WriteLine(balance.ToStringDebug());
        }
        
        [Test]
        public async Task SipsTest()
        {
            var zadarmaApi = new ZadarmaApi(PrivateConstants.Key, PrivateConstants.Secret);
            var sips = await zadarmaApi.Sips();
            
            Assert.That(sips, Is.Not.Null);
            Assert.That(sips.Sips,  Is.Not.Null);
            Assert.That(sips.Sips.Any(), Is.True);
            
            Console.WriteLine(sips.ToStringDebug());
        }
        
        [Test]
        public async Task CallbackTest()
        {
            var zadarmaApi = new ZadarmaApi(PrivateConstants.Key, PrivateConstants.Secret);
            var callback = await zadarmaApi.Callback(PrivateConstants.NumberFrom, PrivateConstants.NumberTo);
            
            Assert.That(callback, Is.Not.Null);
            
            Console.WriteLine(callback.ToStringDebug());
        }
        
        [Test]
        public async Task RecordRequestTest()
        {
            var zadarmaApi = new ZadarmaApi(PrivateConstants.Key, PrivateConstants.Secret);
            var recordRequest = await zadarmaApi.RecordRequest("1506173249.3756289", null);
            
            Assert.That(recordRequest, Is.Not.Null);
            
            Console.WriteLine(recordRequest.ToStringDebug());
        }
        
        [Test]
        public async Task StatisticsPbxTest()
        {
            var zadarmaApi = new ZadarmaApi(PrivateConstants.Key, PrivateConstants.Secret);
            var statistics = await zadarmaApi.StatisticsPbx(DateTime.Now.Add(TimeSpan.FromDays(-7)), DateTime.Now);
            
            Assert.That(statistics, Is.Not.Null);
            
            Console.WriteLine(statistics.ToStringDebug());
        }
    }
}