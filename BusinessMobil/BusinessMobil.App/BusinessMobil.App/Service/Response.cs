using System;
namespace BusinessMobil.App.Service
{
    public class Response
    {
        public Response()
        {
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }
}
