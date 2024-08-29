namespace EFP.RequestList.Libraries.DataStructures.DataBase
{
    /// <summary>
    /// Requested user data.
    /// </summary>
    public class User: BaseEntity
    {
        /// <summary>
        /// Common user name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// List of user aliases.
        /// </summary>
        public List<UserAlias> UserAliases { get; set; }
    }
}
