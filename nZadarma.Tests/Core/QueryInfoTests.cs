using nZadarma.Core;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace nZadarma.Tests.Core
{
    [TestFixture]
    public class QueryInfoTests
    {
        [Test]
        public void ReturnEmptyStringIfNoParams()
        {
            var sut = new QueryInfo();

            var result = sut.Build();
            
            Assert.That(result, Is.Empty);
        }
        
        [Test]
        public void BuildCallbackParams()
        {
            var sut = new QueryInfo()
                .Add("from", "123")
                .Add("to", "qwe")
                .Add("predicted", true)
                ;

            var result = sut.Build(true);
            
            Assert.That(result, Is.EqualTo("from=123&predicted=true&to=qwe"));
        }
        
        [Test]
        public void BuildCallbackParams2()
        {
            var sut = new QueryInfo()
                .Add("version", "2")
                .Add("format", "json");

            var result = sut.Build(true);
            
            Assert.That(result, Is.EqualTo("format=json&version=2"));
        }
    }
}