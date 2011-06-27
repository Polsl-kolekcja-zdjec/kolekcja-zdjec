using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfKolekcjaZdjec.Entities
{
    /// <summary>
    /// Photo partial class.
    /// </summary>
    public partial class Photo
    {
        #region Fields

        /// <summary>
        /// Tags list.
        /// </summary>
        private List<Tag> _tags = new List<Tag>();

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        public List<Tag> Tags
        {
            get { return _tags; }
            set
            {
                _tags = value;
            }
        }

        #endregion
    }

    /// <summary>
    /// Tag partial class.
    /// </summary>
    public partial class Tag
    {
        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.Name;
        }
    }
}