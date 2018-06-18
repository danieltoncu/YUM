using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.YUMServices.Infrastructure
{
    class FilterOptions<T> where T : class
    {
        private readonly Dictionary<string, KeyValuePair<string, Expression<Func<T, bool>>>> _options;

        private readonly Expression<Func<T, bool>> _defaultOption;

        public FilterOptions(Expression<Func<T, bool>> defaultOption)
        {
            _options = new Dictionary<string, KeyValuePair<string, Expression<Func<T, bool>>>>();

            _defaultOption = defaultOption;
        }

        public void AddOption(string optionId, string optionName, Expression<Func<T, bool>> filterFunction)
        {
            _options.Add(optionId,
                new KeyValuePair<string, Expression<Func<T, bool>>>(optionName, filterFunction));
        }

        public Expression<Func<T, bool>> GetFilterFunction(string optionId)
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
            List<Option> filterOptions = new List<Option>();

            foreach (string optionId in _options.Keys)
            {
                filterOptions.Add(
                    new Option
                    {
                        OptionId = optionId,
                        OptionName = _options[optionId].Key
                    });
            }

            return filterOptions;
        }
    }
}
