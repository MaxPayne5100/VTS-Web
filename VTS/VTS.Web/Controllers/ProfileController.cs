﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace VTS.Web.Controllers
{
    public class ProfileController : Controller
    {
        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }
    }
}