using ExamAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ExamManagement.Controllers
{
    public class StudentController : Controller
    {
        private string URLAPi = @"http://localhost:5228/api/Students";
        private HttpClient httpClient= new HttpClient();

        // GET: StudentController
        public async Task<ActionResult> Index()
        {
            var ResponseVal = await httpClient.GetAsync(URLAPi);
            var response = await ResponseVal.Content.ReadAsStringAsync();
            var stud = JsonConvert.DeserializeObject<List<Student>>(response);
            return View(stud);
        }

        // GET: StudentController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            //var ResponseVal = await httpClient.GetAsync(URLAPi+"/" + id.ToString());
            //var response = await ResponseVal.Content.ReadAsStringAsync();
            //var stud = JsonConvert.DeserializeObject<Student>(response);
            var student = await GetstudById(id);
            return View(student);
        }


        private async Task<Student> GetstudById(int id)
        {
            var ResponseVal = await httpClient.GetAsync(URLAPi + "/" + id.ToString());
            var response = await ResponseVal.Content.ReadAsStringAsync();
            var stud = JsonConvert.DeserializeObject<Student>(response);
            return stud;
        }

        private async Task<IEnumerable<Student>> GetStudByName(string name)
        {
            var ResponseVal = await httpClient.GetAsync(URLAPi + "/Name" + name.ToString());
            var response = await ResponseVal.Content.ReadAsStringAsync();
            var stud = JsonConvert.DeserializeObject<List<Student>>(response);
            return stud;
        }
        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Student student)
        {
            try
            {
                //It return obj which will return in string
               var objdata = JsonConvert.SerializeObject(student);
                var ResponseVal = await httpClient.PostAsync(URLAPi,new StringContent(objdata, Encoding.UTF8,"application/json"));
               
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
         // GET: StudentController/Search
        public ActionResult Search()
        {
            return View();
        }

        // POST: StudentController/Search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Search(Student student)
        {   
            try
            {
                var stud= await GetStudByName(student.Name);
                return View(stud);
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return View(await GetstudById(id));
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Student student)
        {
            try
            {
                var objdata = JsonConvert.SerializeObject(student);
                var ResponseVal = await httpClient.PutAsync(URLAPi + "/" + id.ToString(), new StringContent(objdata, Encoding.UTF8));
                Console.WriteLine(ResponseVal);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return View(await GetstudById(id));
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Student student)
        {
            try
            {
                var objdata = JsonConvert.SerializeObject(student);
                var ResponseVal = await httpClient.DeleteAsync(URLAPi + "/" + id.ToString());
                Console.WriteLine(ResponseVal);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
