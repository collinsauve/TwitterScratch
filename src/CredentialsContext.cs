using System.Data.Entity;

namespace TwitterScratch
{
    public class CredentialsContext : DbContext
    {
        public DbSet<TwitterCredentials> TwitterCredentialses { get; set; }
    }
}
