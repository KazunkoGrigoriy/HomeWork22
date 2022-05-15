using HomeWork22.Interface;
using HomeWork22.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeWork22.Controllers
{
    public class RecordController : Controller
    {
        private readonly IRecord Record;

        public RecordController(IRecord record)
        {
            this.Record = record;
        }

        public IActionResult Index()
        {
            return View(Record.GetData());
        }

        [HttpGet]
        [Authorize]
        public IActionResult RecordBook()
        {
            return View(Record.GetData());
        }

        [HttpGet]
        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult RecordPage()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult GetDataFromViewFields(string lastName, string firstName, string middleName, string phone, string adress, string info)
        {
            var record = new Record()
            {
                LastName = lastName,
                FirstName = firstName,
                MiddleName = middleName,
                Phone = phone,
                Info = info
            };
            Record.Add(record);
            return Redirect("~/");
        }
    }
}
