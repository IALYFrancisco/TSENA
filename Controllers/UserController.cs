using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TSENA.Models;

namespace TSENA.Controllers {
    public class UserController : Controller {
        private readonly UserContext _context;

        public UserController(UserContext context){
            _context = context;
        }
    }
}