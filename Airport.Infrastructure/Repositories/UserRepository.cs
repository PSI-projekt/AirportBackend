using System;
using System.Linq;
using System.Threading.Tasks;
using Airport.Domain.Models;
using Airport.Infrastructure.Interfaces;
using Airport.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Airport.Domain.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Airport.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AirportDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(AirportDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<User> GetUserById(int userId)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        public async Task<bool> Edit(UserForEditDto userForEdit)
        {
            var user = await GetUserById(userForEdit.Id);
            try
            {
                _mapper.Map(userForEdit, user);
                return await Update(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        private async Task<bool> Update(User user)
        {
            try
            {
                _context.Update(user);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        public async Task<bool> Delete(int userId)
        {
            var user = await GetUserById(userId);
            try
            {
                user.Username = null;
                user.Email = null;                
                user.IsDeleted = true;
                return await Update(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}