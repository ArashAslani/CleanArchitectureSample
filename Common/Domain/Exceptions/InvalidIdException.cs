namespace Common.Domain.Exceptions
{
    public class InvalidIdException : BaseDomainException
    {

        public InvalidIdException()
        {

        }
        public InvalidIdException(string message) : base(message)
        {

        }
    }
}
