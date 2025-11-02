namespace Restaurant.Domain.Exceptions
{
    public class ForbiddenExceptions : Exception
    {
        public ForbiddenExceptions()
        {
        }
        public ForbiddenExceptions(string message)
            : base(message)
        {
        }
        public ForbiddenExceptions(string message, Exception inner)
            : base(message, inner)
        { 
        }
    
    }
}
