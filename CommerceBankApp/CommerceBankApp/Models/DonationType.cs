using MessagePack;
using System.ComponentModel;

namespace CommerceBankApp.Models
{
    public class DonationType
    {
        public int DonationTypeID { get; set; }

        [DisplayName("Type of Donation")]
        public string DonationTypeName { get; set; }

        // FOREIGN KEYS
        public virtual ICollection<Organization> Organization { get; set; }

        public DonationType()
        {

        }
    }
}
