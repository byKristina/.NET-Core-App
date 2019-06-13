﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Searches
{
    public abstract class BaseSearch 
    {       
        public int PerPage { get; set; } = 2;
        public int PageNumber { get; set; } = 1;
    }
}