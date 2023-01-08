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
            string input;
            int characterCount;
            int spaceCount;
            do
            {
                Console.WriteLine("Veuillez saisir une chaîne de caractères avec les espaces :");
                input = Console.ReadLine();

                characterCount = input.Length;
                spaceCount = 0;

                foreach (char c in input)
                {
                    if (c == ' ')
                    {
                        spaceCount++;
                    }
                }
            }
            while (spaceCount != (int)Math.Ceiling((double)characterCount / 2) - 1);


            return input;

        }

        static void Formatage(ref string chaineAFormater)
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
                if ((elements.Length > 1) && (elements[1] == "+") || (elements[1] == "-") || (elements[1] == "*") || (elements[1] == "/"))
                {
                    // Intervertir la position de l'opérateur et de l'opérande suivant
                    string temp = elements[1];
                    elements[1] = elements[2];
                    elements[2] = temp;
                }

                // Reconstruire la chaîne de caractères à partir des éléments du tableau
                chaineAFormater = String.Join(" ", elements);
            }
        }

        static int? CalculateRPN(string chaineACalculer, Stack<int> pileDeStockage)
        {
            int? result = null;

            if (chaineACalculer == null)
            {
                Console.WriteLine("Erreur : La chaîne est nulle");
                result = null;
            }
            else if (pileDeStockage == null)
            {
                Console.WriteLine("Erreur : Pas de Pile de Stockage");
                result = null;
            }
            else if ((chaineACalculer != null) && (pileDeStockage != null))
            {

                // Variable temporaire pour stocker les chiffres lors de la conversion en nombre
                string variableTemporaireStockageChiffreConverti = "";
                // Parcours de la chaîne de caractères
                foreach (char c in chaineACalculer)
                {
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

                        switch (c)
                        {
                            case '+':
                                int n1 = pileDeStockage.Pop();
                                int n2 = pileDeStockage.Pop();
                                pileDeStockage.Push(n1 + n2);
                                break;
                            case '-':
                                int n3 = pileDeStockage.Pop();
                                int n4 = pileDeStockage.Pop();
                                pileDeStockage.Push(n3 - n4);

                                break;
                            case '*':
                                int n5 = pileDeStockage.Pop();
                                int n6 = pileDeStockage.Pop();
                                pileDeStockage.Push(n5 * n6);
                                break;
                            case '/':
                                int n7 = pileDeStockage.Pop();
                                int n8 = pileDeStockage.Pop();
                                pileDeStockage.Push(n7 / n8);
                                break;

                        }
                    }
                }
                // Si la chaîne temporaire n'est pas vide, conversion en nombre et ajout à la pile
                if (variableTemporaireStockageChiffreConverti != "")
                {
                    pileDeStockage.Push(int.Parse(variableTemporaireStockageChiffreConverti));
                }

                int nombreDelementDansLaPile = pileDeStockage.Count;
                

                switch (nombreDelementDansLaPile)
                {
                    case 0:
                        result = 0;
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

        static void Main(string[] args)
        {
           const string titre = "Calcul RPI : ";


            Stack<int> pileDeStockage = new Stack<int>();

            string calculRPI = SaisieString(titre);

            Formatage(ref calculRPI);

            Console.WriteLine(calculRPI);

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
