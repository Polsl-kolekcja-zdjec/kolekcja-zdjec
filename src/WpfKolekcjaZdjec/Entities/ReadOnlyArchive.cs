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
    public partial class ReadOnlyArchive : Archive, IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public long Capacity
        {
            get { return _capacity; }
            set
            {
                if (_capacity != value)
                {
                    _capacity = value;
                    OnPropertyChanged("Capacity");
                }
            }
        }
        private long _capacity;
    
        [DataMember]
        public string DiscLetter
        {
            get { return _discLetter; }
            set
            {
                if (_discLetter != value)
                {
                    _discLetter = value;
                    OnPropertyChanged("DiscLetter");
                }
            }
        }
        private string _discLetter;
    
        [DataMember]
        public string DiscType
        {
            get { return _discType; }
            set
            {
                if (_discType != value)
                {
                    _discType = value;
                    OnPropertyChanged("DiscType");
                }
            }
        }
        private string _discType;

        #endregion
        #region ChangeTracking
    
        protected override void ClearNavigationProperties()
        {
            base.ClearNavigationProperties();
        }

        #endregion
    }
}
