namespace Lands.Models
{
    public class Response
    {
        /*si IsSuccess viene true Result trae los datos de lo contrario
         * mostrará el Message  */
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }
}
