using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectFlow.Data;
using ProjectFlow.Models.Business_Models;
using ProjectFlow.Models.View_Models;

namespace ProjectFlow.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _db;
        public DashboardController(ApplicationDbContext db)
        {
            _db = db;
        }
        [Authorize]
        public IActionResult Index()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var Dashboard = new DashboardViewModel();
            Dashboard.CreateWorkspace = new CreateWorkspaceViewModel();
            var workspaces = _db.Workspaces.Where(u=>u.UserId == userId).ToList();
            Dashboard.Workspaces = workspaces;

            return View(Dashboard);
        }
        [HttpPost]
        public IActionResult Index(CreateWorkspaceViewModel workspace)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (!string.IsNullOrEmpty(userId))
                {
                    var workspaceAdd = new Workspace
                    {
                        Name = workspace.Name,
                        Color = workspace.Color,
                        UserId = userId,
                    };
                    _db.Workspaces.Add(workspaceAdd);
                    _db.SaveChanges();
                }
                else
                {

                }

            }
            return RedirectToAction("Index");
        }
    }
}
