using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kontrahenci.Controllers
{
    public class HomeController:Controller
    {
        public ViewResult Index() => View();
    }
}
