using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using StackUnderflow.Data;
using StackUnderflow.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StackUnderflow.Business
{
    public class QuestionsService
    {
        private readonly DataContext _context;

        public QuestionsService(DataContext context)
        {
            _context = context;
        }

        public List<Question> GetQuestions()
        {
            var questions = _context.Questions.ToList();

            return questions;
        }

        public Question GetQuestionById(Guid id)
        {
            //TODO:
            //Business rules!!

            var existingQuestion = _context.Questions.Find(id);

            if (existingQuestion == null)
            {
                throw new Exception("Question could not be found.");
            }

            var questionResponses = _context.Responses.Where(r => r.QuestionId == existingQuestion.Id).ToList();

            existingQuestion.Responses = questionResponses;

            return existingQuestion;
        }

        public Question CreateQuestion(Question question, IdentityUser user)
        {
            //TODO:
            //Business rules!!
            if (question.Id != Guid.Empty)
            {
                throw new Exception("Quesiton already exists.");
            }
                
            question.Id = Guid.NewGuid();
            question.Author = user.UserName;

            _context.Questions.Add(question);
            _context.SaveChanges();

            return question;
        }

        public Question UpdateQuestion(Question question)
        {
            //TODO:
            //Business rules!!

            if (question.Id == Guid.Empty)
            {
                throw new Exception("Question does not exist.");
            }

            var existingQuestion = GetQuestionById(question.Id);

            existingQuestion.Title = question.Title;
            existingQuestion.Body = question.Body;
            existingQuestion.Votes = question.Votes;
            existingQuestion.Responses = question.Responses;

            _context.Questions.Update(existingQuestion);
            _context.SaveChanges();

            return question;
        }

        public void DeleteQuestion(Guid id) {

            if (id == Guid.Empty)
            {
                throw new Exception("Question does not exist.");
            }

            var question = GetQuestionById(id);

            _context.Questions.Remove(question);
            _context.SaveChanges();
        }

        public Question UpVote(string id)
        {
            var question = GetQuestionById(new Guid(id));

            question.Votes = question.Votes + 1;

            UpdateQuestion(question);

            return question;
        }

        public Question DownVote(string id)
        {
            var question = GetQuestionById(new Guid(id));

            question.Votes = question.Votes - 1;

            UpdateQuestion(question);

            return question;
        }
    }
}
