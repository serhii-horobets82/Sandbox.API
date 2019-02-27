using Microsoft.EntityFrameworkCore;

namespace Evoflare.API.Models
{
    public class BaseAppContext : DbContext
    {
        public BaseAppContext(DbContextOptions<BaseAppContext> options) : base(options)
        {
        }
    }
}