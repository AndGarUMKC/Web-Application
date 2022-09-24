using MessagePack;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommerceBankApp.Models
{
    public class Organization
    {
        public int organizationID { get; set; }
        public string organizationName { get; set; }
        public float donationGoal { get; set; }
        public float currentDonated { get; set; }
        public string organizationDescription { get; set; }

        [ForeignKey("DonationType")]
        public int donationTypeID { get; set; }
        public virtual DonationType DonationType { get; set; }
        public Organization()
        {

        }
    }
}
