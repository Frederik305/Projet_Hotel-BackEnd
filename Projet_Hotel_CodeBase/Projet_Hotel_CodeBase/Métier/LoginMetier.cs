using Projet_Hotel_CodeBase.DTO;

namespace Projet_Hotel_CodeBase.Métier
{
    public class LoginMetier
    {
        /// <summary>
        /// Vérifie les informations de connexion de l'utilisateur.
        /// </summary>
        /// <param name="loginDTO">Un objet contenant les informations de connexion (email et mot de passe) de l'utilisateur.</param>
        /// <returns>
        /// Retourne un objet <see cref="LoginDTO"/> si les informations de connexion sont valides.
        /// Lève une exception si les informations sont invalides.
        /// </returns>
        public LoginDTO login(LoginDTO loginDTO)
        {
            // Utilisation de l'entité de base de données avec la clause 'using'
            using (var db = new MyDbContext())
            {
                // Création d'une instance des validations métier pour la vérification des informations
                ValidationsMetier validations = new ValidationsMetier();

                // Vérification de la validité des informations de connexion (par exemple, email et mot de passe)
                if (!validations.IsValidClient(loginDTO, db))
                {
                    // Si les informations ne sont pas valides, une exception est lancée
                    throw new Exception(message: "Informations invalides");
                }

                // Si les informations sont valides, retourne le DTO de connexion
                return loginDTO;
            }
        }
    }
}
