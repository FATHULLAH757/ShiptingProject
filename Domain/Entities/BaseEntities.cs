﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BaseEntities
    {
        public int CreatedBy { get; set; } 
        public DateTime ? CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
