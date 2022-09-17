using ProjetoFinal.M06.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoFinal.M06.Core.Interface
{
    public interface ICityEventService
    {
        public List<CityEvent> GetAllEvents();

        public CityEvent GetIdEvent(long idEvent);

        public bool GetIdEventBool(long idEvent);

        public List<CityEvent> GetTitleEvent(string title);

        public List<CityEvent> GetLocalDateEvent(string local, DateTime dateHourEvent);

        public List<CityEvent> GetPriceDateEvent(decimal priceMin, decimal priceMax, DateTime dateHourEvent);

        public bool InsertNewEvent(CityEvent cityEvent);

        public bool ChangeEvent(long idEvent, CityEvent cityEvent);

        public bool DeleteEvent(long idEvent);

        public bool IsThereAnyReservation(long idEvent);

        public bool CheckDateEvent(DateTime dateHourEvent);

        public bool CheckPriceValues(decimal priceMin, decimal priceMax);
    }
}
