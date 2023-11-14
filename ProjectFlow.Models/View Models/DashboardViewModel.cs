﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectFlow.Models.Business_Models;

namespace ProjectFlow.Models.View_Models
{
    public class DashboardViewModel
    {
        public IEnumerable<Workspace>Workspaces { get; set; }
        public CreateWorkspaceViewModel CreateWorkspace { get; set; }
    }
}
