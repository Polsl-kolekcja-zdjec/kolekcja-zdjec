//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;

namespace WpfKolekcjaZdjec.Entities
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(Archive))]
    [KnownType(typeof(Attribute))]
    [KnownType(typeof(Tag))]
    public partial class Photo: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public int ID
        {
            get { return _iD; }
            set
            {
                if (_iD != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'ID' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _iD = value;
                    OnPropertyChanged("ID");
                }
            }
        }
        private int _iD;
    
        [DataMember]
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }
        }
        private string _description;
    
        [DataMember]
        public string FilePath
        {
            get { return _filePath; }
            set
            {
                if (_filePath != value)
                {
                    _filePath = value;
                    OnPropertyChanged("FilePath");
                }
            }
        }
        private string _filePath;
    
        [DataMember]
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged("Title");
                }
            }
        }
        private string _title;
    
        [DataMember]
        public string ThumbnailPath
        {
            get { return _thumbnailPath; }
            set
            {
                if (_thumbnailPath != value)
                {
                    _thumbnailPath = value;
                    OnPropertyChanged("ThumbnailPath");
                }
            }
        }
        private string _thumbnailPath;
    
        [DataMember]
        public Nullable<int> SourceID
        {
            get { return _sourceID; }
            set
            {
                if (_sourceID != value)
                {
                    ChangeTracker.RecordOriginalValue("SourceID", _sourceID);
                    if (!IsDeserializing)
                    {
                        if (Archive != null && Archive.ID != value)
                        {
                            Archive = null;
                        }
                    }
                    _sourceID = value;
                    OnPropertyChanged("SourceID");
                }
            }
        }
        private Nullable<int> _sourceID;
    
        [DataMember]
        public Nullable<int> AttributesID
        {
            get { return _attributesID; }
            set
            {
                if (_attributesID != value)
                {
                    ChangeTracker.RecordOriginalValue("AttributesID", _attributesID);
                    if (!IsDeserializing)
                    {
                        if (Attribute != null && Attribute.ID != value)
                        {
                            Attribute = null;
                        }
                    }
                    _attributesID = value;
                    OnPropertyChanged("AttributesID");
                }
            }
        }
        private Nullable<int> _attributesID;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public Archive Archive
        {
            get { return _archive; }
            set
            {
                if (!ReferenceEquals(_archive, value))
                {
                    var previousValue = _archive;
                    _archive = value;
                    FixupArchive(previousValue);
                    OnNavigationPropertyChanged("Archive");
                }
            }
        }
        private Archive _archive;
    
        [DataMember]
        public Attribute Attribute
        {
            get { return _attribute; }
            set
            {
                if (!ReferenceEquals(_attribute, value))
                {
                    var previousValue = _attribute;
                    _attribute = value;
                    FixupAttribute(previousValue);
                    OnNavigationPropertyChanged("Attribute");
                }
            }
        }
        private Attribute _attribute;
    
        [DataMember]
        public TrackableCollection<Archive> Archives
        {
            get
            {
                if (_archives == null)
                {
                    _archives = new TrackableCollection<Archive>();
                    _archives.CollectionChanged += FixupArchives;
                }
                return _archives;
            }
            set
            {
                if (!ReferenceEquals(_archives, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_archives != null)
                    {
                        _archives.CollectionChanged -= FixupArchives;
                    }
                    _archives = value;
                    if (_archives != null)
                    {
                        _archives.CollectionChanged += FixupArchives;
                    }
                    OnNavigationPropertyChanged("Archives");
                }
            }
        }
        private TrackableCollection<Archive> _archives;
    
        [DataMember]
        public TrackableCollection<Tag> Tags
        {
            get
            {
                if (_tags == null)
                {
                    _tags = new TrackableCollection<Tag>();
                    _tags.CollectionChanged += FixupTags;
                }
                return _tags;
            }
            set
            {
                if (!ReferenceEquals(_tags, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_tags != null)
                    {
                        _tags.CollectionChanged -= FixupTags;
                    }
                    _tags = value;
                    if (_tags != null)
                    {
                        _tags.CollectionChanged += FixupTags;
                    }
                    OnNavigationPropertyChanged("Tags");
                }
            }
        }
        private TrackableCollection<Tag> _tags;

        #endregion
        #region ChangeTracking
    
        protected virtual void OnPropertyChanged(String propertyName)
        {
            if (ChangeTracker.State != ObjectState.Added && ChangeTracker.State != ObjectState.Deleted)
            {
                ChangeTracker.State = ObjectState.Modified;
            }
            if (_propertyChanged != null)
            {
                _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
        protected virtual void OnNavigationPropertyChanged(String propertyName)
        {
            if (_propertyChanged != null)
            {
                _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged{ add { _propertyChanged += value; } remove { _propertyChanged -= value; } }
        private event PropertyChangedEventHandler _propertyChanged;
        private ObjectChangeTracker _changeTracker;
    
        [DataMember]
        public ObjectChangeTracker ChangeTracker
        {
            get
            {
                if (_changeTracker == null)
                {
                    _changeTracker = new ObjectChangeTracker();
                    _changeTracker.ObjectStateChanging += HandleObjectStateChanging;
                }
                return _changeTracker;
            }
            set
            {
                if(_changeTracker != null)
                {
                    _changeTracker.ObjectStateChanging -= HandleObjectStateChanging;
                }
                _changeTracker = value;
                if(_changeTracker != null)
                {
                    _changeTracker.ObjectStateChanging += HandleObjectStateChanging;
                }
            }
        }
    
        private void HandleObjectStateChanging(object sender, ObjectStateChangingEventArgs e)
        {
            if (e.NewState == ObjectState.Deleted)
            {
                ClearNavigationProperties();
            }
        }
    
        protected bool IsDeserializing { get; private set; }
    
        [OnDeserializing]
        public void OnDeserializingMethod(StreamingContext context)
        {
            IsDeserializing = true;
        }
    
        [OnDeserialized]
        public void OnDeserializedMethod(StreamingContext context)
        {
            IsDeserializing = false;
            ChangeTracker.ChangeTrackingEnabled = true;
        }
    
        protected virtual void ClearNavigationProperties()
        {
            Archive = null;
            Attribute = null;
            Archives.Clear();
            Tags.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void FixupArchive(Archive previousValue, bool skipKeys = false)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.Photos.Contains(this))
            {
                previousValue.Photos.Remove(this);
            }
    
            if (Archive != null)
            {
                if (!Archive.Photos.Contains(this))
                {
                    Archive.Photos.Add(this);
                }
    
                SourceID = Archive.ID;
            }
            else if (!skipKeys)
            {
                SourceID = null;
            }
    
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("Archive")
                    && (ChangeTracker.OriginalValues["Archive"] == Archive))
                {
                    ChangeTracker.OriginalValues.Remove("Archive");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("Archive", previousValue);
                }
                if (Archive != null && !Archive.ChangeTracker.ChangeTrackingEnabled)
                {
                    Archive.StartTracking();
                }
            }
        }
    
        private void FixupAttribute(Attribute previousValue, bool skipKeys = false)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.Photos.Contains(this))
            {
                previousValue.Photos.Remove(this);
            }
    
            if (Attribute != null)
            {
                if (!Attribute.Photos.Contains(this))
                {
                    Attribute.Photos.Add(this);
                }
    
                AttributesID = Attribute.ID;
            }
            else if (!skipKeys)
            {
                AttributesID = null;
            }
    
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("Attribute")
                    && (ChangeTracker.OriginalValues["Attribute"] == Attribute))
                {
                    ChangeTracker.OriginalValues.Remove("Attribute");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("Attribute", previousValue);
                }
                if (Attribute != null && !Attribute.ChangeTracker.ChangeTrackingEnabled)
                {
                    Attribute.StartTracking();
                }
            }
        }
    
        private void FixupArchives(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (Archive item in e.NewItems)
                {
                    if (!item.Photos1.Contains(this))
                    {
                        item.Photos1.Add(this);
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("Archives", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Archive item in e.OldItems)
                {
                    if (item.Photos1.Contains(this))
                    {
                        item.Photos1.Remove(this);
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("Archives", item);
                    }
                }
            }
        }
    
        private void FixupTags(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (Tag item in e.NewItems)
                {
                    if (!item.Photos.Contains(this))
                    {
                        item.Photos.Add(this);
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("Tags", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Tag item in e.OldItems)
                {
                    if (item.Photos.Contains(this))
                    {
                        item.Photos.Remove(this);
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("Tags", item);
                    }
                }
            }
        }

        #endregion
    }
}
