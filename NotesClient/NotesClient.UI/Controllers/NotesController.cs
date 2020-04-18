using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotesClient.ServiceAccess;
using NotesClient.ServiceAccess.Models;

namespace NotesClient.UI.Controllers
{
    public class NotesController : Controller
    {
        private readonly INotesService _notesService;

        public NotesController(INotesService notesService)
        {
            _notesService = notesService;
        }

        // GET: notes
        public async Task<ActionResult> Index()
        {
            return View(await _notesService.GetAllAsync());
        }

        // GET: notes/create
        public ActionResult Create()
        {
            return View();
        }

        // POST: notes/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Note note)
        {
            try
            {
                await _notesService.AddAsync(note);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // should provide better error feedback to user
                return View(note);
            }
        }
    }
}
