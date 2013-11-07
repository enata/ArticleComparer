using System;
using ArticleComparer.Core.Processing;

namespace ArticleComparer.Core.Implementation
{
    public sealed class Article : IArticle
    {
        private readonly string _text;

        public Article(string text)
        {
            if (text == null)
                throw new ArgumentNullException();

            _text = text;
        }

        public string Text
        {
            get { return _text; }
        }
    }
}