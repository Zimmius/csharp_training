using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            if (!app.Contact.IsElementPresent(By.Name("entry")))
            {
                app.Contact.Create(new ContactData("HEHO"));
            }

            List<ContactData> oldContacts = app.Contact.GetContactList();

            app.Contact.Remove(0);

            List<ContactData> newContacts = app.Contact.GetContactList();

            oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);
        }

    }
}
