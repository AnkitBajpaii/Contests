using System;
using System.Collections.Generic;

namespace Encoder
{
    public interface IEncodingRuleEngine
    {
        char[] GenerateRules();
    }

    public class EncodingRuleEngine : IEncodingRuleEngine
    {
        public char[] GenerateRules()
        {
            char[] rulesMap = new char[8483];

            for (int i = 0; i <= 64; i++)
            {
                rulesMap[i] = (char)i;

                if ((char)i == ' ')
                {
                    rulesMap[i] = 'y';
                }
            }

            for (int i = 91; i <= 96; i++)
            {
                rulesMap[i] = (char)i;
            }

            for (int i = 123; i <= 255; i++)
            {
                rulesMap[i] = (char)i;
            }

            for (int i = 338; i <= 402; i++)
            {
                rulesMap[i] = (char)i;
            }

            for (int i = 8211; i <= 8482; i++)
            {
                rulesMap[i] = (char)i;
            }

            Dictionary<char, char> vowelsMap = new Dictionary<char, char>();
            vowelsMap.Add('a', '1');
            vowelsMap.Add('e', '2');
            vowelsMap.Add('i', '3');
            vowelsMap.Add('o', '4');
            vowelsMap.Add('u', '5');

            foreach (var kvp in vowelsMap)
            {
                rulesMap[kvp.Key] = kvp.Value;
            }

            for (char ch = 'a'; ch <= 'z'; ch++)
            {
                if (!vowelsMap.ContainsKey(ch))
                {
                    rulesMap[ch] = (char)(ch - 1);

                    if (ch == 'y')
                    {
                        rulesMap[ch] = ' ';
                    }
                }
            }

            return rulesMap;
        }

    }
    public class EncoderProcessor
    {
        private readonly IEncodingRuleEngine _encodingRuleEngine;

        public EncoderProcessor(IEncodingRuleEngine encodingRuleEngine)
        {
            _encodingRuleEngine = encodingRuleEngine;
        }

        public string Encode(string message)
        {
            var rulesMap = _encodingRuleEngine.GenerateRules();

            char[] input = message.ToLower().ToCharArray();
            Stack<char> stack = new Stack<char>();

            int i = 0;
            int curr = 0;
            while (i < input.Length)
            {
                char ch = input[i];
                if (char.IsDigit(ch))
                {
                    stack.Push(ch);
                }
                else
                {
                    while (stack.Count > 0)
                    {
                        input[curr++] = stack.Pop();
                    }

                    input[curr++] = rulesMap[ch];
                }

                i++;
            }

            while (stack.Count > 0 && curr < input.Length)
            {
                input[curr++] = stack.Pop();
            }

            return new String(input);
        }
    }
}