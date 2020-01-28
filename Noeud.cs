using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Projet_final
{
    public class Noeud
    {
        static Random gen = new Random();

        public Position p;
        public Noeud pere;
        public Noeud[] fils;
        public int cross, win;
        public int indiceMeilleurFils;

        public Noeud(Noeud pere, Position p)
        {
            this.pere = pere;
            this.p = p;
            fils = new Noeud[this.p.NbCoups];
        }

        public void CalculMeilleurFils(Func<int, int, float> phi)
        {
            float s;
            float sM = 0;
            if (p.j1aletrait)
            {
                for (int i = 0; i < fils.Length; i++)
                {
                    if (fils[i] == null) { s = phi(0, 0); }
                    else { s = phi(fils[i].win, fils[i].cross); }
                    if (s > sM) { sM = s; indiceMeilleurFils = i; }
                }
            }
            else
            {
                for (int i = 0; i < fils.Length; i++)
                {
                    if (fils[i] == null) { s = phi(0, 0); }
                    else { s = phi(fils[i].cross - fils[i].win, fils[i].cross); }
                    if (s > sM) { sM = s; indiceMeilleurFils = i; }
                }
            }
        }

        public Noeud MeilleurFils()
        {
            if (fils[indiceMeilleurFils] != null)
            {
                return fils[indiceMeilleurFils];
            }
            Position q = p.Clone();
            q.EffectuerCoup(indiceMeilleurFils);
            fils[indiceMeilleurFils] = new Noeud(this, q);
            return fils[indiceMeilleurFils];
        }

        public override string ToString()
        {
            string s = "";
            s = s + "indice MF = " + indiceMeilleurFils;
            s += String.Format(" note= {0}\n", fils[indiceMeilleurFils] == null ? "?" : ((1F * fils[indiceMeilleurFils].win) / fils[indiceMeilleurFils].cross).ToString());
            int sc = 0;
            for (int k = 0; k < fils.Length; k++)
            {
                if (fils[k] != null)
                {
                    sc += fils[k].cross;
                    s += (fils[k].win + "/" + fils[k].cross + " ");
                }
                else s += (0 + "/" + 0 + " ");
            }
            s += "\n nbC=" + (sc / 2);
            return s;
        }
    }

}