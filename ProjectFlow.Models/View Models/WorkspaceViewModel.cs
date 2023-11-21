using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectFlow.Models.Business_Models;
using Task = ProjectFlow.Models.Business_Models.Task;

namespace ProjectFlow.Models.View_Models
{
    public class WorkspaceViewModel
    {
        public Workspace Workspace { get; set; }
        public IEnumerable<Task> Tasks { get; set; }
        public EditWorkspaceViewModel EditWorkspace { get; set; }
    }
}
