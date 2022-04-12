using HeyUrl.Urls.Application.Interface;
using HeyUrl.Urls.Domain.Interfaces;
using HeyUrl.Urls.Domain.Models;
using System;
using System.Threading.Tasks;

namespace HeyUrl.Urls.Application
{
    public class UrlService : IUrlService
    {

        private readonly IUrlRepository _urlRepository;
        public UrlService(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }


        public async Task EncryptUrl(Url url)
        {
            var refactoringId = url.Id * randomBigPrimeNumber % alphabetCountTimesFive;
            string shortUrl = NumberToEncryptedText(refactoringId);
            while (shortUrl.Length <= 4) shortUrl = "A" + shortUrl;
            url.ShortUrl = shortUrl;
            
        }
        
        private static int randomBigPrimeNumber = 502057; //http://www.mathspage.com/is-prime/solved/is-502057-a-prime-number
        private static int alphabetCountTimesFive = 11881376; //26 * 26 * 26 * 26 * 26
        private static char[] alphabetCharacteres = new char[] {
        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z' };

        private string NumberToEncryptedText(int value)
        {
            string result = string.Empty;
            int targetBase = alphabetCharacteres.Length;

            do
            {
                result = alphabetCharacteres[value % targetBase] + result;
                value = value / targetBase;
            }
            while (value > 0);

            return result;
        }

        public async Task Create(Url url)
        {
            await _urlRepository.Add(url);

        }
        public async Task Update(Url url)
        {
            await EncryptUrl(url);
            await _urlRepository.Update(url);
        }

    }
}
