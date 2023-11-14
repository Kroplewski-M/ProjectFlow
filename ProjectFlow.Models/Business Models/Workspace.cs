using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectFlow.Models.Business_Models
{
    public class Workspace
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Color { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
