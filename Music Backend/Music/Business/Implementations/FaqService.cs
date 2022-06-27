using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.ViewModels.FaqViewModels;
using Core;
using Core.Entities;

namespace Business.Implementations
{
    public class FaqService : IFaqService
    {
        #region Injects
        
        private readonly IUnitOfWork _unitOfWork;

        public FaqService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion


        public async Task<List<Faq>> GetAllAsync()
        {
            return await _unitOfWork.faqRepository.GetAllAsync();
        }

        public async Task<Faq> Get(int id)
        {
            return await _unitOfWork.faqRepository.Get(b => b.Id == id);
        }

        public async Task Create(FaqCreateViewModel faqViewModel)
        {

            var newFaq = new Faq()
            {
                Question = faqViewModel.Question,
                Answer = faqViewModel.Answer
            };

            await _unitOfWork.faqRepository.CreateAsync(newFaq);
            await _unitOfWork.SaveAsync();
        }

        public async Task Update(int id, FaqUpdateViewModel faqViewModel)
        {
            Faq dbFaq = await _unitOfWork.faqRepository.Get(b => b.Id == id);

            dbFaq.Question = faqViewModel.Question;
            dbFaq.Answer = faqViewModel.Answer;



            await _unitOfWork.SaveAsync();
        }

        public async Task Remove(int id)
        {
            Faq dbFaq = await _unitOfWork.faqRepository.Get(b => b.Id == id);



            _unitOfWork.faqRepository.Remove(dbFaq);
            await _unitOfWork.SaveAsync();
        }
    }
}
