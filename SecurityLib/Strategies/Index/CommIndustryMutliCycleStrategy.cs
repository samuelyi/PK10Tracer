﻿using System;
using WolfInv.com.BaseObjectsLib;
using WolfInv.com.GuideLib;
namespace WolfInv.com.SecurityLib
{
    #region 基础类及接口
    #endregion


    /// <summary>
    /// 多周期行业选股策略类
    /// </summary>
    public class CommIndustryMutliCycleStrategy : CommIndustryStrategy
    {
        public CommIndustryMutliCycleStrategy(CommDataIntface _w) : base(_w) { }
        public override CommSecurityProcessClass ReverseSelectSecurity(CommStrategyInClass Input)
        {
            CommMutliCycleIndustryStrategyInParams InParam = null;
            return null;
        }

        public override CommSecurityProcessClass BreachSelectSecurity(CommStrategyInClass Input)
        {
            throw new NotImplementedException();
        }

        public override CommSecurityProcessClass BalanceSelectSecurity(CommStrategyInClass Input)
        {
            throw new NotImplementedException();
        }

        public override BaseDataTable ReadSecuritySerialData(string Code)
        {
            throw new NotImplementedException();
        }
    }
}
