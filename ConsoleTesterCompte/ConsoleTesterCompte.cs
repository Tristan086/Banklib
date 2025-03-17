﻿using BanqueLib;

while (true)
{
    Console.Clear();
    Console.WriteLine("""
    
     TESTER COMPTE

     1 - Création d'un compte simple (solde 0$)
     2 - Création d'un compte avec solde
     3 - Création d'un compte gelé
     4 - Déposer
     5 - Retirer
     6 - Vider
     7 - Geler et dégeler
     8 - Getters
     9 - Setter
     a - Exceptions (constructeur)
     b - Exceptions (setter)
     c - Exceptions (peutdéposer et peutretirer)
     d - Exceptions (déposer et retirer)
     e - Exceptions (geler, dégeler et vider)
     q - Quitter

     Votre choix, Tristan Gordon ?

    """);

    switch (Console.ReadKey(true).KeyChar)
    {
        case '1':
            Console.WriteLine(new Compte(1001, "Mme Création simple").Description());
            break;
        case '2':
            Console.WriteLine(new Compte(1002, "         Mme Création avec solde        ", 8008.02m).Description());
            break;
        case '3':
            Console.WriteLine(new Compte(1003, "Mme Création gelée", 1_111_111, Statut.Gelé).Description());
            break;
        case '4': // Déposer
            {
                var compte = new Compte(1004, "M Déposer", 1000);
                Console.WriteLine(" " + compte.PeutDeposer());
                Console.WriteLine(" " + compte.PeutDeposer(1000));
                Console.WriteLine();
                compte.Deposer(1000);
                Console.WriteLine(compte.Description());
            } 
            break;
        case '5': // Retirer
            {
                var compte = new Compte(1005, "M Retirer", 5000);
                Console.WriteLine(" " + compte.PeutRetirer());
                Console.WriteLine(" " + compte.PeutRetirer(1000));
                Console.WriteLine(" " + !compte.PeutRetirer(10000));
                Console.WriteLine();
                compte.Retirer(1000);
                Console.WriteLine(compte.Description());
            }
            break;
        case '6': // Vider
            {
                var compte = new Compte(1006, "M Vider", 15000);
                Console.WriteLine(" " + (compte.Vider() == 15000));
                Console.WriteLine("\n" + compte.Description());
            }
            break;
        case '7': // Geler et dégeler
            {
                var compte = new Compte(1007, "MM Gel et Dégel", 7000);
                compte.Geler();
                Console.WriteLine(" " + !compte.PeutDeposer());
                Console.WriteLine(" " + !compte.PeutRetirer());
                Console.WriteLine("\n" + compte.Description());
                compte.Degeler();
                Console.WriteLine("\n " + compte.PeutDeposer());
                Console.WriteLine(" " + compte.PeutRetirer());
                Console.WriteLine("\n" + compte.Description());
            }
            break;
        case '8': // getters
            {
                var compte = new Compte(1008, "Mme getters", 8000);
                Console.WriteLine(compte.Description());
                Console.WriteLine();
                Console.WriteLine($"          Numéro: {compte.Numero}");
                Console.WriteLine($" Détenteur.trice: {compte.Detenteur}");
                Console.WriteLine($"           Solde: {compte.Solde:C}");
                Console.WriteLine($"          Statut: {compte.Statut}");
                Console.WriteLine($"            Gelé: {compte.EstGele}\n");
            }
            break;
        case '9': // setters
            {
                var compte = new Compte(1008, "Mme setter", 9000);
                compte.SetDetenteur("          Mme setter anglais        ");
                Console.WriteLine($" {compte.Detenteur == "Mme setter anglais"}\n");
                Console.WriteLine(compte.Description());
            }
            break;
        case 'a':  // erreurs de constructions
            {
                (string, string?)[] erreurs = 
                {
                    Utile.ExceptMsg(() => new Compte(0, "Han")),
                    Utile.ExceptMsg(() => new Compte(-1, "Han")),
                    Utile.ExceptMsg(() => new Compte(1, null!)),
                    Utile.ExceptMsg(() => new Compte(1, "")),
                    Utile.ExceptMsg(() => new Compte(1, "    ")),
                    Utile.ExceptMsg(() => new Compte(1, "Han", -1)),
                    Utile.ExceptMsg(() => new Compte(1, "Han", 0.001m)),
                };
                foreach (var (excep, message) in erreurs)
                    Console.WriteLine($"\n {excep}\n {message ?? ""}");
            }
            break;
        case 'b':  // erreurs de setter
            {
                var ok = new Compte(1009, "Obiwan", 7000);
                (string, string?)[] erreurs =
                {
                    Utile.ExceptMsg(() => ok.SetDetenteur(null!)),
                    Utile.ExceptMsg(() => ok.SetDetenteur("")),
                    Utile.ExceptMsg(() => ok.SetDetenteur("  ")),
                };
                foreach (var (excep, message) in erreurs)
                    Console.WriteLine($"\n {excep}\n {message ?? ""}");
            }
            break;
        case 'c':  // erreurs PeutRetirer, PeutDéposer
            {
                var ok = new Compte(1009, "Obiwan", 7000);
                (string, string?)[] erreurs =
                {
                    Utile.ExceptMsg(() => ok.PeutDeposer(-1)),
                    Utile.ExceptMsg(() => ok.PeutDeposer(1.001m)),
                    Utile.ExceptMsg(() => ok.PeutRetirer(-1)),
                    Utile.ExceptMsg(() => ok.PeutRetirer(1.001m)),
                };
                foreach (var (excep, message) in erreurs)
                    Console.WriteLine($"\n {excep}\n {message ?? ""}");
            }
            break;
        case 'd':  // erreurs de retrait et dépôt
            {
                var ok = new Compte(1009, "Obiwan", 7000);
                var gelé = new Compte(1009, "Obiwan", 7000, Statut.Gelé);
                (string, string?)[] erreurs =
                {
                    Utile.ExceptMsg(() => ok.Deposer(1000.001m)),
                    Utile.ExceptMsg(() => gelé.Deposer(1000)),
                    Utile.ExceptMsg(() => ok.Retirer(8000)),
                    Utile.ExceptMsg(() => ok.Retirer(1000.001m)),
                    Utile.ExceptMsg(() => gelé.Retirer(1000)),
                };
                foreach (var (excep, message) in erreurs)
                    Console.WriteLine($"\n {excep}\n {message ?? ""}");
            }
            break;
        case 'e':  // erreurs pour geler, dégeler et vider
            {
                var ok = new Compte(1009, "Obiwan", 0);
                var gelé = new Compte(1009, "Obiwan", 10, Statut.Gelé);
                (string, string?)[] erreurs =
                {
                    Utile.ExceptMsg(() => ok.Degeler()),
                    Utile.ExceptMsg(() => gelé.Geler()),
                    Utile.ExceptMsg(() => ok.Vider()),
                    Utile.ExceptMsg(() => gelé.Vider()),
                };
                foreach (var (excep, message) in erreurs)
                    Console.WriteLine($"\n {excep}\n {message ?? ""}");
            }
            break;
        case 'q':
            Environment.Exit(0); break;
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