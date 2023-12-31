﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Novella.Models
{
    public class Rating
    {
        [Key]
        public int RatingId { get; set; }

        [ForeignKey("Product")]
        public string ProductId { get; set; }

        [ForeignKey("UserAccount")]
        public string UserId { get; set; }

        public string Review { get; set; }
        public decimal RatingValue { get; set; }
        public DateTime DateRated { get; set; }

        // Navigation properties
        public virtual Product Product { get; set; }
        public virtual UserAccount UserAccount { get; set; }
    }
}
