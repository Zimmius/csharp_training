using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void TestAddingContactToGroup()
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
            List<ContactData> groupContacts = group.GetContacts();

            //contactcs in group check
            if (ContactData.GetAll().Except(groupContacts).Count() == 0)
            {
                ContactData contact = ContactData.GetAll().First();
                app.Contact.RemoveContactFromGroup(contact, group);
            }

            List<ContactData> oldList = group.GetContacts();
            ContactData contactToAdd = ContactData.GetAll().Except(oldList).First();

            //actions
            app.Contact.AddContactToGroup(contactToAdd, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contactToAdd);
            newList.Sort();
            oldList.Sort();
            Assert.AreEqual(oldList, newList);
        }
    }
}
