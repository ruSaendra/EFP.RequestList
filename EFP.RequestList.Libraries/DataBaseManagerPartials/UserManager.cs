using EFP.RequestList.Libraries.DataStructures.DataBase;
using EFP.RequestList.Libraries.Filters;
using EFP.RequestList.Libraries.HelperClasses;
using Microsoft.EntityFrameworkCore;

namespace EFP.RequestList.Libraries
{
    // Partial containing methods working with users.
    public static partial class DataBaseManager
    {
        /// <summary>
        /// Query users from base.
        /// </summary>
        /// <param name="amount">Amount of records to query</param>
        /// <param name="startId">User ID to start querying from</param>
        /// <param name="includeAliases">Include user aliases</param>
        /// <param name="filter">Filter applied to query</param>
        /// <returns></returns>
        public static List<User> QueryUsers(int amount = Int32.MaxValue, uint startId = 0, bool includeAliases = false, UserFilter? filter = null)
            => db
                .UserSet
                .IncludeWithCheck(includeAliases, u => u.UserAliases)
                .TryFilter(filter)
                .Where(u => u.Id >= startId)
                .Take(amount)
                .ToList();

        /// <summary>
        /// Query user aliases from base
        /// </summary>
        /// <param name="amount">Amount of records to query</param>
        /// <param name="startId">User alias ID to start querying from</param>
        /// <param name="includeUser">Include user owning alias</param>
        /// <param name="filter">Filter applied to query</param>
        /// <returns></returns>
        public static List<UserAlias> QueryUserAliases(int amount = Int32.MaxValue, uint startId = 0, bool includeUser = false, UserAliasFilter? filter = null)
            => db
                .UserAliasSet
                .IncludeWithCheck(includeUser, ua => ua.User)
                .TryFilter(filter)
                .Where(u => u.Id >= startId)
                .Take(amount)
                .ToList();

        /// <summary>
        /// Check if user with specified username exists
        /// </summary>
        /// <param name="userName">Username to check</param>
        /// <returns></returns>
        public static bool IsUserExists(string userName) 
            => db
                .UserSet
                .Where(u => u.Name == userName)
                .Any();

        /// <summary>
        /// Check if alias with specified name and platform exists
        /// </summary>
        /// <param name="userAlias">Alias name to check</param>
        /// <param name="platformId">ID of platform owning the aliaas</param>
        /// <returns></returns>
        public static bool IsAliasExists(string userAlias, uint platformId)
            => db
                .UserAliasSet
                .Where(ua => ua.Alias == userAlias && ua.PlatformId == platformId)
                .Any();

        /// <summary>
        /// Get user owning alias with specified ID.
        /// </summary>
        /// <param name="aliasId">Alias ID</param>
        /// <returns></returns>
        public static User? GetAliasOwner(uint aliasId)
            => db
                .UserAliasSet
                .Where(ua => ua.Id == aliasId)
                .Include(ua => ua.User)
                .Select(ua => ua.User)
                .FirstOrDefault();

        /// <summary>
        /// Get user owning alias with specified name and platform
        /// </summary>
        /// <param name="userAlias">Alias name</param>
        /// <param name="platformId">Platform</param>
        /// <returns></returns>
        public static User? GetAliasOwner(string userAlias, uint platformId)
            => db
                .UserAliasSet
                .Where(ua => ua.Alias == userAlias && ua.PlatformId == platformId)
                .Include(ua => ua.User)
                .Select(ua => ua.User)
                .FirstOrDefault();

        /// <summary>
        /// Get all aliases owned by user
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns></returns>
        public static List<UserAlias> GetUserAliases(int userId)
            => db
                .UserSet
                .Where(u => u.Id == userId)
                .Include(u => u.UserAliases)
                .SelectMany(u => u.UserAliases)
                .ToList();

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="userName">Username</param>
        /// <param name="platform">Platform if specified</param>
        /// <param name="alias">Alias on specified platform</param>
        /// <exception cref="Exception"></exception>
        public static void CreateUser(string userName, Platform? platform = null, string? alias = null)
        {
            if (IsUserExists(userName))
                throw new Exception($"User {userName} already exists.");

            var user = new User()
            {
                Name = userName,
            };
            
            if (platform != null)
            {
                var userAlias = new UserAlias()
                {
                    Platform = platform,
                    Alias = alias.IsNullOrEmpty() ? userName : alias,
                };

                user.UserAliases.Add(userAlias);
            }

            db.UserSet.Add(user);
            db.SaveChanges();
        }

        /// <summary>
        /// Edit user data
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="newUserName">New username</param>
        public static void EditUser(uint userId, string? newUserName = null)
        {
            var user = db.UserSet.SingleOrDefault(u => u.Id == userId);

            if (user == null)
                return;

            if(newUserName != null)
                user.Name = newUserName;

            db.SaveChanges();
        }

        /// <summary>
        /// Add new alias to user
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="alias">New alias name</param>
        /// <param name="platformId">Platform hosting alias</param>
        public static void AddAliasToUser(uint userId, string aliasName, uint platformId)
        {
            var user = db
                .UserSet
                .First(u => u.Id == userId);

            if (user == null) return;

            var alias = _db?
                .UserAliasSet
                .First(ua => ua.Alias == aliasName && ua.PlatformId == platformId);

            if (alias == null)
            {
                user.UserAliases.Add(new UserAlias()
                {
                    Alias = aliasName,
                    PlatformId = platformId
                });
                db.SaveChanges();
                return;
            }

            alias.UserId = userId;
            db.SaveChanges();
        }
    }
}
