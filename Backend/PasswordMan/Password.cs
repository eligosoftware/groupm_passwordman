namespace PasswordMan
{
    public class Password
    {
        public int Id { get; set; }
        public string category { get; set; }
        public string app { get; set; }
        public string userName { get; set; }
        public string encryptedPassword { get; set; }

        // Static field to remember the count of created passwords
        private static int IdCounter = 1;
        public Password()
        {

        }
        public Password(string category, string app, string userName, string encryptedPassword)
        {
            // Get Id automatically from static field
            this.Id = IdCounter;
            this.category = category;
            this.app = app;
            this.userName = userName;
            this.encryptedPassword = encryptedPassword;

            // Increment the count of passwords
            IdCounter++;
        }
    }
}
