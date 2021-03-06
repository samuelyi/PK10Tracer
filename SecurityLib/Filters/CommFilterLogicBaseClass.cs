﻿using System;
using WolfInv.com.BaseObjectsLib;
namespace WolfInv.com.SecurityLib
{
    /// <summary>
    /// 过滤逻辑基类
    /// </summary>
    public abstract class CommFilterLogicBaseClass : iCommCallBackable
    {
        public CommStrategyBaseClass ExecStrategy { get; set; }
        public BaseDataItemClass BaseInfo;
        public string secCode;
        public DateTime Endt;
        public PriceAdj Rate;
        public Cycle Cycle;
        /// <summary>
        /// 是否右侧选证券
        /// </summary>
        public bool IsRightSelect;
        public CommSecurityProcessClass SecObj;
        public string FilterSubFunc;
        /// <summary>
        /// 回览视窗周期数
        /// </summary>
        public int PassViewDays;
        /// <summary>
        /// 缓冲周期数
        /// </summary>
        public int BuffDays;

        public CommFilterLogicBaseClass(CommSecurityProcessClass secinfo)
        {
            SecObj = secinfo;
        }

        public CommFilterLogicBaseClass(Cycle cyc,PriceAdj rate )
        {
            Cycle = cyc;
            Rate = rate;
        }


        public virtual CommSecurityProcessClass ExecFilter()
        {
            return SecObj;
        }

        public abstract BaseDataTable GetData(int RecordCnt);
    }
}
