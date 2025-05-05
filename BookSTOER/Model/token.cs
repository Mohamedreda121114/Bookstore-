namespace BookSTOER.Model
{
    public class token
    {
        public string Issuer { get; set; }
        public string Aidience { get; set; }
        public int Lifetime { get; set; }
        public string signkey { get; set; }
    }
}
