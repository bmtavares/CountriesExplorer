using CountriesExplorer.Common.Models;

using Prism.Navigation;

namespace CountriesExplorer.Prism.ViewModels
{
    public class CountryDetailPageViewModel : ViewModelBase
    {
        private CountryResponse _country;

        public CountryDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Country Details";
        }

        public CountryResponse Country
        {
            get => _country;
            set => SetProperty(ref _country, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("country"))
            {
                Country = parameters.GetValue<CountryResponse>("country");
                Title = Country.Name;
            }
        }
    }
}
