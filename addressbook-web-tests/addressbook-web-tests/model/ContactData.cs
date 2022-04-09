using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string detailedInformation;
        private string emails;

        public ContactData()
        {
        }

        public ContactData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Firstname == other.Firstname & Lastname == other.Lastname;
        }

        public override int GetHashCode()
        {
            return Firstname.GetHashCode() & Lastname.GetHashCode();
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            if (Lastname.CompareTo(other.Lastname) == 0)
            {
                return Firstname.CompareTo(other.Firstname);
            }
            return Lastname.CompareTo(other.Lastname);
        }

        public override string ToString()
        {
            return Firstname + " " + Lastname;
        }

        [Column(Name = "id"), PrimaryKey]
        public string Id { get; set; }

        [Column(Name = "firstname")]
        public string Firstname { get; set; }

        [Column(Name = "lastname")]
        public string Lastname { get; set; }

        public string Address { get; set; }

        public string HomePhone { get; set; }

        public string MobilePhone { get; set; }

        public string WorkPhone { get; set; }

        public string Email { get; set; }

        public string Email2 { get; set; }

        public string Email3 { get; set; }

        [Column(Name="deprecated")]
        public string Deprecated { get; set; }

        public string AllPhones { 
            get 
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUpPhones(HomePhone) + CleanUpPhones(MobilePhone) + CleanUpPhones(WorkPhone)).Trim();
                }
            } 
            set 
            {
                allPhones = value;
            } 
        }

        public string Emails
        {
            get
            {
                if (emails != null)
                {
                    return emails;
                }
                else
                {
                    if (Email != "")
                    {
                        Email = Email + "\r\n";
                    }
                    if (Email2 != "")
                    {
                        Email2 = Email2 + "\r\n";
                    }
                    if (Email3 != "")
                    {
                        Email3 = Email3 + "\r\n";
                    }
                    return (Email + Email2 + Email3).Trim();
                }
            }
            set
            {
                emails = value;
            }
        }

        public string DetailedInformation
        {
            get
            {
                if(detailedInformation != null)
                {
                    return detailedInformation;
                }
                else
                {
                    if (Lastname != "")
                    {
                        Lastname = " " + Lastname;
                    }
                    return (Firstname + Lastname + "\r\n" + CleanUpAddress(Address) + DetaliedPhones(HomePhone, MobilePhone, WorkPhone) + "\r\n" + Emails).Trim();
                }
            }
            set
            {
                detailedInformation = value;
            }
        }

        private string CleanUpPhones(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") + "\r\n";
            //return Regex.Replace(phone, "[ -()]", "") + "\r\n";
        }

        private string CleanUpAddress(string address)
        {
            if (address == null || address == "")
            {
                return "";
            }
            return  address.Replace(" ", "") + "\r\n";
        }

        private string DetaliedPhones(string homePhone, string mobilePhone, string workPhone)
        {
            if (homePhone == "" & mobilePhone == "" & workPhone == "")
            {
                return "";
            }

            if (homePhone != "")
            {
                homePhone = "H: " + homePhone + "\r\n";
            }
            if (mobilePhone != "")
            {
                mobilePhone = "M: " + mobilePhone + "\r\n";
            }
            if (workPhone != "")
            {
                workPhone = "W: " + workPhone + "\r\n";
            }

            return "\r\n" + homePhone + mobilePhone + workPhone;
        }

        public static List<ContactData> GetAll()
        {
            using (AddressbookDB db = new AddressbookDB())
            {
                return (from c in db.Contacts.Where(x => x.Deprecated == "0000-00-00 00:00:00") select c).ToList();
            }
        }
    }
}
