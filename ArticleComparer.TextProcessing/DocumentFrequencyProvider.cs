using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using ArticleComparer.Core.Processing;

namespace ArticleComparer.TextProcessing
{
    [DataContract]
    public sealed class DocumentFrequencyProvider : IDocumentFrequencyProvider
    {
        [DataMember] private readonly Dictionary<string, int> _tokenCountStorage = new Dictionary<string, int>();

        [DataMember] private int _corpusSize;

        public int CorpusSize
        {
            get { return _corpusSize; }
        }

        public int GeDocumentsWithTokenCount(IToken token)
        {
            if (token == null)
                throw new ArgumentNullException("token");

            if (_tokenCountStorage.ContainsKey(token.Text))
                return _tokenCountStorage[token.Text];
            return 0;
        }

        public void ProcessText(IEnumerable<IToken> text)
        {
            if (text == null)
                throw new ArgumentNullException("text");

            _corpusSize++;
            foreach (string word in text.Select(token => token.Text)
                                        .Distinct())
            {
                if (!_tokenCountStorage.ContainsKey(word))
                    _tokenCountStorage[word] = 0;
                _tokenCountStorage[word]++;
            }
        }
    }
}