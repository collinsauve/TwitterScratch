using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterScratch
{
    [Table("TwitterCredentials")]
    public class TwitterCredentials
    {
        [Key]
        public long TwitterCredentialsId { get; set; }
        public int? UserId { get; set; }
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }
    }
}
