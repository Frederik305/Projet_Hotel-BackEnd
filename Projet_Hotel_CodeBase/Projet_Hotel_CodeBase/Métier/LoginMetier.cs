using Projet_Hotel_CodeBase.DTO;

namespace Projet_Hotel_CodeBase.Métier
{
    public class LoginMetier
    {
        public LoginDTO login(LoginDTO loginDTO)
        {
            using (var db = new MyDbContext())
            {
                ValidationsMetier validations = new ValidationsMetier();
                if (!validations.IsValidClient(loginDTO, db))
                {
                    throw new Exception("Informations invalide");
                }
                return loginDTO;
            }
        }
    }
}
