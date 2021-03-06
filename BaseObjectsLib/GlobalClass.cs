﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using WolfInv.com.DbAccessLib;
using WolfInv.com.LogLib;
using WolfInv.com.ProbMathLib;
namespace WolfInv.com.BaseObjectsLib
{
    [Serializable]
    public class GlobalClass : DetailStringClass,iLog
    {
        static GlobalClass()
        {

            ReadConfig();
            //ReReadStragList();
        }
        public bool loadSucc;//判断加载是否成功，为客户端子目录加载准备的

        public GlobalClass()
        {
            loadSucc = ReadConfig("");
        }
        public GlobalClass(string forWeb)
        {

            loadSucc = ReadConfig(string.IsNullOrEmpty(forWeb)?"":string.Format("config\\{0}\\", forWeb));
        }

        static string _LogUser;
        static string _LogUrl;

        public static string LogUser
        {
            get
            {
                return _LogUser;
            }
        }

        public static string LogUrl
        {
            get
            {
                return _LogUrl;
            }
        }

        public static string WXLogHost
        {
            get { return _wxsvrhost; }
        }
        public static void resetTypeDataPoints()
        {
            _SystemDbTables = null;
            _dataType = null;
            _TypeDataPoints = null;
        }
        public static Dictionary<string, DataTypePoint> _TypeDataPoints;
        public static Dictionary<string, DataTypePoint> TypeDataPoints
        {
            get
            {
                if (_TypeDataPoints != null)
                    return _TypeDataPoints;
                _TypeDataPoints = new Dictionary<string, DataTypePoint>();
                foreach (string key in DataTypes.Keys)
                {
                    //ToLog("test_", string.Join(",",SystemDbTables[key].Select(a=>string.Format("{0}=>{1}",a.Key,a.Value).ToList().ToArray())));
                    if(!SystemDbTables.ContainsKey(key))
                    {
                        continue;
                    }
                    DataTypePoint dtp = new DataTypePoint(key, SystemDbTables[key]);
                    _TypeDataPoints.Add(key, dtp);
                }
                return _TypeDataPoints;
            }
            set //支持回写 2019/3/23
            {
                _TypeDataPoints = value;
            }
        }

        WebInfoClass _webinfo;
        public WebInfoClass WebInfo
        {
            get
            {
                if(_webinfo == null)
                {
                    if(!sSysParams.ContainsKey("WebInfo"))
                    {
                        return null;
                    }
                    _webinfo = new WebInfoClass(sSysParams["WebInfo"]);
                }
                return _webinfo;
            }
            set
            {
                _webinfo = value;
            }
        }

        

        static Dictionary<string, Dictionary<string, string>> _SystemDbTables;
        public static Dictionary<string, Dictionary<string, string>> SystemDbTables
        {
            get
            {
                if (_SystemDbTables == null)
                {
                    if (DataTypes == null)
                    {
                        return null;
                    }
                    _SystemDbTables = new Dictionary<string, Dictionary<string, string>>();
                    foreach (string key in DataTypes.Keys)
                    {
                        if (sSysParams.ContainsKey(key))
                        {
                            _SystemDbTables.Add(key, sSysParams[key]);
                        }
                    }
                }
                return _SystemDbTables;
            }
        }

        static Dictionary<string, string> _dataType;
        public static Dictionary<string, string> DataTypes
        {
            get 
            {
                if (_dataType == null)
                {
                    if (sSysParams == null)
                        return null;

                    if (!sSysParams.ContainsKey("DataType"))
                    {
                        //ToLog("初始化品种清单","获取品种分类定义错误");
                        return null;
                    }
                    _dataType = sSysParams["DataType"];
                }
                
                return _dataType;
            }
        }
        static Dictionary<string, string> _webSites;
        public static Dictionary<string, string> WebSites
        {
            get
            {
                if (_webSites == null)
                {
                    if (sSysParams == null)
                        return null;

                    if (!sSysParams.ContainsKey("WebSites"))
                    {
                        return null;
                    }
                    _webSites = sSysParams["WebSites"];
                }

                return _webSites;
            }
        }
        static Dictionary<string, Dictionary<string, string>> sSysParams;
        Dictionary<string, Dictionary<string, string>> SysParams
        {
            get
            {
                if (sSysParams == null)
                {
                    sSysParams = new Dictionary<string, Dictionary<string, string>>();
                }
                return sSysParams;
            }
        }
        public static string Title = "快乐赛车";
        public static string strLoginUrlModel = "http://www.wolfinv.com/PK10/App/login.asp?User={0}&Password={1}";
        public static string strRequestInstsURL = "http://www.wolfinv.com/pk10/app/requestinsts.asp";
        public static string strAssetInfoURL = "{0}/pk10/app/getAssetLists.asp?type={1}";
        public static string dbServer = "";// "www.wolfinv.com";//"47.95.222.142";//"www.wolfinv.com";
        public static string dbName = "";//"PK10db";
        public static string dbUser = "";//"sa";
        public static string dbPwd = "";//"bolts";
        public static string TXFFC_url = "http://www.off0.com/index.php";
        //public static string PK10_url = "http://d.apiplus.net/newly.do?token=tf066705d12dcb288k&code=bjpk10&format=xml&rows=100";
        //ExpectList t_newExpectData;
        bool b_AllowExchange;
        public static XmlDocument XmlDoc;
        
