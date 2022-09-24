using System.ComponentModel.DataAnnotations.Schema;

namespace CommerceBankApp.Models
{
    public class Payment
    {
        public int paymentID { get; set; }
        public float donatedAmount { get; set; }

        [ForeignKey("Donor")]
        public int donorID { get; set; }
        public virtual Donor Donor { get; set; }

        [ForeignKey("Organization")]
        public int organizationID { get; set; }
        public virtual Organization Organization { get; set; }
        public Payment()
        {

        }
    }
}
