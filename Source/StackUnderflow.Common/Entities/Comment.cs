using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StackUnderflow.Data.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }
        [Required]
        public Guid ResponseId { get; set; }

        public string Author { get; set; }
        [Required]
        public string Body { get; set; }
    }
}