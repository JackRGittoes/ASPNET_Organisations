using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrganisationsDetail.Models
{
    public class Organisation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Organisation Name")]
        public string OrgName { get; set; }

        [Required]
        [Display(Name = "Organisation Code")]
        public string OrgCode { get; set; }

        public string IsHQ { get; set; }

    }
}
