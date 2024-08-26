namespace EFP.RequestList.Libraries.DataStructures.DataBase
{
    /// <summary>
    /// Platform-specific user data.
    /// </summary>
    public class UserAlias
    {
        /// <summary>
        /// User alias ID.
        /// </summary>
        public uint Id { get; set; }
        /// <summary>
        /// User alias specific to a platform.
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// User whom alias is assigned to.
        /// </summary>
        public uint? UserId { get; set; }
        public User? User { get; set; }
        /// <summary>
        /// Platform hosting alias.
        /// </summary>
        public uint PlatformId { get; set; }
        public Platform Platform { get; set; }
    }
}
