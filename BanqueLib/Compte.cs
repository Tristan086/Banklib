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
            _numero = numero;
            SetDetenteur(detenteur);
            decimal.Round(solde, 2);
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
                throw new ArgumentNullException();
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
                throw new ArgumentOutOfRangeException();
            if (!_estGele)
                return true;
            else
                return false;
        }
        public bool PeutRetirer(decimal montant = 1)
        {
            var arrondi = decimal.Round(montant, 2);
            if (montant <= 0 || montant != arrondi)
                throw new ArgumentOutOfRangeException();
            if (!_estGele)
                return true;
            else
                return false;
        }

        public decimal Deposer(decimal montant) {
            var arrondi = decimal.Round(montant, 2);
            if (montant <= 0 || montant != arrondi)
                throw new ArgumentOutOfRangeException();
            if (PeutDeposer(montant))
            {
                _solde += montant;
                return _solde;
            }
            else
                throw new InvalidOperationException();
        }
        public decimal Retirer(decimal montant)
        {
            var arrondi = decimal.Round(montant, 2);
            if (montant <= 0 || montant != arrondi)
                throw new ArgumentOutOfRangeException();
            if (PeutRetirer(montant))
            {
                _solde -= montant;
                return _solde;
            }
            else
                throw new InvalidOperationException();
        }
        public decimal Vider() {
            if (_statut == Statut.Vide || _statut == Statut.Gelé)
                throw new InvalidOperationException();
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
                throw new InvalidOperationException();
            else
                _statut = Statut.Gelé;
        }
        public void Degeler()
        {
            _statut = Statut.OK;
        }
        #endregion
    }
}
