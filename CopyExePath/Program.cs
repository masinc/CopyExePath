using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CopyExePath
{
    static class Program
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);



        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            var hwnd = args.Length == 1 //第1引数にハンドルを入れるとそのハンドルのフルパスを取得する
                ? (IntPtr)Convert.ToInt32(args[0], 16) //16進数文字列から数値に変換
                : GetForegroundWindow();
            GetWindowThreadProcessId(hwnd,
                out var id);

            try
            {
                var process = Process.GetProcessById(id);


                //MessageBox.Show(process.MainModule.FileName);
                Debug.WriteLine(process.MainModule.FileName);
                Clipboard.SetText(process.MainModule.FileName);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

        }
    }
}
