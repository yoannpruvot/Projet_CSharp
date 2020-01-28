using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Projet_final
{
    public enum Resultat { j1gagne, j0gagne, partieNulle, indetermine }

    public abstract class Position
    {
        public bool j1aletrait;
        public Position(bool j1aletrait) { this.j1aletrait = j1aletrait; }
        public Resultat Eval { get; protected set; }
        public int NbCoups { get; protected set; }
        public abstract void EffectuerCoup(int i);
        public abstract Position Clone();
        public abstract void Affiche();
    }

}