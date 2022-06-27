using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Business.ViewModels.TermsConditionsViewModels;
using Core.Entities;

namespace Business.Interfaces
{
    public interface ITermsConditionsService
    {

        Task<List<TermsConditions>> GetAllAsync();
        Task<TermsConditions> Get(int id);
        Task Create(TermsConditionsCreateViewModel termsConditionsViewModel);
        Task Update(int id, TermsConditionsUpdateViewModel termsConditionsViewModel);
        Task Remove(int id);
    }
}
