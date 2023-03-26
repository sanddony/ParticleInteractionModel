using ReactiveUI;
using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ParticleInteractionModel.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region  Events

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion 

        #region Protected Methods
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null){
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
