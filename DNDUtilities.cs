using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SylDNDBot
{
    public static class DNDUtilities
    {
        public static string Roll(string input)
        {
            input = input.ToLower();
            
            StringBuilder builder = new StringBuilder();
            Random roller = new Random();

            Regex roll_regex = new Regex(
                "[1-9][0-9]*d[1-9][0-9]*(\\+[1-9][0-9]*[d][1-9][0-9]*|\\+[1-9][0-9]*)*");
            Match match = roll_regex.Match(input);
            if(match.Success)
            {
                string[] roll_pieces = input.Split('+');
                int[] rolls = new int[roll_pieces.Length];
                int roll_index = 0;
                foreach(string s in roll_pieces)
                {
                    if(s.Contains('d'))
                    {
                        int roll_sum = 0;

                        int die_count = int.Parse(s.Split('d')[0]);
                        int die_sides = int.Parse(s.Split('d')[1]);
                        for(; die_count > 0; die_count--)
                        {
                            int roll = roller.Next(0, die_sides) + 1;
                            roll_sum += roll;
                        }

                        rolls[roll_index] = roll_sum;
                        roll_index++;
                    }
                    else
                    {
                        int number = int.Parse(s);
                        rolls[roll_index] = number;
                        roll_index++;
                    }
                }

                builder.Append(string.Join("+", rolls));
                builder.Append($"={rolls.Sum()}");
            }

            return builder.ToString();
        }
    }
}
