﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WolfInv.com.BaseObjectsLib
{
    public interface ISpecAmount
    {
        Int64 getChipAmount(double RestCash, ChanceClass cc, AmoutSerials amts);
    }
    public interface ITraceChance:ISpecAmount
    {
        bool IsTracing { get; set; }
        bool CheckNeedEndTheChance(ChanceClass cc, bool LastExpectMatched);
    }

    
}