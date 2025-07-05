using API_Consumer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;

namespace API_Consumer.Controllers
{
    public class CourseController : Controller
    {
        private readonly HttpClient _client;

        public CourseController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("api");
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("JWToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetFromJsonAsync<List<CourseDTO>>("course");
            return View(response);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateInstructorsAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateDTO dto)
        {
            var token = HttpContext.Session.GetString("JWToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var result = await _client.PostAsJsonAsync("course", dto);
            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");
            await PopulateInstructorsAsync();
            return View(dto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var token = HttpContext.Session.GetString("JWToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var course = await _client.GetFromJsonAsync<CourseDTO>($"course/{id}");
            if (course == null)
                return NotFound();

            var model = new CourseCreateDTO
            {
                Name = course.Name,
                InstructorId = await GetInstructorIdByName(course.Instructor)
            };

            await PopulateInstructorsAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CourseCreateDTO dto)
        {
            var token = HttpContext.Session.GetString("JWToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.PutAsJsonAsync($"course/{id}", dto);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            await PopulateInstructorsAsync();
            return View(dto);
        }

        private async Task<int> GetInstructorIdByName(string name)
        {

            var instructors = await _client.GetFromJsonAsync<List<InstructorDTO>>("instructor");
            var match = instructors.FirstOrDefault(i => i.Name == name);
            return match != null ? match.Id : 0;
        }


        //public async Task<IActionResult> Delete(int id)
        //{
        //    var course = await _client.GetFromJsonAsync<CourseDTO>($"course/{id}");
        //    if (course == null)
        //        return NotFound();

        //    ViewBag.CourseId = id;
        //    return View(course);
        //}

        //[HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var token = HttpContext.Session.GetString("JWToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.DeleteAsync($"course/{id}");
            return RedirectToAction("Index");
        }



        private async Task PopulateInstructorsAsync()
        {
            var instructors = await _client.GetFromJsonAsync<List<InstructorDTO>>("instructor");
            ViewBag.Instructors = new SelectList(instructors, "Id", "Name");
        }

    }
}
