using BookAuth.Models;

namespace BookAuth.Repository.User
{
    public class user:Iuser
    {
        private readonly DataContext db;

        public user(DataContext db)
        {
            this.db = db;
        }

        public UserRegister Login(UserLogin user)
        {
            return db.userRegisters.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
        }

        public int Register(UserRegister user)
        {
            db.userRegisters.Add(user);
            return db.SaveChanges();
        }
    }
}

