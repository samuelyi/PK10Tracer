﻿using System;
using System.Collections.Generic;
using WolfInv.com.LogLib;
using WolfInv.com.BaseObjectsLib;
namespace WolfInv.com.SecurityLib
{
    public abstract class DataReader : LogableClass,IDataReader
    {
        /// <summary>
        /// 数据类型，PK10,TXFFC
        /// </summary>
        protected string strDataType;

        protected string strNewestTable;
        protected string strHistoryTable;
        protected string strMissHistoryTable;
        protected string strMissNewestTable;
        protected string strResultTable;
        protected string strChanceTable;

        public abstract ExpectList GetMissedData(bool IsHistoryData, string strBegT);
        public abstract ExpectList getNewestData(ExpectList NewestData, ExpectList ExistData);
        public abstract DbChanceList getNoCloseChances(string strDataOwner);
        public abstract ExpectList ReadHistory();
        public abstract ExpectList ReadHistory(long buffs);
        public abstract ExpectList ReadHistory(long From, long buffs);
        public abstract ExpectList ReadHistory(long From, long buffs, bool desc);
        public abstract ExpectList ReadHistory(string begt, string endt);
        public abstract ExpectList ReadNewestData(DateTime fromdate);
        public abstract ExpectList ReadNewestData(int LastLng);
        public abstract ExpectList ReadNewestData(int ExpectNo, int Cnt);
        public abstract ExpectList ReadNewestData(int ExpectNo, int Cnt, bool FromHistoryTable);
        public abstract int SaveChances(List<ChanceClass> list, string strDataOwner);
        public abstract int SaveHistoryData(ExpectList InData);
        public abstract int SaveNewestData(ExpectList InData);

        protected void InitTables()
        {
            if (GlobalClass.SystemDbTables == null)
            {
                return;
            }
            if (GlobalClass.SystemDbTables.ContainsKey(this.strDataType))
            {
                Dictionary<string, string> dic = GlobalClass.SystemDbTables[this.strDataType];

                if (dic.ContainsKey("NewestTable")) strNewestTable = dic["NewestTable"];
                if (dic.ContainsKey("HistoryTable")) strHistoryTable = dic["HistoryTable"];
                if (dic.ContainsKey("MissHistoryTable")) strMissHistoryTable = dic["MissHistoryTable"];
                if (dic.ContainsKey("MissNewestTable")) strMissNewestTable = dic["MissNewestTable"];
                if (dic.ContainsKey("ResultTable")) strResultTable = dic["ResultTable"];
                if (dic.ContainsKey("ChanceTable")) strChanceTable = dic["ChanceTable"];
            }
        }

    }


}