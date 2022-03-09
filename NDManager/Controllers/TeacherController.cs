using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NDManager.Data;
using NDManager.Data.Models;
using System.Threading.Tasks;

namespace NDManager.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ILogger<TeacherController> _logger;
        private readonly INdmRepository<Teacher> _repository;
        
        public TeacherController(ILogger<TeacherController> logger, INdmRepository<Teacher> repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var ret = await _repository.GetAllTeachersAsync();

            return View(ret);
        }
        [HttpGet]
        public IActionResult AddOrEdit(int? id = 0)
        {
            if (id == 0) return View(new Teacher());
            return View(_repository.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEdit(int? id, Teacher teacher)
        {
            if (id != teacher.Id && id != 0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (id == teacher.Id && id == 0)
                {
                    await _repository.InsertAsync(teacher);
                }
                else
                    await _repository.UpdateAsync(teacher);

                return RedirectToAction("Index", "Teacher");
            }
            return View(teacher);
        }
        public IActionResult Delete(int id)
        {
            var teacher = _repository.GetById(id);
            if (teacher.Id == 0)
            {
                return NotFound();
            }

            return View(teacher);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacher = _repository.GetById(id);
            await _repository.DeleteAsync(teacher);

            return RedirectToAction("Index");
        }
    }
}
