using AutoMapper;
using PostOfficeBackend.DAL.Entities;
using PostOfficeBackend.Dtos;

namespace PostOfficeBackend.Profiles
{
    public class MainProfile: Profile
    {
        public MainProfile()
        {
            CreateMap<Post, PostDto>();
            CreateMap<Parcel, ParcelDto>();
        }
    }
}
