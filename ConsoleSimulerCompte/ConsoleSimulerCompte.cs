using BanqueLib;

var compte = new Compte(Random.Shared.Next(100, 1000), "Tristan Gordon");
while (true)
{
    Console.Clear();
    Console.WriteLine(compte.Description());
    Console.WriteLine("""
    
     TESTER COMPTE

     1 - Modifier Détenteur
     2 - Peut déposer
     3 - Peut retirer
     4 - Peut retirer (montant)
     5 - Déposer (montant)
     6 - Retirer (montant)
     7 - Vider
     8 - Geler
     9 - Dégeler
     q - Quitter
     r - Reset

     Votre choix, Tristan Gordon ?

    """);
    
    switch (Console.ReadKey(true).KeyChar)
    {
        case '1': // Modifier Détenteur
            compte.SetDetenteur($"{compte.Detenteur} {Random.Shared.Next(1, 100)}");
            Console.WriteLine($"** Détenteur modifié pour: {compte.Detenteur}");
            break;
        case '2': // Peut Déposer
            if (compte.PeutDeposer())
                Console.WriteLine("** Peut déposer? Oui.");
            else
                Console.WriteLine("** Peut déposer? Non.");
            break;
        case '3': // Peut Retirer
            if (compte.PeutRetirer())
                Console.WriteLine("** Peut retirer? Oui.");
            else
                Console.WriteLine("** Peut retirer? Non.");
            break;
        case '4': // Peut Retirer (montant)
            {
                decimal montant = (decimal)Math.Round(Random.Shared.NextDouble() * 100, 2);
                if (compte.PeutRetirer(montant))
                    Console.WriteLine($"** Peut retirer {montant:C}? Oui.");
                else
                    Console.WriteLine($"** Peut retirer {montant:C}? Non.");
            }
            break;
        case '5': // Déposer
            {
                decimal montant = (decimal)Math.Round(Random.Shared.NextDouble() * 100, 2);
                if (compte.PeutDeposer()) {
                    compte.Deposer(montant);
                    Console.WriteLine($"** Dépôt de {montant:C}");
                }
                else
                    Console.WriteLine($"** Impossible de déposer {montant:C}");
            }
            break;
        case '6': // Retirer
            {
                decimal montant = (decimal)Math.Round(Random.Shared.NextDouble() * 100, 2);
                if (compte.PeutRetirer())
                {
                    compte.Retirer(montant);
                    Console.WriteLine($"** Retrait de {montant:C}");
                }
                else
                    Console.WriteLine($"** Impossible de retirer {montant:C}");
            }
            break;
        case '7': // Vider
            {
                if (compte.Statut == Statut.OK)
                {
                    Console.WriteLine($"** Retrait complet de {compte.Solde:C}.");
                    compte.Vider();
                }
                else
                    Console.WriteLine("** Impossible de vider un compte vide ou gelé.");
            }
            break;
        case '8': // Geler
            {
                if (!compte.EstGele)
                {
                    compte.Geler();
                    Console.WriteLine("** Le compte a été gelé.");
                }
                else
                    Console.WriteLine("** Impossible de geler un compte déjà gelé.");
            }
            break;
        case '9': // Dégeler
            {
                if (compte.EstGele)
                {
                    compte.Degeler();
                    Console.WriteLine("** Le compte a été dégelé.");
                }
                else
                    Console.WriteLine("** Impossible de dégeler un compte non gelé.");
            }
            break;
        case 'q':
            Environment.Exit(0); break;
        case 'r':
            compte = new Compte(Random.Shared.Next(100, 1000), "Tristan Gordon");
            Console.WriteLine("Un nouveau compte a été créé.");
            break;
        default:
            Console.WriteLine(" Mauvais choix"); break;
    }
    Console.WriteLine("\n Appuyer sur ENTER pour continuer...");
    Console.ReadLine();
}

#pragma warning disable S3903 // Types should be defined in named namespaces
static class Utile
{
    public static (string, string?) ExceptMsg(Action action)
    {
        try
        {
            action();
            return ("EXCEPTION attendue", null);
        }
        catch (Exception ex)
        {
            return (ex.GetType().Name, ex.Message);
        }
    }
}