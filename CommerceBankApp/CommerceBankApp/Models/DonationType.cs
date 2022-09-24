using MessagePack;

namespace CommerceBankApp.Models
{
    public class DonationType
    {
        public int donationTypeID { get; set; }
        public string donationTypeName { get; set; }
        public string donationTypeDescription { get; set; }
        public DonationType()
        {

        }
    }
}
