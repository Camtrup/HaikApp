﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Haik.Pages
{
  public class RegisterModel : PageModel
  {
    private readonly ILogger<RegisterModel> _logger;

    public RegisterModel(ILogger<RegisterModel> logger)
    {
      _logger = logger;
    }

    public void OnPost()
    {
      var username = Request.Form["username"];
      Console.WriteLine("Hello, World");
    }
  }
}
