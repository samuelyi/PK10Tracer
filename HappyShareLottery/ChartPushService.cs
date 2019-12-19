﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Reflection;
using WolfInv.com.ShareLotteryLib;
using XmlProcess;
using System.Linq;
namespace HappyShareLottery
{
    /// <summary>
    /// 群消息推送服务
    /// </summary>
    partial class ChartPushService : ServiceBase
    {
        List<Timer> AllTimers = new List<Timer>();
        XmlNode defaultSetting = null;
        public ChartPushService()
        {
            InitializeComponent();
            Dictionary<string, XmlNode> allPlans = getPlanXmlDictionary();
            AllTimers = new List<Timer>();
            foreach(string key in allPlans.Keys)
            {
                XmlNode node = allPlans[key];
                wxChartPushClass wxp = new wxChartPushClass();
                wxp.Init(node);
                if (wxp.disabled)
                    continue;
                if(string.IsNullOrEmpty(wxp.chartName))
                {
                    continue;
                }
                if(Program.allContacts == null)
                {
                    continue;
                }
                var contact = Program.allContacts.Where(a => a.Key.StartsWith("@@") == true);
                contact = contact.Where(a =>  a.Value.Equals(wxp.chartName));
                if(contact.Count()==0)
                {
                    continue;
                }
                wxp.chartUid = contact.First().Key;
                Timer tm = new Timer();
                tm.Interval = wxp.interMinutes * 60 * 1000;
                tm.Tick += sendTo;
                tm.Tag = wxp;
                tm.Enabled = false;
                AllTimers.Add(tm);
            }
        }


        private void sendTo(object sender, EventArgs e)
        {
            wxChartPushClass wxp = (sender as Timer).Tag as wxChartPushClass;
            if (wxp == null)
                return;
            if(wxp.chartUid == null)
            {
                return;
            }
            wxp.pushGoods();
        }



        public void AddChartPoints(List<wxChartPushClass> newLists)
        {

        }

        Dictionary<string, XmlNode> getPlanXmlDictionary()
        {

            Dictionary<string, XmlNode> ret = new Dictionary<string, XmlNode>();
            
            string xml = TextFileComm.getFileText("pushPlan.xml", "xml");
            if (xml == null)
                return ret;
            XmlDocument xmldoc = new XmlDocument();
            try
            {
                xmldoc.LoadXml(xml);
                
                XmlNode root = xmldoc.SelectSingleNode("root");
                defaultSetting = root.SelectSingleNode("DefaultSetting");
                wxChartPushClass.initSysSetting(defaultSetting);
                wxChartPushClass.initEliteList(root.SelectSingleNode("elites"));
                XmlNodeList nlist = root.SelectNodes("toWxChats/chatPoint");
                foreach (XmlNode node in nlist)
                {

                    string name = XmlUtil.GetSubNodeText(node, "@name");
                    if(!ret.ContainsKey(name))
                    {
                        ret.Add(name, node);
                    }
                }
            }
            catch (Exception ce)
            {
                return ret;
            }
            return ret;
        }


        protected override void OnStart(string[] args)
        {
            // TODO: 在此处添加代码以启动服务。
            for(int i=0;i< AllTimers.Count;i++)
            {
                AllTimers[i].Enabled = true;
                sendTo(AllTimers[0], null);
            }

        }

        

        public void Start()
        {
            OnStart(null);
        }
        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
            for (int i = 0; i < AllTimers.Count; i++)
            {
                AllTimers[i].Enabled = false;
            }

        }
    }
}
