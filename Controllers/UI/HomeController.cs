using BBCWebAPI.Data;
using BBCWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBCWebAPI.Functions;

namespace BBCWebAPI.Controllers.UI
{
    public class HomeController:BaseController
    {
        private DataContext dataContext;
        public HomeController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        [Route("/HomePage")]
        public IActionResult HomePage(string topicID,string topicName)
        {
            try
            {
                BasePage(dataContext);
                //send topicID,topicName to View HomePage 
                ViewBag.topicID = topicID;
                ViewBag.topicName = topicName;
                //show list lesson follow topic
                ShowListLesson(topicID);
                return View("Views/Pages/HomePage.cshtml");
            }
            catch(Exception ex)
            {

            }
            return null;
        }
        [Route("/HomePage/DetailLessonPage")]
        public IActionResult ToDetailLessonPage(string lessonID)
        {
            try
            {
                BasePage(dataContext);
                //show component of lesson item to DetailLessonPage
                ShowLessonItem(lessonID);
                return View("Views/Pages/Details/DetailLessonPage.cshtml");
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("HomePage");
        }
        [Route("/HomePage/EditLessonPage")]
        public IActionResult ToEditLessonPage(string topicID, string topicName, string lessonID)
        {
            try
            {
                BasePage(dataContext);
                //send lessonID to method UpdateLesson
                TempData["lessonID"] = lessonID;
                //show components of lesson item
                ShowLessonItem(lessonID);
                return View("Views/Pages/Edits/EditLessonPage.cshtml");
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("HomePage");
        }
        [Route("/HomePage/EditLessonPage/UpdateLesson")]
        public IActionResult UpdateLesson(string nameLesson, string year, string audioURLOnline, string audioURLDowload,
            string imageURL, string transcript, string actor, string summary, string vocabulary)
        {
            try
            {
                //receive lessonID from method ToEditLessonPage
                string lessonID = TempData["lessonID"].ToString();
                var update = dataContext.Lessons.Where(lesson => lesson.ID == lessonID).FirstOrDefault();
                if(update!=null)
                {
                    update.Name = nameLesson;
                    update.Year = Int32.Parse(year);
                    update.FileURLOnline = audioURLOnline;
                    update.FileURLDowload = audioURLDowload;
                    update.ImageURL = imageURL;
                    update.Transcript = transcript;
                    update.Actor = actor;
                    update.Sumary = summary;
                    update.Vocabulary = vocabulary;
                    update.UpdatedDate = (DateTime.Now).ToString();
                }
                
                dataContext.SaveChangesAsync();
                return RedirectToAction("HomePage");
            }
            catch(Exception ex)
            { }
            return RedirectToAction("ToEditLessonPage");
        }
        [Route("/HomePage/CreateLesson")]
        public IActionResult ToCreateLessonPage(string topicID, string topicName)
        {
            try
            {
                BasePage(dataContext);
                //sent topicID to method Insert Lesson
                TempData["topicID"] = topicID;
                //send name topic to View HomePage
                ViewBag.topicName = topicName;
                return View("Views/Pages/Creates/CreateLessonPage.cshtml");
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("HomePage");
        }
        [Route("/HomePage/CreateLessonPage/InsertLesson")]
        public IActionResult InsertLesson(string id,string name, string year, string audioURLOnline, string audioURLDowload,
            string imageURL, string transcript, string actor, string summary, string vocabulary, string createdDate)
        {
            try
            {
                var lesson = dataContext.Lessons.FirstOrDefault();
                if (lesson != null)
                {
                    lesson.ID = RandomObject.RandomString(20);
                    lesson.Name = name;                    
                    lesson.IDTP =TempData["topicID"].ToString();
                    lesson.Year = Int32.Parse(year);
                    lesson.FileURLOnline = audioURLOnline;
                    lesson.FileURLDowload = audioURLDowload;
                    lesson.ImageURL = imageURL;
                    lesson.Transcript = transcript;
                    lesson.Actor = actor;
                    lesson.Sumary = summary;
                    lesson.Vocabulary = vocabulary;
                    lesson.CreatedDate = (DateTime.Now).ToString();
                    lesson.UpdatedDate = null;
                }
                dataContext.Lessons.Add(lesson);
                dataContext.SaveChangesAsync();
                return RedirectToAction("HomePage");
            }
            catch (Exception ex)
            { }
            return RedirectToAction("ToCreateLessonPage");
        }
        [Route("/Home/DeleteLesson")]
        public IActionResult DeleteLesson(string lessonID)
        {
            try{
                var deleteLesson = dataContext.Lessons.SingleOrDefault(lesson => lesson.ID == lessonID);
                var deleteQuestion = dataContext.Questions.Where(question => question.LessonID == lessonID).ToList();
                if (deleteLesson != null)
                {                   
                    foreach(var question in deleteQuestion)
                    {
                        var deleteAnswer = dataContext.Answers.Where(answer => answer.QuestionID == question.QuestionID).ToList();
                        foreach(var answer in deleteAnswer)
                        {
                            dataContext.Answers.Remove(answer);
                            dataContext.SaveChanges();
                        }
                        dataContext.Questions.Remove(question);
                        dataContext.SaveChanges();
                    }
                    dataContext.Lessons.Remove(deleteLesson);
                    dataContext.SaveChanges();
                }
                return RedirectToAction("HomePage");
            }
            catch(Exception ex)
            {

            }
            return RedirectToAction("HomePage");
        }
               
        private void ShowListLesson(string topicID)
        {
            List<Lesson> listLessons = new List<Lesson>();
            if (topicID!=null)
            {
                 listLessons = (from lesson in dataContext.Lessons
                                            where lesson.IDTP == topicID
                                            select lesson).ToList();                
            }
            else
            {
                listLessons = (from lesson in dataContext.Lessons
                               where lesson.IDTP == "6M"
                               select lesson).ToList();
            }
            ViewBag.listLessons = listLessons;
        }
        
        private void ShowLessonItem(string lessonID)
        {
            Lesson lessonItem = (from lesson in dataContext.Lessons
                                 where lesson.ID == lessonID
                                 select lesson).SingleOrDefault();
            ViewBag.lessonItem = lessonItem;
        }        
    }
}
