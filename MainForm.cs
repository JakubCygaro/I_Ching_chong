using I_Ching.Models;

namespace I_Ching
{
    public partial class MainForm : Form
    {
        private HexagramHandler _handler;
        public MainForm()
        {
            InitializeComponent();
            try
            {
                _handler = new HexagramHandler();
            }
            catch (System.IO.FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Close();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        private void button_Divine_Click(object sender, EventArgs e)
        {
            Fortune fortune;
            _handler.MakeNew();
            DisplayHexagram();
            GenerateFortune();
            try
            {
                fortune = _handler.GetFortune();
                label_Name.Text = fortune.Name;
                textBox_FortuneDesc.Text = fortune.Description;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DisplayHexagram()
        {
            if(_handler.Hexagram is null)
                return;

            PaintPanel(panel_Lower1, panel_LowBrake1, _handler.Hexagram.LowerTrigram[0]);
            PaintPanel(panel_Lower2, panel_LowBrake2, _handler.Hexagram.LowerTrigram[1]);
            PaintPanel(panel_Lower3, panel_LowBrake3, _handler.Hexagram.LowerTrigram[2]);

            PaintPanel(panel_Upper1, panel_UpBrake1, _handler.Hexagram.UpperTrigram[0]);
            PaintPanel(panel_Upper2, panel_UpBrake2, _handler.Hexagram.UpperTrigram[1]);
            PaintPanel(panel_Upper3, panel_UpBrake3, _handler.Hexagram.UpperTrigram[2]);
        }
        private void PaintPanel(Panel p, Panel brek, I_Ching.Models.Trigram.Line line)
        {
            switch (line)
            {
                case Trigram.Line.Full or Trigram.Line.FullDot:
                    p.BackColor = Color.Black;
                    brek.BackColor = Color.Black;
                    break;
                case Trigram.Line.Broken or Trigram.Line.BrokenDot:
                    p.BackColor = Color.Black;
                    brek.BackColor= Color.White;
                    break;
            }
        }
        private void GenerateFortune()
        {
            label_Fortune.Text = $"{_handler.Hexagram.UpperTrigram.Nature} above" +
                $" {_handler.Hexagram.LowerTrigram.Nature} below.";
            if (_handler.Hexagram.HasChangable)
                label_CanChange.Text = "Can Change";
            else
                label_CanChange.Text = "No Change";
        }

        private void button_Change_Click(object sender, EventArgs e)
        {
            if (_handler.Hexagram is null)
                return;
            Fortune fortune;
            _handler.Change();
            DisplayHexagram();
            GenerateFortune();
            try
            {
                fortune = _handler.GetFortune();
                label_Name.Text = fortune.Name;
                textBox_FortuneDesc.Text = fortune.Description;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}