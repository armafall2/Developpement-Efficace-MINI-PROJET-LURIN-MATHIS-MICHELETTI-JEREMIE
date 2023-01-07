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
            s = Console.ReadLine();
            return s;

        }

        static void Formatage(ref string s)
        {
            // Découper la chaîne de caractères en éléments séparés par des espaces
            string[] elements = s.Split(' ');

            // Vérifier si le deuxième élément est un opérateur
            if (elements.Length > 1 && elements[1] == "+" || elements[1] == "-" || elements[1] == "*" || elements[1] == "/")
            {
                // Intervertir la position de l'opérateur et de l'opérande suivant
                string temp = elements[1];
                elements[1] = elements[2];
                elements[2] = temp;
            }

            // Reconstruire la chaîne de caractères à partir des éléments du tableau
            s = String.Join(" ", elements);
        }



        static int CalculateRPN(string s, Stack<int> stack)
            {

                // Variable temporaire pour stocker les chiffres lors de la conversion en nombre
                string temp = "";
                // Parcours de la chaîne de caractères
                foreach (char c in s)
                {
                    // Si c'est un chiffre, ajout du chiffre à la chaîne temporaire
                    if (char.IsDigit(c))
                    {
                        temp += c;
                    }
                    // Si c'est un opérateur ou un espace, on traite la chaîne temporaire comme un nombre s'il y en a un, et on traite l'opérateur ou l'espace
                    else
                    {
                        // Si la chaîne temporaire n'est pas vide, conversion en nombre et ajout à la pile
                        if (temp != "")
                        {
                            stack.Push(int.Parse(temp));
                            temp = "";
                        }
                        // Si c'est un opérateur, on récupère les deux derniers nombres de la pile et on effectue l'opération
                        if (c == '+')
                        {
                            int n1 = stack.Pop();
                            int n2 = stack.Pop();
                            stack.Push(n1 + n2);
                        }
                        else if (c == '-')
                        {
                            int n1 = stack.Pop();
                            int n2 = stack.Pop();
                            stack.Push(n2 - n1);
                        }
                        else if (c == '*')
                        {
                            int n1 = stack.Pop();
                            int n2 = stack.Pop();
                            stack.Push(n1 * n2);
                        }
                        else if (c == '/')
                        {
                            int n1 = stack.Pop();
                            int n2 = stack.Pop();
                            stack.Push(n2 / n1);
                        }
                    }
                }
                // Si la chaîne temporaire n'est pas vide, conversion en nombre et ajout à la pile
                if (temp != "")
                {
                    stack.Push(int.Parse(temp));
                }

                // Si la pile ne contient qu'un seul élément, c'est le résultat du calcul
                if (stack.Count == 1)
                {
                    return stack.Peek();
                }
                // Si la pile contient plusieurs éléments, cela signifie que la chaîne de caractères ne respecte pas la notation RPN
                else
                {
                    // On affiche un message d'erreur et on vide la pile
                    if (stack.Count > 1)
                    {
                        Console.WriteLine("Erreur : chaîne de caractères non valide pour une notation RPN");
                        stack.Clear();
                    }
                    // Si la pile ne contient qu'un élément, c'est le résultat du calcul RPN
                    else if (stack.Count == 1)
                    {
                        return stack.Pop();
                    }
                    // Si la pile est vide, il n'y a pas eu de calcul à effectuer
                    else
                    {
                        return 0;
                    }
                return stack.Peek();
            }
            }

                    static void Main(string[] args)
        {
            string titre = "Calcul RPI : ";


            Stack<int> pileDeStockage = new Stack<int>();

            string calculRPI = SaisieString(titre);

            Formatage(ref calculRPI);

            CalculateRPN(calculRPI, pileDeStockage);

            Console.WriteLine($"Le resultat du calcul en notatation postfixé '{calculRPI}' est {pileDeStockage.Peek()}");

            Console.ReadKey();

        }
    }
}
