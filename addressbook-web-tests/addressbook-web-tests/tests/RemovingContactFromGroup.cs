using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class RemovingContactFromGroup : AuthTestBase
    {
        [Test]
        public void TestRemovingContactFromGroup()
        {
            //groups existing check
            if (GroupData.GetAll().Count() == 0)
            {
                app.Groups.Create(new GroupData("newGroup"));
            }

            //contacts existing check
            if (ContactData.GetAll().Count == 0)
            {
                app.Contact.Create(new ContactData("Vasiliy", "Ivaonv"));
            }

            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();

            //contactcs in group check
            if (oldList.Count() == 0)
            {
                ContactData contact = ContactData.GetAll().First();
                app.Contact.AddContactToGroup(contact, group);
            }

            ContactData contactToRemove = group.GetContacts().First();

            //actions
            app.Contact.RemoveContactFromGroup(contactToRemove, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Remove(contactToRemove);
            newList.Sort();
            oldList.Sort();
            Assert.AreEqual(oldList, newList);
        }
    }
}
