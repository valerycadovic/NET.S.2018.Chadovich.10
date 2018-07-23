namespace GeneralTestTypes
{
    using System;

    /// <inheritdoc />
    /// <summary>
    /// Test class to simulate comparable reference types behavior
    /// </summary>
    public class Book : IComparable<Book>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Book"/> class.
        /// </summary>
        /// <param name="pages">The pages.</param>
        public Book(int pages)
        {
            this.Pages = pages;
        }

        /// <summary>
        /// Gets the pages.
        /// </summary>
        /// <value>
        /// The pages.
        /// </value>
        public int Pages { get; }

        /// <inheritdoc/>
        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="other" /> in the sort order.  Zero This instance occurs in the same position in the sort order as <paramref name="other" />. Greater than zero This instance follows <paramref name="other" /> in the sort order.
        /// </returns>
        public int CompareTo(Book other)
        {
            if (ReferenceEquals(this, other))
            {
                return 0;
            }

            if (other is null)
            {
                return 1;
            }

            return Pages.CompareTo(other.Pages);
        }
    }
}
