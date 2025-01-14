﻿using System.ComponentModel.DataAnnotations;

namespace notebook_api.Contracts.V1.Requests
{
    public class CreateNoteRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        [MinLength(10)]
        public string Body { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
