using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InstitutoEducativo.Models
{
    public class Role : IdentityRole<Guid>
    {
        //[Display(Name="nombreRole")]
        //public override string Name { get; set; }
        


    }
}