        public static Dictionary<string, AmoutSerials> AllSerialSettings;

        #region 自定义属性
        public string WXLogUrl
        {
            get
            {
                if (SysParams.ContainsKey("System"))
                {
                    if (SysParams["System"].ContainsKey("WXLogUrl"))
                    {
                        
                        _LogUrl = SysParams["System"]["WXLogUrl"];
                        return _LogUrl;
                    }
                }
                return null;
            }
        }

        static string _wxsvrhost;
        public string WXSVRHost
        {
            get
            {
                if (_wxsvrhost != null)
                {
                    return _wxsvrhost;
                }
                if (SysParams.ContainsKey("System"))
                {
                    if (SysParams["System"].ContainsKey("WXSVRHost"))
                    {
                        _wxsvrhost = SysParams["System"]["WXSVRHost"];
                        return _wxsvrhost;
                    }
                }
                return null;
            }
            set
            {
                _wxsvrhost = value;
                SysParams["System"]["WXSVRHost"] = value;
            }
        }
        public string WXLogNoticeUser
        {
            get
            {
                if(_LogUser != null)
                {
                    return _LogUser;
                }
                if (SysParams.ContainsKey("System"))
                {
                    if (SysParams["System"].ContainsKey("WXLogNoticeUser"))
                    {
                        _LogUser = SysParams["System"]["WXLogNoticeUser"];
                        return _LogUser;
                    }
                }
                return null;
            }
            set
            {
                _LogUser = value;
                SysParams["System"]["WXLogNoticeUser"] = value;
            }
        }

        public string SvrConfigUrl
        {
            get
            {
                if (SysParams.ContainsKey("System"))
                {
                    if (SysParams["System"].ContainsKey("SvrConfigUrl"))
                    {
                        return SysParams["System"]["SvrConfigUrl"];
                    }
                }
                return null;
            }
        }

        //////public string SvrScriptUrl
        //////{
        //////    get
        //////    {
        //////        if (SysParams.ContainsKey("System"))
        //////        {
        //////            if (SysParams["System"].ContainsKey("SvrScriptUrl"))
        //////            {
        //////                return SysParams["System"]["SvrScriptUrl"];
        //////            }
        //////        }
        //////        return null;
        //////    }
        //////}

        public string InstHost
        {
            get
            {
                if (SysParams.ContainsKey("System"))
                {
                    if (SysParams["System"].ContainsKey("InstHost"))
                    {
                        return SysParams["System"]["InstHost"];
                    }
                }
                return null;
            }
        }

        public string SendMsgFromWebRequest
        {
            get
            {
                if (SysParams.ContainsKey("System"))
                {
                    if (SysParams["System"].ContainsKey("SendMsgFromWebRequest"))
                    {
                        return SysParams["System"]["SendMsgFromWebRequest"];
                    }
                }
                return "0";
            }
        }

        public int ChipUnit
        {
            get
            {
                if (SysParams.ContainsKey("Exchange"))
                {
                    if (SysParams["Exchange"].ContainsKey("ChipUnit"))
                    {
                        return int.Parse(SysParams["Exchange"]["ChipUnit"]);
                    }
                }
                return 2;
            }
        }

        public string SysUser
        {
            get
            {
                if (SysParams.ContainsKey("System"))
                {
                    if (SysParams["System"].ContainsKey("SysUser"))
                    {
                        return SysParams["System"]["SysUser"];
                    }
                }
                return null;
            }
        }

        public static string LoadTextFile(string filename,string folder="")
        {
            string filepath = string.Format("{2}\\{1}\\{0}",filename,folder,AppDomain.CurrentDomain.BaseDirectory);
            if (!File.Exists(filepath))
                return null;
            string ret = null;
            try
            {
                ret = File.ReadAllText(filepath);
            }
            catch(Exception e)
            {
                return ret;
            }
            finally
            {
                
            }
            return ret;
        }

        public bool NeedAutoReset
        {
            get
            {
                if (SysParams.ContainsKey("System"))
                {
                    if (SysParams["System"].ContainsKey("needAutoReset"))
                    {
                        return SysParams["System"]["needAutoReset"]=="1";
                    }
                    
                }
                return false;
            }
        }

        public static Int64 DefaultMaxLost;
        public Int64 DefMaxLost
        {
            get
            {
                if (SysParams.ContainsKey("System"))
                {
                    if (SysParams["System"].ContainsKey("DefMaxLost"))
                    {
                        DefaultMaxLost = Int64.Parse(SysParams["System"]["DefMaxLost"]);
                        return DefaultMaxLost;
                    }
                }
                return 0;
            }
        }

        public int DefFirstAmt
        {
            get
            {
                if (SysParams.ContainsKey("System"))
                {
                    if (SysParams["System"].ContainsKey("DefFirstAmt"))
                    {
                        return int.Parse(SysParams["System"]["DefFirstAmt"]);
                    }
                }
                return 0;
            }
        }

