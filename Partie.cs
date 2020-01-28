using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Projet_final
{
    public class Partie
    {
        public Position pCourante;
        Joueur j1, j0;
        public Resultat r;

        public Partie(Joueur j1, Joueur j0, Position pInitiale)
        {
            this.j1 = j1;
            this.j0 = j0;
            pCourante = pInitiale.Clone();
        }

        public void NouveauMatch(Position pInitiale)
        {
            pCourante = pInitiale.Clone();
        }

        public void Commencer(bool affichage = true)
        {
            j1.NouvellePartie();
            j0.NouvellePartie();
            do
            {
                if (affichage) pCourante.Affiche();
                if (pCourante.j1aletrait)
                {
                    pCourante.EffectuerCoup(j1.Jouer(pCourante.Clone()));
                }
                else
                {
                    pCourante.EffectuerCoup(j0.Jouer(pCourante.Clone()));
                }
            } while (pCourante.NbCoups > 0);
            r = pCourante.Eval;
            if (affichage)
            {
                pCourante.Affiche();
                switch (r)
                {
                    case Resultat.j1gagne: Console.WriteLine("j1 {0} a gagné.", j1); break;
                    case Resultat.j0gagne: Console.WriteLine("j0 {0} a gagné.", j0); break;
                    case Resultat.partieNulle: Console.WriteLine("Partie nulle."); break;
                }
            }
        }
    }
}