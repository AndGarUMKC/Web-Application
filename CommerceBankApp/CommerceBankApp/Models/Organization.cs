using CommerceBankApp.Areas.Identity.Data;
using MessagePack;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommerceBankApp.Models
{
    public class Organization
    {
        public int OrganizationID { get; set; }

        [Required(ErrorMessage = "Please enter the name of the fundraiser")]
        [DisplayName("Name of Fundraiser")]
        public string OrganizationName { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Please enter the donation goal to reach")]
        [DisplayName("Goal Amount")]
        public float DonationGoal { get; set; }

        [Required(ErrorMessage = "Please enter the description of the fundraiser")]
        [DisplayName("Description")]
        public string OrganizationDescription { get; set; }

        [Url]
        [DisplayName("URL of Image")]
        [Required(ErrorMessage = "Please enter a URL of an image")]
        public string ImageUrl { get; set; }

        // FOREIGN KEYS
        [ForeignKey("ApplicationUserId")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; } // MUST USE QUESTION MARK FOR APPLICATION USER REFERENCE

        public virtual ICollection<DonationType>? DonationType { get; set; }

        public virtual ICollection<Payment>? Payment { get; set; }

        public Organization()
        {

        }
    }
}
