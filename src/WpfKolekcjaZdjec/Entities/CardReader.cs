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
    public partial class CardReader : Archive, IObjectWithChangeTracker, INotifyPropertyChanged
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
        public string HddLetter
        {
            get { return _hddLetter; }
            set
            {
                if (_hddLetter != value)
                {
                    _hddLetter = value;
                    OnPropertyChanged("HddLetter");
                }
            }
        }
        private string _hddLetter;
    
        [DataMember]
        public string DeviceId
        {
            get { return _deviceId; }
            set
            {
                if (_deviceId != value)
                {
                    _deviceId = value;
                    OnPropertyChanged("DeviceId");
                }
            }
        }
        private string _deviceId;

        #endregion
        #region ChangeTracking
    
        protected override void ClearNavigationProperties()
        {
            base.ClearNavigationProperties();
        }

        #endregion
    }
}
