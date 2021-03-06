﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WolfInv.com.PK10CorePress;
using System.ComponentModel;
using WolfInv.com.ProbMathLib;
using WolfInv.com.MachineLearnLib;
using WolfInv.com.BaseObjectsLib;
using System.IO;
using System.Reflection;

namespace WolfInv.com.Strags
{

    interface INeedLoadLocalTrainData
    {
        string LoadLocalTrainData();
    }

    [DescriptionAttribute("简单最大熵选号策略"),
        DisplayName("简单最大熵选号策略")]
    [Serializable]
    public class Strag_SimpleMaxEntryClass : ChanceTraceStragClass,INeedLoadLocalTrainData
    { 
        int StepCnt = 5;
        int MinFilterCnt = 20;
        int MaxFilterCnt = 100;
        public Strag_SimpleMaxEntryClass()
            : base()
        {
            _StragClassName = "简单最大熵选号策略";
        }

        /// <summary>
        /// 计算峰值
        /// </summary>
        /// <param name="N">回览次数</param>
        /// <param name="K">成功次数</param>
        /// <param name="p">正常概率</param>
        
        

        public override List<ChanceClass> getChances(BaseCollection sc, ExpectData ed)
        {
            List<ChanceClass> ret = new List<ChanceClass>();
            MLDataFactory pkdls = new MLDataFactory();// this.LastUseData());
            //Dictionary<int, Dictionary<int, int>> res = pkdls.getAllShiftAndColMaxProbList(this.ReviewExpectCnt- this.InputMinTimes-1, this.InputMinTimes,true);
            List<MLFeature<int>> features = pkdls.getAllSpecRowRoundFeatures(this.LastUseData().Count - 1, this.InputMinTimes,0);
            Dictionary<string, int> AllCodes = new Dictionary<string, int>();
            for (int i = 0; i < features.Count; i++)
            {
                MLFeature<int> feature = features[i];
                MaxEnt me = new MaxEnt();
                if (MaxEnt.FeatureSummary == null)
                {
                    me.OnLoadLocalFile = LoadLocalTrainData;
                    me.LoadSummary();
                }
                me.FillStructBySummary(i);
                MLInstance<int, int> instance = new MLInstance<int, int>(feature);
                int label = me.Classify(instance);
                if (label < 0)
                    continue;
                string strAllCode = string.Format("{0}/{1}", (i+1)%10, label);
                AllCodes.Add(strAllCode, 1);
            }
            
            ////foreach (int key in res.Keys)
            ////    AllCodes.Add(string.Format("{0}/{1}",key,string.Join("",res[key].Keys.ToArray())),1);
            
                      
            if (true)
            {
                ChanceClass cc = new ChanceClass();
                cc.SignExpectNo = ed.Expect;
                cc.ChanceType = 2;
                cc.InputTimes = 1;
                cc.strInputTimes = string.Join("_", new string[2] { "1", "1"});
                //cc.AllowMaxHoldTimeCnt = this.AllowMaxHoldTimeCnt;
                cc.InputExpect = ed;
                cc.StragId = this.GUID;
                //cc.MinWinRate = this.CommSetting.GetGlobalSetting().DefaultHoldAmtSerials.MaxRates[cc.ChipCount - 1];
                cc.IsTracer = 0;
                cc.HoldTimeCnt = 1;
                cc.Odds = this.CommSetting.GetGlobalSetting().Odds;
                cc.ExpectCode = ed.Expect;
                cc.ChanceCode = string.Join("+", AllCodes.Keys.ToArray());
                cc.ChipCount = ChanceClass.getChipsByCode(cc.ChanceCode);
                cc.CreateTime = DateTime.Now;
                cc.UpdateTime = DateTime.Now;
                cc.AllowMaxHoldTimeCnt = 1;
                cc.Closed = false;
                ret.Add(cc);
            }

            return ret;
        }

        public override StagConfigSetting getInitStagSetting()
        {
            return new StagConfigSetting();
        }

        
        public override Type getTheChanceType()
        {
            return typeof(NolimitTraceChance);
        }

        bool _IsTracing;
        public bool IsTracing
        {
            get
            {
                return _IsTracing;
            }
            set
            {
                _IsTracing = value;
            }
        }

        public override bool CheckNeedEndTheChance(ChanceClass cc, bool LastExpectMatched)
        {
            return true;
        }

        public override long getChipAmount(double RestCash, ChanceClass cc, AmoutSerials amts)
        {
            try
            {
                
                if (cc.IncrementType == InterestType.CompoundInterest)
                {
                    if (cc.AllowMaxHoldTimeCnt > 0 && cc.HoldTimeCnt > 1)
                    {
                        return 0;
                    }
                    //
                    double rate = cc.FixRate.Value;
                    //double rate = KellyMethodClass.KellyFormula(1, 9.75, 0.1032);
                    long ret = (long)Math.Ceiling((double)(RestCash * rate));
                    return ret;
                }
                if ( cc.HoldTimeCnt > 1)
                {
                    return 0;
                }
                return cc.FixAmt.Value;

            }
            catch (Exception e)
            {
                Log("错误", string.Format("获取单码金额错误:{0}", e.Message), e.StackTrace);
            }
            return 1;
        }

        public string LoadLocalTrainData()
        {
            try
            {
                DetailStringClass dsc = new DetailStringClass();
                string pathSummaryPath = string.Format("{0}\\{1}_summary.data", Path.GetDirectoryName(this.GetType().Assembly.Location), typeof(MaxEnt).Name);
                string strText = File.ReadAllText(pathSummaryPath);
                return strText;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }

 
}
