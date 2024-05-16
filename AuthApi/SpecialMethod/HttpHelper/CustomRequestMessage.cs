namespace AuthApi.SpecialMethod.HttpHelper
{
    public class CustomRequestMessage
    {
        public string Data { get; set; }
        public List<string>? Includes { get; set; }
        public string? Expression { get; set; }



        public CustomRequestMessage() { }

        public CustomRequestMessage(string data)
        {
            Data = data;
        }


    }
}
