using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using NUnit.Framework;

namespace NHibernate.Test.NHSpecificTest.NH2577
{
    public class Fixture : BugTestCase
    {
        [Test]
        public void QueryWithLinq_WhenAskedForSubqueryWithFormula_ShouldSetRightParametersOrder()
        {
            using (var session = OpenSession())
            {
                session.Save(new NetProtocol());
                session.Save(new NetProtocol());
                session.Save(new NetProtocol());
                var protocol4 = new NetProtocol();
                session.Save(protocol4);

                var terminal = new Terminal
                {
                    Attributes = new Dictionary<long, string>(),
                    Street = "Sesame",
                    NetProtocol = protocol4
                };

                terminal.Attributes.Add(1, "a");
                terminal.Attributes.Add(2, "b");
                terminal.Attributes.Add(124, "c");

                session.Save(terminal);

                session.Flush();
                session.Clear();

                var terminals = session.Query<Terminal>()
                    .Where(x => x.NetProtocol.Id == 4)
                    .OrderBy(x => x.Attributes[124])
                    .ToList();

                Assert.AreEqual(1, terminals.Count);
                Assert.AreEqual(terminal.Id, terminals[0].Id);
                Assert.AreEqual(3, terminals[0].Attributes.Keys.Count);
            }
        }

        protected override void OnTearDown()
        {
            using (var session = OpenSession())
            using (var tx = session.BeginTransaction())
            {
                session.Delete("from System.Object");
                tx.Commit();
            }
        }
    }
}