using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PasswordMan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        readonly IPasswordRepository _passwordRepository;
        
        // Set reference to Password Repository 
        public PasswordController(IPasswordRepository passwordRepository)
        {
            _passwordRepository = passwordRepository;
        }


        // GET: api/<PasswordController>
        [HttpGet]
        public ActionResult<List<Password>> Get()
        {
            return Ok(_passwordRepository.GetAllPasswords());
        }

        // GET api/<PasswordController>/5
        [HttpGet("{username}/{decrypt:int?}")]
        public ActionResult<List<Password>> Get(string username,int decrypt=0)
        {
            // Convert decrypt parameter from int to bool
            bool decryptPasswords = false;
            if (decrypt == 1)
            {
                decryptPasswords = true;
            }
            List<Password> passwordList = _passwordRepository.GetPasswordsByUser(username, decryptPasswords);
            if (passwordList.Count > 0) 
            {
                return Ok(passwordList);
            }
            else
            {
                // If the list is empty, return NotFound 404 Status Code
                return NotFound();
            }
        }

        // GET api/<PasswordController>/5
        [HttpGet("{id:int}/{decrypt:int?}")]
        public ActionResult<List<Password>> Get(int id, int decrypt = 0)
        {
            // Convert decrypt parameter from int to bool
            bool decryptPasswords = false;
            if (decrypt == 1)
            {
                decryptPasswords = true;
            }
            Password password = _passwordRepository.GetPasswordById(id, decryptPasswords);
            if (password != null)
            {
                return Ok(password);
            }
            else
            {
                // If password not found, return NotFound 404 Status Code
                return NotFound();
            }
        }


        // POST api/<PasswordController>
        [HttpPost]
        public ActionResult<List<Password>> Post([FromBody] Password password)
        {
            
            password.encryptedPassword = PasswordRepository.Base64Encode(password.encryptedPassword);

            List<Password> passwordList = _passwordRepository.AddPassword(password);
            return Ok(passwordList);

        }

        // PUT api/<PasswordController>/5
        [HttpPut("{id}")]
        public ActionResult<Password> Put(int id, [FromBody] Password password)
        {
            Password foundPassword = _passwordRepository.UpdatePassword(id,password);
            if (foundPassword != null)
            {
                return Ok(foundPassword);
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE api/<PasswordController>/5
        [HttpDelete("{id}")]
        public ActionResult<Password> Delete(int id)
        {
            Password foundPassword = _passwordRepository.DeletePassword(id);
            if (foundPassword != null)
            {
                return Ok(foundPassword);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
