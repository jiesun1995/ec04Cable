using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Common
{
    public class SystemHelper
    {
        public static void OnlyRun(string name,Action action)
        {
            bool running = true;
            System.Threading.Mutex mutex = new System.Threading.Mutex(false, name);
            try
            {
                running = !mutex.WaitOne(0, false);            //这一句有可能会报错，所以要Try起来
                action();
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
        }
    }
}
