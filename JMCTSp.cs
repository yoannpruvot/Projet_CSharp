
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Projet_final
{
    public class JMCTSp : Joueur
    {
        private Random[] gen;
        static Stopwatch sw = new Stopwatch();
        private Object verrou = new Object();

        float a, b;
        int temps;
        List<Task<int>> TaskList;
        Noeud racine;
        int N;
        public JMCTSp(float a, float b, int temps, int N)
        {
            this.a = 2 * a;
            this.b = 2 * b;
            this.temps = temps;
            this.N = N;
            this.gen = new Random[N];
            for (int i = 0; i < N; i++)
            {
                this.gen[i] = new Random();
            }
        }

        public override string ToString()
        {
            return string.Format("JMCTS[{0} - {1} - temps={2}]", a / 2, b / 2, temps);
        }

        int JeuHasard(Position p, int i)
        {
            Position q = p.Clone();
            int re = 1;
            while (q.NbCoups > 0)
            {
                    q.EffectuerCoup(gen[i].Next(0, q.NbCoups));
            }
            if (q.Eval == Resultat.j1gagne) { re = 2; }
            if (q.Eval == Resultat.j0gagne) { re = 0; }
            return re;
        }

        public override int Jouer(Position p)
        {
            sw.Restart();
            Func<int, int, float> phi = (W, C) => (a + W) / (b + C);
            if(racine == null)
            {
                racine = new Noeud(null, p);
            }
            else
            {
                racine = racine.MeilleurFils();
                for (int i = 0; i < racine.p.NbCoups; i++)
                {
                    if ((racine.fils[i]!=null) && (racine.fils[i].p.Equals(p)))
                    {
                        racine = racine.fils[i];
                        break;
                    }
                }
            }

            int iter = 0;
            int totale_re;
            while (sw.ElapsedMilliseconds < temps)
            {
                this.TaskList = new List<Task<int>>();
                totale_re = 0;
                Noeud no = racine;

                do // Sélection
                {
                    no.CalculMeilleurFils(phi);
                    no = no.MeilleurFils();

                } while (no.cross > 0 && no.fils.Length > 0);


                for (int i = 0; i < this.N; i++)
                {
                    int j = i;
                    TaskList.Add(Task.Run(() => JeuHasard(no.p, j)));
                }
                Task.WaitAll(TaskList.ToArray());
                for (int i = 0; i < TaskList.Count; i++)
                {
                    totale_re += TaskList[i].Result;
                }

                while (no != null) // Rétropropagation
                {
                    no.cross += this.N * 2;
                    no.win += totale_re;
                    no = no.pere;
                }

                iter++;
            }
            racine.CalculMeilleurFils(phi);
            Console.WriteLine("{0} itérations", iter);
            Console.WriteLine(racine);
            return racine.indiceMeilleurFils;
        }
        public override void NouvellePartie()
        {
            this.racine = null;
        }
    }
}