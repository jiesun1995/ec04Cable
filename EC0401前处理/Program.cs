﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Common;

namespace EC0401前处理
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
            System.Threading.Mutex mutex = new System.Threading.Mutex(false, "SpineColorOnlyRunOnce");
            bool running = true;
            try
            {

                running = !mutex.WaitOne(0, false);            //这一句有可能会报错，所以要Try起来
                LogManager.Debug("开始加载配置");
                DataContent.LoadConfig();
                LogManager.Debug("加载配置完成");
            }
            catch (Exception ex)
            {
                LogManager.Fatal(ex);
            }
            if (running)
            {
                MessageBox.Show("已经运行了一个实例（或旧实例尚未完全关闭），为避免发生异常请不要重复运行程序!", "提示", MessageBoxButtons.OK);
                System.Environment.Exit(0);
            }

            Application.Run(new FrmMain());
        }
    }
}
