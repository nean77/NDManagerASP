using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NDManager.Models;
using System.Diagnostics;
using NDManager.Data;
using NDManager.Data.Models;

namespace NDManager.Controllers
{
    public class GroupController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INdmRepository<Group> _repository;

        public GroupController(ILogger<HomeController> logger, INdmRepository<Group> repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public IActionResult Index()
        {
            var ret = _repository.GetAllGroups();

            return View(ret);
        }

        [HttpGet]
        public IActionResult AddOrEdit(int id = 0)
        {
            return View(new Group());
        }

        [HttpPost]
        public IActionResult AddOrEdit()
        {
            return View();
        }
    }
}
