namespace Lib.Startup.BaseClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ComponentModel;
    using System.Windows;

    public class BaseVM : DependencyObject, INotifyPropertyChanged
    {
        private string _header;
        public string Header
        {
            get { return _header; }
            set
            {
                _header = value;
                OnPropertyChanged("Header");
            }
        }

        private bool _isSelected = true;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged("IsSelected");
                OnIsSelectedChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void OnIsSelectedChanged()
        { }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public virtual void Closing(System.ComponentModel.CancelEventArgs args)
        { }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Closed()
        { }

        /// <summary>
        /// Metoda wirtualna wywoływana tylko raz podczas pierwszego załadowania się formatki
        /// Może być wykorzystywana do pobrania danych zaraz po starcie nowego widoku
        /// Oczywiście pobieranie danych robimy na osobnym wątku + kręciołek ;)
        /// </summary>
        public virtual void InitialDataLoad()
        { }

        private bool _isActive;
        /// <summary>
        /// 
        /// </summary>
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    OnPropertyChanged("IsActive");
                    OnActiveChanged();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void OnActiveChanged()
        {
            if (IsActiveChanged != null)
                IsActiveChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler IsActiveChanged;

        /// <summary>
        /// 
        /// </summary>
        protected void RaiseAllPropertiesChanged()
        {
            foreach( string s in GetType().GetProperties().Select(x => x.Name))
                OnPropertyChanged(s);
        }

        #region INotifyPropertyChanged,

        /// <summary>
        /// Zdarzenie do obsługi zmiany wartości któejś z właściwości
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Metoda do wysyłania powiadomień o konieczności uaktualnienia bindingu którejś z właściwości
        /// </summary>
        /// <param name="property">właściwość</param>
        public void OnPropertyChanged(string property)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        #endregion INotifyPropertyChanged
    }
}
