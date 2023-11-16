using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectFlow.Models.Business_Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set;}

        //FOREIGN KEYS
        public int TaskStatusId { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public int WorkspaceId { get; set; }
        public Workspace Workspace { get; set; }
    }
}
