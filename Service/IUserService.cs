﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Service.Pattern;

namespace Service
{
    public interface IUserService:IService<user>
    {
    }
}
