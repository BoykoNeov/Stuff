namespace SHA_256
{
    using System;

    public static class SHA256
    {
        public static string Calculate(string input)
        {
            throw new NotImplementedException();
        }

        internal static uint Ch (uint x, uint y, uint z)
        {
            uint result = 0;
            result = (x & y) ^ (~x & z);
            return result;
        }
    }
}