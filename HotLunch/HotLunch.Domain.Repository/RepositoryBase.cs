using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotLunch.Domain.Repository
{
    public abstract class RepositoryBase
    {
        private readonly string _connectionStringName;

        protected RepositoryBase(string connectionStringName)
        {
            _connectionStringName = connectionStringName;
        }

        protected Database GetDatabase()
        {
            return new Database(_connectionStringName);
        }

        protected Database GetDatabase(string connectionStringName)
        {
            return new Database(connectionStringName);
        }
    }
}
