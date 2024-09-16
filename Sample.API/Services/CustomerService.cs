using Microsoft.AspNetCore.JsonPatch;
using Sample.API.Infrastructure;
using Sample.API.Models;
using Sample.API.Persistence.Repositories;
using Sample.API.Services.Interfaces;
using System.Linq.Expressions;

namespace Sample.API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Retrieve customers with optional filtering and pagination
        public async Task<PaginatedResult<Customer>> GetCustomersAsync(Expression<Func<Customer, bool>> filter = null, int page = 1, int pageSize = 10)
        {
            return await _unitOfWork.Customers.FilterAsync(filter, page, pageSize, orderBy: c => c.OrderBy(cust => cust.Name));
        }

        // Retrieve a single customer by ID
        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            return await _unitOfWork.Customers.GetByIdAsync(customerId);
        }

        // Create a new customer
        public async Task AddCustomerAsync(Customer customer)
        {
            await _unitOfWork.Customers.AddAsync(customer);
            await _unitOfWork.CompleteAsync();
        }

        // Update an existing customer
        public async Task UpdateCustomerAsync(Customer customer)
        {
            _unitOfWork.Customers.Update(customer);
            await _unitOfWork.CompleteAsync();
        }

        // Patch a customer
        public async Task PatchCustomerAsync(int customerId, JsonPatchDocument<Customer> patchDoc)
        {
            // Fetch the order by ID
            var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);

            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {customerId} not found.");
            }

            // Apply the patch
            _unitOfWork.Customers.ApplyPatch(customer, patchDoc);

            // Save changes via Unit of Work
            await _unitOfWork.CompleteAsync();
        }

        // Delete a customer
        public async Task DeleteCustomerAsync(int customerId)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);
            if (customer != null)
            {
                _unitOfWork.Customers.Delete(customer);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}
