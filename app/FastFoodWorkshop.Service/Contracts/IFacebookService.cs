namespace FastFoodWorkshop.Service.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFacebookService
    {
        Task<List<string>> GetFacebookInfoAsync();
    }
}
