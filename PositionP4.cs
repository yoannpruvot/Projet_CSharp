using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Projet_final
{
    public class PositionP4 : Position
    {
        public PositionP4(bool j1aletrait) : base(j1aletrait)
        {
            this.NbCoups = 7;
            this.Eval = Resultat.indetermine;
            this.etat_jeu = new Char[6, 7];
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    this.etat_jeu[i,j] = ' ';
                }
            }
        }
        public Char[,] etat_jeu;
        public override void EffectuerCoup(int j)
        {
            bool full = (this.etat_jeu[0, j] != ' ');
            for (int temp = 0; temp < 7; temp++)
            {
                full = (this.etat_jeu[0, temp] != ' ');
                if (full && temp <= j)
                    j++;
            }
            int ligne = 0;
            for (int i = 5; i >= 0; i--)
            {
                if (this.etat_jeu[i, j] == ' ')
                {
                    this.etat_jeu[i, j] = this.j1aletrait ? 'X' : 'O';
                    ligne = i;
                    if (ligne == 0)
                        this.NbCoups--;
                    break;
                }
            }
            this.verif_ligne(ligne);
            this.verif_diag(ligne, j);
            this.verif_colone(j);
            this.verif_nul();
            this.j1aletrait = !this.j1aletrait;
        }
        public void verif_diag(int ligne, int colone)
        {
            int i = ligne;
            int j = colone;
            int aligne = 1;
            while ((i != 0) && (j != 0))
            {
                i--;
                j--;
            }
            while ((i < 5) && (j < 6))
            {
                if (this.etat_jeu[i + 1, j + 1] == this.etat_jeu[i, j] && this.etat_jeu[i, j] != ' ')
                    aligne++;
                else
                    aligne = 1;
                j++;
                i++;
                if (aligne == 4)
                {
                    if (this.j1aletrait)
                        this.Eval = Resultat.j1gagne;
                    else
                        this.Eval = Resultat.j0gagne;
                    this.NbCoups = 0;
                    return;
                }
            }

            i = ligne;
            j = colone;
            aligne = 1;
            while ((i != 5) && (j != 0))
            {
                i++;
                j--;
            }
            while ((i > 0) && (j < 6))
            {
                if (this.etat_jeu[i - 1, j + 1] == this.etat_jeu[i, j] && this.etat_jeu[i, j] != ' ')
                    aligne++;
                else
                    aligne = 1;
                i--;
                j++;
                if (aligne == 4)
                {
                    if (this.j1aletrait)
                        this.Eval = Resultat.j1gagne;
                    else
                        this.Eval = Resultat.j0gagne;
                    this.NbCoups = 0;
                    break;
                }
            }
        }
        public void verif_ligne(int ligne)
        {
            int i = ligne;
            int aligne = 1;
            for (int j = 0; j < 6; j++)
            {
                if (this.etat_jeu[i, j] == this.etat_jeu[i, j + 1] && this.etat_jeu[i, j] != ' ')
                    aligne++;
                else
                    aligne = 1;
                if (aligne == 4)
                {
                    if (this.j1aletrait)
                        this.Eval = Resultat.j1gagne;
                    else
                        this.Eval = Resultat.j0gagne;
                    this.NbCoups = 0;
                    return;
                }
            }
        }
        public void verif_colone(int colone)
        {
            int j = colone;
            int aligne = 1;
            for (int i = 0; i < 5; i++)
            {
                if (this.etat_jeu[i, j] == this.etat_jeu[i + 1, j] && this.etat_jeu[i, j] != ' ')
                    aligne++;
                else
                    aligne = 1;
                if (aligne == 4)
                {
                    if (this.j1aletrait)
                        this.Eval = Resultat.j1gagne;
                    else
                        this.Eval = Resultat.j0gagne;
                    this.NbCoups = 0;
                    return;
                }
            }
        }
        public void verif_nul()
        {
            for (int j = 0; j < 7; j++)
            {
                if (this.etat_jeu[0, j] == ' ')
                    return;
            }
            if (this.Eval == Resultat.indetermine)
            {
                this.Eval = Resultat.partieNulle;
                this.NbCoups = 0;
            }
        }
        public override Position Clone()
        {
            PositionP4 clone = new PositionP4(this.j1aletrait);
            clone.Eval = this.Eval;
            clone.NbCoups = this.NbCoups;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    clone.etat_jeu[i, j] = this.etat_jeu[i, j];
                }
            }
            return clone;
        }
        public override void Affiche()
        {
            if (j1aletrait)
                Console.WriteLine($"Le joueur 1 a la main et il reste {this.NbCoups} coups possible.");
            else
                Console.WriteLine($"Le joueur 0 a la main et il reste {this.NbCoups} coups possible.");
            for (int i = 0; i < 6; i++)
            {
                Console.Write('|');
                for (int j = 0; j < 7; j++)
                {
                    Console.Write($"{this.etat_jeu[i, j]}|");
                }
                Console.WriteLine("");
            }
        }
        public override bool Equals(Object obj)
        {
            if ((obj == null) || ! this.GetType().Equals(obj.GetType()))
                return false;
            PositionP4 position = (PositionP4) obj;
            bool equal = (this.NbCoups == position.NbCoups) && (this.Eval == position.Eval) && (this.j1aletrait == position.j1aletrait);
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (this.etat_jeu[i,j] != position.etat_jeu[i,j])
                    {
                        equal = false;
                        break;
                    }
                }
                if (!equal)
                    break;
            }
            return equal;
        }
        public override int GetHashCode()
        {
            return this.NbCoups;
        }
    }
}