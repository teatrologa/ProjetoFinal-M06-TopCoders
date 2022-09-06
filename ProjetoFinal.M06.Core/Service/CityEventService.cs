using ProjetoFinal.M06.Core.Interface;
using ProjetoFinal.M06.Core.Models;

namespace ProjetoFinal.M06.Core.Service
{
    public class CityEventService : ICityEventService
    {
        private readonly ICityEventRepository _cityEventRepository;
        public CityEventService(ICityEventRepository cityEventRepository)
        {
            _cityEventRepository = cityEventRepository;
        }

        public List<CityEvent> GetAllEvents() { return _cityEventRepository.GetAllEvents(); }

        public CityEvent GetIdEvent(long idEvent) { return _cityEventRepository.GetIdEvent(idEvent); }

        public List<CityEvent> GetTitleEvent(string title) { return _cityEventRepository.GetTitleEvent(title); }

        public bool InsertNewEvent(CityEvent cityEvent) { return _cityEventRepository.InsertNewEvent(cityEvent); }

        public bool ChangeEvent(long idEvent, CityEvent cityEvent) { return _cityEventRepository.ChangeEvent(idEvent, cityEvent); }

        public bool DeleteEvent(long idEvent) { return _cityEventRepository.DeleteEvent(idEvent); }

    }
}