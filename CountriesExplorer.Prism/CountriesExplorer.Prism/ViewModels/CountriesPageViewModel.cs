using CountriesExplorer.Common.Models;
using CountriesExplorer.Common.Services;
using CountriesExplorer.Prism.ItemViewModels;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;

namespace CountriesExplorer.Prism.ViewModels
{
    public class CountriesPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;

        private bool _isRunning;

        private List<CountryResponse> _myCountries;

        private ObservableCollection<CountryItemViewModel> _countries;


        public CountriesPageViewModel(INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;

            Title = "Countries";
            LoadCountriesAsync();
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public ObservableCollection<CountryItemViewModel> Countries
        {
            get => _countries;
            set => SetProperty(ref _countries, value);
        }

        private async void LoadCountriesAsync()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Connection to the server has failed.", "okeh"); //TODO : CHANGE IT
                return;
            }

            IsRunning = true;

            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetListAsync<CountryResponse>(
                url,
                "/rest/v2",
                "/all");//🤷‍

            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Server could not provide a response.", "oh no"); //TODO: CHANGE THIS TOO
                return;
            }

            _myCountries = (List<CountryResponse>)response.Result;
            ShowCountries();
        }

        private void ShowCountries()
        {
            //if (string.IsNullOrEmpty(Search))
            //{
            Countries = new ObservableCollection<CountryItemViewModel>(_myCountries.Select(p =>
                new CountryItemViewModel(_navigationService)
                {
                    Name = p.Name,
                    TopLevelDomain = p.TopLevelDomain,
                    Alpha2Code = p.Alpha2Code,
                    Alpha3Code = p.Alpha3Code,
                    CallingCodes = p.CallingCodes,
                    Capital = p.Capital,
                    AltSpellings = p.AltSpellings,
                    Region = p.Region,
                    Subregion = p.Subregion,
                    Population = p.Population,
                    LatLng = p.LatLng,
                    Demonym = p.Demonym,
                    Area = p.Area,
                    Gini = p.Gini,
                    Timezones = p.Timezones,
                    Borders = p.Borders,
                    NativeName = p.NativeName,
                    NumericCode = p.NumericCode,
                    Currencies = p.Currencies,
                    Languages = p.Languages,
                    Translations = p.Translations,
                    Flag = p.Flag,
                    RegionalBlocs = p.RegionalBlocs,
                    Cioc = p.Cioc

                })
                    .ToList());
            //}
            //else
            //{
            //    Products = new ObservableCollection<ProductItemViewModel>(_myProducts.Select(p =>
            //    new ProductItemViewModel(_navigationService)
            //    {
            //        Category = p.Category,
            //        Description = p.Description,
            //        Id = p.Id,
            //        IsActive = p.IsActive,
            //        IsStarred = p.IsStarred,
            //        Name = p.Name,
            //        Price = p.Price,
            //        ProductImages = p.ProductImages
            //    })
            //        .Where(p => p.Name.ToLower().Contains(Search.ToLower()))
            //        .ToList());
            //}
        }
    }
}
