using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Projet_final
{
    public class JMCTSP : Joueur
    {
        public Random[] gen;
        static Stopwatch sw = new Stopwatch();
        float a, b;
        int temps;
        List<Task> TaskList;
        NoeudP racine;
        int N;
        int[] re;
        public JMCTSP(float a, float b, int temps, int N)
        {
            this.a = 2 * a;
            this.b = 2 * b;
            this.temps = temps;
            this.N = N;
            this.re = new int[N];
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

        public void Calcul(NoeudP no, Func<int, int, float> phi, int i)
        {
            do // Sélection
            {
                no.CalculMeilleurFils(phi);
                no = no.MeilleurFils(i);
            } while (no.cross[i] > 0 && no.fils.Length > 0);


            re[i] = JeuHasard(no.p, i); // Simulation

            while (no != null) // Rétropropagation
            {
                no.cross[i] += 2;
                no.win[i] += re[i];
                no = no.pere;
            }
            Console.WriteLine("test");
        }

        public override int Jouer(Position p)
        {
            sw.Restart();
            Func<int, int, float> phi = (W, C) => (a + W) / (b + C);

            racine = new NoeudP(null, p, this.N);

            if(racine == null)
            {
                racine = new NoeudP(null, p, this.N);
            }
            else
            {
                racine = racine.MeilleurFils(0);
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
            while (sw.ElapsedMilliseconds < temps)
            {
                this.TaskList = new List<Task>();
                NoeudP no = racine;
                for (int i = 0; i < this.N; i++)
                {
                    int j = i;
                    this.TaskList.Add(Task.Run(() => Calcul(no, phi, j)));
                    iter++;
                }

                Task.WaitAll(this.TaskList.ToArray());
            }
            Console.WriteLine("{0} itérations", iter);
            Console.WriteLine(racine);
            racine.CalculMeilleurFils(phi);
            return racine.indiceMeilleurFils[0];
        }
        public override void NouvellePartie()
        {
            this.racine = null;
        }
    }
}