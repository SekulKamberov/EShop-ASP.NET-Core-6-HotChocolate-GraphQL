﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DTO.Store
{
    public class UpdateStoreInput
    {
        public string Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
    }
}