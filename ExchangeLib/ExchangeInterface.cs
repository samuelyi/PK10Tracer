﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WolfInv.com.PK10CorePress;
using WolfInv.com.Strags;
using WolfInv.com.BaseObjectsLib;
namespace WolfInv.com.ExchangeLib
{
    ////public class ExchangeInterface
    ////{
    ////    public bool Save(List<ExchanceClass> ccs)
    ////    { return true; }
    ////}

    public class ExchangeChance<T>:MarshalByRefObject where T:TimeSerialData
    {
        public Int64 Id;
        ChanceClass<T> _cc;
        BaseStragClass<T> _sc;
        ExchangeService es;
        public ExchangeService Server { get { return es; } }
        string _InExpect;
        public string ExpectNo
        {
            get
            {
                return _InExpect;
            }
        }

        public string ExExpectNo
        {
            get;set;
        }

        public ChanceClass<T> OwnerChance
        {
            get
            {
                return _cc;
            }
        }

        public BaseStragClass<T> OccurStrag
        {
            get
            {
                return _sc;
            }
        }

        public ExchangeChance(ExchangeService _es,BaseStragClass<T> sc,string InExpectNo, string CurrExpectNo, ChanceClass<T> cc)
        {
            _cc = cc;
            _sc = sc;
            _InExpect = InExpectNo;
            ExExpectNo = CurrExpectNo;
            es = _es;
        }

        public Int64 ExchangeAmount;
        public int MatchChips;
        public double ExchangeRate;
        public Int64 Cost
        {
            get
            {
                return this.ExchangeAmount * this.OwnerChance.ChipCount;
            }
        }
        public double Gained;
        public double Profit
        {
            get
            {
                return Gained - Cost;
            }
        }

        

    }
    
}
