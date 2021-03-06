namespace Catel.Test.Data
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Catel.Data;

    /// <summary>
    /// IniFile Data object class which fully supports serialization, property changed notifications,
    /// backwards compatibility and error checking.
    /// </summary>
#if NET
    [Serializable]
#endif
    public class IniFile : ComparableModelBase
    {
        #region Fields
        #endregion

        #region Constructors
        /// <summary>
        ///   Initializes a new object from scratch.
        /// </summary>
        public IniFile()
        {
            IniEntryCollection = new List<IniEntry>();
        }

#if NET
        /// <summary>
        ///   Initializes a new object based on <see cref = "SerializationInfo" />.
        /// </summary>
        /// <param name = "info"><see cref = "SerializationInfo" /> that contains the information.</param>
        /// <param name = "context"><see cref = "StreamingContext" />.</param>
        protected IniFile(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
        #endregion

        #region Properties
        /// <summary>
        ///   Gets or sets the filename.
        /// </summary>
        public string FileName
        {
            get { return GetValue<string>(FileNameProperty); }
            set { SetValue(FileNameProperty, value); }
        }

        /// <summary>
        ///   Register the property so it is known in the class.
        /// </summary>
        public static readonly PropertyData FileNameProperty = RegisterProperty("FileName", typeof(string), string.Empty);

        /// <summary>
        ///   Gets or sets the collection of ini entries..
        /// </summary>
        /// <remarks>
        ///   This type is a ObservableCollection{T} by purpose.
        /// </remarks>
        public List<IniEntry> IniEntryCollection
        {
            get { return GetValue<List<IniEntry>>(IniEntryCollectionProperty); }
            set { SetValue(IniEntryCollectionProperty, value); }
        }

        /// <summary>
        ///   Register the property so it is known in the class.
        /// </summary>
        public static readonly PropertyData IniEntryCollectionProperty = RegisterProperty("IniEntryCollection", typeof(List<IniEntry>), null);
        #endregion
    }
}