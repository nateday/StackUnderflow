using Microsoft.AspNetCore.Identity;
using StackUnderflow.Data;
using StackUnderflow.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackUnderflow.Business
{
    public class ResponsesService
    {
        private readonly DataContext _context;

        public ResponsesService(DataContext context)
        {
            _context = context;
        }

        public List<Response> GetResponses()
        {
            var responses = _context.Responses.ToList();

            return responses;
        }

        public Response GetResponseById(Guid id)
        {
            //TODO:
            //Business rules!!

            var existingResponse = _context.Responses.Find(id);

            if (existingResponse == null)
            {
                throw new Exception("Question could not be found.");
            }

            var responseComments = _context.Comments.Where(c => c.ResponseId == existingResponse.Id).ToList();

            existingResponse.Comments = responseComments;

            return existingResponse;
        }

        public Response CreateResponse(Response response, IdentityUser user)
        {
            //TODO:
            //Business rules!!
            if (response.Id != Guid.Empty)
            {
                throw new Exception("Quesiton already exists.");
            }

            response.Id = Guid.NewGuid();
            response.Author = user.UserName;

            _context.Responses.Add(response);
            _context.SaveChanges();

            return response;
        }

        public Response UpdateResponse(Response response)
        {
            //TODO:
            //Business rules!!

            if (response.Id == Guid.Empty)
            {
                throw new Exception("Question does not exist.");
            }

            var existingResponse = GetResponseById(response.Id);
            
            existingResponse.Body = response.Body;
            existingResponse.Comments = response.Comments;

            _context.Responses.Update(existingResponse);
            _context.SaveChanges();

            return response;
        }

        public void DeleteResponse(Guid id)
        {

            if (id == Guid.Empty)
            {
                throw new Exception("Question does not exist.");
            }

            var response = GetResponseById(id);

            _context.Responses.Remove(response);
            _context.SaveChanges();
        }

        public Response UpVote(string id)
        {
            var response = GetResponseById(new Guid(id));

            response.Votes = response.Votes + 1;

            UpdateResponse(response);

            return response;
        }

        public Response DownVote(string id)
        {
            var response = GetResponseById(new Guid(id));

            response.Votes = response.Votes - 1;

            UpdateResponse(response);

            return response;
        }
    }
}
