namespace hey_url_challenge_code_dotnet.Helper
{
    public static class WebHelper
    {
        private static int randomBigPrimeNumber = 502057; //http://www.mathspage.com/is-prime/solved/is-502057-a-prime-number
        private static int alphabetCountTimesFive = 11881376; //26 * 26 * 26 * 26 * 26
        private static char[] alphabetCharacteres = new char[] {
        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z' };

        public static string Encrypt(int id)
        {
            
            var refactoringId = id * randomBigPrimeNumber % alphabetCountTimesFive;
            string shortUrl = NumberToEncryptedText(refactoringId);
            while (shortUrl.Length <= 4) shortUrl = "A" + shortUrl;
            return shortUrl;
        }

        public static string NumberToEncryptedText(int value)
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
    }
}
