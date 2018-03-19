using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Mediator
{
    public class BaseMediator<T> where T : class
    {
        public string Method { get; set; }
        public T ObjectView { get; set; }
        public int Id { get; set; }
    }
}
