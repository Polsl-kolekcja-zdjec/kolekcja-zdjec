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
    [KnownType(typeof(Tag))]
    [KnownType(typeof(Photo))]
    public partial class Tag: IObjectWithChangeTracker, INotifyPropertyChanged
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
                    if (!IsDeserializing)
                    {
                        if (Tag1 != null && Tag1.ID != value)
                        {
                            Tag1 = null;
                        }
                    }
                    _iD = value;
                    OnPropertyChanged("ID");
                }
            }
        }
        private int _iD;
    
        [DataMember]
        public System.DateTime CreationDate
        {
            get { return _creationDate; }
            set
            {
                if (_creationDate != value)
                {
                    _creationDate = value;
                    OnPropertyChanged("CreationDate");
                }
            }
        }
        private System.DateTime _creationDate;
    
        [DataMember]
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        private string _name;
    
        [DataMember]
        public string IconPath
        {
            get { return _iconPath; }
            set
            {
                if (_iconPath != value)
                {
                    _iconPath = value;
                    OnPropertyChanged("IconPath");
                }
            }
        }
        private string _iconPath;
    
        [DataMember]
        public Nullable<int> ParentID
        {
            get { return _parentID; }
            set
            {
                if (_parentID != value)
                {
                    _parentID = value;
                    OnPropertyChanged("ParentID");
                }
            }
        }
        private Nullable<int> _parentID;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public Tag Tags1
        {
            get { return _tags1; }
            set
            {
                if (!ReferenceEquals(_tags1, value))
                {
                    var previousValue = _tags1;
                    _tags1 = value;
                    FixupTags1(previousValue);
                    OnNavigationPropertyChanged("Tags1");
                }
            }
        }
        private Tag _tags1;
    
        [DataMember]
        public Tag Tag1
        {
            get { return _tag1; }
            set
            {
                if (!ReferenceEquals(_tag1, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added && value != null)
                    {
                        // This the dependent end of an identifying relationship, so the principal end cannot be changed if it is already set,
                        // otherwise it can only be set to an entity with a primary key that is the same value as the dependent's foreign key.
                        if (ID != value.ID)
                        {
                            throw new InvalidOperationException("The principal end of an identifying relationship can only be changed when the dependent end is in the Added state.");
                        }
                    }
                    var previousValue = _tag1;
                    _tag1 = value;
                    FixupTag1(previousValue);
                    OnNavigationPropertyChanged("Tag1");
                }
            }
        }
        private Tag _tag1;
    
        [DataMember]
        public TrackableCollection<Photo> Photos
        {
            get
            {
                if (_photos == null)
                {
                    _photos = new TrackableCollection<Photo>();
                    _photos.CollectionChanged += FixupPhotos;
                }
                return _photos;
            }
            set
            {
                if (!ReferenceEquals(_photos, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_photos != null)
                    {
                        _photos.CollectionChanged -= FixupPhotos;
                    }
                    _photos = value;
                    if (_photos != null)
                    {
                        _photos.CollectionChanged += FixupPhotos;
                    }
                    OnNavigationPropertyChanged("Photos");
                }
            }
        }
        private TrackableCollection<Photo> _photos;

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
    
        // This entity type is the dependent end in at least one association that performs cascade deletes.
        // This event handler will process notifications that occur when the principal end is deleted.
        internal void HandleCascadeDelete(object sender, ObjectStateChangingEventArgs e)
        {
            if (e.NewState == ObjectState.Deleted)
            {
                this.MarkAsDeleted();
            }
        }
    
        protected virtual void ClearNavigationProperties()
        {
            Tags1 = null;
            Tag1 = null;
            Photos.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void FixupTags1(Tag previousValue)
        {
            // This is the principal end in an association that performs cascade deletes.
            // Update the event listener to refer to the new dependent.
            if (previousValue != null)
            {
                ChangeTracker.ObjectStateChanging -= previousValue.HandleCascadeDelete;
            }
    
            if (Tags1 != null)
            {
                ChangeTracker.ObjectStateChanging += Tags1.HandleCascadeDelete;
            }
    
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && ReferenceEquals(previousValue.Tag1, this))
            {
                previousValue.Tag1 = null;
            }
    
            if (Tags1 != null)
            {
                Tags1.Tag1 = this;
            }
    
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("Tags1")
                    && (ChangeTracker.OriginalValues["Tags1"] == Tags1))
                {
                    ChangeTracker.OriginalValues.Remove("Tags1");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("Tags1", previousValue);
                    // This is the principal end of an identifying association, so the dependent must be deleted when the relationship is removed.
                    // If the current state of the dependent is Added, the relationship can be changed without causing the dependent to be deleted.
                    if (previousValue != null && previousValue.ChangeTracker.State != ObjectState.Added)
                    {
                        previousValue.MarkAsDeleted();
                    }
                }
                if (Tags1 != null && !Tags1.ChangeTracker.ChangeTrackingEnabled)
                {
                    Tags1.StartTracking();
                }
            }
        }
    
        private void FixupTag1(Tag previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && ReferenceEquals(previousValue.Tags1, this))
            {
                previousValue.Tags1 = null;
            }
    
            if (Tag1 != null)
            {
                Tag1.Tags1 = this;
                ID = Tag1.ID;
            }
    
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("Tag1")
                    && (ChangeTracker.OriginalValues["Tag1"] == Tag1))
                {
                    ChangeTracker.OriginalValues.Remove("Tag1");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("Tag1", previousValue);
                }
                if (Tag1 != null && !Tag1.ChangeTracker.ChangeTrackingEnabled)
                {
                    Tag1.StartTracking();
                }
            }
        }
    
        private void FixupPhotos(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (Photo item in e.NewItems)
                {
                    if (!item.Tags.Contains(this))
                    {
                        item.Tags.Add(this);
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("Photos", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Photo item in e.OldItems)
                {
                    if (item.Tags.Contains(this))
                    {
                        item.Tags.Remove(this);
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("Photos", item);
                    }
                }
            }
        }

        #endregion
    }
}
