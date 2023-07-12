namespace frontend.Models.Graph
{
	public class CalendarGraphDTO: MeGraphDTO
	{
        public List<ObjCalendar> Value { get; set; }
        public string Folder { get; set; }
    }

    public class ObjCalendar
    {
        public string Subject { get; set; }
        public BodyCalendarDTO Body { get; set; }
        public StartDTO Start { get; set; }
        public EndDTO End { get; set; }
        public LocationDTO Location { get; set; }
        public OrganizerDTO organizer { get; set; }
        public string Id { get; set; }
    }


    public class BodyCalendarDTO
    {
        public string ContentType { get; set; }
        public string Content { get; set; }
    }

    public class StartDTO
    {
        public string DateTime { get; set; }
    }

    public class EndDTO
    {
        public string DateTime { get; set; }
    }
    public class LocationDTO
    {
        public string DisplayName { get; set; }
    }
    public class OrganizerDTO
    {
        public EmailAddressCalendarDTO emailAddress { get; set; }
    }
    public class EmailAddressCalendarDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }



}
