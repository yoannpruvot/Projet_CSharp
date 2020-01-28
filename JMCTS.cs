using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Projet_final
{
    public class JMCTS : Joueur
    {
        public static Random gen = new Random();
        static Stopwatch sw = new Stopwatch();
        float a, b;
        int temps;
        Noeud racine;
        public JMCTS(float a, float b, int temps)
        {
            this.a = 2 * a;
            this.b = 2 * b;
            this.temps = temps;
        }
        public override string ToString()
        {
            return string.Format("JMCTS[{0} - {1} - temps={2}]", a / 2, b / 2, temps);
        }

        int JeuHasard(Position p)
        {
            Position q = p.Clone();
            int re = 1;
            while (q.NbCoups > 0)
            {
                q.EffectuerCoup(gen.Next(0, q.NbCoups));
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
            while (sw.ElapsedMilliseconds < temps)
            {
                Noeud no = racine;

                do // Sélection
                {
                    no.CalculMeilleurFils(phi);
                    no = no.MeilleurFils();
                } while (no.cross > 0 && no.fils.Length > 0);


                int re = JeuHasard(no.p); // Simulation

                while (no != null) // Rétropropagation
                {
                    no.cross += 2;
                    no.win += re;
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