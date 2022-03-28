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
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData newData = new ContactData("qweq", null);

            if (app.Contact.IsElementPresent(By.Name("entry")) != true)
            {
                app.Contact.Create(new ContactData("Vasiliy", null));
            }

            List<ContactData> oldContacts = app.Contact.GetContactList();

            app.Contact.Modify(0, newData);

            List<ContactData> newContacts = app.Contact.GetContactList();
            oldContacts[0].Firstname = newData.Firstname;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
