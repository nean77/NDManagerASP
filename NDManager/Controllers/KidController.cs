using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using NDManager.Data;
using NDManager.Data.Models;
using System.Threading.Tasks;

namespace NDManager.Controllers
{
    public class KidController : Controller
    {
        private readonly ILogger<KidController> _logger;
        private readonly INdmRepository<Kid> _repository;

        public KidController(ILogger<KidController> logger, INdmRepository<Kid> repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var groups = await _repository.GetAllActiveGroupsAsync();
            ViewBag.Groups = new SelectList(groups, "Id", "Name");
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> List(Group group)
        {

            var list = await _repository.GetAllKidsByGroupAsync(group.Id);

            Group g = await _repository.GetGroupByIdAsync(group.Id);
            if (g.Name != null)
            {
                ViewBag.GroupName = g.Name;
            }

            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int? id = 0)
        {
            var groups = await _repository.GetAllActiveGroupsAsync();
            ViewBag.Groups = groups;
            if (id == 0) return View(new Kid());
            return View(_repository.GetById(id));
        }
        [HttpPost]
        public async Task<IActionResult> AddOrEdit(int? id, Kid kid)
        {
            if (id != kid.Id && id != 0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (id == kid.Id && id == 0)
                {
                    await _repository.InsertAsync(kid);
                }
                else
                    await _repository.UpdateAsync(kid);

                return RedirectToAction("List", new { id = kid.GroupId });
            }
            return View(kid);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var kid = await _repository.GetKidByIdAsync(id);
            ViewBag.ReturnUrl = "/Kid/List?Id=" + kid.GroupId;
            if (kid.Id == 0)
            {
                return NotFound();
            }

            return View(kid);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kid = _repository.GetById(id);
            var groupId = kid.GroupId;
            await _repository.DeleteAsync(kid);

            return RedirectToAction("List", new { id=groupId});
        }
        
    }
}