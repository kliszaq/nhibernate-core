using System.Collections.Generic;

namespace NHibernate.Test.NHSpecificTest.NH2577
{
    public class NetProtocol
    {
        public virtual int Id { get; set; }
    }

    public class Terminal
    {
        public virtual int Id { get; set; }

        public virtual NetProtocol NetProtocol { get; set; }

        public virtual string Street { get; set; }

        public virtual string StreetXml { get; set; }

        public virtual string StreetXml2 { get; set; }

        public virtual IDictionary<long, string> Attributes { get; set; }
    }
}
