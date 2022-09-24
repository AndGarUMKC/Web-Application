using System.ComponentModel.DataAnnotations.Schema;

namespace CommerceBankApp.Models
{
    public class Account
    {
        public int accountID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }

        [ForeignKey("Donor")]
        public int? donorID { get; set; }
        public virtual Donor Donor { get; set; }
        public Account()
        {
               
        }       
    }
}
