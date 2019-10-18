namespace FFmpegWrapper.Views
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using NilaLib;

    public partial class ExecutionForm : Form
    {
        private readonly LockObject<Task> task = LockObject.New();
        private readonly LockObject<CancellationTokenSource> cancelToken = LockObject.New();
        private readonly LockObject<Process> proc = LockObject.New();
        private readonly string bin;
        private readonly string parameters;

        public ExecutionForm() : this(string.Empty, string.Empty)
        {
        }

        public ExecutionForm(string bin, string parameters)
        {
            this.bin = bin;
            this.parameters = parameters;
            this.InitializeComponent();

            this.rtBoxCommand.Text = string.Format("{0} {1}", this.bin, this.parameters);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.task.Value = Task.Run(() => this.Execute(this.bin, this.parameters));
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosed(e);
            Task task = null;
            this.task.ActionWithLock(x => task = x);
            if (task != null)
            {
                if (App.Question("タスクが終了していません。{0}強制終了しますか？", Environment.NewLine) != DialogResult.OK)
                {
                    e.Cancel = true;
                    return;
                }

                Process proc = null;
                this.proc.ActionWithLock(x => proc = x);
                if (proc != null)
                {
                    proc.Kill();
                }

                task.Wait();
            }
        }

        private void Execute(string bin, string parameters)
        {
            try
            {
                var si = new ProcessStartInfo(bin, parameters);
                si.CreateNoWindow = true;
                si.RedirectStandardOutput = true;
                si.RedirectStandardError = true;
                si.UseShellExecute = false;
                using (var proc = new Process())
                using (var cancelToken = new CancellationTokenSource())
                {
                    proc.EnableRaisingEvents = true;
                    proc.StartInfo = si;
                    proc.OutputDataReceived += (_, e) => this.AppendLog(this.rtBoxLog, e.Data);
                    proc.ErrorDataReceived += (_, e) => this.AppendLog(this.rtBoxLog, e.Data);
                    proc.Exited += (_, e) =>
                    {
                        cancelToken.Cancel();
                        this.task.Value = null;
                    };

                    this.proc.Value = proc;
                    proc.Start();
                    proc.BeginOutputReadLine();
                    proc.BeginErrorReadLine();
                    cancelToken.Token.WaitHandle.WaitOne();
                }
            }
            catch (Exception ex)
            {
                this.AppendLog(this.rtBoxLog, string.Format("{0}{1}{2}", ex.Message, Environment.NewLine, ex.StackTrace));
            }
        }

        private void AppendLog(RichTextBox rtBox, string log)
        {
            if (rtBox.InvokeRequired)
            {
                rtBox.Invoke(new Action(() => this.AppendLog(rtBox, log)));
                return;
            }

            rtBox.AppendText((log ?? string.Empty) + Environment.NewLine);
        }
    }
}
