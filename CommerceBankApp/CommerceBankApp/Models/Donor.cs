using System.ComponentModel.DataAnnotations.Schema;

namespace CommerceBankApp.Models
{
    public class Donor
    {
        public int donorID { get; set; }
        public string billingAddress { get; set; }
        public string homeAddress { get; set; }

        [ForeignKey("Account")]
        public int accountID { get; set; }
        public virtual Account Account { get; set; }

        [ForeignKey("DonorInfo")]
        public int donorInfoID { get; set; }
        public virtual DonorInfo DonorInfo { get; set; }

        [ForeignKey("Payment")]
        public int? paymentID { get; set; }
        public virtual Payment Payment { get; set; }
        public Donor()
        {

        }
    }
}
