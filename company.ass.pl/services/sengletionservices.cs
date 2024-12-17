
namespace company.ass.pl.services
{
    public class sengletionservices : Isengletionservices
    {
        public Guid Guid { get; set; }

        public sengletionservices()
        {
            Guid = Guid.NewGuid();

        }
        public string GetGuid()
        {
            return Guid.ToString();
        }
        public override string ToString()
        {
            return Guid.ToString();
        }
    }
}
