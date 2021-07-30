using System;
namespace Api.Common
{
    public class ErrorModel
    {
        public string Hint { get; set; }
        public string Message { get; set; }
        public string Stack { get; set; }
    }
}
