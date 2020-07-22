
using MalinkaSerwer.Services;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MalinkaSerwer.Tests
{
    [TestFixture]
    public class DomoticzTests
    {
        [Test]
        public async Task Czy_poprawnie_wlacza_klimatyzacje()
        {
            var test = new DomoticzRequestHandlerMock();
            var result = await test.SetAc(true);
            Assert.AreEqual(result.status, "True");
        }
        [Test]
        public async Task Czy_poprawnie_wylacza_klimatyzacje()
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
        [Test]
        public async Task Czy_poprawnie_swiatlo_zmienia_stany()
        {
            var test = new DomoticzRequestHandlerMock();
            var result = await test.SetLight(false);
            Assert.AreEqual(result.status, "False");
            result = await test.SetLight(true);
            Assert.AreEqual(result.status, "True");
        }
        [Test]
        public async Task Czy_poprawnie_klimatyzacja_zmienia_stany()
        {
            var test = new DomoticzRequestHandlerMock();
            var result = await test.SetAc(false);
            Assert.AreEqual(result.status, "False");
            result = await test.SetAc(true);
            Assert.AreEqual(result.status, "True");
        }
        [Test]
        public void Czy_poprawnie_wlacza_smart_house()
        {
            var test = new DomoticzRequestHandlerMock();
            test.IsSmartHomeOn = true;
            Assert.AreEqual(test.IsSmartHomeOn, true);
        }
        [Test]
        public void Czy_poprawnie_wylacza_smart_house()
        {
            var test = new DomoticzRequestHandlerMock();
            test.IsSmartHomeOn = false;
            Assert.AreEqual(test.IsSmartHomeOn, false);
        }
        [Test]
        public void Czy_smarthouse_uruchamia_klimatyzacje()
        {
            var test = new DomoticzRequestHandlerMock();
            test.IsSmartHomeOn = true;
            Thread.Sleep(TimeSpan.FromSeconds(10));
            Assert.AreEqual(test.IsAcOn, true);
        }
    }
}
