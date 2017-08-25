﻿using Fan.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fan.Data
{
    /// <summary>
    /// Contract for a category repository.
    /// </summary>
    public interface ICategoryRepository 
    {
        /// <summary>
        /// Creates a new <see cref="Category"/>.
        /// </summary>
        Task<Category> CreateAsync(Category category);

        /// <summary>
        /// Deletes a <see cref="Category"/> by id and re-categorize its posts to the given 
        /// default category id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="defaultCategoryId">
        /// This is the BlogSettings DefaultCategoryId, I choose to have it pass in
        /// from BLL for convenience instead of querying Meta for it.
        /// </param>
        Task DeleteAsync(int id, int defaultCategoryId);

        /// <summary>
        /// Returns all categories or empty list if no categories found.
        /// </summary>
        Task<List<Category>> GetListAsync();

        /// <summary>
        /// Updates a <see cref="Category"/>.
        /// </summary>
        /// <param name="category">Not all implementations use this parameter, such as the Sql ones.</param>
        Task<Category> UpdateAsync(Category category);
    }
}
