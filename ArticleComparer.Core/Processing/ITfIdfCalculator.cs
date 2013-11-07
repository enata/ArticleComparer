using System.Collections.Generic;
using ArticleComparer.Core.Implementation;

namespace ArticleComparer.Core.Processing
{
    public interface ITfIdfCalculator
    {
        Dictionary<IToken, TfIdf> Calculate(IEnumerable<IToken> tokenizedText);
    }
}