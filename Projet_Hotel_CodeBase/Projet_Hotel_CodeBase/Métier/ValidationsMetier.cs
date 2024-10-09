using Projet_Hotel_CodeBase.DTO;

namespace Projet_Hotel_CodeBase.Métier
{
    public class ValidationsMetier
    {
        public bool EmailExists(ClientDTO clientDTO)
        {
            using (var db = new MyDbContext())
            {
               var client = db.Clients
               .FirstOrDefault(c => c.CliCourriel == clientDTO.CliCourriel);

               if (client == null)
               {
                    return false;
               }
                return true;
            }
        }
        public bool PasswordExists(ClientDTO clientDTO)
        {
            using (var db = new MyDbContext())
            {
                var client = db.Clients
                .FirstOrDefault(c => c.CliMotDePasse == clientDTO.CliMotDePasse);

                if (client == null)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
