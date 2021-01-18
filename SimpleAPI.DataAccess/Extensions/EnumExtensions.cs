using Microsoft.AspNetCore.Identity;
using SimpleAPI.Common.Enums;

namespace SimpleAPI.DataAccess.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Make sure the Postions are retrieve is always the same Id, and String
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static Position ToPositionEntity(this PlayerPosition role)
        {
            return new Position()
            {
                PositionId = ((int)role),
                Name = role.ToString()
            };
        }

        /// <summary>
        /// Make sure the Identity retrieve is always the same Id, and String
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public static IdentityRole AsIdentityRole(this SCCRoles role)
        {

            return new IdentityRole(role.ToString())
            {
                Id = ((int)role).ToString(),
                NormalizedName = role.ToString().ToUpper()
            };
        }
    }
}
