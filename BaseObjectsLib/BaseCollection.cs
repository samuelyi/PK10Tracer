﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace WolfInv.com.BaseObjectsLib
{
    public abstract class BaseCollection<T> : IBaseCollection where T : TimeSerialData
    {
        public abstract AmoutSerials getOptSerials(DataTypePoint dtp, string type, int len, double odds, Int64 MaxValue, int FirstAmt, bool NeedAddFirst);
        public abstract DataTable getTableFromSpecCondition(DataTypePoint dtp,string currExpect, int Period, string strPos, string strTaget, params object[] others);
        /// <summary>
        /// 外部现成的统计数据，可以直接供策略统计分析，不必自己重新从数据库中计算结果
        /// </summary>
        public abstract DataSet ExDataTable(DataTypePoint dtp, string expect, Func<DataTypePoint,string, DataSet> convertFunc);
        public abstract DataTable Table { get; }
        public abstract DataTableEx CarDistributionTable { get; }
        public abstract DataTableEx CarTable { get; }
        public abstract DataTableEx SerialDistributionTable { get; }

        public abstract bool isByNo { get; set; }

        public List<Dictionary<int, string>> Data;
        public ExpectList<T> ___orgData;
        public abstract List<double> getAllDistrStdDev(int n,int c);
        public abstract DataTableEx getSubTable(int cnt, int n);
        public abstract int FindLastDataExistCount(int StartPos, int lng, string StrKey, string val);
        public abstract int FindLastDataExistCount(int lng, string StrPos, string key);
        public abstract string FindSpecColumnValue(int id, string strKey);
        public abstract Dictionary<string, double> getAllShiftCnt(int ReviewCnt, int TrainCnt);
        public abstract Dictionary<string, Matrix> getC_K_NStep(int reviewCnt, int StepCnt);
        public abstract List<double> getEntropyList(int reviewCnt);

        public abstract bool isMatch(string code, string currCode);
    }

    public interface IBaseCollection
    {
        /// <summary>
        /// 从指定条件获得表，兼容外部Web和本地数据库
        /// </summary>
        /// <param name="dtp"></param>
        /// <param name="Period"></param>
        /// <param name="strPos"></param>
        /// <param name="strTaget"></param>
        /// <returns></returns>
        DataTable getTableFromSpecCondition(DataTypePoint dtp, string currExpect, int Period, string strPos, string strTaget,params object[] others);
        DataSet ExDataTable(DataTypePoint dtp, string expect, Func<DataTypePoint,string, DataSet> convertFunc);
        DataTableEx CarDistributionTable { get; }
        DataTableEx CarTable { get; }
        DataTableEx SerialDistributionTable { get; }
        DataTable Table { get; }

        int FindLastDataExistCount(int StartPos, int lng, string StrKey, string val);
        int FindLastDataExistCount(int lng, string StrPos, string key);
        string FindSpecColumnValue(int id, string strKey);
        List<double> getAllDistrStdDev(int reviewCnt, int stepLong);
        Dictionary<string, double> getAllShiftCnt(int ReviewCnt, int TrainCnt);
        Dictionary<string, Matrix> getC_K_NStep(int reviewCnt, int StepCnt);
        List<double> getEntropyList(int reviewCnt);
        DataTableEx getSubTable(int FromId, int lng);
    }
}
