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
        public void addChambre(short CHA_numero, bool CHA_etat, string CHA_autreInfo, Guid FK_TYP_id)
        {
            using (var db = new MyDbContext())
            {
                var chambreEntity = new Chambre();
                chambreEntity.PkChaId = Guid.NewGuid();
                chambreEntity.ChaNumero = CHA_numero;
                chambreEntity.ChaEtat = CHA_etat;
                chambreEntity.ChaAutreInfo = CHA_autreInfo;
                chambreEntity.FkTypId = FK_TYP_id;

                db.Chambres.Add(chambreEntity);
                db.SaveChanges();
            }
        }
    }
}
