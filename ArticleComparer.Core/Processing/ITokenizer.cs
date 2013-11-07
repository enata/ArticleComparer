using System.Collections.Generic;

namespace ArticleComparer.Core.Processing
{
    public interface ITokenizer
    {
        IEnumerable<IToken> Tokenize(string text);
    }
}