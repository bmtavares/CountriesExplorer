
using CountriesExplorer.Common.Models;
using CountriesExplorer.Prism.Views;

using Prism.Commands;
using Prism.Navigation;

namespace CountriesExplorer.Prism.ItemViewModels
{
    public class CountryItemViewModel : CountryResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectCountryCommand;

        public CountryItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectCountryCommand => _selectCountryCommand ??
            (_selectCountryCommand = new DelegateCommand(SelectCountryAsync));


        private async void SelectCountryAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
               { "country", this }
            };


            await _navigationService.NavigateAsync(nameof(CountryDetailPage), parameters);
        }
    }
}
