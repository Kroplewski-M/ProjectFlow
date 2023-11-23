using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectFlow.Data;
using ProjectFlow.Models.View_Models;
using ProjectFlow.Models.Business_Models;
using System.Threading.Tasks;

namespace ProjectFlow.Web.Controllers
{
    public class WorkspaceController : Controller
    {
        private readonly ApplicationDbContext _db;

        public WorkspaceController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult MyWorkspace(int id)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var board = _db.Workspaces.FirstOrDefault(u => u.Id == id);
            
            if (board != null && board.UserId == userId)
            {
                var workspace = new WorkspaceViewModel
                {
                    Workspace = board,
                    Tasks = _db.Tasks.Where(u => u.WorkspaceId == board.Id).Select(t=> new TaskViewModel
                    {
                        Id = t.Id,
                        Title = t.Title,
                        CreatedDate = t.CreatedDate,
                        TaskStatusId = t.TaskStatusId,
                        IsEditing = false
                    }).ToList(),
                    EditWorkspace= new EditWorkspaceViewModel
                    {
                        Id = board.Id,
                        Name = board.Name,
                        Color = board.backgroundColor,
                    }
                };

                return View(workspace);
            }
            TempData["error"] = "You dont have access to this dashboard!";
            return RedirectToAction("Index","Dashboard");
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
        [HttpPost]
        public IActionResult UpdateWorkspace(EditWorkspaceViewModel editWorkspace)
        {
            try
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    TempData["error"] = "Error while updating workspace!";
                    return Json(new { success = false, message = "Error while updating workspace" });
                }
                var workspace = _db.Workspaces.Where(u => u.UserId == userId).FirstOrDefault(u => u.Id == editWorkspace.Id);
                if (workspace == null)
                {
                    TempData["error"] = "Error while updating workspace!";
                    return Json(new { success = false, message = "Error while updating workspace" });
                }

                if (editWorkspace.Name != workspace.Name)
                {
                    workspace.Name = editWorkspace.Name;
                }
                if (editWorkspace.Color != workspace.backgroundColor)
                {
                    workspace.backgroundColor = editWorkspace.Color;
                }

                _db.SaveChanges();
                return Json(new { success = true, message = "workspace updated" });

            }
            catch (Exception ex)
            {
                TempData["error"] = "Error while updating workspace!";
                return Json(new { success = false, message = $"Error while updating workspace: {ex.Message}" });
            }
        }

        [HttpPost]
        public IActionResult AddTaskToWorkspace(int workspaceId,int taskStatusId,string taskValue)
        {
            try
            {
                var addTask = new Models.Business_Models.Task
                {
                    Title = taskValue,
                    Description = "",
                    CreatedDate = DateTime.Now,
                    TaskStatusId = taskStatusId,
                    WorkspaceId = workspaceId,
                };
                _db.Tasks.Add(addTask);
                _db.SaveChanges();
                return Json(new { success = true, message = "task added" });
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error while added task!";
                return Json(new { success = false, message = $"Error while updating workspace: {ex.Message}" });
            }
        }
        [HttpDelete]
        public IActionResult DeleteTask(int id)
        {
            try
            {
                var task = _db.Tasks.FirstOrDefault(x => x.Id == id);
                if (task != null)
                {
                    _db.Tasks.Remove(task);
                    _db.SaveChanges();
                    return Json(new { success = true, message = "task added" });
                }
                    return Json(new { success = false, message = "task not deleted" });
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error while deleting task!";
                return Json(new { success = false, message = $"Error while updating workspace: {ex.Message}" });
            }
        }
        [HttpPost]
        public IActionResult MoveTask(int Id, int taskStatus)
        {
            try
            {
                var task = _db.Tasks.FirstOrDefault(x => x.Id == Id);
                if(task != null)
                {
                    task.TaskStatusId = taskStatus;
                    _db.SaveChanges();
                    return Json(new { success = true, message = "task moved" });
                }
                return Json(new { success = false, message = "task not moved" });

            }
            catch (Exception ex)
            {
                TempData["error"] = "Error while deleting task!";
                return Json(new { success = false, message = $"Error while updating workspace: {ex.Message}" });
            }
        }
        #endregion

    }
}
