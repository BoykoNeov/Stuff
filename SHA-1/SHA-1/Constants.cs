namespace SHA_1
{
    public class Constants
    {
        // initial hash values
        const string H0 = "67452301";
        const string H1 = "efcdab89";
        const string H2 = "98badcfe";
        const string H3 = "10325476";
        const string H4 = "c3d2e1f0";

        const string Kt_0to_19 = "5a827999"; // for rounds 0 to 19
        const string Kt_20to39 = "6ed9eba1"; // for rounds 20 to 39
        const string Kt_40to59 = "8f1bbcdc"; // for rounds 40 to 59
        const string Kt_60to79 = "ca62c1d6"; // for rounds 60 to 79

        // Convert.ToUInt32(hex, 16)

    }
}