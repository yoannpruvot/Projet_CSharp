using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Projet_final
{
    class Program
    {

        static void Championnat(int nombre_joueurs)
        {
            int N = nombre_joueurs;
            JMCTS[] joueurs = new JMCTS[N];
            for (int i = 0; i < N; i++)
                joueurs[i] = new JMCTS(i+1, i+1, 100);
            PositionP4 position;
            Partie partie;
            int[] victoires = new int[N];
            Stopwatch watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (i!=j)
                    {
                        position = new PositionP4(true);
                        partie = new Partie(joueurs[i], joueurs[j], position);
                        partie.Commencer(false);
                        if(partie.r == Resultat.j0gagne)
                            victoires[j]++;
                        if(partie.r == Resultat.j1gagne)
                            victoires[i]++;
                    }
                }
                Console.Write($"{i} ");
            }
            watch.Stop();
            Console.WriteLine();
            Console.WriteLine("Time elapsed: {0}", watch.Elapsed);
            for (int i = 0; i < N; i++)
                Console.WriteLine($"Parametre a = {i+1}, nombre de victoires: {victoires[i]}.");
        }
        static void Championnatp(int nombre_joueurs)
        {
            int N = nombre_joueurs;
            JMCTSp[] joueurs = new JMCTSp[N];
            for (int i = 0; i < N; i++)
                joueurs[i] = new JMCTSp(i+1, i+1, 100, 4);
            PositionP4 position;
            Partie partie;
            int[] victoires = new int[N];
            Stopwatch watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (i!=j)
                    {
                        position = new PositionP4(true);
                        partie = new Partie(joueurs[i], joueurs[j], position);
                        partie.Commencer(false);
                        if(partie.r == Resultat.j0gagne)
                            victoires[j]++;
                        if(partie.r == Resultat.j1gagne)
                            victoires[i]++;
                    }
                }
                Console.Write($"{i} ");
            }
            watch.Stop();
            Console.WriteLine();
            Console.WriteLine("Time elapsed: {0}", watch.Elapsed);
            for (int i = 0; i < N; i++)
                Console.WriteLine($"Parametre a = {i+1}, nombre de victoires: {victoires[i]}.");
        }
        static void VersusP4( string Joueur1 = "Humain", string Joueur0 = "JMCTS", int NbParties = 1)
        {
            int a1 = 1;
            int a2 = 1;
            int temps = 100;
            int NbThread = 4;
            Joueur j1;
            Joueur j0;
            switch (Joueur1)
            {
                case "JMCTS":
                    j1 = new JMCTS(a1,a1,temps);
                    break;
                case "JMCTSp":
                    j1 = new JMCTSp(a1,a1,temps, NbThread);
                    break;
                case "JMCTSP":
                    j1 = new JMCTSP(a1,a1,temps, NbThread);
                    break;
                default :
                    j1 = new JoueurHumainPuissance();
                    break;
            }
            switch (Joueur0)
            {
                case "Humain":
                    j0 = new JoueurHumainPuissance();
                    break;
                case "JMCTSp":
                    j0 = new JMCTSp(a2,a2,temps, NbThread);
                    break;
                case "JMCTSP":
                    j0 = new JMCTSP(a2,a2,temps, NbThread);
                    break;
                default:
                    j0 = new JMCTS(a2,a2,temps);
                    break;
            }
            PositionP4 p;
            Partie partie;
            int score_j1=0;
            int score_j0=0;
            bool start = true;
            Console.WriteLine("Score : (J1 - J0)");
            for (int i = 0; i < NbParties; i++)
            {
                p = new PositionP4(start);
                partie = new Partie(j1, j0, p);
                partie.Commencer(true);
                switch (partie.r)
                {
                    case Resultat.j1gagne:
                        score_j1 ++;
                        break;
                    case Resultat.j0gagne:
                        score_j0 ++;
                        break;
                }
                start = !start;
                Console.WriteLine($"{score_j1} - {score_j0}");
            }
            Console.WriteLine($"Joueur1 : {Joueur1}({a1},{a1},{temps}) VS Joueur0 : {Joueur0}({a2},{a2},{temps}) sur {NbParties}.");
            Console.WriteLine($"Le joueur1 a gagné {score_j1} parties.");
            Console.WriteLine($"Le joueur0 a gagné {score_j0} parties.");

        }
        static void Main(string[] args)
        {
            //Championnat(50);
            //Championnatp(50);
            //VersusP4("JMCTS","Humain",1);
        }
    }
}
