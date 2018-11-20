using AstronomyPictureOfTheDay.Entities;
using FakeItEasy;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AstronomyPictureOfTheDay.Tests
{
    [TestFixture()]
    public class Test
    {
        string key = "testing";
        [Test]
        public void TestMethod1()
        {
            Assert.AreEqual(1, 1);
        }

        [Test]
        public void TestExceptionReturnFalse()
        {
            var rest = A.Fake<IRestServiceCaller>();
            A.CallTo(() => rest.GetAPODJsonAsync(key))
                               .ThrowsAsync(new Exception(key));

            NasaPictureOfTheDay nasa = new NasaPictureOfTheDay(rest);

            var results = nasa.GetTodaysPictureAsync(key).Result;


            Assert.AreEqual(results.Success, false);
            Assert.IsNotNull(results.exception);
        }

        [Test]
        public void TestGetResults()
        {
            var rest = A.Fake<IRestServiceCaller>();
            A.CallTo(() => rest.GetAPODJsonAsync(key))
                               .Returns<Task<string>>
                               (Task.FromResult<string>( "{\"copyright\": \"Ole C. SalomonsenArctic Light Photo\",\"date\": \"2018-11-18\",\"explanation\": \"It was Halloween and the sky looked like a creature. Exactly which creature, the astrophotographer was unsure but (possibly you can suggest one). Exactly what caused this  eerie apparition in 2013 was sure: one of the best auroral displays in recent years. This spectacular aurora had an unusually high degree of detail. Pictured here, the vivid green and purple  auroral colors are caused by high atmospheric oxygen and nitrogen reacting to a burst of incoming electrons.  Birch trees in Troms\u00f8, Norway formed an also eerie foreground. Recently, new photogenic auroras have accompanied new geomagnetic storms.\",\"hdurl\": \"https://apod.nasa.gov/apod/image/1811/creatureaurora_salomonsen_600.jpg\",\"media_type\": \"image\",\"service_version\": \"v1\",\"title\": \"Creature Aurora Over Norway\",\"url\": \"https://apod.nasa.gov/apod/image/1811/creatureaurora_salomonsen_960.jpg\"}"));

            NasaPictureOfTheDay nasa = new NasaPictureOfTheDay(rest);

            PictureOfTheDayResponse results = null;

            Task.Run(async () => { results = await nasa.GetTodaysPictureAsync(key); }).Wait();

            Assert.AreEqual(results.Success, true);
            Assert.IsNotNull(results.pictureOfTheDay);
        }
    }
}
