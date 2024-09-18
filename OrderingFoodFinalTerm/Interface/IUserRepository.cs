namespace OrderingFoodFinalTerm.Interface
{
    public interface IUserRepository
    {
        ICollection <User> GetAll();
        User GetUserById(int id);

        User GetUserByName(string name);

        bool CreateUser(UserDTO request);

        bool ValidatePassword(User user, string password);

        string CreateToken(User user);

        void DeleteUser(int id);
        List<User> Search(string key);

        //void ChangePassword(int id, string newPassword, string confirmPassword);


    }
}
