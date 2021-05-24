using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manageme.Data;
using Manageme.Data.Models;
using Manageme.Forms;
using Manageme.Services.Shared;
using Manageme.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Manageme.Services
{
    public class CategoryService
    {
        private IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<ServiceResult<CategoryViewModel>>
            AddCategoryAsync (long userId, CreateCategoryForm form)
        {
            if (string.IsNullOrWhiteSpace(form.Name))
            {
                return ServiceResult.BadRequest<CategoryViewModel>(
                    "Category must have a name."
                );
            }

            var user = await _unitOfWork.Users.GetAsQueryable()
                .Include(u => u.Categories)
                .SingleOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return ServiceResult.NotFound<CategoryViewModel>(
                    "User not found."
                );
            }

            if (user.Categories.Any(c =>
                c.Name.Equals(
                    form.Name.Trim(),
                    StringComparison.InvariantCultureIgnoreCase
                )
            ))
            {
                return ServiceResult.Conflict<CategoryViewModel>(
                    "This category is already defined."
                );
            }

            var category = new Category(form.Name, userId);

            await _unitOfWork.Categories.AddAsync(category);

            await _unitOfWork.SaveAsync();

            return ServiceResult.Ok(new CategoryViewModel(category)); 
        }

        public async Task<ServiceResult<List<CategoryViewModel>>>
            GetCategoriesAsync(long userId)
        {
            var user = await _unitOfWork.Users.GetAsQueryable()
                .Include(u => u.Categories)
                .SingleOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return ServiceResult.NotFound<List<CategoryViewModel>>(
                    "User not found."
                );
            }

            return ServiceResult.Ok(
                user.Categories
                    .Select(c => new CategoryViewModel(c))
                    .ToList()
            );
        }

        public async Task<ServiceResult>
            DeleteCategoryAsync(long userId, long categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsQueryable()
                .SingleOrDefaultAsync(c => 
                    c.UserId == userId && c.Id == categoryId
                );

            if (category == null)
            {
                return ServiceResult.NotFound("Category not found.");
            }

            _unitOfWork.Categories.Remove(categoryId);

            await _unitOfWork.SaveAsync();
            
            return ServiceResult.Ok();
        }
    }
}