        public bool AllowExchange
        {
            get
            {
                if(SysParams.Count > 0)
                {
                    b_AllowExchange= SysParams["System"]["AllowExchange"] == "1"? true:false;
                }
                return b_AllowExchange;
            }
            set
            {
                b_AllowExchange = value;
            }
        }
        string _ForWeb;
        public string ForWeb
        {
            get
            {
                if (_ForWeb == null)
                {
                    if (SysParams.Count > 0 && SysParams["System"].ContainsKey("ForWeb"))
                    {
                        _ForWeb = SysParams["System"]["ForWeb"];
                    }
                    return _ForWeb;
                } 
                return _ForWeb;
            }
            set
            {
                _ForWeb = value;
                SysParams["System"]["ForWeb"] = value;
            }
            
        }

        public string InstFormat
        {
            get
            {
                if (SysParams.Count > 0 && SysParams["System"].ContainsKey("InstFormat"))
                    return SysParams["System"]["InstFormat"];
                return ForWeb;//没有就返回forweb
            }
        }

        public int TotalCnt
        {
            get
            {
                if(SysParams.Count > 0)
                    return int.Parse((SysParams["System"]["TotalCnt"]));
                return 0;
            }
        }

        public bool AllowHedge
        {
            get
            {
                if(SysParams.Count > 0 && SysParams["System"].ContainsKey("AllowHedge"))
                    return SysParams["System"]["AllowHedge"]=="1"?true:false;
                return false;
            }
        }

        public bool JoinHedge
        {
            get
            {
                if(SysParams.Count > 0)
                    return SysParams["System"]["JoinHedge"]=="1"?true:false;
                return false;
            }
        }

        public long HedgeTimes
        {
            get
            {
                if(SysParams.Count > 0)
                    return long.Parse(SysParams["System"]["HedgeTimes"]);
                return 0;
            }
        }

        public int IsClient{get{
            if(SysParams.Count > 0)
                return int.Parse(SysParams["System"]["IsClient"]);
            return 0;
        }}

        string _ClentUserName;
        public string ClientUserName
        {
            get
            {
                if (_ClentUserName == null)
                {
                    if (SysParams.Count > 0 && SysParams["System"].ContainsKey("ClientUsername"))
                        _ClentUserName = SysParams["System"]["ClientUsername"];
                    return "";
                }
                return _ClentUserName;
            }
            set
            {
                _ClentUserName = value;
                SysParams["System"]["ClientUsername"] = value;
            }
        }


        string _ClientUserPwd;
        public string ClientPassword
        {
            get
            {
                if (_ClientUserPwd == null)
                {
                    if (SysParams.Count > 0)
                        _ClientUserPwd = SysParams["System"]["ClientPassword"];
                    else
                        return "";
                }
                return _ClientUserPwd;
            }
            set
            {
                _ClientUserPwd = value;
                SysParams["System"]["ClientPassword"] = value;
            }
        }

        public string ClientAliasName
        {
            get;set;
        }

        public string LoginUrlModel
        {
            get
            {
                if (SysParams.Count > 0 && SysParams["System"].ContainsKey("LoginUrlModel"))
                    return SysParams["System"]["LoginUrlModel"];
                return "";
            }
        }

        public string LoginPage
        {
            get
            {
                if (SysParams.Count > 0 && SysParams["System"].ContainsKey("LoginPage"))
                    return SysParams["System"]["LoginPage"];
                return "";
            }
        }

        public string LoginPageUserNameId
        {
            get
            {
                if (SysParams.Count > 0 && SysParams["System"].ContainsKey("LoginPageUserNameId"))
                    return SysParams["System"]["LoginPageUserNameId"];
                return "";
            }
        }

        public string LetteryPageUserKeyId
        {
            get
            {
                if (SysParams.Count > 0 && SysParams["System"].ContainsKey("LetteryPageUserKeyId"))
                    return SysParams["System"]["LetteryPageUserKeyId"];
                return "";
            }
        }
        public string LetteryPageUserKeyValue
        {
            get
            {
                if (SysParams.Count > 0 && SysParams["System"].ContainsKey("LetteryPageUserKeyValue"))
                    return SysParams["System"]["LetteryPageUserKeyValue"];
                return "";
            }
        }
        
        public string LotteryPage
        {
            get
            {
                if (SysParams.Count > 0 && SysParams["System"].ContainsKey("LotteryPage"))
                    return SysParams["System"]["LotteryPage"];
                return "";
            }
        }
        public string LoginValidPwdUrl
        {
            get
            {
                if (SysParams.Count > 0)
                    return SysParams["System"]["LoginValidPwdUrl"];
                return "";
            }
        }

        public string StatusUrlModel
        {
            get
            {
                if (SysParams.Count > 0)
                    return SysParams["System"]["StatusUrlModel"];
                return "";
            }

        }

        public string LoginDefaultHost{
            get
            {
                if (SysParams.Count > 0)
                    return SysParams["System"]["LoginDefaultHost"];
                return "";
            }
            set
            {
                SysParams["System"]["LoginDefaultHost"] = value;
            }
        }

        public string NavHost
        {
            get
            {
                if (SysParams.Count > 0)
                    return SysParams["System"]["NavHost"];
                return "";
            }
            set
            {
                SysParams["System"]["NavHost"] = value;
            }
        }

