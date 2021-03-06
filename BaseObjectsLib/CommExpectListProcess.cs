﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfInv.com.BaseObjectsLib
{
    public abstract class CommExpectListProcess<T> : ICommExpectListProcess<T> where T:TimeSerialData
    {
        protected ExpectList<T> Parent_data;
        protected CommExpectListProcess(ExpectList<T> _data)
        {
            Parent_data = _data;
        }
        public abstract List<Dictionary<int, string>> getNoDispNums(int reviewCnt);
        public abstract BaseCollection<T> getSerialData(int reviewCnt, bool ByNo) ;
    }

    public interface ICommExpectListProcess<T> where T : TimeSerialData
    {
        List<Dictionary<int, string>> getNoDispNums(int reviewCnt);
        //FullRepeatInfo getRepeatDic(int buffs, int reviewCnt, bool byNo);
        //Dictionary<int, RepeatInfo> getRepeatInfo(int frm, int cnt, bool byNo);
        BaseCollection<T> getSerialData(int reviewCnt, bool ByNo);
    }
}
