namespace IEnumerableHelpersLib
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Set of <see cref="IEnumerable{T}"/> extension methods
    /// </summary>
    public static class IEnumerableGenericHelper
    {
        /// <summary>
        /// Builds new enumerable sequence from the elements of this sequence which match to specified predicate 
        /// </summary>
        /// <typeparam name="T">Type of sequence elements</typeparam>
        /// <param name="self">This sequence</param>
        /// <param name="filter">The filter predicate</param>
        /// <returns>
        /// New filtered sequence
        /// </returns>
        /// <exception cref="ArgumentNullException">Throws when self or filter is null</exception>
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> self, Predicate<T> filter)
        {
            ValidateOnNull(self, nameof(self));
            ValidateOnNull(filter, nameof(filter));

            IEnumerable<T> Do()
            {
                foreach (var item in self)
                {
                    if (filter(item))
                    {
                        yield return item;
                    }
                }
            }

            return Do();
        }

        /// <summary>
        /// Converts each element of the sequence due the specified converter
        /// </summary>
        /// <typeparam name="T">Type of sequence elements</typeparam>
        /// <param name="self">The self.</param>
        /// <param name="convert">The convert.</param>
        /// <returns>New mapped sequence</returns>
        /// <exception cref="ArgumentNullException">Throws when self or convert is null</exception>
        public static IEnumerable<T> Map<T>(this IEnumerable<T> self, Converter<T, T> convert)
        {
            ValidateOnNull(self, nameof(self));
            ValidateOnNull(convert, nameof(convert));

            IEnumerable<T> Do()
            {
                foreach (var item in self)
                {
                    yield return convert(item);
                }
            }

            return Do();
        }

        /// <summary>
        /// Validates object on null
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="name">The name.</param>
        /// <exception cref="ArgumentNullException">Throws when object is null</exception>
        private static void ValidateOnNull<T>(T obj, string name) where T : class
        {
            if (obj is null)
            {
                throw new ArgumentNullException($"{name} is null");
            }
        }
    }
}
