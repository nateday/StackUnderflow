using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StackUnderflow.Data.Entities
{
    public class Question
    {
        public Guid Id { get; set; }

        public string Author { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public int Votes { get; set; }

        public ICollection<Response> Responses { get; set; }
    }
}
