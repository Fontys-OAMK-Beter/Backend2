namespace GroopySwoopyAPI.Models
{
    public class Event
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string PictureURL { get; set; }
        public int group_id { get; set; }
    }
}