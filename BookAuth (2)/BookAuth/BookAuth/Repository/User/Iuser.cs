using BookAuth.Models;

namespace BookAuth.Repository.User
{
    public interface Iuser
    {
        UserRegister Login(UserLogin user);
        int Register(UserRegister user);
    }
}
