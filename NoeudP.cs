using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Projet_final
{
    public class NoeudP
    {
        static Random gen = new Random();

        public Position p;
        public NoeudP pere;
        public NoeudP[] fils;
        public NoeudP(NoeudP pere, Position p, int nb_thread)
        {
            this.pere = pere;
            this.p = p;
            this.fils = new NoeudP[this.p.NbCoups];
            this.N = nb_thread;
            this.win = new int[nb_thread];
            this.cross = new int[nb_thread];
            for (int i = 0; i < N; i++)
            {
                this.win[i]=0;
                this.cross[i]=0;
            }
            this.indiceMeilleurFils = new int[nb_thread];
        }
        public int N;
        public int[] win, cross;
        public int tot_win
        {
            get
            {
                int tot_W = 0;
                for (int i = 0; i < this.N; i++)
                {
                    tot_W += win[i];
                }
                return tot_W;
            }
        }
        public int tot_cross
        {
            get
            {
                int tot_C = 0;
                for (int i = 0; i < this.N; i++)
                {
                    tot_C += cross[i];
                }
                return tot_C;
            }
        }
        public int[] indiceMeilleurFils;

        public void CalculMeilleurFils(Func<int, int, float> phi)
        {
            float s;
            float sM = 0;
            if (p.j1aletrait)
            {
                for (int i = 0; i < fils.Length; i++)
                {
                    if (fils[i] == null) { s = phi(0, 0); }
                    else { s = phi(this.tot_win, this.tot_cross); }
                    if (s > sM) { sM = s; indiceMeilleurFils[0] = i; }
                }
            }
            else
            {
                for (int i = 0; i < fils.Length; i++)
                {
                    if (fils[i] == null) { s = phi(0, 0); }
                    else { s = phi(this.tot_cross - this.tot_win, this.tot_cross); }
                    if (s > sM) { sM = s; indiceMeilleurFils[0] = i; }
                }
            }
        }
        public NoeudP MeilleurFils(int j)
        {
            if (fils[indiceMeilleurFils[j]] != null)
            {
                return fils[indiceMeilleurFils[j]];
            }
            Position q = p.Clone();
            q.EffectuerCoup(indiceMeilleurFils[j]);
            fils[indiceMeilleurFils[j]] = new NoeudP(this, q, this.N);
            return fils[indiceMeilleurFils[j]];
        }
        public override string ToString()
        {
            string s = "";
            s = s + "indice MF = " + indiceMeilleurFils;
            s += String.Format(" note= {0}\n", fils[indiceMeilleurFils[0]] == null ? "?" : ((1F * fils[indiceMeilleurFils[0]].tot_win) / fils[indiceMeilleurFils[0]].tot_cross).ToString());
            int sc = 0;
            for (int k = 0; k < fils.Length; k++)
            {
                if (fils[k] != null)
                {
                    sc += fils[k].tot_cross;
                    s += (fils[k].tot_win + "/" + fils[k].tot_cross + " ");
                }
                else s += (0 + "/" + 0 + " ");
            }
            s += "\n nbC=" + (sc / 2);
            return s;
        }

    }
}