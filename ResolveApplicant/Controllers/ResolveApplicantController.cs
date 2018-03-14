using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResolveApplicant.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace ResolveApplicant.Controllers
{
    [Route("api/ResolveApplicant")]
    public class ResolveApplicantController : Controller
    {
        private DatabaseContext _context;

        public ResolveApplicantController(DatabaseContext context)
        {
            _context = context;
        }

        // GET api/ResolveApplicant
        [HttpGet]
        public string Get()
        {
            string strResolveApplicant = "Resolve an Applicant and provide the latest status.";
            return strResolveApplicant;

        }

        // POST api/ResolveApplicant
        [HttpPost]
        public string ResolveNewApplicant([FromBody]Applicant applicant)
        {
            string strReturn = string.Empty;
            string strUniqueCode = applicant.appUniqueCode;
            string strStatus = applicant.appStatus;

            strReturn = Resolve(strUniqueCode, strStatus);
            return strReturn;
        }

        private string Resolve(string strUniqueCode, string strStatus)
        {
            string strReturn = string.Empty;
            using (_context)
            {
                var thisApplicant = _context.Applicants.FirstOrDefault(a => a.appUniqueCode == strUniqueCode);
                if (thisApplicant != null)
                {
                    thisApplicant.appStatus = strStatus;
                    _context.Applicants.Update(thisApplicant);
                    _context.SaveChanges();
                }

                strReturn = "Updated";
                return strReturn;
            }
        }
    }

}
