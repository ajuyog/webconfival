namespace frontend.Models.Graph
{
    
    public class MailFoldersDTO
    {
        public List<ObjFolderDTO> Value { get; set; }
    }
    public class ObjFolderDTO
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }

    }
}
