using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommerceBankApp.Models
{
    public class Account
    {
        public int accountID { get; set; }
        [DisplayName("First Name")]
        public string firstName { get; set; }
        [DisplayName("Last Name")]
        public string lastName { get; set; }
        [DisplayName("User Name")]
        public string username { get; set; }
        [DisplayName("Password")]
        public string password { get; set; }
        [DisplayName("Email Address")]
        public string email { get; set; }
        [DisplayName("Phone Number")]
        public string phoneNumber { get; set; }

        [ForeignKey("Donor")]
        public int? donorID { get; set; }
        public virtual Donor Donor { get; set; }
        public Account()
        {
               
        }       
    }
}
