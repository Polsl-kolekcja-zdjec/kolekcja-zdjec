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
    [KnownType(typeof(StandardAttributes))]
    [KnownType(typeof(ExifParameters))]
    [KnownType(typeof(Photo))]
    public abstract partial class BaseAttributes: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'Id' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        private int _id;
    
        [DataMember]
        public string Author
        {
            get { return _author; }
            set
            {
                if (_author != value)
                {
                    _author = value;
                    OnPropertyChanged("Author");
                }
            }
        }
        private string _author;
    
        [DataMember]
        public short BitsPerPixel
        {
            get { return _bitsPerPixel; }
            set
            {
                if (_bitsPerPixel != value)
                {
                    _bitsPerPixel = value;
                    OnPropertyChanged("BitsPerPixel");
                }
            }
        }
        private short _bitsPerPixel;
    
        [DataMember]
        public string Hash
        {
            get { return _hash; }
            set
            {
                if (_hash != value)
                {
                    _hash = value;
                    OnPropertyChanged("Hash");
                }
            }
        }
        private string _hash;
    
        [DataMember]
        public long Height
        {
            get { return _height; }
            set
            {
                if (_height != value)
                {
                    _height = value;
                    OnPropertyChanged("Height");
                }
            }
        }
        private long _height;
    
        [DataMember]
        public long Width
        {
            get { return _width; }
            set
            {
                if (_width != value)
                {
                    _width = value;
                    OnPropertyChanged("Width");
                }
            }
        }
        private long _width;
    
        [DataMember]
        public short Rate
        {
            get { return _rate; }
            set
            {
                if (_rate != value)
                {
                    _rate = value;
                    OnPropertyChanged("Rate");
                }
            }
        }
        private short _rate = 0;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public TrackableCollection<Photo> Photo
        {
            get
            {
                if (_photo == null)
                {
                    _photo = new TrackableCollection<Photo>();
                    _photo.CollectionChanged += FixupPhoto;
                }
                return _photo;
            }
            set
            {
                if (!ReferenceEquals(_photo, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_photo != null)
                    {
                        _photo.CollectionChanged -= FixupPhoto;
                    }
                    _photo = value;
                    if (_photo != null)
                    {
                        _photo.CollectionChanged += FixupPhoto;
                    }
                    OnNavigationPropertyChanged("Photo");
                }
            }
        }
        private TrackableCollection<Photo> _photo;

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
            Photo.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void FixupPhoto(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (Photo item in e.NewItems)
                {
                    item.Attributes = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("Photo", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Photo item in e.OldItems)
                {
                    if (ReferenceEquals(item.Attributes, this))
                    {
                        item.Attributes = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("Photo", item);
                    }
                }
            }
        }

        #endregion
    }
}
