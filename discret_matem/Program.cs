using System;
using System.Collections.Generic;
using System.Threading;

namespace discret_matem
{
    class Program
    {
        const char SKNF_simbol = 'v';
        const char SDNF_simbol = '^';
        const string Space = "   ";

        static void Main(string[] args)
        {
            bool isOpen = true;
            while (isOpen)
            {
                string read = "";
                Console.Write("Пример значения:\r\n'1001',\r\n'1 0 0 1'\r\nВведите значение: ");
                read = Console.ReadLine();
                read = Remove_trash(read,'1','0');

                if (read.Length == 0 || read.Length % 2 != 0)
                {
                    Console.WriteLine("//не верно введено значение//");
                    continue;
                }

                int l = read.Length;
                int n = Exp(l, 2);

                Console.WriteLine(main_text(n));

                for(int i = 0; i < l; i++)
                {
                    Console.WriteLine(Line(i, n) + Space + read[i] + Space + SKNF_value(Line(i, n) + read[i]) + Space + SDNF_value(Line(i, n) + read[i]));
                }

                Console.WriteLine("SKNF: " + SKNF_result(read));
                Console.WriteLine("SDNF: " + SDNF_result(read));

                Console.WriteLine("\r\n закрыть программу? \r\n нажмите Y/N");

                ConsoleKey key = ConsoleKey.Escape;
                int c = 0;
                while (true)
                {
                    key = Console.ReadKey().Key;
                    if (key == ConsoleKey.Y) { isOpen = false; break; }
                    else if (key == ConsoleKey.N) { Console.Clear(); break; }
                    else
                    {
                        switch (c)
                        {
                            case 0:
                                Console.WriteLine("\r\nнажмите 'Y' или 'N'");
                                break;
                            case 1:
                                Console.WriteLine("\r\nсложно попасть по нужной кнопке?!");
                                break;
                            case 2:
                                Console.WriteLine("\r\nты издеваешься надо мной?!");
                                break;
                            case 3:
                                Console.WriteLine("\r\nэто начинает надоедать...");
                                break;
                            case 4:
                                Console.WriteLine("\r\nой всё!");
                                Thread.Sleep(600);
                                break;
                        }
                        if (c == 4) { isOpen = false; break; }
                        c++;
                    }
                }
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

        static string format(string text, int length, char space = ' ')
        {
            string r = text;
            while (r.Length < length)
            {
                r = space + r;
            }
            return r;
        }

        static string main_text(int n)
        {
            string r = "";
            for (int i = 0; i < n; i++)
            {
                r += ((char)(65 + i)).ToString();
            }
            r += Space + "N" + Space + format("SKNF", n * 2 + n - 1) + Space + format("SDNF", n * 2 + n - 1);
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
            if (space_active) r = format(r, value.Length * 3 - 4);
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
            if (space_active) r = format(r, value.Length * 3 - 4);
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