        public string LoginHostList{get{
            if(SysParams.Count > 0)
                return SysParams["System"]["LoginHostList"];
            return "";
        }}

        public int LoginInstFillOrEnCode{get{
            if(SysParams.Count > 0)
                return int.Parse(SysParams["System"]["LoginInstFillOrEnCode"]);
            return 0;
        }}


        public int LoginInFrame{get{
            if(SysParams.Count > 0)
                return int.Parse(SysParams["System"]["LoginInFrame"]);
            return 0;
        }}

        public int MinChips{get{
            if(SysParams.Count > 0)
                return int.Parse(SysParams["System"]["MinChips"]);
            return 0;
        }}

        public int CheckNewestDataDays
        {
            get
            {
                if (SysParams.Count > 0 && SysParams["System"].ContainsKey("CheckNewestDataDays"))
                    return int.Parse(SysParams["System"]["CheckNewestDataDays"]);
                return 0;
            }
        }

        public string WebNavUrl 
        {
            get
            {
                if (SysParams.Count > 0 && SysParams["System"].ContainsKey("WebNavUrl"))
                    return SysParams["System"]["WebNavUrl"];
                return null;
            }
        }

        public string HostLoginUrl
        {
            get
            {
                if (SysParams.Count > 0 && SysParams["System"].ContainsKey("HostLoginUrl"))
                    return SysParams["System"]["HostLoginUrl"];
                return null;
            }
        }

        public string HostAmountUrl
        {
            get
            {
                if (SysParams.Count > 0 && SysParams["System"].ContainsKey("HostAmountUrl"))
                    return SysParams["System"]["HostAmountUrl"];
                return null;
            }
        }

        public string HostBetUrl
        {
            get
            {
                if (SysParams.Count > 0 && SysParams["System"].ContainsKey("HostBetUrl"))
                    return SysParams["System"]["HostBetUrl"];
                return null;
            }
        }
        /// <summary>
        /// 正常消息通知开关
        /// </summary>
        public bool NormalNoticeFlag
        {
            get
            {
                if (SysParams.Count > 0 && SysParams["System"].ContainsKey("NormalNoticeFlag"))
                    return SysParams["System"]["NormalNoticeFlag"]=="1";
                return false;
            }
        }

        /// <summary>
        /// 意外消息通知开关
        /// </summary>
        public bool ExceptNoticeFlag
        {
            get
            {
                if (SysParams.Count > 0 && SysParams["System"].ContainsKey("ExceptNoticeFlag"))
                    return SysParams["System"]["ExceptNoticeFlag"] == "1";
                return false;
            }
        }

        #region 归入datatypepoint
        //////public int RecieveSecondsForPK10
        //////{
        //////    get
        //////    {
        //////        if (SysParams.Count > 0)
        //////            return int.Parse(SysParams["System"]["RecieveSecondsForPK10"]);
        //////        return 0;
        //////    }
        //////}

        //////public DateTime RecieveStartTimeForPK10
        //////{
        //////    get
        //////    {
        //////        if (SysParams.Count > 0)
        //////            return DateTime.Parse(SysParams["System"]["RecieveStartTimeForPK10"]);
        //////        return DateTime.MinValue;
        //////    }
        //////}

        //////public int RecieveSecondsForTXFFC
        //////{
        //////    get
        //////    {
        //////        if (SysParams.Count > 0)
        //////            return int.Parse(SysParams["System"]["RecieveSecondsForTXFFC"]);
        //////        return 0;
        //////    }
        //////}
        #endregion

        public int SingleColMinTimes
        {
            get{
            if(SysParams.Count > 0)
                return int.Parse(SysParams["System"]["SingleColMinTimes"]);
            return 0;
        }}
        

        public int StartCols{
            get
            {
            if(SysParams.Count > 0)
                return int.Parse(SysParams["System"]["StartCol"]);
            return 0;

            }
        }
        double _Odds = double.NaN;
        public double Odds
        {
            get
            {
                if (double.IsNaN(_Odds))
                {
                    if (SysParams.ContainsKey("System") && SysParams["System"].ContainsKey("Odds"))
                        _Odds = double.Parse(SysParams["System"]["Odds"]);
                }
                return _Odds;
            }
            set
            {
                _Odds = value;
                SysParams["System"]["Odds"] = value.ToString();
            }
        }

        public long InterVal{get{
            if(SysParams.Count > 0)
                return long.Parse(SysParams["System"]["InterVal"]);
            return 0;
        }}

        //////public int BackColor{get{
        //////    BackColor*/ return RGB(int.Parse((SysParams["System"]["BackColor_R"]);, int.Parse((SysParams["System"]["BackColor_G"]);, int.Parse((SysParams["System"]["BackColor_B"]);)
        //////}}

        public long HistoryFromPage
        {
            get
            {
                if(SysParams.Count > 0)
                    return long.Parse(SysParams["Research"]["FromPage"]);
                return 0;
            }
        }

        public long NewestHistoryExpect
        {
            get
            {
                if(SysParams.Count > 0)
                    return long.Parse(SysParams["Research"]["NewestHistoryExpect"]);
                return 0;
            }
        }

        public int MutliColMinTimes
        {
            get
            {
                if(SysParams.Count > 0)
                    return int.Parse(SysParams["System"]["MutliColMinTimes"]);
                return 0;
            }
        }

