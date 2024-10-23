using Projet_Hotel_CodeBase.DTO;

namespace Projet_Hotel_CodeBase.Métier
{
    public class ValidationsMetier
    {
        public bool TelephoneExists(ClientDTO clientDTO)
        {
            using (var db = new MyDbContext())
            {
                var client = db.Clients
                .FirstOrDefault(c => c.CliTelephoneMobile == clientDTO.CliTelephoneMobile);

                if (client == null)
                {
                    return false;
                }
                return true;
            }
        }
        public bool IsCurrentClientEmail(ClientDTO clientDTO)
        {
            using (var db = new MyDbContext())
            {
                var hasSameEmailAsAnotherClient = db.Clients
                    .Any(c => c.CliCourriel == clientDTO.CliCourriel && c.PkCliId != clientDTO.PkCliId);

                return !hasSameEmailAsAnotherClient;
            }
        }

        public bool IsCurrentClientPhone(ClientDTO clientDTO)
        {
            using (var db = new MyDbContext())
            {
                var hasSameEmailAsAnotherClient = db.Clients
                    .Any(c => c.CliTelephoneMobile == clientDTO.CliTelephoneMobile && c.PkCliId != clientDTO.PkCliId);

                return !hasSameEmailAsAnotherClient;
            }
        }

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
