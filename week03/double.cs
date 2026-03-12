using System;

public class Program
{
    public static void Main()
    {
        string input = Console.ReadLine(); // Lit l'entrée (ex: "1/3")
        string[] parts = input.Split('/');

        if (parts.Length == 2)
        {
            double numerateur = Convert.ToDouble(parts[0]);
            double denominateur = Convert.ToDouble(parts[1]);

            double resultat = numerateur / denominateur;
            Console.WriteLine(resultat);
        }
    }
}