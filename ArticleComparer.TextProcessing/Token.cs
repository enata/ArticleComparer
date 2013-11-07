using System;
using ArticleComparer.Core.Processing;

namespace ArticleComparer.TextProcessing
{
    public sealed class Token : IToken
    {
        private readonly string _text;

        public Token(string text)
        {
            if (text == null)
                throw new ArgumentNullException("text");

            _text = text;
        }

        public string Text
        {
            get { return _text; }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return string.Equals(_text, ((Token) obj)._text);
        }

        public override int GetHashCode()
        {
            return _text.GetHashCode();
        }
    }
}