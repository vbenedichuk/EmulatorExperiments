using EmulationKit.Abstractions;
using EmulationKit.i8080;
using EmulationKit.UT88Min.Components;
using Microsoft.Win32;
using System.Diagnostics;

namespace EmulationKit.UT88Min
{
    public partial class Form1 : Form
    {
        private readonly UtMemory _memory = new UtMemory();
        private readonly UtIO _io = new UtIO();
        private readonly ICpu _cpu;
        private readonly Task _ticksTask;
        private bool _ticksEnabled = false;
        private bool _formClosing = false;
        public Form1()
        {
            InitializeComponent();
            _cpu = new CpuI8080(_memory, _io);
            _ticksTask = Task.Run(TicksAction);
        }

        private void TicksAction()
        {
            Stopwatch ticks = new Stopwatch();
            ticks.Restart();
            while (!_formClosing)
            {
                if (_ticksEnabled)
                {
                    if (ticks.ElapsedTicks > 5)
                    {
                        _cpu.Tick();
                        ticks.Restart();
                    //Invoke((MethodInvoker)delegate
                    //{
                    //    UpdateUI();
                    //});
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Keyboard_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var tag = (string)button.Tag;
                _io.KeyPress(byte.Parse(tag));
            }
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            _cpu.Reset();
            UpdateUI();
            CycleNo.Text = "0";
            timer1.Enabled = false;
        }

        private void UpdateUI()
        {
            indicators.Text = _memory.GetIndicatorsState();
            var registers = _cpu.GetRegisters();
            registersView.Items.Clear();
            registersView.Items.AddRange(GetListIems(registers));
            var flags = _cpu.GetFlags();
            flagsView.Items.Clear();
            flagsView.Items.AddRange(GetListIems(flags));
        }

        private ListViewItem[] GetListIems(Dictionary<string, bool> registers)
        {
            var result = new List<ListViewItem>();
            foreach (var i in registers)
            {
                result.Add(new ListViewItem(new string[] { i.Key, i.Value ? "1" : "0" }));
            }
            return result.ToArray();
        }

        private ListViewItem[] GetListIems(Dictionary<string, int> registers)
        {
            var result = new List<ListViewItem>();
            foreach (var i in registers)
            {
                result.Add(new ListViewItem(new string[] { i.Key, i.Value.ToString("X2") }));
            }
            return result.ToArray();
        }

        private void TickButton_Click(object sender, EventArgs e)
        {
            if (manualRB.Checked)
            {
                _cpu.Tick();
                UpdateUI();
            }
            else
            {
                _ticksEnabled = true;
                timer1.Enabled = true;
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            _ticksEnabled = false;
            timer1.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _formClosing = true;
        }

        private void ShowStack_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateUI();
        }
    }
}