        public int SingleCarRepeatCnt{get{
            if(SysParams.Count > 0)
                return int.Parse(SysParams["Research"]["SingleCarRepeatCnt"]);
            return 0;
        }}

        public string RepeatCheckCnt{get{
            if(SysParams.Count > 0)
                return SysParams["Research"]["RepeatCheckCnt"];
            return "";
        }}

        public int ResearchStartCol{get{
            if(SysParams.Count > 0)
                return int.Parse(SysParams["Research"]["StartCol"]);
            return 0;
        }}
        public string ValidOldestHistoryExpect{get{
            if(SysParams.Count > 0)
                return SysParams["Research"]["ValidOldestHistoryExpect"];
            return "";
        }}

        public long AssetInitCash{get{
            if(SysParams.Count > 0)
                return int.Parse(SysParams["Asset"]["InitCash"]);
            return 0;
        }}

        public int AssetCosted{
            get
            {
                if(SysParams.Count > 0)
                    return int.Parse(SysParams["Asset"]["Costed"]);
                return 0;
            }
            set
            {
                SysParams["Asset"]["Costed"] = value.ToString();
            }
        }

        public float AssetGained{
            get
            {
                if(SysParams.Count > 0)
                    return float.Parse(SysParams["Asset"]["Gained"]);
                return 0;
            }
            set
            {
                SysParams["Asset"]["Gained"] = value.ToString();
            }
        }

        public float AssetAChanceMaxRate
        {
            get
            {
                if(SysParams.Count > 0)
                    return float.Parse(SysParams["Asset"]["AChanceMaxRate"]);
                return 0;
            }
        }

        public float AssetTotalMaxRate
        {
            get
            {
                if(SysParams.Count > 0)
                    return float.Parse(SysParams["Asset"]["TotalMaxRate"]);
                return 0;
            }
        }

        public float AssetTotal{
            get{
        
                if(SysParams.Count > 0)
                    return float.Parse(SysParams["Asset"]["TotalCash"]);
                return 0;
            }
            set
            {
                SysParams["Asset"]["TotalCash"] = value.ToString();
            }
        }

        public int MinTimeForChance(int Times)
        {
                if(SysParams.Count > 0)
                {
                    string strItem = string.Format("MinTimesFor{0}",Times);
                    if(SysParams["Exchange"].ContainsKey(strItem))
                        return int.Parse(SysParams["Exchange"][strItem]);
                }
                return 0;
        }

        public string[] UnitChipArray(int Cols)
        {
            if(SysParams.Count > 0)/*UnitChipArray*/ 
            {
                if(!SysParams.ContainsKey("Exchange")) return new string[0];
                if(!SysParams["Exchange"].ContainsKey("Serial"+Cols.ToString())) return new string[0];
                if(SysParams["Exchange"]["Serial"+Cols.ToString()]!=null)
                    return SysParams["Exchange"]["Serial" + Cols.ToString()].Split(',');
            }
            return new string[0];
        }

        ////public ExpectData NewestExpectData
        ////{
        ////    get
        ////    {        
        ////        if(t_newExpectData != null) 
        ////            return t_newExpectData[0];
        ////        return null;
        ////    }
        ////}

        ////public ExpectList CurrExpectData
        ////{
        ////    get
        ////    {
        ////        return t_newExpectData;
        ////    }
        ////}

        ////public void SetCurrExpectData(ExpectList el)
        ////{
        ////    t_newExpectData =el;
        ////}


        public int SerTotal(int Cols)
        {
            if(SysParams.Count > 0)
            {
                return int.Parse(SysParams["Exchange"]["SerTotal" +Cols.ToString()]);
            }
            return 0;
        }

        #region 客户端资产单元投资配置\
        Dictionary<string, AssetInfoConfig> _AssetUnits;
        public Dictionary<string,AssetInfoConfig> AssetUnits
        {
            get
            {
                if (_AssetUnits == null)
                {
                    _AssetUnits = new Dictionary<string, AssetInfoConfig>();
                    if (SysParams.Count > 0 && SysParams.ContainsKey("AssetUnits"))
                    {
                        Dictionary<string, string> assetunits = SysParams["AssetUnits"];
                        foreach (string key in assetunits.Keys)
                        {
                            //AssetInfoConfig aic = aic = DetailStringClass.GetObjectByXml<AssetInfoConfig>(assetunits[key]);
                            _AssetUnits.Add(key, new AssetInfoConfig(GlobalClass.readXmlItems(assetunits[key])));
                        }
                        //return assetunits; assetunits.ToDictionary(p => p.Key, p => int.Parse(p.Value));
                    }
                }
                return _AssetUnits;
            }
            set
            {
                if (value == null)
                    return;
                _AssetUnits = value;
                if(SysParams == null)
                {
                    return;//SysParams = new Dictionary<string, Dictionary<string, string>>();
                }
                SysParams["AssetUnits"] = new Dictionary<string, string>();
                value.Keys.ToList().ForEach(
                    a=> {
                        SysParams["AssetUnits"].Add(a, GlobalClass.writeXmlItems(value[a].getStringDic(),a,value[a].value));
                    });

            }
        }
        #endregion

        #endregion

