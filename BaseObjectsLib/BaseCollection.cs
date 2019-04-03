﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace WolfInv.com.BaseObjectsLib
{
    public abstract class BaseCollection<T>:IBaseCollection where T:TimeSerialData
    {
        public DataTable Table { get; }
        public abstract DataTableEx CarDistributionTable { get; }
        public abstract DataTableEx CarTable { get; }
        public abstract DataTableEx SerialDistributionTable { get; }

        public abstract bool isByNo { get; set; }

        public List<Dictionary<int, string>> Data;
        public ExpectList<T> orgData;
        public abstract List<double> getAllDistrStdDev(int n,int c);
        public abstract DataTableEx getSubTable(int cnt, int n);
        public abstract int FindLastDataExistCount(int StartPos, int lng, string StrKey, string val);
        public abstract int FindLastDataExistCount(int lng, string StrPos, string key);
        public abstract string FindSpecColumnValue(int id, string strKey);
        public abstract Dictionary<string, double> getAllShiftCnt(int ReviewCnt, int TrainCnt);
        public abstract Dictionary<string, Matrix> getC_K_NStep(int reviewCnt, int StepCnt);
        public abstract List<double> getEntropyList(int reviewCnt);
    }

    public interface IBaseCollection
    {
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