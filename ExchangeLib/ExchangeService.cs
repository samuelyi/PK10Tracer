﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WolfInv.com.PK10CorePress;
using WolfInv.com.Strags;
using System.Data;
using WolfInv.com.LogLib;
using WolfInv.com.BaseObjectsLib;
using System.Timers;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace WolfInv.com.ExchangeLib
{
     [Serializable]
     public class ExchangeDataTable : DataTable
    {
        Int64 Eindex = 0;
        double Odds = 0;
        public ExchangeDataTable()//纯粹为支持反序列化而建，从不调用
        {

            //Odds = 9.75;
            //InitColumns();
        }
        protected ExchangeDataTable(SerializationInfo info, StreamingContext context):base(info,context)
        {
            
            ////Odds = 9.75;
            ////InitColumns();
            ////DataTable dt = info.GetValue("this",typeof(DataTable)) as DataTable;
            ////this.Rows.Add(dt.Select());
            //base.GetObjectData(info, context);
            //Value = info.GetBoolean("Test_Value");
        }
        public ExchangeDataTable(double odds):base()
        {
            Odds = odds;
            InitColumns();
        }

        void InitColumns()
        {
            string cols = "Id,ExpectNo,ExExpectNo,ChanceCode,OccurStrag,Odds,Chips,Amount,ExecRate,Cost,Gained,Profit,CreateTime,UpdateTime,StragId,UserId";
            string[] colArr = cols.Split(',');
            for (int i = 0; i < colArr.Length; i++)
            {
                this.Columns.Add(colArr[i]);
            }
        }

        public ExchangeChance<T> AddAChance<T>(ExchangeChance<T> ec) where T : TimeSerialData
        {
            DataRow dr = this.NewRow();
            Eindex++;
            ec.Id = Eindex;
            dr["Id"] = Eindex;
            dr["ExpectNo"] = long.Parse(ec.ExpectNo) ;
            dr["ExExpectNo"] = ec.ExExpectNo;
            dr["ChanceCode"] = ec.OwnerChance.ChanceCode;
            dr["Chips"] = ec.OwnerChance.ChipCount;
            dr["Odds"] = ec.OccurStrag.CommSetting.Odds;
            dr["Amount"] = ec.ExchangeAmount;
            dr["ExecRate"] = ec.ExchangeRate;
            dr["Cost"] = ec.Cost;
            dr["CreateTime"] = DateTime.Now.ToString();
            Rows.Add(dr);
            return ec;
        }

        public bool UpdateChance<T>(ExchangeChance<T> ec,out double Gained) where T:TimeSerialData
        {
            string sqlmodule = "{0}='{1}'";
            Gained = 0;
            string sql = string.Format(sqlmodule, "Id", ec.Id);
            DataRow[] drs = this.Select(sql,"");
            if (drs.Length < 1)
            {
                //throw new Exception("需要更新的下注Id不存在");
                return false;
            }
                
            DataRow dr = drs[0];
            ec.OwnerChance.Odds = double.Parse(dr["Odds"].ToString());
            double CurrOdds = ec.OwnerChance.getRealOdds(); //double.Parse(dr["Odds"].ToString());
            double dGained = CurrOdds * ec.ExchangeAmount;
            double dCost = (double)ec.Cost;
            Gained = 0;
            if (ec.MatchChips > 0)
            {
                Gained = dGained;
            }
            dr["Gained"] = Gained;
            dr["Profit"] = dGained - dCost;
            dr["UpdateTime"] = DateTime.Now.ToString();
            return true;
        }
    }

}
