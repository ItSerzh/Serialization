﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Serialization
{
    //[Serializable]
    public class F
    {
        [JsonInclude]
        int i1, i2, i3, i4, i5;
        public F Get() => new F() { i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 };
    }
}