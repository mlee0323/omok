using System.Threading;
using System.Windows.Forms;

namespace Omok_Server2.Core
{
    public static class ThreadManager
    {
        public static void AddThread(ListView view, Thread thread)
        {
            if (view.InvokeRequired)
            {
                view.Invoke(new MethodInvoker(() => AddThread(view, thread)));
            }
            else
            {
                ListViewItem item = new ListViewItem(thread.Name ?? $"Thread {thread.ManagedThreadId}");
                item.SubItems.Add(thread.ThreadState.ToString());
                view.Items.Add(item);
            }
        }

        public static void UpdateStatus(ListView view)
        {
            if (view.InvokeRequired)
            {
                view.Invoke(new MethodInvoker(() => UpdateStatus(view)));
            }
            else
            {
                foreach (ListViewItem item in view.Items)
                {
                    if (int.TryParse(item.Text.Replace("Thread ", ""), out int tid))
                    {
                        item.SubItems[1].Text = "활성"; // 실제 쓰레드 상태 트래킹은 따로 필요함
                    }
                }
            }
        }
    }
}
