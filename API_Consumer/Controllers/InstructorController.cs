using API_Consumer.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_Consumer.Controllers
{
    public class InstructorController : Controller
    {
        private readonly HttpClient _client;

        public InstructorController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("api");
        }

        // GET: /Instructor
        public async Task<IActionResult> Index()
        {
            var instructors = await _client.GetFromJsonAsync<List<InstructorDTO>>("instructor");
            return View(instructors);
        }

        // GET: /Instructor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Instructor/Create
        [HttpPost]
        public async Task<IActionResult> Create(InstructorCreateDTO dto)
        {
            var response = await _client.PostAsJsonAsync("instructor", dto);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Failed to create instructor.");
            return View(dto);
        }

        // GET: /Instructor/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var instructor = await _client.GetFromJsonAsync<InstructorDTO>($"instructor/{id}");
            if (instructor == null) return NotFound();

            ViewBag.Id = instructor.Id;

            var dto = new InstructorCreateDTO
            {
                Name = instructor.Name
                // CourseIds can be added here if needed
            };

            return View(dto);
        }

        // POST: /Instructor/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, InstructorCreateDTO dto)
        {
            var response = await _client.PutAsJsonAsync($"instructor/{id}", dto);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Failed to update instructor.");
            return View(dto);
        }

        // GET: /Instructor/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var instructor = await _client.GetFromJsonAsync<InstructorDTO>($"instructor/{id}");
            if (instructor == null) return NotFound();

            ViewBag.InstructorId = id;
            return View(instructor);
        }

        // POST: /Instructor/DeleteConfirmed
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _client.DeleteAsync($"instructor/{id}");
            return RedirectToAction("Index");
        }
    }
}
