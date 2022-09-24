using System.ComponentModel.DataAnnotations.Schema;

namespace CommerceBankApp.Models
{
    public class DonorInfo
    {
        public int donorInfoID { get; set; }
        public string cardNumber { get; set; }
        public string cardExpiration { get; set; }
        public string cvcNumber { get; set; }
        public string bankRoutingNumber { get; set; }
        public string bankAccountNumber { get; set; }

        [ForeignKey("Donor")]
        public int donorID { get; set; }
        public virtual Donor Donor { get; set; }
        public DonorInfo()
        {

        }
    }
}
