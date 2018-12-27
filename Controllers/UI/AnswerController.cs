using BBCWebAPI.Data;
using BBCWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBCWebAPI.Controllers.UI
{
    public class AnswerController:BaseController
    {
        DataContext dataContext = null;
        public AnswerController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        [Route("HomePage/ListQuestionPage/ListAnswerPage")]
        public IActionResult ToListAnswerPage( string questionID, string contentQuestion)
        {
            try
            {
                BasePage(dataContext);
                var listAnswers = dataContext.Answers.Where(answer => answer.QuestionID == questionID).ToList();
                ViewBag.questionID = questionID;
                ViewBag.contentQuestion = contentQuestion;
                TempData["questionID"] = questionID;
                TempData["contentQuestion"] = contentQuestion;
                ViewBag.listAnswers = listAnswers;
                return View("Views/Pages/List/ListAnswerPage.cshtml");
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        [Route("HomePage/ListQuestionPage/ListAnswerPage/CreateAnswerPage")]
        public IActionResult ToCreateAnswerPage(string questionID, string contentQuestion)
        {
            try
            {
                BasePage(dataContext);
                ViewBag.questionID = questionID;
                TempData["questionID"] = questionID;
                TempData["contentQuestion"] = contentQuestion;
                ViewBag.contentQuestion = contentQuestion;
                return View("Views/Pages/Creates/CreateAnswerPage.cshtml");
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        [Route("HomePage/ListQuestionPage/ListAnswerPage/InsertAnswer")]
        public IActionResult InsertAnswer(string content, string correct)
        {
            try
            {
                List<string> _listAnswerID = (from ans in dataContext.Answers
                                   select ans.AnswerID).ToList();
                int answerID = 0;
                foreach(var ans in _listAnswerID)
                {
                    if(Int32.Parse(ans.Trim().ToString())>answerID)
                    {
                        answerID = Int32.Parse(ans.Trim().ToString());
                    }
                }
                var answer = dataContext.Answers.FirstOrDefault();
                if (answer != null)
                {
                    answer.AnswerID = (answerID + 1).ToString();
                    answer.Content = content;
                    answer.Correct = correct;
                    answer.QuestionID = TempData["questionID"].ToString();
                }
                dataContext.Answers.Add(answer);
                dataContext.SaveChanges();
                return RedirectToAction("ToListAnswerPage", new { questionID = TempData["questionID"].ToString(), contentQuestion = TempData["contentQuestion"].ToString() });
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        [Route("HomePage/ListQuestionPage/ListAnswerPage/EditAnswerPage")]
        public IActionResult ToEditAnswerPage(string questionID, string contentQuestion, string answerID)
        {
            try
            {
                BasePage(dataContext);
                ViewBag.questionID = questionID;
                TempData["questionID"] = questionID;
                TempData["contentQuestion"] = contentQuestion;
                TempData["answerID"] = answerID;
                ViewBag.contentQuestion = contentQuestion;
                var answerItem = dataContext.Answers.Where(ans => ans.AnswerID == answerID).FirstOrDefault();
                ViewBag.answerItem = answerItem;
                return View("Views/Pages/Edits/EditAnswerPage.cshtml");
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        [Route("HomePage/ListQuestionPage/ListAnswerPage/EditAnswerPage/UpdateAnswer")]
        public IActionResult UpdateAnswer(string content, string correct)
        {
            try
            {
                string answerID = TempData["answerID"].ToString();
                var updateAnswer = dataContext.Answers.Where(ans => ans.AnswerID == answerID).FirstOrDefault();
                if (updateAnswer != null)
                {
                    updateAnswer.Content = content;
                    updateAnswer.Correct = correct;
                }
                dataContext.Answers.Update(updateAnswer);
                dataContext.SaveChanges();
                return RedirectToAction("ToListAnswerPage", new { questionID = TempData["questionID"].ToString(), contentQuestion = TempData["contentQuestion"].ToString() });
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        [Route("HomePage/ListQuestionPage/ListAnswerPage/DeleAnswer")]
        public IActionResult DeleteAnswer(string answerID)
        {
            try
            {
                var deleteAnswer = dataContext.Answers.SingleOrDefault(answer => answer.AnswerID == answerID);
                if (deleteAnswer != null)
                {
                    dataContext.Answers.Remove(deleteAnswer);
                    dataContext.SaveChanges();
                }
                return RedirectToAction("ToListAnswerPage", new { questionID = TempData["questionID"].ToString(), contentQuestion = TempData["contentQuestion"].ToString() });
            }
            catch (Exception ex)
            {

            }
            return null;
        }

    }
}
