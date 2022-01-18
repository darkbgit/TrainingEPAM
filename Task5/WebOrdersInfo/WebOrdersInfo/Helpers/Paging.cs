using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.WebUtilities;

namespace WebOrdersInfo.Helpers
{
    public class Paging
    {
        private readonly int _pageSize;
        private readonly int _pageNumber;
        private readonly string _url;

        private readonly int _maxPageCount;

        public Paging(int pageSize, int pageNumber, int total, string url)
        {
            _pageSize = pageSize;
            _pageNumber = pageNumber;
            _url = url;

            _maxPageCount = (int)Math.Ceiling((double)total / _pageSize);
        }

        public int[] GetPaging()
        {
            const int MAX_BEFORE = 5;
            const int MAX_AFTER = 5;

            var first = _pageNumber - MAX_BEFORE > 1 ? _pageNumber - MAX_BEFORE : 1;
            var last = _pageNumber + MAX_AFTER < _maxPageCount ? _pageNumber + MAX_AFTER : _maxPageCount;

            return Enumerable.Range(first, last).ToArray();
        }

        public bool IsCurrent(int i) => _pageNumber == i;
        public bool IsFirst => IsCurrent(1);
        public bool IsLast => IsCurrent(_maxPageCount);

        public string GetUrl(int i)
        {
            var uri = new Uri(_url);
            var baseUri = uri.GetComponents(UriComponents.Scheme |
                                                UriComponents.Port |
                                                UriComponents.Host |
                                                UriComponents.Path,
                                                UriFormat.UriEscaped);
            var query = QueryHelpers.ParseQuery(uri.Query);

            var items = query.SelectMany(x => x.Value,
                    (col, value) => new KeyValuePair<string, string>(col.Key, value))
                .ToList();
            items.RemoveAll(x => x.Key is "pageNumber" or "pageSize");

            var qb = new QueryBuilder(items);

            qb.Add("pageNumber", i.ToString());
            qb.Add("pageSize", _pageSize.ToString());

            var fullUri = baseUri + qb.ToQueryString();
            return fullUri;
        }

        public string FirstUrl => GetUrl(1);
        public string LastUrl => GetUrl(_maxPageCount);

        public string PrevUrl => GetUrl(_pageNumber - 1);
        public string NextUrl => GetUrl(_pageNumber + 1);

    }
}
