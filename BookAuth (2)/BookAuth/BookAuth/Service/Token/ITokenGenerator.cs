namespace BookAuth.Service.Token
{
    public interface ITokenGenerator
    {
        string GenerateToken(string email, string role);
    }
}