        public static AmoutSerials? _DefaultHoldAmtSerials = null;
        public AmoutSerials DefaultHoldAmtSerials
        {
            get
            {
                if (_DefaultHoldAmtSerials == null )
                {
                    _DefaultHoldAmtSerials = getOptSerials(double.Parse(sSysParams["System"]["Odds"]), Int64.Parse(sSysParams["System"]["DefMaxLost"]), int.Parse(sSysParams["System"]["DefFirstAmt"]));
                }
                return _DefaultHoldAmtSerials.Value;
            }
        }

        public static DbClass getCurrDb(string strType)
        {
            dbServer = TypeDataPoints[strType].DbHost;
            dbUser = TypeDataPoints[strType].DbUser;
            dbName = TypeDataPoints[strType].DbName;
            dbPwd = TypeDataPoints[strType].DbPassword;

            return new DbClass(dbServer, dbUser, dbPwd, dbName);
        }

        public static MongoDBBase getCurrNoSQLDb(string typename)
        {
            //return new MongoDbClass("pk10.wolfinv.com:27017", "", dbPwd, "QuantAxis");"
            MongoDBBase db = new MongoDBBase(TypeDataPoints[typename].DbHost, TypeDataPoints[typename].DbUser, TypeDataPoints[typename].DbPassword, TypeDataPoints[typename].DbName);
            db.DBTypeName = typename;
            return db;
        }

        
        
        static string _strStragJsons = null;
        
        static string _strStragPlanXml = null;
        
        public string getStragXml()
        {
            return _strStragJsons;
        }

        
        public void setStragXml(string value)
        {
            if (_strStragJsons != value)
            {
                if (value != null)
                {
                    _strStragJsons = value;
                }
            }
            if (_strStragJsons != null)
            {
                SaveStragList(_strStragJsons);
            }
        }
       
