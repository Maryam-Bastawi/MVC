
namespace company.ass.pl.services
{
    public class scopedservices : Iscopedservices
    {
        public Guid Guid { get ; set ; }

        public scopedservices()
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
