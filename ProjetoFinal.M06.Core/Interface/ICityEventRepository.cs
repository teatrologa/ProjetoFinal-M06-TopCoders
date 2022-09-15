using ProjetoFinal.M06.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoFinal.M06.Core.Interface
{
    public interface ICityEventRepository
    {
        public List<CityEvent> GetAllEvents();

        public CityEvent GetIdEvent(long idEvent);

        public List<CityEvent> GetTitleEvent(string title);

        public List<CityEvent> GetLocalDateEvent(string local, DateTime dateHourEvent);

        public List<CityEvent> GetPriceDateEvent(decimal priceMin, decimal priceMax, DateTime dateHourEvent);

        public bool InsertNewEvent(CityEvent cityEvent);

        public bool ChangeEvent(long idEvent, CityEvent cityEvent);

        public bool DeleteEvent(long idEvent);
    }
}
