namespace Lands.ViewModels
{
    using Lands.Models;
    using Lands.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class LandsViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        //es observable para poderla pintar en un listview
        private ObservableCollection<Land> lands;
        #endregion

        #region Properties
        public ObservableCollection<Land> Lands
        {
            get { return this.lands;}
            set { SetValue(ref this.lands, value); }
        }
        #endregion

        #region Constructors
        public LandsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadLands();
        }
        #endregion

        #region Methods
        private async void LoadLands()
        {
            //Verificamos que haya concexión a internet
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                //si no hay conexion
                await App.Current.MainPage.DisplayAlert("Error", connection.Message, "Accept");

                //es como dar flecha atras en la navegación nos devolvemos de la page
                await App.Current.MainPage.Navigation.PopAsync();
                return;
            }

            /*le enviamos al metodo getlist los parámetros 
             * urlbase = ttp://www.restcountries.eu
             * prefijo = /rest
             * Controlador = /v2/all  */
            var response = await this.apiService.GetList<Land>(
              "http://restcountries.eu",
              "/rest",
              "/v2/all");


            //si conectó y devolvió los valores de la api
            if (!response.IsSuccess)
            {
             //   await App.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }
            var list = (List<Land>)response.Result;
            this.Lands = new ObservableCollection<Land>(list);
        }
        #endregion
    }
}
