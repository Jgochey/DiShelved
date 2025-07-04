﻿using DiShelved.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DiShelved.Interfaces
{
    public interface ILocationService
    {
        Task<IEnumerable<Location>> GetLocationsByUserIdAsync(int userId);
        Task<Location?> GetLocationByIdAsync(int id);
        Task<IEnumerable<Location>> GetLocationsByUserUidAsync(string Uid);
        Task<Location> CreateLocationAsync(Location Location);
        Task<Location> UpdateLocationAsync(int Id, Location Location);
        Task<bool> DeleteLocationAsync(int id);
    }
}
