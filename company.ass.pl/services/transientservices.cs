
namespace company.ass.pl.services
{
    public class transientservices : Itransientservices
    {
        public Guid Guid { get; set; }

        public transientservices()
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
