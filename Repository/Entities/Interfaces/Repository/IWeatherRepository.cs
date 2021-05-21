using Repository.Models;
using Repository.Models.AccuWeatherAPI.AutoComplete;
using System.Threading.Tasks;

namespace Repository.Entities.Interfaces.Repository
{
    public interface IWeatherRepository
    {
        Task<AutoCompleteData> AutoCompleteAsync(string searchParam);
    }
}
