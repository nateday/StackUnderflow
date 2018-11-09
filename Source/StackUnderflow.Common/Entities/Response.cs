using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StackUnderflow.Data.Entities
{
    public class Response
    {
        public Guid Id { get; set; }
        [Required]
        public Guid QuestionId { get; set; }

        public string Author { get; set; }
        [Required]
        public string Body { get; set; }
        public int Votes { get; set; }
        public bool IsSolution { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}