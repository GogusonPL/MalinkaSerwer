using MalinkaSerwer.Services;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MalinkaSerwer.Tests
{
    [TestFixture]
    public class DomoticzTests
    {
        [Test]
        public async Task Czy__poprawnie_wlacza_klimatyzacje()
        {
            var test = new DomoticzRequestHandlerMock();
            var result = await test.SetAc(true);
            Assert.AreEqual(result.status, "True");
        }
        [Test]
        public async Task Czy__poprawnie_wylacza_klimatyzacje()
        {
            var test = new DomoticzRequestHandlerMock();
            var result = await test.SetAc(false);
            Assert.AreEqual(result.status, "False");
        }
        [Test]
        public async Task Czy_poprawnie_wlacza_swiatlo()
        {
            var test = new DomoticzRequestHandlerMock();
            var result = await test.SetLight(true);
            Assert.AreEqual(result.status, "True");
        }
        [Test]
        public async Task Czy_poprawnie_wylacza_swiatlo()
        {
            var test = new DomoticzRequestHandlerMock();
            var result = await test.SetLight(false);
            Assert.AreEqual(result.status, "False");
        }
    }
}
