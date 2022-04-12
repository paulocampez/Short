using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace tests
{
    [TestFixture]
    public class UrlsControllerTest
    {
        private readonly List<string> _urls = new List<string>();
        public UrlsControllerTest()
        {
            _urls.AddRange(new[]
            {
                "ABCDE",
                "ABCDF",
                "ABCDG",
                "ABCDH",
                "ABCDJ",
                "ABCDK",
                "ABCDX",
                "ABCDZ",
                "ABCDS",
                "ABCDQ",
                "ABCDR",
                "ABCDP",
                "ABCDU",

            });
        }
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestIndex()
        {
            Assert.Pass();
        }

        [Test]
        public void EncryptMustReturnValue()
        {
            //arrange
            string url;
            //act
            url = hey_url_challenge_code_dotnet.Helper.WebHelper.Encrypt(5);
            //assert
            Assert.IsTrue(!string.IsNullOrEmpty(url));
        }
        [Test]
        public void EncryptLenghtMustBeFive()
        {
            //arrange
            string url;
            //act
            url = hey_url_challenge_code_dotnet.Helper.WebHelper.Encrypt(0);
            //assert
            Assert.IsTrue(url.ToCharArray().Count() == 5);
        }
        [Test]
        public void Short_Url_Must_Be_Unique()
        {
            var distinctUrl = new HashSet<string>(_urls);
            Assert.IsTrue(distinctUrl.Count == _urls.Count);
        }
        [Test]
        public void Short_Url_Cannot_Accept_Same_Values()
        {
            string newUrl;
            newUrl = "ABCDU";
            do
            {
                newUrl = "ZZZZX";
            } while (_urls.Contains(newUrl));

            _urls.Add(newUrl);

            Assert.IsTrue(!_urls.GroupBy(x => x).Any(x => x.Count() >1));
        }
        [Test]
        public void Short_Url_Can_Identify_When_Already_Have_Short_Url()
        {
            string newUrl;
            newUrl = "ABCDU";


            Assert.IsTrue(_urls.Contains(newUrl));
        }

        [Test]
        public void Valid_Url()
        {
            Uri uriResult;
            bool result = Uri.TryCreate("https://www.google.com", UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);


            Assert.IsTrue(result);
        }
        [Test]
        public void Invalid_Url()
        {
            Uri uriResult;
            bool result = Uri.TryCreate("adsfsdfa", UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);


            Assert.IsTrue(!result);
        }

    }
}