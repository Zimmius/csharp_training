using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTests : AuthTestBase
    {
        [Test]
        public void TestContactInformation()
        {
            ContactData fromTable = app.Contact.GetContactInformationFromTable(0);
            ContactData fromForm = app.Contact.GetContactInformationFromEditForm(0);

            //verification
            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromTable.Emails, fromForm.Emails);
        }

        [Test]
        public void TestDetailedContactInformation()
        {
            ContactData fromForm = app.Contact.GetContactInformationFromEditForm(0);
            ContactData fromDetails = app.Contact.GetContactInformationFromDetails(0);

            //verification
            Assert.AreEqual(fromForm.DetailedInformation, fromDetails.DetailedInformation);
        }
    }
}
