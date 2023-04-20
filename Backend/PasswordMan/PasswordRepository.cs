using Microsoft.EntityFrameworkCore;
namespace PasswordMan
{
    public class PasswordRepository : IPasswordRepository
    {

        public PasswordRepository() {        
        }
        // Get list of all passwords, encrypted
        public List<Password> GetAllPasswords()
        {
            using( var context = new ApiContext())
            {
                var list = context.Passwords
                    .ToList();

                return list;
            }
        }
        // Get list of user's passwords by username, encrypted or decrypted
        public List<Password> GetPasswordsByUser(String userName,bool decryptPasswords)
        {
            using (var context = new ApiContext())
            {
                var list = context.Passwords
                    .ToList();
                // Filter list by username
                var list_filtered = list.FindAll(f => f.userName == userName);

                // If decryptPasswords parameter was set to true, decrypt password
                if (decryptPasswords)
                    list_filtered.ForEach(i =>{i.encryptedPassword = Base64Decode(i.encryptedPassword);});
                
                return list_filtered;
            }
        }
        // Get password by id
        public Password GetPasswordById(int id, bool decryptPasswords)
        {
            using (var context = new ApiContext())
            {
                var list = context.Passwords
                    .ToList();

                // Filter list by id
                var foundPassword = list.FirstOrDefault(f => f.Id == id);

                // If a password with this id was found, check decrypt param and decrypt if needed
                if (foundPassword !=null && decryptPasswords)
                    foundPassword.encryptedPassword = Base64Decode(foundPassword.encryptedPassword);

                return foundPassword;
            }
        }

        // Add password to the DB and return a new list
        public List<Password> AddPassword(Password password)
        {
            using (var context = new ApiContext())
            {
                context.Passwords.Add(password);
                // Save changes to store new password in DB
                context.SaveChanges();

                return context.Passwords
                    .ToList();
            }
        }
        // Encode plain text into Base64 and return encoded value
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        // Decode encoded text into plain text and return it
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        // Update the password with given id and new password data, and return the new password, encrypted
        public Password UpdatePassword(int id, Password password)
        {
            using (var context = new ApiContext())
            {
                var foundPassword = context.Passwords.FirstOrDefault(f => f.Id == id);

                // If password was found, update the fields values with new data
                if (foundPassword != null)
                {
                    foundPassword.category = password.category;
                    foundPassword.app = password.app;
                    foundPassword.userName = password.userName;
                    foundPassword.encryptedPassword = password.encryptedPassword;

                    // Encrypt the password before saving
                    foundPassword.encryptedPassword = Base64Encode(foundPassword.encryptedPassword);
                }

                context.SaveChanges();

                return foundPassword;
            }
        }
        // Delete the password with given id, and return the deleted password, encrypted
        public Password DeletePassword(int id)
        {
            using (var context = new ApiContext())
            {
                var foundPassword = context.Passwords.FirstOrDefault(f => f.Id == id);


                if (foundPassword != null)
                {
                    context.Passwords.Remove(foundPassword);
                }

                context.SaveChanges();

                return foundPassword;
            }
        }
    }
}
