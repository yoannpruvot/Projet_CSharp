using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Projet_final
{
    public class PositionA : Position
    {
        public PositionA(bool j1aletrait) : base(j1aletrait)
        {
            this.NbCoups = 3;
            this.Eval = Resultat.indetermine;
            this.reste_allumettes = 21;
        }
        public int reste_allumettes { get; protected set; }

        public override void EffectuerCoup(int i)
        {
            this.reste_allumettes -= i + 1;
            if (this.reste_allumettes == 0)
            {
                if (j1aletrait)
                {
                    this.Eval = Resultat.j0gagne;
                    this.NbCoups = 0;
                }
                else
                {
                    this.Eval = Resultat.j1gagne;
                    this.NbCoups = 0;
                }
            }
            if (this.reste_allumettes <= this.NbCoups)
            {
                this.NbCoups = this.reste_allumettes;
            }
            this.j1aletrait = !this.j1aletrait;
        }
        public override Position Clone()
        {
            PositionA clone = new PositionA(this.j1aletrait);
            clone.Eval = this.Eval;
            clone.NbCoups = this.NbCoups;
            clone.reste_allumettes = this.reste_allumettes;
            return clone;
        }
        public override void Affiche()
        {
            if (j1aletrait)
            {
                Console.WriteLine($"Le joueur 1 a la main et il reste {this.reste_allumettes} allumettes.");
            }
            else
            {
                Console.WriteLine($"Le joueur 0 a la main et il reste {this.reste_allumettes} allumettes.");
            }
        }
        public override bool Equals(Object obj)
        {
            if ((obj == null) || ! this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            PositionA position = (PositionA) obj;
            bool equal = (this.NbCoups == position.NbCoups) && (this.Eval == position.Eval) && (this.j1aletrait == position.j1aletrait) && (this.reste_allumettes == position.reste_allumettes);
            return equal;
        }
        public override int GetHashCode()
        {
            return this.NbCoups;
        }
    }
}