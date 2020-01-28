using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Projet_final
{
    public abstract class Joueur
    {
        public abstract int Jouer(Position p);
        public virtual void NouvellePartie() { }
    }
}