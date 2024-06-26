﻿using FinalProject.Models.Post;

namespace FinalProject.Contracts.Posts
{
    public interface IPostServices
    {
        Task<List<PostForGettingDTO>> GetAllPostsAsync();
        Task<PostForGettingDetailedDTO> GetSinglePostsAsync(int id);
        Task AddPostsAsync(PostForCreatingDTO model);
        Task UpdatePostsAsync(PostForUpdatingDTO model);
        Task ChangePostsStatusAsync(PostForChangingStatusDTO model);
        Task ChangePostsStateAsync(PostForChangingStateDTO model);
        Task DeletePostAsync(int id);
    }
}
