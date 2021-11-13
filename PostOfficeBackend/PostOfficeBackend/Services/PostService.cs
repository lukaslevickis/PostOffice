using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PostOfficeBackend.DAL.Entities;
using PostOfficeBackend.DAL.Repositories;
using PostOfficeBackend.Dtos;

namespace PostOfficeBackend.Services
{
    public class PostService
    {
        private readonly IGenericRepository<Post> _postRepository;
        private readonly IMapper _mapper;

        public PostService(IGenericRepository<Post> postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<List<PostDto>> GetAllAsync()
        {
            List<Post> posts = await _postRepository.GetAllAsync(null, q => q.OrderBy(s => s.Town));
            return _mapper.Map<List<Post>, List<PostDto>>(posts);
        }

        public async Task<PostDto> GetByIdAsync(int id)
        {
            Post post = await _postRepository.GetByIDAsync(id);
            if (post == null)
            {
                return null;
            }

            return _mapper.Map<Post, PostDto>(post);
        }

        public async Task<PostDto> CreateAsync(Post post)
        {
            Post entity = await _postRepository.CreateAsync(post);
            return _mapper.Map<Post, PostDto>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            Post post = await _postRepository.GetByIDAsync(id);
            await _postRepository.DeleteAsync(post);
        }

        public async Task<PostDto> UpdateAsync(Post post)
        {
            Post entity = await _postRepository.UpdateAsync(post);
            return _mapper.Map<Post, PostDto>(entity);
        }
    }
}
