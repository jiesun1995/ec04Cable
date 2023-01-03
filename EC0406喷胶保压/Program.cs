﻿using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EC0406喷胶保压
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LogManager.Init(null);
            SystemHelper.OnlyRun("SpineColorOnlyRunOnce", () => {
                LogManager.Debug("开始加载配置");
                DataContent.LoadConfig();
                WCFHelper.CreateServer();
                LogManager.Debug("加载配置完成");
            });
            Application.Run(new FrmMain());
        }
    }
}
