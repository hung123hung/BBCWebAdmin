using BBCWebAPI.Data;
using BBCWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBCWebAPI.Controllers.UI
{
    public class QuestionController:BaseController
    {
        DataContext dataContext = null;
        public QuestionController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        [Route("HomePage/ListQuestionPage")]
        public IActionResult ToListQuestionPage(string lessonID, string lessonName)
        {
            try
            {
                BasePage(dataContext);
                ViewBag.lessonID = lessonID;
                ViewBag.lessonName = lessonName;
                TempData["lessonID"] = lessonID;
                TempData["lessonName"] = lessonName;
                ShowListQuestion(lessonID);
                return View("Views/Pages/List/ListQuestionPage.cshtml");
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        [Route("HomePage/ListQuestionPage/CreateQuestionPage")]
        public IActionResult ToCreateQuestionPage( string lessonID, string lessonName)
        {
            try
            {
                BasePage(dataContext);
                ViewBag.lessonID = lessonID;
                TempData["lessonID"] = lessonID;
                TempData["lessonName"] = lessonName;
                ViewBag.lessonName = lessonName;
                return View("Views/Pages/Creates/CreateQuestionPage.cshtml");
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        [Route("HomePage/ListQuestionPage/CreateQuestionPage/InsertQuestion")]
        public IActionResult InsertQuestion(string content, string typeQuestion)
        {
            try
            {
                List<string> listQuestionID = (from qs in dataContext.Questions
                                               select qs.QuestionID).ToList();
                int questionID = 0;
                foreach(var ques in listQuestionID)
                {
                    if(Int32.Parse(ques.Trim().ToString())>questionID)
                    {
                        questionID = Int32.Parse(ques.Trim().ToString());
                    }
                }
                var question = dataContext.Questions.FirstOrDefault();
                if (question != null)
                {
                    question.QuestionID = (questionID + 1).ToString();
                    question.Content = content;
                    question.TypeQuestion = typeQuestion;
                    question.LessonID = TempData["lessonID"].ToString();
                }
                dataContext.Questions.Add(question);
                dataContext.SaveChangesAsync();
                return RedirectToAction("ToListQuestionPage", new { lessonID = TempData["lessonID"].ToString(), lessonName = TempData["lessonName"].ToString() });
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        [Route("HomePage/ListQuestionPage/EditQuestionPage")]
        public IActionResult ToEditQuestionPage( string lessonID, string lessonName, string questionID)
        {
            try
            {
                BasePage(dataContext);
                ViewBag.lessonName = lessonName;
                //Send values lessonID,lessonName,questionID to Action Method UpdateQuestion
                TempData["lessonID"] = lessonID;
                TempData["lessonName"] = lessonName;
                TempData["questionID"] = questionID;
                Question questionItem = (from question in dataContext.Questions
                                         where question.QuestionID == questionID
                                         select question).FirstOrDefault();
                ViewBag.questionItem = questionItem;
                return View("Views/Pages/Edits/EditQuestionPage.cshtml");
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        [Route("HomePage/ListQuestionPage/EditQuestionPage/UpdateQuestion")]
        public IActionResult UpdateQuestion(string content, string typeQuestion)
        {
            try
            {
                string questionID = TempData["questionID"].ToString();
                var updateQuestion = dataContext.Questions.Where(question => question.QuestionID == questionID).FirstOrDefault();
                if (updateQuestion != null)
                {
                    updateQuestion.Content = content;
                    updateQuestion.TypeQuestion = typeQuestion;
                }
                dataContext.Questions.Update(updateQuestion);
                dataContext.SaveChanges();
                return RedirectToAction("ToListQuestionPage", new { lessonID = TempData["lessonID"].ToString(), lessonName = TempData["lessonName"].ToString() });
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        [Route("HomePage/ListQuestionPage/DeleteQuestion")]
        public IActionResult DeleteQuestion(string questionID)
        {
            try
            {
                var deleteQuestion = dataContext.Questions.SingleOrDefault(question => question.QuestionID == questionID);
                List<Answer> deleteAnswer = dataContext.Answers.Where(answer => answer.QuestionID == questionID).ToList();
                if (deleteQuestion != null)
                {
                    foreach (var ans in deleteAnswer)
                    {
                        dataContext.Answers.Remove(ans);
                    }
                    dataContext.Questions.Remove(deleteQuestion);
                    dataContext.SaveChanges();
                }
                return RedirectToAction("ToListQuestionPage", new { lessonID = TempData["lessonID"].ToString(), lessonName = TempData["lessonName"].ToString() });
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        private void ShowListQuestion(string lessonID)
        {
            List<Question> listQuestions = new List<Question>();
            listQuestions = (from question in dataContext.Questions
                             where question.LessonID == lessonID
                             select question).ToList();
            ViewBag.listQuestions = listQuestions;
        }
    }
}
