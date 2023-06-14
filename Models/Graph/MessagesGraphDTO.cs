namespace confinancia.Models.Graph
{
    public class MessagesGraphDTO: MeGraphDTO
    {
        public List<ObjMessage> value { get; set; }
    }

    public class ObjMessage
    {
        public string ReceivedDateTime { get; set; }
        public string Subject { get; set; }
        public string BodyPreview { get; set; }
        public Body Body { get; set; }
        public Sender Sender { get; set; }
        //public ToRecipients ToRecipients { get; set; }

    }
    public class Body
    {
        public string ContentType { get; set; }
        public string Content { get; set; }
    }

    public class Sender 
    {
        public EmailAddress EmailAddress { get; set; }
    }

    public class ToRecipients
    {
        public EmailAddress EmailAddress { get; set; }
    }

    public class EmailAddress
    {
        public string Address { get; set; }
    }
}
