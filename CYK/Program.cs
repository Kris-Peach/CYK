using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CYK
{
    class Program
    {

        static List<string> concat(List<string> rule1, List<string> rule2)
        {
            List<string> listRule = new List<string>();
            for (int i = 0; i < rule2.Count(); i++)
            {
                for (int j = 0; j < rule1.Count(); j++)
                {
                    string str = rule1[j] + rule2[i];
                    listRule.Add(str);
                }
            }
            return listRule;

        }
        static void Main(string[] args)
        {
            List<Grammar> Rules = new List<Grammar>();
            //Add rules grammar//
            Console.WriteLine("Введите полный путь файла:");
            string path = Console.ReadLine();
            string[] lines = System.IO.File.ReadAllLines(@path);
            string Word = lines[0];//Input word//
            int MatSize = Word.Length;
            for (int q = 1; q < lines.Length; q++)
            {
                string line = lines[q];
                string[] words = Regex.Split(line, @"->");
                Rules.Add(new Grammar(words[0], words[1]));
            }
            //Create matrix
            List<string>[,] Matrix = new List<string>[MatSize, MatSize];
            //Add last row matrix
            for (int i = 0; i < MatSize; i++)
            {
                List<string> tmp = new List<string>();
                foreach (Grammar gr in Rules)
                {
                    if (gr.Begotten.Contains(Word[i].ToString()))
                    {
                        tmp.Add(gr.Generator);
                    }
                }
                Matrix[MatSize - 1, i] = tmp;
            }
            int column = MatSize - 1; int k = 1;
            for (int j = MatSize - 2; j > -1; j--) 
            {
                for (int i = 0; i < column; i++)
                {
                    int row = MatSize - 1;
                    List<string> tmp_list = new List<string>();
                    List<string> newRules = new List<string>();
                    for (int w = 0; w < k; w++)//count new rules
                    {
                        newRules.AddRange(concat(Matrix[row - w, i], Matrix[row - k + 1 + w, 1 + i + w]));
                    }
                    for (int z = 0; z < newRules.Count; z++)
                    {
                        foreach (Grammar gr in Rules)
                        {
                            if (gr.Begotten.Contains(newRules[z]))
                            {
                                if (tmp_list.Contains(gr.Generator) == false)
                                    tmp_list.Add(gr.Generator);
                            }
                        }
                    }
                    Matrix[j, i] = tmp_list;
                }
                column--;
                k++;
            }
            if (Matrix[0, 0].Contains("S")) Console.WriteLine("Проверяемая последовательность принадлежит грамматике");
            else Console.WriteLine("Проверяемая последовательность не принадлежит грамматике");
            Console.WriteLine("Матрица:");
            //output matrix
            int cc = MatSize - 1;
            List<string> outlist = new List<string>();
            for (int j = 0; j < MatSize; j++)
            {
                for (int i = 0; i < MatSize; i++)
                {
                    if (Matrix[j, i] != null)
                    {
                        outlist = Matrix[j, i];
                        if (outlist.Count() == 0) Console.Write("0" + " ");
                        else
                        {
                            for (int t = 0; t < outlist.Count(); t++)
                            {
                                Console.Write(outlist[t]);
                            }
                            Console.Write(" ");
                        }
                    }
                    else
                    { break; }
                }
                Console.WriteLine();
            }
            Console.Read();
            return ;
        }
    }
}