        static bool ReadConfig(string folder="")
        {
            resetTypeDataPoints();
            sSysParams = null;
            sSysParams = new Dictionary<string, Dictionary<string, string>>();
            XmlDocument doc = new XmlDocument();
            string strPath = typeof(GlobalClass).Assembly.Location;
            string strXmlPath = string.Format( "{0}\\{1}\\config.xml", Path.GetDirectoryName(strPath),folder);
            try
            {
                doc.Load(strXmlPath);
                XmlDocument configDoc = doc;
                XmlNodeList configs = configDoc.SelectNodes("root/configs/config");
                for (int i = 0; i < configs.Count; i++)
                {
                    XmlNode node = configs[i];
                    string TypeName = node.Attributes["type"].Value;
                    Dictionary<string, string> configtypeDir = new Dictionary<string, string>();
                    XmlNodeList configitems = node.SelectNodes("./item");
                    for (int j = 0; j < configitems.Count; j++)
                    {
                        string name = configitems[j].Attributes["key"].Value;
                        string val = configitems[j].Attributes["value"].Value;
                        if(configitems[j].SelectNodes("./item").Count>0)//还有子项，以该项的xml作为value,value值无效
                        {
                            val = configitems[j].OuterXml;
                        }
                        if (!configtypeDir.ContainsKey(name))
                            configtypeDir.Add(name, val);
                        else
                            configtypeDir[name] = val;
                    }
                    sSysParams.Add(TypeName, configtypeDir);
                }
            }
            catch (Exception ce)
            {
                ToLog("读取配置文件错误", ce.Message);
                return false;
            }
            return true;
        }
        public static Dictionary<string,string> readXmlItems(string strNode)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(strNode);
                ret = readXmlItems(xmldoc.SelectSingleNode("item"));
            }
            catch
            {
                ret.Add(strNode, strNode);
            }
            return ret;
        }
        public static Dictionary<string,string> readXmlItems(XmlNode node)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            XmlNodeList nodes = node.SelectNodes("./item");
            foreach(XmlNode snode in nodes)
            {
                if(snode.SelectNodes("./item").Count==0)
                {
                    ret.Add(snode.Attributes["key"].Value, snode.Attributes["value"].Value);
                }
                else
                {
                    ret.Add(snode.Attributes["key"].Value, snode.OuterXml);
                }
            }
            return ret;
        }

        public static string writeXmlItems(Dictionary<string,string>  val,string keyName,object keyValue=null)
        {
            string ret = string.Format("<item key=\"{0}\" value=\"{1}\"/>",keyName,keyValue);
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(ret);
            
            try
            {
                XmlNode root = xmldoc.SelectSingleNode("item");
                foreach(string key in val.Keys)
                {
                    
                    if (val[key].StartsWith("<item") && val[key].EndsWith("</item>"))
                    {
                        try
                        {
                            XmlDocument sdoc = new XmlDocument();
                            sdoc.LoadXml(val[key]);
                            root.AppendChild(xmldoc.ImportNode(sdoc.SelectSingleNode("item"),true));
                        }
                        catch
                        {

                        }
                    }
                    else
                    {
                        XmlNode newnode = xmldoc.CreateElement("item");
                        XmlAttribute att = xmldoc.CreateAttribute("key");
                        att.Value = key;
                        newnode.Attributes.Append(att);
                        att = xmldoc.CreateAttribute("value");
                        att.Value = val[key];
                        newnode.Attributes.Append(att);
                        root.AppendChild(newnode);
                    }
                }
            }
            catch
            {

            }
            return xmldoc.OuterXml;
        }


        public static void SetConfig(string websit = "")
        {
            string forweb = "";
            if(!string.IsNullOrEmpty(websit))
            {
                forweb = string.Format("config\\{0}\\", websit);
                     
            }
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml("<root><configs/></root>");
            XmlNode configNodes = xmldoc.SelectSingleNode("root/configs");
            foreach(string configkey in sSysParams.Keys)
            {

                XmlNode configNode = xmldoc.CreateElement("config");
                XmlAttribute atttypekey = xmldoc.CreateAttribute("type");
                atttypekey.Value = configkey;
                configNode.Attributes.Append(atttypekey);
                foreach (string itemkey in sSysParams[configkey].Keys)
                {
                    XmlNode itemnode = xmldoc.CreateElement("item");
                    XmlAttribute attkey = xmldoc.CreateAttribute("key");
                    XmlAttribute attvalue = xmldoc.CreateAttribute("value");
                    string strVal = sSysParams[configkey][itemkey];
                    XmlDocument subdoc = new XmlDocument();
                    bool isSNode = false;
                    try
                    {
                        subdoc.LoadXml(strVal);//如果能加载进
                        strVal = "";
                        isSNode = true;
                    }
                    catch
                    {

                    }
                    if (!isSNode)//不是子节点，只是简单加item节点包括key，value
                    {
                        attkey.Value = itemkey;
                        attvalue.Value = sSysParams[configkey][itemkey];
                        itemnode.Attributes.Append(attkey);
                        itemnode.Attributes.Append(attvalue);
                        configNode.AppendChild(itemnode);
                    }
                    else //把整个value值作为节点加进去
                    {
                        configNode.AppendChild(xmldoc.ImportNode(subdoc.SelectSingleNode("item"), true));
                    }
                }
                configNodes.AppendChild(configNode);
            }
            string strPath = typeof(GlobalClass).Assembly.Location;
            string strXmlPath =string.Format("{0}\\{1}\\config.xml", Path.GetDirectoryName(strPath), forweb);
            try
            {
                xmldoc.Save(strXmlPath);
            }
            catch(Exception e)
            {
                string strContent = xmldoc.OuterXml;
                ToLog("错误", string.Format("保存到文件[{0}]错误:{1}", strPath, strContent), string.Format("{0}", e.Message));
            }
        }

        public static string ReReadStragList()
        {
            _strStragJsons = ReadFile("StragList.db");
            return _strStragJsons;
        }

        public string HostKey
        {
            get
            {
                if (SysParams.Count > 0 && SysParams["System"].ContainsKey("HostKey"))
                {
                    return SysParams["System"]["HostKey"];
                }
                return "";
            }
        }

        public string UseBrowser
        {
            get
            {
                if (SysParams.Count > 0 && SysParams["System"].ContainsKey("UseBrowser"))
                {
                    return SysParams["System"]["UseBrowser"];
                }
                return "";
            }
        }

        public string LoginedFlag
        {
            get
            {
                if (SysParams.Count > 0 && SysParams["System"].ContainsKey("LoginedFlag"))
                {
                    return SysParams["System"]["LoginedFlag"];
                }
                return "";
            }
        }

        public string AmountId
        {
            get
            {
                if (SysParams.Count > 0 && SysParams["System"].ContainsKey("AmountId"))
                {
                    return SysParams["System"]["AmountId"];
                }
                return "";
            }
        }

        public static bool SaveStragList(string str)
        {
            if (_strStragJsons == str)
                return true;
            _strStragJsons = str;
            return SaveFile("StragList.db", str);
        }

        public static string ReReadAssetUnitList()
        {
            return ReadFile("AssetUnits.db");
        }

        public static bool SaveAssetUnits(string str)
        {
            return SaveFile("AssetUnits.db", str);
        }
        
        public static string getStragRunningPlan(bool UseNewestData)
        {
            bool ReadNewest = false;
            if (UseNewestData)//需要使用最新数据
            {
                ReadNewest = true;
            }
            if (_strStragPlanXml == null || _strStragPlanXml.Trim().Length == 0)//老数据为空
            {
                ReadNewest = true;
            }
            if(ReadNewest)
                _strStragPlanXml = ReadFile("StragRunPlan.db");
            return _strStragPlanXml; 
        }

        public static bool setStragRunningPlan(string strXml)
        {
            bool ret = false;
            if (strXml == null)
                return false;
            if (_strStragPlanXml!= null && strXml.Trim() == _strStragPlanXml.Trim())
            {
                return true;
            }
            ret = SaveFile("StragRunPlan.db", strXml);
            if (ret)
            {
                _strStragPlanXml = strXml;
            }
            return ret;
        }
        
        static string ReadFile(string filename)
        {
            StreamReader sr = null;
            string strContent = null;
            string strPath = typeof(GlobalClass).Assembly.Location;
            string strJsonPath = string.Format("{0}\\{1}", Path.GetDirectoryName(strPath), filename);
            try
            {
                sr = new StreamReader(strJsonPath);
                strContent = sr.ReadToEnd();
                sr.Close();
            }
            catch(Exception e)
            {
                ToLog("错误",string.Format("读取文件[{0}]错误",filename),e.Message);
                return null;
            }
            return strContent;
        }

        static bool SaveFile(string filename,string strContent)
        {
            string strPath = typeof(GlobalClass).Assembly.Location;
            string strJsonPath =string.Format("{0}\\{1}",Path.GetDirectoryName(strPath) ,filename);
            try
            {
                StreamWriter sw = new StreamWriter(strJsonPath, false);
                sw.Write(strContent);
                sw.Close();
                //ToLog("保存策略", "成功");
            }
            catch(Exception c)
            {
                ToLog("错误",string.Format("保存到文件[{0}]错误:{1}",filename,strContent), string.Format("{0}",c.Message));
                return false;
            }
            return true;
        }
        /// <summary>
        /// 计算最优投注金额数组
        /// </summary>
        /// <param name="odds">赔率</param>
        /// <param name="MaxValue">最大投入</param>
        /// <returns></returns>
        public static AmoutSerials getOptSerials(double odds, Int64 MaxValue, int FirstAmt)
        {
            return getOptSerials(odds, MaxValue, FirstAmt, false);
        }

        

        /// <summary>
        /// 计算最优投注金额数组
        /// </summary>
        /// <param name="odds">赔率</param>
        /// <param name="MaxValue">最大投入</param>
        /// <returns></returns>
        public static AmoutSerials getOptSerials(double odds, Int64 MaxValue,int FirstAmt,bool NeedAddFirst)
        {
            string model = "key_{0}_{1}_{2}";
            string key = string.Format(model, odds, MaxValue, FirstAmt);
            if (AllSerialSettings == null)
                AllSerialSettings = new Dictionary<string, AmoutSerials>();
            if (AllSerialSettings.ContainsKey(key))
                return AllSerialSettings[key];
            AmoutSerials retval = new AmoutSerials();
            if (double.IsNaN(odds) || MaxValue == 0)
            {
                return retval;
            }
            int[] ret = new int[8];
            double[] Rates = new double[8];
            Int64[] MaxSum = new Int64[8];
            Int64[][] Serials = new Int64[8][];
            //int MaxValue = 20000;
            //double odds = 9.75;
            for (int i = 0; i < ret.Length; i++)
            {
                Int64[] Ser = new Int64[0];
                int MaxCnts = 1;
                double bRate = 0.0005;
                double stepRate = 0.001;
                Int64 CurrSum = getSum(i + 1, MaxCnts, FirstAmt, odds, 0, out Ser);
                while (CurrSum < MaxValue)//计算出在指定资金范围内保本所能达到的次数
                {
                    MaxCnts++;
                    CurrSum = getSum(i + 1, MaxCnts, 1, odds, 0, out Ser);
                }
                MaxCnts--; //回退1
                long TestSum = getSum(i + 1, MaxCnts, FirstAmt, odds, bRate, out Ser);
                if (TestSum > MaxValue)//如果最少盈利下所需资金大于指定值，所能达到的次数减一
                {
                    bRate = 0;
                    stepRate = 0.0001;
                    CurrSum = getSum(i + 1, MaxCnts, FirstAmt, odds, bRate, out Ser);
                }
                else
                {
                    CurrSum = TestSum;
                }
                Int64 LastSum = CurrSum;
                double LastRate = bRate;
                while (CurrSum < MaxValue)
                {
                    LastSum = CurrSum;
                    LastRate = bRate;
                    bRate += stepRate;
                    CurrSum = getSum(i + 1, MaxCnts, FirstAmt, odds, bRate, out Ser);
                }
                CurrSum = getSum(i + 1, MaxCnts, FirstAmt, odds, LastRate, out Ser);
                if (i == 0)//对于单注，多加10个元素，给重复策略用。
                {
                    getSum(i + 1, MaxCnts+10, FirstAmt, odds, LastRate, out Ser);
                }
                if (NeedAddFirst)
                {
                    List<long> list = Ser.ToList();
                    ret[i] = MaxCnts + 1;
                    list.Insert(0, FirstAmt);
                    Rates[i] = bRate - stepRate;
                    MaxSum[i] = LastSum + FirstAmt;
                    Serials[i] = list.ToArray();
                }
                else
                {
                    ret[i] = MaxCnts ;
                    Rates[i] = bRate - stepRate;
                    MaxSum[i] = LastSum ;
                    Serials[i] = Ser;
                }
            }
            retval.MaxHoldCnts = ret;
            retval.MaxRates = Rates;
            retval.Serials = Serials;
            if (!AllSerialSettings.ContainsKey(key))//防止计算过程中有其他设置请求了
            {
                AllSerialSettings.Add(key, retval);
            }
            return retval;
        }

        static Int64 getSum(int chips, int holdcnt, int firstAmt, double odd, double MinWRate, out Int64[] serial)
        {
            Int64 sum = 0;
            serial = new Int64[holdcnt];
            for (int i = 1; i <= holdcnt; i++)
            {
                serial[i - 1] = FixCountMethods.getTheAmount(chips, i, firstAmt, odd, MinWRate);
                sum += serial[i - 1] * chips;
            }
            return sum;
        }
    }
}
