using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CommerceBankApp.Models;
using Microsoft.AspNetCore.Identity;

namespace CommerceBankApp.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{

    // FOREIGN KEYS
    public virtual ICollection<Organization>? Organization { get; set; }

    public virtual ICollection<Payment>? Payment { get; set; }

}

