using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Projet_final
{
    public class JoueurHumainPuissance : Joueur
    {
        public override int Jouer(Position p)
        {
            int colone = 0;
            do
            {
                Console.WriteLine("Dans quelle colonne voulez-vous jouer?"); // Il faut entrer la i-eme colonne possible dans laquelle le joueur veut jouer.
                colone = int.Parse(Console.ReadLine());
            } while (colone < 0 && colone >= p.NbCoups);
            return colone - 1;
        }
        public override void NouvellePartie()
        {
            Console.WriteLine("Hello, Nouvelle Partie.");
        }
    }
}