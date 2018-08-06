namespace Lands.ViewModels
{
    using Lands.Views;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel : BaseViewModel
    {

        #region Attributes
        /*estos atributos son los que necesitamos refrescar en las vistas 
         utilizando el InotifyPropertyChanged, utilizando en las vistas el Mode = "TwoWay  
         deben ser privados y con minuscula inicial*/
        private string email;
        private string password;
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Properties
        public string Email { get { return this.email; } set { SetValue(ref this.email,value); } }
        public string Password { get { return this.password; } set { SetValue(ref this.password, value); } }
        public bool IsRunning { get { return this.isRunning; } set { SetValue(ref this.isRunning, value); } }
        public bool IsRemembered { get; set; }
        public bool IsEnabled { get { return this.isEnabled; } set { SetValue(ref this.isEnabled, value); } }
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            this.IsRemembered = true;
            this.IsEnabled = true;

            this.Email = "josepabon8@gmail.com";
            this.Password = "1234";

            /* http://www.restcountries.eu/rest/v2/all */
        }
        #endregion

        #region Commands
        /*incluir el nuget mvvmLightLibs para usar el relayccomand  */
        public ICommand LoginCommand { get { return new RelayCommand(Login); } }

        private async void Login()
        {
            //manera correcta de utilizar el email == ""
            if (string.IsNullOrEmpty(this.Email))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must enter an Email", "Ok");
                return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await App.Current.MainPage.DisplayAlert("Error", "You must enter a Password", "Ok");
                return;
            }
            this.IsRunning = true;
            this.IsEnabled = false;

            if (this.Email != "josepabon8@gmail.com" || this.Password != "1234")
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await App.Current.MainPage.DisplayAlert("Error", "Email or Password Incorrect", "Ok");
                this.Password = string.Empty;
                return;
            }
        /*    this.IsRunning = false;
            this.IsEnabled = true;
            this.Email = string.Empty;
            this.Password = string.Empty;   */
            /* el siguiente código garantiza que se ejecute LandsPage en una sola
             * instancia antes de pintar la vista LandsPage, alineamos la LandsViweModel 
             alineada a ella */

/*            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Lands = new LandsViewModel();
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new LandsPage());
            */

            MainViewModel.GetInstance().Lands = new LandsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new LandsPage());
            this.IsRunning = false;
            this.IsEnabled = true;

            this.Email = string.Empty;
            this.Password = string.Empty;
        }
        #endregion
    }
}
