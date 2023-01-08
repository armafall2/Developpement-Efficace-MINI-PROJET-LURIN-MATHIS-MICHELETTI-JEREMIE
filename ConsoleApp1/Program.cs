using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static string SaisieString(string titre)
        {
            Console.WriteLine(titre);
            string saisiDuCalcul;
            int compteurDeCaractere;
            int compteurDespace;
            do
            {
                Console.WriteLine("Veuillez saisir une chaîne de caractères avec les espaces :");
                saisiDuCalcul = Console.ReadLine();

                compteurDeCaractere = saisiDuCalcul.Length;
                compteurDespace = 0;

                for (int i = 0; i < saisiDuCalcul.Length; i++)
                {
                    char c = saisiDuCalcul[i];
                    if (c == ' ')
                    {
                        compteurDespace++;
                    }
                }
            }
            while (compteurDespace != (int)Math.Ceiling((double)compteurDeCaractere / 2) - 1);
            return saisiDuCalcul;

        }

        static string Formatage(ref string chaineAFormater)
        { 
            if (chaineAFormater == null)
            {
                Console.WriteLine("Erreur : Pas de chaîne a formater");
            }
            else
            {
                // Découper la chaîne de caractères en éléments séparés par des espaces
                string[] elements = chaineAFormater.Split(' ');

                // Vérifier si le deuxième élément est un opérateur
                if (((elements.Length > 1) && (elements[1] == "+")) || (elements[1] == "-") || (elements[1] == "*") || (elements[1] == "/"))
                {
                    // Intervertir la position de l'opérateur et de l'opérande suivant
                    string temp = elements[1];
                    elements[1] = elements[2];
                    elements[2] = temp;
                }

                // Reconstruire la chaîne de caractères à partir des éléments du tableau
                chaineAFormater = String.Join(" ", elements);
            }
            return chaineAFormater;
        }

        static void SwitchSurSymbole(char operateur, Stack<int> pileDeStockage)
        {

            int n1, n2;

            switch (operateur)
            {
                case '+':
                    n1 = pileDeStockage.Pop();
                    n2 = pileDeStockage.Pop();
                    pileDeStockage.Push(n1 + n2);
                    break;
                case '-':
                    n1 = pileDeStockage.Pop();
                    n2 = pileDeStockage.Pop();
                    pileDeStockage.Push(n1 - n2);
                    break;
                case '*':
                    n1 = pileDeStockage.Pop();
                    n2 = pileDeStockage.Pop();
                    pileDeStockage.Push(n1 * n2);
                    break;
                case '/':
                    n1 = pileDeStockage.Pop();
                    n2 = pileDeStockage.Pop();
                    pileDeStockage.Push(n1 / n2);
                    break;


            }
        }

        static int? CalculateRPN(string chaineACalculer, Stack<int> pileDeStockage)
        {
            int? result = null;
            if (string.IsNullOrEmpty(chaineACalculer) || pileDeStockage == null)
            {
                Console.WriteLine("Erreur : La chaîne ou la pile de stockage est nulle");
                return null;
            }
            else
            {
                if ((chaineACalculer != null) && (pileDeStockage != null))
                {
                    // Variable temporaire pour stocker les chiffres lors de la conversion en nombre
                    string variableTemporaireStockageChiffreConverti = "";
                    // Parcours de la chaîne de caractères
                    for (int i = 0; i < chaineACalculer.Length; i++)
                    {
                        char c = chaineACalculer[i];

                        // Si c'est un chiffre, ajout du chiffre à la chaîne temporaire
                        if (char.IsDigit(c))
                        {
                            variableTemporaireStockageChiffreConverti += c;
                        }
                        // Si c'est un opérateur ou un espace, on traite la chaîne temporaire comme un nombre s'il y en a un, et on traite l'opérateur ou l'espace
                        else
                        {
                            // Si la chaîne temporaire n'est pas vide, conversion en nombre et ajout à la pile
                            if (variableTemporaireStockageChiffreConverti != "")
                            {
                                pileDeStockage.Push(int.Parse(variableTemporaireStockageChiffreConverti));
                                variableTemporaireStockageChiffreConverti = "";
                            }
                            // Si c'est un opérateur, on récupère les deux derniers nombres de la pile et on effectue l'opération

                            SwitchSurSymbole(c, pileDeStockage);
                        }
                    }
                    // Si la chaîne temporaire n'est pas vide, conversion en nombre et ajout à la pile
                    if (variableTemporaireStockageChiffreConverti != "")
                    {
                        pileDeStockage.Push(int.Parse(variableTemporaireStockageChiffreConverti));
                    }

                    int nombreDelementDansLaPile = pileDeStockage.Count;


                    switch(nombreDelementDansLaPile)
                    {
                        case 0:
                            result = 0;
                            Console.WriteLine("Erreur : aucun opérande valide dans la chaîne de caractères");
                            break;

                        case 1:
                            result = pileDeStockage.Peek();
                            break;

                        default:
                            if (nombreDelementDansLaPile > 1)
                            {
                                Console.WriteLine("Erreur : chaîne de caractères non valide pour une notation RPN");
                                pileDeStockage.Clear();
                                result = null;
                            }
                            break;
                    }
                }
                
                return result;
            }
        }
        static void Main()
        {
           const string titre = "Calcul RPI : ";


            Stack<int> pileDeStockage = new Stack<int>();

            string calculRPI = SaisieString(titre);


            Console.WriteLine(Formatage(ref calculRPI));

           

          int? result =  CalculateRPN(calculRPI, pileDeStockage);

            if (result != null)
            {
                Console.WriteLine($"Le resultat du calcul en notatation postfixé '{calculRPI}' est {pileDeStockage.Peek()}");
            }
            else
            {
                Console.WriteLine("Erreur : résultat nul");
            }
            Console.ReadKey();

        }
    }
}
