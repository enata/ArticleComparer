using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArticleComparer.Core.Processing;

namespace ArticleComparer.TextProcessing
{
    public sealed class Tokenizer : ITokenizer
    {
        public IEnumerable<IToken> Tokenize(string text)
        {
            if (text == null)
                throw new ArgumentNullException("text");

            IEnumerable<string> splitText = Split(text);
            IEnumerable<Token> result = splitText.Select(st => new Token(st));
            return result;
        }

        private IEnumerable<string> Split(string text)
        {
            var result = new List<string>();
            var builder = new StringBuilder();
            foreach (char symbol in text)
            {
                if (char.IsLetterOrDigit(symbol))
                    builder.Append(char.ToLower(symbol));
                else if (builder.Length > 0)
                {
                    result.Add(builder.ToString());
                    builder.Clear();
                }
            }
            return result;
        }
    }
}