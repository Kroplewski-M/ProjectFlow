using Microsoft.AspNetCore.Mvc;
using ProjectFlow.Data;

namespace ProjectFlow.Web.Controllers
{
    public class WorkspaceController : Controller
    {
        private readonly ApplicationDbContext _db;

        public WorkspaceController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult MyWorkspace()
        {
            
            return View();
        }


        #region API Calls

        [HttpDelete]
        public IActionResult DeleteWorkspace(int id)
        {
            try
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return Json(new { success = false, message = "Error while deleting workspace" });
                }
                var workspace = _db.Workspaces.Where(u => u.UserId == userId).FirstOrDefault(u => u.Id == id);
                if (workspace == null)
                {
                    return Json(new { success = false, message = "Error while deleting workspace" });
                }
                _db.Workspaces.Remove(workspace);
                _db.SaveChanges();
                return Json(new { success = true, message = "Workspace deleted" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error while deleting workspace: {ex.Message}" });
            }
        }
        [HttpDelete]
        public IActionResult remove(int id)
        {
            var workspace = _db.Workspaces.FirstOrDefault(u => u.Id == id);
            if (workspace != null)
            {
                _db.Workspaces.Remove(workspace);
                _db.SaveChanges();
                return Json(new { success = true, message = "Removed Workspace: "});
            }
            return Json(new { success = false, message = "Error: while removing Workspace " });

        }
        #endregion
    }
}
