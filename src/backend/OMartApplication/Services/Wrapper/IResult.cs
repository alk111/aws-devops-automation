﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMartApplication.Services.Wrapper
{
    
        public interface IResult
        {
            List<string> Messages { get; set; }

            bool Succeeded { get; set; }
        }

        public interface IResult<out T> : IResult
        {
            T Data { get; }
        }
    
}
