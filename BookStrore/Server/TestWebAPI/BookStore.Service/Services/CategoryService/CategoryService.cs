﻿using BookStore.Data.Repositories.Interfaces;
using BookStore.Data.Entities;
using BookStore.API.DTOs.Category;
using BookStore.API.Services.CategoryService;
using BookStore.Common.DTOs.Category;

namespace BookStore.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<AddCategoryResponse> CreateAsync(AddCategoryRequest addCategoryRequest)
        {
            using (var transaction = _categoryRepository.DatabaseTransaction())
                try
                {
                    var addCategory = new Category
                    {
                        CategoryName = addCategoryRequest.CategoryName,
                        Description = addCategoryRequest.Description,
                    };
                    var category = await _categoryRepository.CreateAsync(addCategory);

                    _categoryRepository.SaveChanges();

                    transaction.Commit();

                    return new AddCategoryResponse
                    {
                        IsSucced = true,
                    };
                }
                catch (Exception)
                {
                    transaction.RollBack();

                    return new AddCategoryResponse
                    {
                        IsSucced = false,
                    };
                }
        }

        //public async Task<BookCategoryDetail> GetBooksByCategoryIdAsync(int id)
        //{
        //    using (var transaction = _detailRepository.DatabaseTransaction())
        //        try
        //        {
        //            var book = await _detailRepository.GetAsync(c => c.CategoryId == id);

        //            if (book == null)
        //            {
        //                return null;
        //            }

        //            return new BookCategoryDetail
        //            {
        //                BookId = book.BookId,
        //                CategoryId = id,
        //            };

        //            transaction.Commit();
        //        }
        //        catch
        //        {
        //            transaction.RollBack();

        //            return null;
        //        }
        //}

        public async Task<CategoryViewModel> GetCategoryByIdAsync(int id)
        {
            using (var transaction = _categoryRepository.DatabaseTransaction())
                try
                {
                    var category = await _categoryRepository.GetAsync(c => c.CategoryId == id, x=> x.Book);

                    if (category == null)
                    {
                        return null;
                    }

                    transaction.Commit();

                    return new CategoryViewModel
                    {
                        CategoryId = category.CategoryId,
                        CategoryName = category.CategoryName,
                        Description = category.Description
                    };
                }
                catch
                {
                    transaction.RollBack();

                    return null;
                }
        }

        public async Task<UpdateCategoryResponse> UpdateAsync(int Id,UpdateCategoryRequest updateCategoryRequest)
        {
            using (var transaction = _categoryRepository.DatabaseTransaction())
                try
                {
                    var category = await _categoryRepository.GetAsync(s => s.CategoryId == Id, x=> x.Book);
                    if (category != null)
                    {
                        category.CategoryId = Id;
                        category.CategoryName = updateCategoryRequest.CategoryName;
                        category.Description = updateCategoryRequest.Description;

                        _categoryRepository.SaveChanges();
                    }
                    _categoryRepository.UpdateAsync(category);

                    transaction.Commit();

                    return new UpdateCategoryResponse
                    {
                        IsSucced = true,
                    };
                }
                catch (Exception)
                {
                    transaction.RollBack();

                    return new UpdateCategoryResponse
                    {
                        IsSucced = false,
                    };
                }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var transaction = _categoryRepository.DatabaseTransaction())
                try
                {
                    var category = await _categoryRepository.GetAsync(s => s.CategoryId == id, x=> x.Book);
                    if (category != null)
                    {
                        _categoryRepository.DeleteAsync(category);

                        _categoryRepository.SaveChanges();
                    }
                    transaction.Commit();

                    return true;
                }
                catch (Exception)
                {
                    transaction.RollBack();

                    return false;
                }
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllWithOdata(x => true, null);
        }
    }
}
