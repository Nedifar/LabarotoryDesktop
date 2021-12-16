using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apiLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataControll : ControllerBase
    {
        Models.context context;
        public DataControll(Models.context db)
        {
            context = db;
        }
        [HttpGet]
        [Route("getnews")]
        public async Task<ActionResult<Models.New>> GetNews()
        {
            List<Models.New> list = context.News.ToList();
            return new ObjectResult(list);
        }
        [HttpPost]
        [Route("postpacient")]
        public async Task<ActionResult<Models.Patient>> Autentithication(Models.Patient patient)
        {
            Models.Patient pat = context.Patients.Where(b => b.Login == patient.Login && b.Password == patient.Password).FirstOrDefault();
            if (pat == null)
                return new ObjectResult(null);
            else
                return new ObjectResult(pat);
        }
        [HttpGet]
        [Route("getservices")]
        public async Task<ActionResult<Models.Service>> GetServices()
        {
            List<Models.Service> services = context.Services.ToList();
            return new ObjectResult(services);
        }
        [HttpPost]
        [Route("addpatient")]
        public async Task<IActionResult> AddPatient(Models.Patient patient)
        {
            context.Patients.Add(patient);
            context.SaveChanges();
            return Ok();
        }
        [HttpPost]
        [Route("editpatients")]
        public async Task<IActionResult> EditPacient(Models.Patient patient)
        {
            Models.Patient patientold = context.Patients.Where(p => p.Id_Patient == patient.Id_Patient).FirstOrDefault();
            patientold.Email = patient.Email;
            patientold.Telephone = patient.Telephone;
            patientold.Password = patient.Password;
            context.SaveChanges();
            return Ok();
        }
    }

    
}
