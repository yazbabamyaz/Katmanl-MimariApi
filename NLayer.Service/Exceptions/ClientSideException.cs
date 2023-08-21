namespace NLayer.Service.Exceptions
{
    //client taraflı hata ise...customexception
    public class ClientSideException : Exception
    {
        public ClientSideException(string message) : base(message)//Exception ın ctor una gönderdik
        {

        }
    }
}
