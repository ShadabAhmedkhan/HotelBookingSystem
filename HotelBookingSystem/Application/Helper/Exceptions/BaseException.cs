namespace HotelBookingSystem.Application.Helper.Exceptions
{
    public class BaseException : Exception
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; }
        public int Code { get; set; }
        public new string InnerException { get; set; }

        public BaseException(string message = "Error", int code = 400)
  : base(message)
        {
            Success = false;
            Message = message;
            InnerException = message;
            Code = code;
        }
    }
}
