namespace Evoflare.API.Models
{
    public partial class Role
    {

        public Role(int id, int roleId, string name, string summary, string description)
        {
            Id = id;
            RoleId = roleId;
            Name = name;
            Summary = summary;
            Description = description;
        }
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
    }
}
