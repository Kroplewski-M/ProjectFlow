using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ProjectFlow.Models.Business_Models;

namespace ProjectFlow.Models.View_Models
{
    public class DashboardViewModel
    {
        [ValidateNever]
        public IEnumerable<Workspace>Workspaces { get; set; }
        public CreateWorkspaceViewModel CreateWorkspace { get; set; }
    }
}
