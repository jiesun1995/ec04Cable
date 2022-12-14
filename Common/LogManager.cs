using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Common
{
    public static class LogManager
    {
        private static log4net.ILog _log;
        private static ListView _listView;
        private static ConcurrentQueue<Tuple<string,Color>> _queue = new ConcurrentQueue<Tuple<string, Color>>();
        /// <summary>
        /// 日志框架初始化
        /// </summary>
        public static void Init(ListView listView)
        {
            log4net.Config.XmlConfigurator.Configure();
            _log = log4net.LogManager.GetLogger(typeof(LogManager));
            if (listView != null)
            {
               
                _listView = listView;
                _listView.Columns.Clear();
                ColumnHeader col = new ColumnHeader()
                {
                    Text = "时间",
                };
                ColumnHeader col1 = new ColumnHeader()
                {
                    Text = "详细信息",
                    TextAlign = HorizontalAlignment.Left,
                    Width = 500,
                };
                _listView.Columns.Add(new ColumnHeader() { Width = 0});
                _listView.Columns.Add(col);
                _listView.Columns.Add(col1);

                for (int i = _queue.Count; i > 0; i--)
                {
                    Tuple<string, Color> val = null;
                    if (_queue.TryDequeue(out val))
                    {
                        _listView.BeginUpdate();
                        ListViewItem listViewItem = new ListViewItem();
                        listViewItem.SubItems.Add(DateTime.Now.ToString("HH:mm:ss"));
                        listViewItem.SubItems.Add(val.Item1);
                        listViewItem.BackColor = val.Item2;
                        _listView.Items.Add(listViewItem);

                        if (_listView.Items.Count >= 100)
                        {
                            _listView.Items.RemoveAt(0);
                            _listView.Items[_listView.Items.Count - 1].EnsureVisible();
                        }
                        _listView.EndUpdate();
                    }
                }
            }
        }
        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="mesage"></param>
        public static void Debug(object mesage)
        {
            _log.Debug(mesage);
            UIShow(mesage.ToString(), Color.White);
        }
        /// <summary>
        /// 一般信息
        /// </summary>
        /// <param name="mesage"></param>
        public static void Info(object mesage)
        {
            _log.Info(mesage);
            UIShow(mesage.ToString(), Color.Green);
        }
        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="mesage"></param>
        public static void Warn(object mesage)
        {
            _log.Warn(mesage);
            UIShow(mesage.ToString(), Color.Yellow);
        }
        /// <summary>
        /// 一般错误
        /// </summary>
        /// <param name="mesage"></param>
        public static void Error(object mesage)
        {
            _log.Error(mesage);
            UIShow(mesage.ToString(), Color.Red);
        }
        /// <summary>
        /// 致命错误
        /// </summary>
        /// <param name="mesage"></param>
        public static void Fatal(object mesage)
        {
            _log.Fatal(mesage);
            UIShow(mesage.ToString(), Color.Brown);
        }

        private static void UIShow(string message, Color color)
        {
            _queue.Enqueue(new Tuple<string, Color>(message, color));
            if (_listView == null)
                return;
           
            var task = Task.Factory.StartNew(() =>
            {
                if (_queue.Count <= 0)
                    return;
                Tuple<string, Color> val = null;
                if (_queue.TryDequeue(out val))
                {
                    _listView.Invoke((EventHandler)delegate
                    {
                        _listView.BeginUpdate();
                        ListViewItem listViewItem = new ListViewItem();
                        listViewItem.SubItems.Add(DateTime.Now.ToString("HH:mm:ss"));
                        listViewItem.SubItems.Add(val.Item1);
                        listViewItem.BackColor = val.Item2;
                        _listView.Items.Add(listViewItem);

                        if (_listView.Items.Count >= 100)
                        {
                            _listView.Items.RemoveAt(0);
                            _listView.Items[_listView.Items.Count - 1].EnsureVisible();
                        }
                        _listView.EnsureVisible(_listView.Items.Count-1);
                        _listView.EndUpdate();
                    });
                }
            });
            //task.Wait();
        }
    }
}
