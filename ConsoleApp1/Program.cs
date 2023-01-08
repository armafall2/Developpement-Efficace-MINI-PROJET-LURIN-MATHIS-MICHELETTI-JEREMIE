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
            string s;

            do
            {
                s = Console.ReadLine();
            } while (s == " ");
 

            return s;

        }

        static void Formatage(ref string chaineAFormater)
        {
            if (chaineAFormater == null)
            {
                throw new ArgumentNullException("chaineAFormater");
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

            if (chaineACalculer == null)
            {
                Console.WriteLine("Erreur : La chaîne est nulle");
                return null;
            }
            else if (pileDeStockage == null)
            {
                Console.WriteLine("Erreur : Pas de Pile de Stockage");
                return null;
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

                // Si la pile ne contient qu'un seul élément, c'est le résultat du calcul
                if (pileDeStockage.Count == 1)
                {
                    return pileDeStockage.Peek();
                }
                // Si la pile contient plusieurs éléments, cela signifie que la chaîne de caractères ne respecte pas la notation RPN
                else
                {
                    // On affiche un message d'erreur et on vide la pile
                    if (pileDeStockage.Count > 1)
                    {
                        Console.WriteLine("Erreur : chaîne de caractères non valide pour une notation RPN");
                        pileDeStockage.Clear();
                        return null;
                    }
                    // Si la pile ne contient qu'un élément, c'est le résultat du calcul RPN
                    else if (pileDeStockage.Count == 1)
                    {
                        return pileDeStockage.Pop();
                    }
                    // Si la pile est vide, il n'y a pas eu de calcul à effectuer
                    else
                    {
                        return 0;
                    }
                    
                }
                
            }
            return pileDeStockage.Peek();
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
