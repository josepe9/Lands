using System;
using System.Collections.Generic;
using System.Text;

namespace Lands.ViewModels
{
    public class MainViewModel
    {
        #region ViewModel
        public LoginViewModel Login { get; set; }
        public LandsViewModel Lands { get; set; }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
        }
        #endregion

        #region Singleton
        /* poder hacer un llamado de la mainviewmodel desde cualquier clase */
        private static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }
        #endregion
    }
}
