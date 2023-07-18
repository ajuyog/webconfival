namespace frontend.Models.Graph
{
    public class BodyMessageDTO
    {
        public MessageMailDTO message { get; set; }
    }

    public class MessageMailDTO
    {
        public string subject { get; set; }
        public BodyDTO body { get; set; }
        public List<RecipientDTO> toRecipients { get; set; }
    }

    public class BodyDTO
    {
        public string contentType { get; set; }
        public string content { get; set; }
    }
    public class RecipientDTO
    {
        public EmailAddressDTO emailAddress { get; set; }
    }

    public class EmailAddressDTO
    {
        public string address { get; set; }
    }

}
