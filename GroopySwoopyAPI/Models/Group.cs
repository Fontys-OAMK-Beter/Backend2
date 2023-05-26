namespace GroopySwoopyAPI.Models
{
    public class Group
    {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<string> MemberIDs { get; set; }
            public int AdminID { get; set; }
    }
}
