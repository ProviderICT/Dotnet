using ExamAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ExamManagement.Controllers
{
    public class MarksController : Controller
    {

        private string marksURL = "http://localhost:5228/api/Marks";
        private HttpClient httpClient= new HttpClient();

        // GET: MarksController
        public async Task<ActionResult> Index()
        {
            var ResponseVal = await httpClient.GetAsync(marksURL);
            var result = await ResponseVal.Content.ReadAsStringAsync();
            var marks = JsonConvert.DeserializeObject<List<Marks>>(result);
            return View(marks);
        }

        private async Task<Marks> GetMarksById(int id)
        {
            var ResponseVal = await httpClient.GetAsync(marksURL + "/" + id.ToString());
            var result = await ResponseVal.Content.ReadAsStringAsync();
            var marks = JsonConvert.DeserializeObject<Marks>(result);
            return marks;
        }

        // GET: MarksController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View(await GetMarksById(id));
        }

        // GET: MarksController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MarksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Marks marks)
        {
            try
            {
                var jsondata = JsonConvert.SerializeObject(marks);
                var ResponseVal = await httpClient.PostAsync(marksURL, new StringContent(jsondata, System.Text.Encoding.UTF8,"application/json"));
               
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MarksController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return View(await GetMarksById(id));
        }

        // POST: MarksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Marks marks)
        {
            try
            {
                var jsondata = JsonConvert.SerializeObject(marks);
                var ResponseVal = await httpClient.PutAsync(marksURL + "/" + id.ToString(), new StringContent(jsondata, System.Text.Encoding.UTF8));

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MarksController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return View(await GetMarksById(id));
        }

        // POST: MarksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Marks marks)
        {
            try
            {
                var jsondata = JsonConvert.SerializeObject(marks);
                var ResponseVal = await httpClient.DeleteAsync(marksURL +"/"+ id.ToString());

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
