using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectFlow.Models.Business_Models
{
    public class TaskStatus
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
