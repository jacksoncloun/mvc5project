﻿using Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IBll
{
    public interface IRedisService
    {
        void SaveInRedis();
    }
}
