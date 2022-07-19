using System.Runtime.Serialization;

namespace RealWorldApp.BAL.Services
{
    [Serializable]
    public class BadRequestException : Exception
    {
        public BadRequestException(string error) : base(error)
        {

        }
    }
}