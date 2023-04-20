namespace PasswordMan
{
    public interface IPasswordRepository
    {
        List<Password> AddPassword(Password password);
        public List<Password> GetAllPasswords();
        Password GetPasswordById(int id, bool decryptPasswords);
        public List<Password> GetPasswordsByUser(string username, bool decrypt);

        public Password UpdatePassword(int id, Password password);

        public Password DeletePassword(int id);
    }
}
