using System;

namespace upword.Api.Entities
{
    public class UserWord
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public string WordId { get; set; }
        public ApplicationUser User { get; set; }
        public Word Word { get; set; }
    }
}
