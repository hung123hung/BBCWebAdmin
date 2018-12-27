using BBCWebAPI.Data;
using BBCWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBCWebAPI.Controllers.UI
{
    public class BaseController:Controller
    {
        protected void BasePage(DataContext dataContext)
        {
            List<Topic> listTopics = new List<Topic>();
            listTopics = dataContext.Topics.ToList();
            //send list Topic to View HomePage
            ViewBag.listTopics = listTopics;
        }
    }
}
