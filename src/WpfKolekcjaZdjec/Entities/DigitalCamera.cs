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
    public partial class DigitalCamera : Archive, IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
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
