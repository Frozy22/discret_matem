using System;
using System.Collections.Generic;

namespace discret_matem
{
    class Program
    {
        const char SKNF_simbol = 'v';
        const char SDNF_simbol = '^';

        static void Main(string[] args)
        {
            bool isOpen = true;
            while (isOpen)
            {
                string read = "";
                read = Console.ReadLine();
                read = Remove_trash(read,'1','0');

                if (read.Length == 0 || read.Length % 2 != 0)
                {
                    Console.WriteLine("//не верно введено значение//");
                    continue;
                }

                int l = read.Length;
                int n = Exp(l, 2);

                for(int i = 0; i < l; i++)
                {
                    Console.Write(Line(i, n));
                    Console.Write("  " + read[i]);
                    Console.Write("  " + SKNF_value(Line(i,n) + read[i]));
                    Console.Write("  " + SDNF_value(Line(i, n) + read[i]));
                    Console.WriteLine("");
                }

                Console.WriteLine(SKNF_result(read));
                Console.WriteLine(SDNF_result(read));
            }
        }

        static string Line(int line, int length)
        {
            string r = Convert.ToString(line, 2);
            while (r.Length < length)
            {
                r = "0" + r;
            }
            return r;
        }

        static string SKNF_value(string value, bool space_active = true)
        {
            string r = "";
            for(int i = 0; i < value.Length-1; i++)
            {
                if (value[value.Length - 1] == '0')
                { 
                    r += value[i] == '0' ? ((char)(65 + i)).ToString() : "-" + ((char)(65 + i)).ToString(); 
                    if (i < value.Length - 2) r += SKNF_simbol;
                }
            }
            if(space_active) while (r.Length < value.Length*2-1)
            {
                r = " " + r;
            }
            return r;
        }

        static string SKNF_result(string values)
        {
            string r = "";
            string line = "";
            int l = values.Length;
            int n = Exp(l, 2);
            for (int i = 0; i < l; i++)
            {
                line = SKNF_value(Line(i,n) + values[i], false);
                if (r.Length > 0 && i < l - 1 && r[r.Length-1] != SDNF_simbol) r += SDNF_simbol;
                if (line.Contains('A')) r += "(" + line + ")";
            }
            if (r[r.Length - 1] == SDNF_simbol) r = r.Remove(r.Length - 1, 1);
            return r;
        }

        static string SDNF_value(string value, bool space_active = true)
        {
            string r = "";
            for (int i = 0; i < value.Length - 1; i++)
            {
                if (value[value.Length - 1] == '1')
                {
                    r += value[i] == '1' ? ((char)(65 + i)).ToString() : "-" + ((char)(65 + i)).ToString();
                    if (i < value.Length - 2) r += SDNF_simbol;
                }
            }
            if (space_active) while (r.Length < value.Length * 2 - 1)
            {
                r = " " + r;
            }
            return r;
        }

        static string SDNF_result(string values)
        {
            string r = "";
            string line = "";
            int l = values.Length;
            int n = Exp(l, 2);
            for (int i = 0; i < l; i++)
            {
                line = SDNF_value(Line(i, n) + values[i], false);
                if (r.Length > 0 && i < l - 1 && r[r.Length - 1] != SKNF_simbol) r += SKNF_simbol;
                if (line.Contains('A')) r += "(" + line + ")";
            }
            if (r[r.Length - 1] == SKNF_simbol) r = r.Remove(r.Length - 1, 1);
            return r;
        }

        static int Exp(int value, int main)
        {
            int r = 1;
            while (value > main)
            {
                r++;
                value /= main;
            }
            return r;
        }

        static string Remove_trash(string value, params char[] save)
        {
            string r = "";
            foreach (var v in value)
            {
                foreach (var c in save)
                {
                    if (v == c) r += c;
                }
            }
            return r;
        }
    }
}
