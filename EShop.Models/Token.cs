﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Models
{
    public class Token : BaseEntity, IBaseEntity
    {
        [Key]
        public string RefreshToken { get; set; }

        public string JwtId { get; set; }

        public bool Invalidated { get; set; }

        public string UserId { get; set; }
        public DateTime ExpiryDate { get; set; } = DateTime.UtcNow.AddMonths(3);

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}