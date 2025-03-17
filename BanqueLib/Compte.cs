namespace BanqueLib
{
    //éléments requis à la classe
    public enum Statut {OK, Gelé, Vide}
    //classe
    public class Compte
    {
        #region ---- Attributs ----
        private readonly int _numero;
        private string _detenteur;
        private decimal _solde;
        private bool _estGele = false;
        private Statut _statut;
        #endregion
        #region ---- Constructeur ----
        public Compte(int numero, string detenteur, decimal solde = 0m, Statut statut = Statut.OK, bool estGele = false)
        {
            if (numero <= 0)
                throw new ArgumentOutOfRangeException("Numéro");
            _numero = numero;
            SetDetenteur(detenteur);
            decimal arrondi = decimal.Round(solde, 2);
            if (solde < 0 || solde != arrondi)
                throw new ArgumentOutOfRangeException("Solde");
            _solde = solde;
            _estGele = estGele;
            _statut = statut;
        }
        #endregion
        #region ---- Getters ----
        public int Numero => _numero;
        public string Detenteur => _detenteur;
        public decimal Solde => _solde;
        public bool EstGele => _estGele;
        public Statut Statut => _statut;
        #endregion
        #region ---- Setters ----
        public void SetDetenteur(string detenteur) {
            if (string.IsNullOrWhiteSpace(detenteur))
                throw new ArgumentNullException("Détenteur");
            detenteur = detenteur.Trim();
            _detenteur = detenteur;
        }
        #endregion
        #region ---- Méthodes ----
        public string Description() {
            string description = "";
            description += $"[TG] *******************************************\n";
            description += $"[TG] *                                         *\n";
            description += $"[TG] *    COMPTE {_numero, -30}*\n";
            description += $"[TG] *       De: {_detenteur, -30}*\n";
            description += $"[TG] *    Solde: {_solde, -30:C}*\n";
            description += $"[TG] *   Statut: {_statut, -30}*\n";
            description += $"[TG] *                                         *\n";
            description += $"[TG] *******************************************\n";
            return description;
        }
        public bool PeutDeposer(decimal montant = 1) {
            var arrondi = decimal.Round(montant, 2);
            if (montant <= 0 || montant != arrondi)
                throw new ArgumentOutOfRangeException("Montant");
            if (!_estGele)
                return true;
            else
                return false;
        }
        public bool PeutRetirer(decimal montant = 1)
        {
            var arrondi = decimal.Round(montant, 2);
            if (montant <= 0 || montant != arrondi)
                throw new ArgumentOutOfRangeException("Montant");
            if (!_estGele)
                return true;
            else
                return false;
        }

        public decimal Deposer(decimal montant) {
            var arrondi = decimal.Round(montant, 2);
            if (montant <= 0 || montant != arrondi)
                throw new ArgumentOutOfRangeException("Montant");
            if (PeutDeposer(montant) && _statut != Statut.Gelé)
            {
                _solde += montant;
                return _solde;
            }
            else
                throw new InvalidOperationException("Déposer");
        }
        public decimal Retirer(decimal montant)
        {
            var arrondi = decimal.Round(montant, 2);
            if (montant <= 0 || montant != arrondi)
                throw new ArgumentOutOfRangeException("Montant");
            if (PeutRetirer(montant) && _statut == Statut.OK && montant <= _solde)
            {
                _solde -= montant;
                return _solde;
            }
            else
                throw new InvalidOperationException("Retirer");
        }
        public decimal Vider() {
            if (_statut != Statut.OK || _solde <= 0)
                throw new InvalidOperationException("Vider");
            else
            {
                decimal montant = _solde;
                _solde -= montant;
                _statut = Statut.Vide;
                return montant;
            }
        }
        public void Geler()
        {
            if (_statut == Statut.Gelé)
                throw new InvalidOperationException("Geler");
            else
                _statut = Statut.Gelé;
        }
        public void Degeler()
        {
            if (_statut != Statut.OK)
                _statut = Statut.OK;
            else
                throw new InvalidOperationException("Dégeler");
        }
        #endregion
    }
}
