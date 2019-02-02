﻿using Domain.Entity;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IClientStatService : IService<client>
    {
        int getNumberNewClients();
        int getNumberClients();
        int getNumberClientsOnGoing();
        List<string> getListOfAdress();
    }
}