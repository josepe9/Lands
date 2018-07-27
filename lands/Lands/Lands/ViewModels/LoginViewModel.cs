namespace Lands.ViewModels
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using static System.Net.Mime.MediaTypeNames;

    public class LoginViewModel
    {
        #region Properties
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsRunning { get; set; }
        public bool IsRemembered { get; set; }
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            this.IsRemembered = true;
        }
        #endregion

        #region Commands
        /*incluir el nuget mvvmLightLibs para usar el relayccomand  */
        public ICommand LoginCommand { get { return new RelayCommand(Login); }}

        private async void Login()
        {
            //manera correcta de utilizar el email == ""
            if (string.IsNullOrEmpty(this.Email))
            {
               // var aa = await DisplayAlert("Error", "You must enter an Email", "Accept");
                return;
            }
        }
        #endregion
    }
}
