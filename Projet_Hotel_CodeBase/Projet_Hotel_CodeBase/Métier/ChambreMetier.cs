namespace Projet_Hotel_CodeBase.Métier
{
    public class ChambreMetier
    {
        public Chambre[] requestChambres()
        {
            using (var db = new MyDbContext())
            {
                return db.Chambres.ToArray();
            }
        }

        public Chambre requestChambreByNum(short numChambre)
        {
            using (var db = new MyDbContext())
            {
                var chambreEntity = db.Chambres.SingleOrDefault(c => c.ChaNumero == numChambre);
                return chambreEntity;
            }
        }
        public void addChambre(Chambre chamre)
        {
            using (var db = new MyDbContext())
            {       
                db.Chambres.Add(chamre);
                db.SaveChanges();
            }
        }
    }
}
