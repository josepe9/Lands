namespace Lands.ViewModels
{
    using Lands.Models;
    using Lands.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;

    public class LandsViewModel : BaseViewModel
    {
        #region Services
        private  ApiService apiService;
        #endregion

        #region Attributes
        //es observable para poderla pintar en un listview
        private ObservableCollection<Land> lands;
        //para refrescar el listvie de la vista LandsPage
        private bool isRefreshing;
        //Filtro para el buscador en el listview
        private string filter;
        //para crear la lista y poder hacer los filtros y tener la lista en memoria
        private List<Land> landsList;
        #endregion

        #region Properties
        public ObservableCollection<Land> Lands
        {
            get { return this.lands;}
            set { SetValue(ref this.lands, value); }
        }
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }
        public string Filter
        {
            get { return this.filter; }
            set {
                SetValue(ref this.filter, value);
                this.Search();
            }
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
            //indicar que la lista del listview está cargando
            this.IsRefreshing = true;
            //Verificamos que haya concexión a internet
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.isRefreshing = false;
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
                this.IsRefreshing = false;
                await App.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                await App.Current.MainPage.Navigation.PopAsync();
                return;
            }
            this.landsList = (List<Land>)response.Result;
            this.Lands = new ObservableCollection<Land>(this.landsList);
            this.IsRefreshing = false;
        }
        #endregion

        #region Commands
        //para refrescar con el método vuelve y se llama a él mismo
        public ICommand RefreshCommand{ get { return new RelayCommand(LoadLands); } }
        //llamar al método que hace los filtros
        public ICommand SearchCommand { get { return new RelayCommand(Search); } }

        private void Search()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                this.Lands = new ObservableCollection<Land>(this.landsList);
            }
            else
            {
                this.Lands = new ObservableCollection<Land>(
                    this.landsList.Where(
                        l => l.Name.Contains(this.Filter.ToLower()) ||
                             l.Capital.Contains(this.Filter.ToLower())));
            }
        }
        #endregion
    }
}
