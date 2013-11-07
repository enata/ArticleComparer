using System;
using ArticleComparer.Core.Processing;

namespace ArticleComparer.Core.Implementation
{
    public sealed class TfIdf
    {
        private readonly IToken _token;
        private readonly double _value;

        public TfIdf(double value, IToken token)
        {
            if (token == null)
                throw new ArgumentNullException("token");
            if (value < 0)
                throw new ArgumentException("Value should be >= 0", "value");

            _value = value;
            _token = token;
        }

        public double Value
        {
            get { return _value; }
        }

        public IToken Token
        {
            get { return _token; }
        }
    }
}