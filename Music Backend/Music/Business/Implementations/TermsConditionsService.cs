using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.ViewModels.TermsConditionsViewModels;
using Core;
using Core.Entities;

namespace Business.Implementations
{
    public class TermsConditionsService : ITermsConditionsService
    {
        #region Injects

        private readonly IUnitOfWork _unitOfWork;

        public TermsConditionsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        public async Task<List<TermsConditions>> GetAllAsync()
        {
            return await _unitOfWork.termsConditionsRepository.GetAllAsync();
        }

        public async Task<TermsConditions> Get(int id)
        {
            return await _unitOfWork.termsConditionsRepository.Get(b => b.Id == id);
        }

        public async Task Create(TermsConditionsCreateViewModel termsConditionsViewModel)
        {
            var newTermsConditions = new TermsConditions()
            {
                Text = termsConditionsViewModel.Text,
            };

            await _unitOfWork.termsConditionsRepository.CreateAsync(newTermsConditions);
            await _unitOfWork.SaveAsync();
        }

        public async Task Update(int id, TermsConditionsUpdateViewModel termsConditionsViewModel)
        {
            TermsConditions dbTermsConditions = await _unitOfWork.termsConditionsRepository.Get(b => b.Id == id);

            dbTermsConditions.Text = termsConditionsViewModel.Text;



            await _unitOfWork.SaveAsync();
        }

        public async Task Remove(int id)
        {
            TermsConditions dbTermsConditions = await _unitOfWork.termsConditionsRepository.Get(b => b.Id == id);



            _unitOfWork.termsConditionsRepository.Remove(dbTermsConditions);
            await _unitOfWork.SaveAsync();
        }
    }
}
