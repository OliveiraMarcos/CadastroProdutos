using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class Response
    {
        public int StatusCode { get; set; }
        public bool HasError { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
        public IList<ValidationFailure> Rason { get; set; }
    }
}
