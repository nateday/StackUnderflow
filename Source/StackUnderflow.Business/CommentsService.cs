using Microsoft.AspNetCore.Identity;
using StackUnderflow.Data;
using StackUnderflow.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackUnderflow.Business
{
    public class CommentsService
    {
        private readonly DataContext _context;

        public CommentsService(DataContext context)
        {
            _context = context;
        }

        public List<Comment> GetComments()
        {
            var responses = _context.Comments.ToList();

            return responses;
        }

        public Comment GetCommentById(Guid id)
        {
            //TODO:
            //Business rules!!

            var existingComment = _context.Comments.Find(id);

            if (existingComment == null)
            {
                throw new Exception("Comment could not be found.");
            }

            return existingComment;
        }

        public Comment CreateComment(Comment comment, IdentityUser user)
        {
            //TODO:
            //Business rules!!
            if (comment.Id != Guid.Empty)
            {
                throw new Exception("Comment already exists.");
            }

            comment.Id = Guid.NewGuid();
            comment.Author = user.UserName;

            _context.Comments.Add(comment);
            _context.SaveChanges();

            return comment;
        }

        public Comment UpdateComment(Comment comment)
        {
            //TODO:
            //Business rules!!

            if (comment.Id == Guid.Empty)
            {
                throw new Exception("Question does not exist.");
            }

            var existingComment = GetCommentById(comment.Id);

            _context.Comments.Update(existingComment);
            _context.SaveChanges();

            return comment;
        }

        public void DeleteComment(Guid id)
        {

            if (id == Guid.Empty)
            {
                throw new Exception("Question does not exist.");
            }

            var comment = GetCommentById(id);

            _context.Comments.Remove(comment);
            _context.SaveChanges();
        }
    }
}
