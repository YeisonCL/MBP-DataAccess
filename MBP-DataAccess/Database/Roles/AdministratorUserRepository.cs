using MBP_Cross.DTO.DatabaseDTO;
using MBP_Logic.Interface.RepositoryInterface.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBP_DataAccess.Database.Roles
{
    public class AdministratorUserRepository : IAdministratorUserRepository
    {
        /// <summary>
        /// Agrega una nueva fila a la tabla ADMIN_USER con los datos dados
        /// </summary>
        /// <param name="pUserData">Nuevos datos</param>
        public void addAdminUser(AdminUserDTO pUserData)
        {
            throw new NotImplementedException();
        }
    }
}
