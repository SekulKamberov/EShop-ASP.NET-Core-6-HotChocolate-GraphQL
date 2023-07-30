﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DTO.UsersDtos
{
    public class UserPayload
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
         
        public string UserName { get; set; } 
        public string Gender { get; set; }
        public string AvatarUrl { get; set; }
    }
}