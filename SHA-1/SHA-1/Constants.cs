namespace SHA_1
{
    internal class Constants
    {
        // initial hash values
        internal const uint H0 = 1732584193; // 67452301 in hex
        internal const uint H1 = 4023233417; // efcdab89 in hex
        internal const uint H2 = 2562383102; // 98badcfe in hex
        internal const uint H3 = 271733878; // 10325476 in hex
        internal const uint H4 = 3285377520; // c3d2e1f0 in hex

        internal const uint Kt_0to_19 = 1518500249; // 5a827999 in hex, for rounds 0 to 19
        internal const uint Kt_20to39 = 1859775393; // 6ed9eba1 in hex, for rounds 20 to 39
        internal const uint Kt_40to59 = 2400959708; // 8f1bbcdc in hex, for rounds 40 to 59
        internal const uint Kt_60to79 = 3395469782; // ca62c1d6 in hex for rounds 60 to 79
    }
}