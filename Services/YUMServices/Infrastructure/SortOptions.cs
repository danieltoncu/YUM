using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.YUMServices.Infrastructure
{
    class SortOptions<T> where T : class
    {
        private readonly Dictionary<string, KeyValuePair<string, Func<IQueryable<T>, IOrderedQueryable<T>>>> _options;

        private readonly Func<IQueryable<T>, IOrderedQueryable<T>> _defaultOption;

        public SortOptions(Func<IQueryable<T>, IOrderedQueryable<T>> defaultOption)
        {
            _options = new Dictionary<string, KeyValuePair<string, Func<IQueryable<T>, IOrderedQueryable<T>>>>();

            _defaultOption = defaultOption;
        }

        public void AddOption(string optionId, string optionName, Func<IQueryable<T>, IOrderedQueryable<T>> sortFunction)
        {
            _options.Add(optionId,
                new KeyValuePair<string, Func<IQueryable<T>, IOrderedQueryable<T>>>(optionName, sortFunction));
        }

        public Func<IQueryable<T>, IOrderedQueryable<T>> GetSortFunction(string optionId)
        {
            if (String.IsNullOrEmpty(optionId))
            {
                return _defaultOption;
            }

            if (_options.ContainsKey(optionId))
            {
                return _options[optionId].Value;
            }

            return _defaultOption;
        }

        public IEnumerable<Option> GetOptions()
        {
            List<Option> sortOptions = new List<Option>();

            foreach (string optionId in _options.Keys)
            {
                sortOptions.Add(
                    new Option
                    {
                        OptionId = optionId,
                        OptionName = _options[optionId].Key
                    });
            }

            return sortOptions;
        }
    }
}
