using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Business.ViewModels.FaqViewModels;
using Core.Entities;

namespace Business.Interfaces
{
    public interface IFaqService
    {
       
            Task<List<Faq>> GetAllAsync();
            Task<Faq> Get(int id);
            Task Create(FaqCreateViewModel faqViewModel);
            Task Update(int id, FaqUpdateViewModel faqViewModel);
            Task Remove(int id);
    }
}
