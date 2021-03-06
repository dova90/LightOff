﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LightOff
{
    class Program
    {

        static void SetMap(int[,] field)
        {
            Random random = new Random();

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    field[i, j] = random.Next(0, 2);
                    if (field[i, j] == 0)
                        field[i, j]--;
                }
            }
        }

        static void Show(int[,] field)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (field[i, j] == 1)
                        Console.Write(field[i, j]);
                    else
                        Console.Write("0");
                }
                Console.Write("\n");
            }
        }
        static void Change(int[,] field, int x, int y)
        {
            field[x, y] *= -1;
            if (y < 9) field[x, y + 1] *= -1;
            if (x < 9) field[x + 1, y] *= -1;
            if (x > 0) field[x - 1, y] *= -1;
            if (y > 0) field[x, y - 1] *= -1;

        }
        static bool Win(int[,] field)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (field[i, j] == 1)
                        return true;
                }
            }
            return false;
        }
        static void ReverseHistory(int[,] field, int reverse, List<int> xHistory, List<int> yHistory,ref int count)
        {
            int maks =-1+ xHistory.Count;
            count -= reverse;
            for (int i=0; i<reverse; i++)
            {
                int index = maks - i;
                Change(field, xHistory[index], yHistory[index]);
                xHistory.RemoveAt(index);
                yHistory.RemoveAt(index);
            }
        }
        static bool CorrectData(int reverse, int count)
        {
            if (reverse > count)
            {
                Console.Write("bledne dane jeszcze raz");
                return true;
            }
            else
                return false;
        }

        static void Main(string[] args)
        {
            
            int[,] field = new int[10, 10];
            int Record = 32000;
            string Game;
            bool Game2=true;

            do
            {
                List<int> xHistory = new List<int>();
                List<int> yHistory = new List<int>();

                int count = 0;
                SetMap(field);
                Show(field);

                int x = 0;
                int y = 0;
                int chan;
                int reverse = 0;

                do
                {
                    Console.Write("Cofasz ruchy czy gramy dalej? [c/g]");
                    string end = Console.ReadLine();
                    if (end.Equals("c"))
                    {
                        Console.Write("ile ruchow cofamy?\t");
                        do
                        {


                            try
                            {
                                reverse = int.Parse(Console.ReadLine());

                            }
                            catch (FormatException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        } while (CorrectData(reverse, count));
                        
                        ReverseHistory(field, reverse, xHistory, yHistory,ref count);
                        Console.Clear();
                        Show(field);
                        Console.WriteLine(count);
                    }






                    do
                    {
                        //Console.WriteLine("--------------------------------");
                        try
                        {


                            x = int.Parse(Console.ReadLine());
                            y = int.Parse(Console.ReadLine());
                        } catch (FormatException e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        if (x < 10 && y < 10)
                            chan = 1;
                        else
                        {
                            chan = 0;
                            Console.WriteLine("Error zle dane");
                        }
                    } while (chan != 1);

                    Change(field, x, y);
                    count++;

                    xHistory.Add(x);
                    yHistory.Add(y);
                    Console.Clear();
                    Show(field);
                    Console.WriteLine("Liczba wykonanych ruchow : {0}", count);

                } while (Win(field));
                if (!Win(field))
                {
                    Console.WriteLine("Gratulacje wygrales!!!");
                    if(Record>count)
                    {
                        Record = count;
                        Console.WriteLine("Podwojne gratulacje pobiles rekord");
                    }
                }
                Console.Write("gramy dalej czy nie? [g/n]");
                Game = Console.ReadLine();
                if (Game.Equals("n"))
                    Game2 = false;

            } while(Game2);

            Console.Read();


        }
    }
}
