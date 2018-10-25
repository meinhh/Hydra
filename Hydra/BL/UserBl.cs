using System;
using Hydra.DAL;
using Hydra.Data;
using Hydra.Models;

namespace Hydra.BL
{
    public class UserBl
    {
        private readonly UserDataAccess _userDal;

        public UserBl(HydraContext context)
        {
            _userDal = new UserDataAccess(context);
        }

        public void AddUser(string id, string name, Gender gender)
        {
            if (_userDal.IsUserExist(id))
                return;

            _userDal.AddUser(id, name, gender);
        }
    }
}
