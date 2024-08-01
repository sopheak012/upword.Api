namespace upword.Api.Dtos
{
    public class UserWordDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string WordId { get; set; }
        public WordDto Word { get; set; } // Assuming you have a WordDto class for the related Word entity
    }
}
