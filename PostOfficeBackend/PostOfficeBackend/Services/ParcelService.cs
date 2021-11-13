using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using PostOfficeBackend.DAL.Entities;
using PostOfficeBackend.DAL.Repositories;
using PostOfficeBackend.Dtos;

namespace PostOfficeBackend.Services
{
    public class ParcelService
    {
        private readonly IGenericRepository<Parcel> _parcelRepository;
        private readonly IMapper _mapper;

        public ParcelService(IGenericRepository<Parcel> parcelRepository, IMapper mapper)
        {
            _parcelRepository = parcelRepository;
            _mapper = mapper;
        }

        public async Task<List<ParcelDto>> GetAllAsync(int? postId = null)
        {
            Expression<Func<Parcel, bool>> filterBy = (postId != null) ?
                                                          r => r.PostId == postId :
                                                          null;

            List<Parcel> parcels = await _parcelRepository.GetAllAsync(filterBy, q => q.OrderByDescending(s => s.Weight));
            return _mapper.Map<List<Parcel>, List<ParcelDto>>(parcels);
        }

        public async Task<ParcelDto> GetByIdAsync(int id)
        {
            Parcel parcel = await _parcelRepository.GetByIDAsync(id);
            if (parcel == null)
            {
                return null;
            }

            return _mapper.Map<Parcel, ParcelDto>(parcel);
        }

        public async Task<ParcelDto> CreateAsync(Parcel parcel)
        {
            Parcel entity = await _parcelRepository.CreateAsync(parcel);
            return _mapper.Map<Parcel, ParcelDto>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            Parcel parcel = await _parcelRepository.GetByIDAsync(id);
            await _parcelRepository.DeleteAsync(parcel);
        }

        public async Task<ParcelDto> UpdateAsync(Parcel parcel)
        {
            Parcel entity = await _parcelRepository.UpdateAsync(parcel);
            return _mapper.Map<Parcel, ParcelDto>(entity);
        }
    }
}
