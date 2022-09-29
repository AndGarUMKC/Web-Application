using MessagePack;
using System.ComponentModel;

namespace CommerceBankApp.Models
{
    public class DonationType
    {
        public int donationTypeID { get; set; }

        [DisplayName("Type of Donation")]
        public string donationTypeName { get; set; }

        // FOREIGN KEYS
        public virtual ICollection<Organization> Organization { get; set; }

        public DonationType()
        {

        }
    }
}
