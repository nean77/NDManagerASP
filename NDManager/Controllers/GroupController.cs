using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NDManager.Data;
using NDManager.Data.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NDManager.Controllers
{
    public class GroupController : Controller
    {
        private readonly ILogger<GroupController> _logger;
        private readonly INdmRepository<Group> _repository;

        public GroupController(ILogger<GroupController> logger, INdmRepository<Group> repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var ret = await _repository.GetAllGroupsAsync();

            return View(ret);
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int? id = 0)
        {
            var teachers = await _repository.GetAllTeachersAsync();
            ViewBag.Teachers = teachers;
            if (id == 0) return View(new Group());
            return View(_repository.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEdit(int? id, Group group)
        {
            if (id != group.Id && id != 0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (id == group.Id && id == 0)
                {
                    await _repository.InsertAsync(group);
                }
                else
                    await _repository.UpdateAsync(group);

                return RedirectToAction("Index", "Group");
            }
            return View(group);
        }
    }
}
