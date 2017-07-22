using MemoryGame.Helpers;
using MemoryGame.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace MemoryGame.Controllers
{
    public class GameController : Controller
    {

        private readonly IIconHelper _iconHelper;
        private readonly IIconPostHelper _iconPostHelper;
        private readonly INumbersHelper _numbersHelper;

        public GameController(IIconHelper iconHelper, INumbersHelper numbersHelper, IIconPostHelper iconPostHelper)
        {
            _iconHelper = iconHelper;
            _numbersHelper = numbersHelper;
            _iconPostHelper = iconPostHelper;
        }

        public IActionResult Menu()
        {
            return View();
        }

        public IActionResult Numbers()
        {
            var level = 0;

            var currentLevel = HttpContext.Session.GetString("Level");
            if (string.IsNullOrEmpty(currentLevel))
            {
                HttpContext.Session.SetString("Level", "1");
                level = 1;
            }
            else
            {
                level = Convert.ToInt32(currentLevel);
            }

            var model = _numbersHelper.GetRandomNumberFromRange(level);

            HttpContext.Session.SetString("Number", model.ToString());

            return View(model);
        }


        [HttpPost]
        public HttpStatusCode NumbersPost(int answer)
        {
            var number = Convert.ToInt32(HttpContext.Session.GetString("Number"));

            if (answer == number)
            {
                var currentLevel = Convert.ToInt32(HttpContext.Session.GetString("Level"));
                currentLevel++;
                HttpContext.Session.SetString("Level", currentLevel.ToString());

            }

            return HttpStatusCode.OK;
        }


        public IActionResult Icons()
        {

            var iconlevel = 0;

            var currentLevel = HttpContext.Session.GetString("IconLevel");
            if (string.IsNullOrEmpty(currentLevel))
            {
                HttpContext.Session.SetString("IconLevel", "1");
                iconlevel = 1;
            }
            else
            {
                iconlevel = Convert.ToInt32(currentLevel);
            }

            int numberOfAnswers = iconlevel + 3;
            int numberOfIcons = numberOfAnswers * 2;
            
            IconViewModel model = new IconViewModel {AllIcons = _iconHelper.GetIcons(numberOfIcons) };

            model.InitIcons = model.AllIcons.Take(numberOfAnswers).ToList();

            var str = JsonConvert.SerializeObject(model.InitIcons);
            HttpContext.Session.SetString("Answers", str);

            var rnd = new Random();

            model.AllIcons = model.AllIcons.OrderBy(item => rnd.Next()).ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult IconsPost(List<string> values)
        {

            var str = HttpContext.Session.GetString("Answers");
            var answers = JsonConvert.DeserializeObject<List<string>>(str);

            var userAnswers = _iconPostHelper.FormatIconText(values);

            var result = answers.OrderBy(i => i).SequenceEqual(userAnswers.OrderBy(i => i));

            if (result)
            {
                var currentLevel = Convert.ToInt32(HttpContext.Session.GetString("IconLevel"));
                currentLevel++;
                HttpContext.Session.SetString("IconLevel", currentLevel.ToString());
            }

            return RedirectToAction("Icons");
        }
    }
}