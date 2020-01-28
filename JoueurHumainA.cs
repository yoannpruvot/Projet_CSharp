using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Projet_final
{
    public class JoueurHumainA : Joueur
    {
        public override int Jouer(Position p)
        {
            int NbAllumettes = 0;
            do
            {
                Console.WriteLine("Combien d'allumettes voulez vous tirer?");
                NbAllumettes = int.Parse(Console.ReadLine());
            } while (NbAllumettes > 3 | NbAllumettes < 0 | NbAllumettes > p.NbCoups);
            return NbAllumettes - 1;
        }
        public override void NouvellePartie()
        {
            Console.WriteLine("Hello, Nouvelle Partie.");
        }
    }
}