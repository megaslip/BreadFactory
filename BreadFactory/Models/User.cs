namespace BreadFactory.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string FullName { get; set; }

        public bool IsAdmin => Role == UserRoles.Admin;
        public bool IsTechnologistOrAdmin => Role == UserRoles.Admin || Role == UserRoles.Technologist;
        public bool IsOperatorOrHigher => Role == UserRoles.Admin || Role == UserRoles.Technologist || Role == UserRoles.Operator;
    }

    public static class UserRoles
    {
        public const string Admin = "Администратор";
        public const string Technologist = "Технолог";
        public const string Operator = "Оператор";
    }
}