﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.PERSISTENCE.Utils
{
    public class ResponseModel
    {
        public string Message { get; set; }
    }

    public class ErrorModel
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public dynamic? Data { get; set; }
    }
}
