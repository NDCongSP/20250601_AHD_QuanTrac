using Ahd.Core;
using Ahd.Winforms.Controls;
using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistrationForm1
{
    public partial class FrmTran : Form
    {
        private FrmMain _mainForm;
        private DataTranModel _lastData = new DataTranModel(); // giá trị lần trước
        private string connectionString = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=DauTieng;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        private readonly Dictionary<string, string> lastValues = new Dictionary<string, string>();
        public FrmTran(FrmMain frmMain)
        {        
            InitializeComponent();
            //  Load += FrmTran_Load;
            _mainForm = frmMain; // ✅ Gán trước khi sử dụng
            if (_mainForm != null)
            {
                // Subscribe to events
                _mainForm.S1RemoteChanged += MainForm_S1RemoteChanged;
                _mainForm.S1LocalChanged += MainForm_S1LocalChanged;
                _mainForm.S1AutoChanged += MainForm_S1AutoChanged;
                _mainForm.S1ManChanged += MainForm_S1ManChanged;
                _mainForm.S1LocalStopChanged += MainForm_S1LocalStopChanged;

                _mainForm.S2RemoteChanged += S2_Remote_ValueChanged;
                _mainForm.S2LocalChanged += S2_Local_ValueChanged;
                _mainForm.S2AutoChanged += S2_Auto_ValueChanged;
                _mainForm.S2ManChanged += S2_Man_ValueChanged;
                _mainForm.S2LocalStopChanged += S2_Local_Stop_ValueChanged;

                _mainForm.S3RemoteChanged += S3_Remote_ValueChanged;
                _mainForm.S3LocalChanged += S3_Local_ValueChanged;
                _mainForm.S3AutoChanged += S3_Auto_ValueChanged;
                _mainForm.S3ManChanged += S3_Man_ValueChanged;
                _mainForm.S3LocalStopChanged += S3_Local_Stop_ValueChanged;
                // Subscribe to DC Running events
                _mainForm.S1_DC1_RunningChanged += S1_DC1_Running_ValueChanged;
                _mainForm.S1_DC2_RunningChanged += S1_DC2_Running_ValueChanged;
                _mainForm.S1_DC3_RunningChanged += S1_DC3_Running_ValueChanged;
                _mainForm.S2_DC1_RunningChanged += S2_DC1_Running_ValueChanged;
                _mainForm.S2_DC2_RunningChanged += S2_DC2_Running_ValueChanged;
                _mainForm.S2_DC3_RunningChanged += S2_DC3_Running_ValueChanged;
                _mainForm.S3_DC1_RunningChanged += S3_DC1_Running_ValueChanged;
                _mainForm.S3_DC2_RunningChanged += S3_DC2_Running_ValueChanged;
                _mainForm.S3_DC3_RunningChanged += S3_DC3_Running_ValueChanged;
                // Subscribe to Door events
                _mainForm.Door1_OpeningChanged += Door1_Opening_ValueChanged;
                _mainForm.Door1_ClosingChanged += Door1_Closing_ValueChanged;
                _mainForm.Door2_OpeningChanged += Door2_Opening_ValueChanged;
                _mainForm.Door2_ClosingChanged += Door2_Closing_ValueChanged;
                _mainForm.Door3_OpeningChanged += Door3_Opening_ValueChanged;
                _mainForm.Door3_ClosingChanged += Door3_Closing_ValueChanged;
                _mainForm.Door4_OpeningChanged += Door4_Opening_ValueChanged;
                _mainForm.Door4_ClosingChanged += Door4_Closing_ValueChanged;
                _mainForm.Door5_OpeningChanged += Door5_Opening_ValueChanged;
                _mainForm.Door5_ClosingChanged += Door5_Closing_ValueChanged;
                _mainForm.Door6_OpeningChanged += Door6_Opening_ValueChanged;
                _mainForm.Door6_ClosingChanged += Door6_Closing_ValueChanged;




            }
            else
            {
                MessageBox.Show("FrmMain instance is null. Please check.");
            }
            // ✅ Load trạng thái ban đầu ngay khi khởi tạo
            LoadInitialValues();

          //  EnsureTableCreated(); // tạo bảng nếu chưa có
        }

        

        private void LoadInitialValues() // Load giá trị ban đầu từ FrmMain
        {


            // Sử dụng hàm GetCurrentValue() từ FrmMain
            label1.Text = $"S1_Remote: {_mainForm.GetS1RemoteValue()}"; bnt_Remote_T1.BackColor = _mainForm.GetS1RemoteValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Remote_T2.BackColor = _mainForm.GetS1RemoteValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Remote_T3.BackColor = _mainForm.GetS2RemoteValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Remote_T4.BackColor = _mainForm.GetS2RemoteValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Remote_T5.BackColor = _mainForm.GetS3RemoteValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Remote_T6.BackColor = _mainForm.GetS3RemoteValue() == "1" ? Color.GreenYellow : DefaultBackColor;        
           
            label2.Text = $"S1_Local: {_mainForm.GetS1LocalValue()}"; bnt_Local_T1.BackColor = _mainForm.GetS1LocalValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Local_T2.BackColor = _mainForm.GetS1LocalValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Local_T3.BackColor = _mainForm.GetS2LocalValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Local_T4.BackColor = _mainForm.GetS2LocalValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Local_T5.BackColor = _mainForm.GetS3LocalValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Local_T6.BackColor = _mainForm.GetS3LocalValue() == "1" ? Color.GreenYellow : DefaultBackColor;

            label3.Text = $"S1_Auto: {_mainForm.GetS1AutoValue()}"; bnt_Auto_T1.BackColor = _mainForm.GetS1AutoValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Auto_T2.BackColor = _mainForm.GetS1AutoValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Auto_T3.BackColor = _mainForm.GetS2AutoValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Auto_T4.BackColor = _mainForm.GetS2AutoValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Auto_T5.BackColor = _mainForm.GetS3AutoValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Auto_T6.BackColor = _mainForm.GetS3AutoValue() == "1" ? Color.GreenYellow : DefaultBackColor;

            label4.Text = $"S1_Man: {_mainForm.GetS1ManValue()}"; bnt_Hand_T1.BackColor = _mainForm.GetS1ManValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Hand_T2.BackColor = _mainForm.GetS1ManValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Hand_T3.BackColor = _mainForm.GetS2ManValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Hand_T4.BackColor = _mainForm.GetS2ManValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Hand_T5.BackColor = _mainForm.GetS3ManValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Hand_T6.BackColor = _mainForm.GetS3ManValue() == "1" ? Color.GreenYellow : DefaultBackColor;

            label5.Text = $"S1_Local_Stop: {_mainForm.GetS1LocalStopValue()}"; bnt_Estop_T1.BackColor = _mainForm.GetS1LocalStopValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Estop_T2.BackColor = _mainForm.GetS1LocalStopValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Estop_T3.BackColor = _mainForm.GetS2LocalStopValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Estop_T4.BackColor = _mainForm.GetS2LocalStopValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Estop_T5.BackColor = _mainForm.GetS3LocalStopValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Estop_T6.BackColor = _mainForm.GetS3LocalStopValue() == "1" ? Color.GreenYellow : DefaultBackColor;





        }


        // Bảng điều khiển 3
        private void S3_Remote_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { bnt_Remote_T5.BackColor = Color.GreenYellow; bnt_Remote_T6.BackColor = Color.GreenYellow; });
            }
            else
                this.Invoke((MethodInvoker)delegate { bnt_Remote_T5.BackColor = DefaultBackColor; bnt_Remote_T6.BackColor = DefaultBackColor; });
        }
        private void S3_Local_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { bnt_Local_T5.BackColor = Color.GreenYellow; bnt_Local_T6.BackColor = Color.GreenYellow; });
            }
            else
                this.Invoke((MethodInvoker)delegate { bnt_Local_T5.BackColor = DefaultBackColor; bnt_Local_T6.BackColor = DefaultBackColor; });
        }
        private void S3_Auto_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { bnt_Auto_T5.BackColor = Color.GreenYellow; bnt_Auto_T6.BackColor = Color.GreenYellow; });
            }
            else
                this.Invoke((MethodInvoker)delegate { bnt_Auto_T5.BackColor = DefaultBackColor; bnt_Auto_T6.BackColor = DefaultBackColor; });
        }
        private void S3_Man_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { bnt_Hand_T5.BackColor = Color.GreenYellow; bnt_Hand_T6.BackColor = Color.GreenYellow; });
            }
            else
                this.Invoke((MethodInvoker)delegate { bnt_Hand_T5.BackColor = DefaultBackColor; bnt_Hand_T6.BackColor = DefaultBackColor; });
        }
        private void S3_Local_Stop_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { bnt_Estop_T5.BackColor = Color.GreenYellow; bnt_Estop_T6.BackColor = Color.GreenYellow; });
            }
            else
                this.Invoke((MethodInvoker)delegate { bnt_Estop_T5.BackColor = DefaultBackColor; bnt_Estop_T6.BackColor = DefaultBackColor; });
        }
        // End bảng điều khiển 3
        // Bảng điều khiển 2
        private void S2_Remote_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { bnt_Remote_T3.BackColor = Color.GreenYellow; bnt_Remote_T4.BackColor = Color.GreenYellow; });
            }
            else
                this.Invoke((MethodInvoker)delegate { bnt_Remote_T3.BackColor = DefaultBackColor; bnt_Remote_T4.BackColor = DefaultBackColor; });
        }
        private void S2_Local_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { bnt_Local_T3.BackColor = Color.GreenYellow; bnt_Local_T4.BackColor = Color.GreenYellow; });
            }
            else
                this.Invoke((MethodInvoker)delegate { bnt_Local_T3.BackColor = DefaultBackColor; bnt_Local_T4.BackColor = DefaultBackColor; });
        }
        private void S2_Auto_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { bnt_Auto_T3.BackColor = Color.GreenYellow; bnt_Auto_T4.BackColor = Color.GreenYellow; });
            }
            else
                this.Invoke((MethodInvoker)delegate { bnt_Auto_T3.BackColor = DefaultBackColor; bnt_Auto_T4.BackColor = DefaultBackColor; });
        }
        private void S2_Man_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { bnt_Hand_T3.BackColor = Color.GreenYellow; bnt_Hand_T4.BackColor = Color.GreenYellow; });
            }
            else
                this.Invoke((MethodInvoker)delegate { bnt_Hand_T3.BackColor = DefaultBackColor; bnt_Hand_T4.BackColor = DefaultBackColor; });
        }
        private void S2_Local_Stop_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { bnt_Estop_T3.BackColor = Color.GreenYellow; bnt_Estop_T4.BackColor = Color.GreenYellow; });
            }
            else
                this.Invoke((MethodInvoker)delegate { bnt_Estop_T3.BackColor = DefaultBackColor; bnt_Estop_T4.BackColor = DefaultBackColor; });
        }       
        private void MainForm_S1RemoteChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { bnt_Remote_T1.BackColor = Color.GreenYellow;bnt_Remote_T2.BackColor = Color.GreenYellow;  });
            }
            else
                this.Invoke((MethodInvoker)delegate { bnt_Remote_T1.BackColor = DefaultBackColor;bnt_Remote_T2.BackColor = DefaultBackColor; });
        }
        private void MainForm_S1LocalChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { bnt_Local_T1.BackColor = Color.GreenYellow; bnt_Local_T2.BackColor = Color.GreenYellow; });
            }
            else
                this.Invoke((MethodInvoker)delegate { bnt_Local_T1.BackColor = DefaultBackColor; bnt_Local_T2.BackColor = DefaultBackColor; });
        }
        private void MainForm_S1AutoChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { bnt_Auto_T1.BackColor = Color.GreenYellow; bnt_Auto_T2.BackColor = Color.GreenYellow; });
            }
            else
                this.Invoke((MethodInvoker)delegate { bnt_Auto_T1.BackColor = DefaultBackColor; bnt_Auto_T2.BackColor = DefaultBackColor; });
        }
        private void MainForm_S1ManChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { bnt_Hand_T1.BackColor = Color.GreenYellow; bnt_Hand_T2.BackColor = Color.GreenYellow; });
            }
            else
                this.Invoke((MethodInvoker)delegate { bnt_Hand_T1.BackColor = DefaultBackColor; bnt_Hand_T2.BackColor = DefaultBackColor; });
        }
        private void MainForm_S1LocalStopChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { bnt_Estop_T1.BackColor = Color.GreenYellow; bnt_Estop_T2.BackColor = Color.GreenYellow; });
            }
            else
                this.Invoke((MethodInvoker)delegate { bnt_Estop_T1.BackColor = DefaultBackColor; bnt_Estop_T2.BackColor = DefaultBackColor; });
        }

        // End Bảng điều khiển 1

        //Tràn 6
        private void Door6_PressureLow_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door6_PressureLow.Visible = true; Pic_Door6_PressureLow_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door6_PressureLow_Stop.Visible = true; Pic_Door6_PressureLow.Visible = false; });
        }
        private void Door6_PressureHigh_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door6_PressureHigh.Visible = true; Pic_Door6_PressureHigh_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door6_PressureHigh_Stop.Visible = true; Pic_Door6_PressureHigh.Visible = false; });
        }
        private void Door6_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door6_Close.Visible = true; Pic_Door6_Close_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door6_Close_Stop.Visible = true; Pic_Door6_Close.Visible = false; });
        }
        private void Door6_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door6_Open.Visible = true; Pic_Door6_Open_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door6_Open_Stop.Visible = true; Pic_Door6_Open.Visible = false; });
        }
        private void Door6_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door6_Closing.Visible = true; Pic_Door6_Closing_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door6_Closing_Stop.Visible = true; Pic_Door6_Closing.Visible = false; });
        }
        private void Door6_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door6_Opening.Visible = true; Pic_Door6_Opening_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door6_Opening_Stop.Visible = true; Pic_Door6_Opening.Visible = false; });
        }
        //End Tràn 6
        // Tràn 5
        private void Door5_PressureHigh_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door5_PressureHigh.Visible = true; Pic_Door5_PressureHigh_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door5_PressureHigh_Stop.Visible = true; Pic_Door5_PressureHigh.Visible = false; });
        }
        private void Door5_PressureLow_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door5_PressureLow.Visible = true; Pic_Door5_PressureLow_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door5_PressureLow_Stop.Visible = true; Pic_Door5_PressureLow.Visible = false; });
        }
        private void Door5_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door5_Opening.Visible = true; Pic_Door5_Opening_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door5_Opening_Stop.Visible = true; Pic_Door5_Opening.Visible = false; });
        }
        private void Door5_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door5_Closing.Visible = true; Pic_Door5_Closing_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door5_Closing_Stop.Visible = true; Pic_Door5_Closing.Visible = false; });
        }
        private void Door5_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door5_Open.Visible = true; Pic_Door5_Open_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door5_Open_Stop.Visible = true; Pic_Door5_Open.Visible = false; });
        }
        private void Door5_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door5_Close.Visible = true; Pic_Door5_Close_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door5_Close_Stop.Visible = true; Pic_Door5_Close.Visible = false; });
        }

        private void Doorlock5_2Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock5_2Open.Visible = true; Pic_Doorlock5_2Open_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock5_2Open_Stop.Visible = true; Pic_Doorlock5_2Open.Visible = false; });
        }
        private void Doorlock5_2Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock5_2Close.Visible = true; Pic_Doorlock5_2Close_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock5_2Close_Stop.Visible = true; Pic_Doorlock5_2Close.Visible = false; });
        }
        private void Doorlock5_1Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock5_1Open.Visible = true; Pic_Doorlock5_1Open_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock5_1Open_Stop.Visible = true; Pic_Doorlock5_1Open.Visible = false; });
        }
        private void Doorlock5_1Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock5_1Close.Visible = true; Pic_Doorlock5_1Close_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock5_1Close_Stop.Visible = true; Pic_Doorlock5_1Close.Visible = false; });
        }
        //End Tràn 5
        //Tràn 4
        private void Door4_PressureHigh_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door4_PressureHigh.Visible = true; Pic_Door4_PressureHigh_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door4_PressureHigh_Stop.Visible = true; Pic_Door4_PressureHigh.Visible = false; });
        }
        private void Door4_PressureLow_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door4_PressureLow.Visible = true; Pic_Door4_PressureLow_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door4_PressureLow_Stop.Visible = true; Pic_Door4_PressureLow.Visible = false; });
        }
        private void Door4_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door4_Opening.Visible = true; Pic_Door4_Opening_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door4_Opening_Stop.Visible = true; Pic_Door4_Opening.Visible = false; });
        }
        private void Door4_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door4_Closing.Visible = true; Pic_Door4_Closing_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door4_Closing_Stop.Visible = true; Pic_Door4_Closing.Visible = false; });
        }
        private void Door4_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door4_Open.Visible = true; Pic_Door4_Open_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door4_Open_Stop.Visible = true; Pic_Door4_Open.Visible = false; });
        }
        private void Door4_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door4_Close.Visible = true; Pic_Door4_Close_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door4_Close_Stop.Visible = true; Pic_Door4_Close.Visible = false; });
        }

        private void Doorlock4_2Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock4_2Open.Visible = true; Pic_Doorlock4_2Open_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock4_2Open_Stop.Visible = true; Pic_Doorlock4_2Open.Visible = false; });
        }
        private void Doorlock4_2Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock4_2Close.Visible = true; Pic_Doorlock4_2Close_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock4_2Close_Stop.Visible = true; Pic_Doorlock4_2Close.Visible = false; });
        }
        private void Doorlock4_1Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock4_1Open.Visible = true; Pic_Doorlock4_1Open_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock4_1Open_Stop.Visible = true; Pic_Doorlock4_1Open.Visible = false; });
        }
        private void Doorlock4_1Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock4_1Close.Visible = true; Pic_Doorlock4_1Close_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock4_1Close_Stop.Visible = true; Pic_Doorlock4_1Close.Visible = false; });
        }
        // End Tràn 4

        // Tràn 2
        private void Door2_PressureHigh_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door2_PressureHigh.Visible = true; Pic_Door2_PressureHigh_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door2_PressureHigh_Stop.Visible = true; Pic_Door2_PressureHigh.Visible = false; });
        }
        private void Door2_PressureLow_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door2_PressureLow.Visible = true; Pic_Door2_PressureLow_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door2_PressureLow_Stop.Visible = true; Pic_Door2_PressureLow.Visible = false; });
        }
        private void Door2_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door2_Opening.Visible = true; Pic_Door2_Opening_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door2_Opening_Stop.Visible = true; Pic_Door2_Opening.Visible = false; });
        }
        private void Door2_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door2_Closing.Visible = true; Pic_Door2_Closing_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door2_Closing_Stop.Visible = true; Pic_Door2_Closing.Visible = false; });
        }

        private void Door2_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door2_Open.Visible = true; Pic_Door2_Open_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door2_Open_Stop.Visible = true; Pic_Door2_Open.Visible = false; });
        }
        private void Door2_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door2_Close.Visible = true; Pic_Door2_Close_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door2_Close_Stop.Visible = true; Pic_Door2_Close.Visible = false; });
        }
        // Tràn 3,
        private void Door3_PressureHigh_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door3_PressureHigh.Visible = true; Pic_Door3_PressureHigh_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door3_PressureHigh_Stop.Visible = true; Pic_Door3_PressureHigh.Visible = false; });
        }
        private void Door3_PressureLow_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door3_PressureLow.Visible = true; Pic_Door3_PressureLow_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door3_PressureLow_Stop.Visible = true; Pic_Door3_PressureLow.Visible = false; });
        }
        private void Door3_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door3_Opening.Visible = true; Pic_Door3_Opening_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door3_Opening_Stop.Visible = true; Pic_Door3_Opening.Visible = false; });
        }
        private void Door3_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door3_Closing.Visible = true; Pic_Door3_Closing_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door3_Closing_Stop.Visible = true; Pic_Door3_Closing.Visible = false; });
        }
        private void Door3_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door3_Open.Visible = true; Pic_Door3_Open_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door3_Open_Stop.Visible = true; Pic_Door3_Open.Visible = false; });
        }
        private void Door3_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door3_Close.Visible = true; Pic_Door3_Close_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door3_Close_Stop.Visible = true; Pic_Door3_Close.Visible = false; });
        }

        private void Doorlock3_2Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock3_2Open.Visible = true; Pic_Doorlock3_2Open_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock3_2Open_Stop.Visible = true; Pic_Doorlock3_2Open.Visible = false; });
        }
        private void Doorlock3_2Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock3_2Close.Visible = true; Pic_Doorlock3_2Close_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock3_2Close_Stop.Visible = true; Pic_Doorlock3_2Close.Visible = false; });
        }
        private void Doorlock3_1Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock3_1Open.Visible = true; Pic_Doorlock3_1Open_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock3_1Open_Stop.Visible = true; Pic_Doorlock3_1Open.Visible = false; });
        }
        private void Doorlock3_1Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock3_1Close.Visible = true; Pic_Doorlock3_1Close_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock3_1Close_Stop.Visible = true; Pic_Doorlock3_1Close.Visible = false; });
        }
        // Hết Tràn 3,4
        private void Door1_PressureLow_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door1_PressureLow.Visible = true; Pic_Door1_PressureLow_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door1_PressureLow_Stop.Visible = true; Pic_Door1_PressureLow.Visible = false; });
        }
        private void Door1_PressureHigh_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door1_PressureHigh.Visible = true; Pic_Door1_PressureHigh_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door1_PressureHigh_Stop.Visible = true; Pic_Door1_PressureHigh.Visible = false; });
        }
        private void Door1_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door1_Close.Visible = true; Pic_Door1_Close_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door1_Close_Stop.Visible = true; Pic_Door1_Close.Visible = false; });
        }
        private void Door1_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door1_Open.Visible = true; Pic_Door1_Open_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door1_Open_Stop.Visible = true; Pic_Door1_Open.Visible = false; });
        }
        private void Door1_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door1_Closing.Visible = true;Pic_Door1_Closing_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door1_Closing_Stop.Visible = true; Pic_Door1_Closing.Visible = false; });
        }
        private void Door1_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Door1_Opening.Visible = true;Pic_Door1_Opening_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Door1_Opening_Stop.Visible = true; Pic_Door1_Opening.Visible = false; });
        }
        private void Doorlock2_2Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock2_2Open.Visible = true; Pic_Doorlock2_2Open_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock2_2Open_Stop.Visible = true; Pic_Doorlock2_2Open.Visible = false; });
        }
        private void Doorlock2_2Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock2_2Close.Visible = true; Pic_Doorlock2_2Close_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock2_2Close_Stop.Visible = true; Pic_Doorlock2_2Close.Visible = false; });
        }
        private void Doorlock2_1Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock2_1Open.Visible = true;  Pic_Doorlock2_1Open_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock2_1Open_Stop.Visible = true; Pic_Doorlock2_1Open.Visible = false; });
        }
        private void Doorlock2_1Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock2_1Close.Visible = true; Pic_Doorlock2_1Close_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Doorlock2_1Close_Stop.Visible = true; Pic_Doorlock2_1Close.Visible = false; });
        }
        // Trạng thái lổi Trạm 3
        private void S3_DC3_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_S3_DC3_Over.Visible = true; PicT6_S3_DC3_Over.Visible = true; Pic_S3_DC3_Over_Stop.Visible = false; PicT6_S3_DC3_Over_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_S3_DC3_Over_Stop.Visible = true; PicT6_S3_DC3_Over_Stop.Visible = true; Pic_S3_DC3_Over.Visible = false; PicT6_S3_DC3_Over.Visible = false; });
        }
        private void S3_DC2_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_S3_DC2_Over.Visible = true; PicT6_S3_DC2_Over.Visible = true; Pic_S3_DC2_Over_Stop.Visible = false; PicT6_S3_DC2_Over_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_S3_DC2_Over_Stop.Visible = true; PicT6_S3_DC2_Over_Stop.Visible = true; Pic_S3_DC2_Over.Visible = false; PicT6_S3_DC2_Over.Visible = false; });
        }
        private void S3_DC1_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_S3_DC1_Over.Visible = true; PicT6_S3_DC1_Over.Visible = true; Pic_S3_DC1_Over_Stop.Visible = false; PicT6_S3_DC1_Over_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_S3_DC1_Over_Stop.Visible = true; PicT6_S3_DC1_Over_Stop.Visible = true; Pic_S3_DC1_Over.Visible = false; PicT6_S3_DC1_Over.Visible = false; });
        }

        private void S3_DC3_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_S3_DC3_Running.Visible = true; PicT6_S3_DC3_Running.Visible = true; Pic_S3_DC3_Stop.Visible = false; PicT6_S3_DC3_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_S3_DC3_Stop.Visible = true; PicT6_S3_DC3_Stop.Visible = true; Pic_S3_DC3_Running.Visible = false; PicT6_S3_DC3_Running.Visible = false; });
        }
        private void S3_DC2_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_S3_DC2_Running.Visible = true; PicT6_S3_DC2_Running.Visible = true; Pic_S3_DC2_Stop.Visible = false; PicT6_S3_DC2_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_S3_DC2_Stop.Visible = true; PicT6_S3_DC2_Stop.Visible = true; Pic_S3_DC2_Running.Visible = false; PicT6_S3_DC2_Running.Visible = false; });
        }
        private void S3_DC1_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_S3_DC1_Running.Visible = true; PicT6_S3_DC1_Running.Visible = true; Pic_S3_DC1_Stop.Visible = false; PicT6_S3_DC1_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_S3_DC1_Stop.Visible = true; PicT6_S3_DC1_Stop.Visible = true; Pic_S3_DC1_Running.Visible = false; PicT6_S3_DC1_Running.Visible = false; });
        }
        // Trạng thái lổi Trạm 2
        private void S2_DC3_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_S2_DC3_Over.Visible = true; PicT4_S2_DC3_Over.Visible = true; Pic_S2_DC3_Over_Stop.Visible = false; PicT4_S2_DC3_Over_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_S2_DC3_Over_Stop.Visible = true; PicT4_S2_DC3_Over_Stop.Visible = true; Pic_S2_DC3_Over.Visible = false; PicT4_S2_DC3_Over.Visible = false; });
        }
        private void S2_DC2_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_S2_DC2_Over.Visible = true; PicT4_S2_DC2_Over.Visible = true; Pic_S2_DC2_Over_Stop.Visible = false; PicT4_S2_DC2_Over_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_S2_DC2_Over_Stop.Visible = true; PicT4_S2_DC2_Over_Stop.Visible = true; Pic_S2_DC2_Over.Visible = false; PicT4_S2_DC2_Over.Visible = false; });
        }
        private void S2_DC1_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_S2_DC1_Over.Visible = true; PicT4_S2_DC1_Over.Visible = true; Pic_S2_DC1_Over_Stop.Visible = false; PicT4_S2_DC1_Over_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_S2_DC1_Over_Stop.Visible = true; PicT4_S2_DC1_Over_Stop.Visible = true; Pic_S2_DC1_Over.Visible = false; PicT4_S2_DC1_Over.Visible = false; });
        }      
        // Running Trạm 2
        private void S2_DC1_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_S2_DC1_Running.Visible = true; PicT4_S2_DC1_Running.Visible = true; Pic_S2_DC1_Stop.Visible = false; PicT4_S2_DC1_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_S2_DC1_Stop.Visible = true; PicT4_S2_DC1_Stop.Visible = true; Pic_S2_DC1_Running.Visible = false; PicT4_S2_DC1_Running.Visible = false; });
        }
        private void S2_DC2_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_S2_DC2_Running.Visible = true; PicT4_S2_DC2_Running.Visible = true; Pic_S2_DC2_Stop.Visible = false; PicT4_S2_DC2_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_S2_DC2_Stop.Visible = true; PicT4_S2_DC2_Stop.Visible = true; Pic_S2_DC2_Running.Visible = false; PicT4_S2_DC2_Running.Visible = false; });
        }
        private void S2_DC3_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_S2_DC3_Running.Visible = true; PicT4_S2_DC3_Running.Visible = true; Pic_S2_DC3_Stop.Visible = false; PicT4_S2_DC3_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_S2_DC3_Stop.Visible = true; PicT4_S2_DC3_Stop.Visible = true; Pic_S2_DC3_Running.Visible = false; PicT4_S2_DC3_Running.Visible = false; });
        }
        private void S1_DC3_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_S1_DC3_Over.Visible = true; PicT2_S1_DC3_Over.Visible = true; Pic_S1_DC3_Over_Stop.Visible = false; PicT2_S1_DC3_Over_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_S1_DC3_Over_Stop.Visible = true; PicT2_S1_DC3_Over_Stop.Visible = true; Pic_S1_DC3_Over.Visible = false; PicT2_S1_DC3_Over.Visible = false; });
        }
        private void S1_DC2_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_S1_DC2_Over.Visible = true; PicT2_S1_DC2_Over.Visible = true; Pic_S1_DC2_Over_Stop.Visible = false; PicT2_S1_DC2_Over_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_S1_DC2_Over_Stop.Visible = true; PicT2_S1_DC2_Over_Stop.Visible = true; Pic_S1_DC2_Over.Visible = false; PicT2_S1_DC2_Over.Visible = false; });
        }
        private void S1_DC1_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_S1_DC1_Over.Visible = true; PicT2_S1_DC1_Over.Visible = true; Pic_S1_DC1_Over_Stop.Visible = false; PicT2_S1_DC1_Over_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_S1_DC1_Over_Stop.Visible = true; PicT2_S1_DC1_Over_Stop.Visible = true; Pic_S1_DC1_Over.Visible = false; PicT2_S1_DC1_Over.Visible = false; });
        }
        private void S1_DC3_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_S1_DC3_Running.Visible = true; PicT2_S1_DC3_Running.Visible = true; Pic_S1_DC3_Stop.Visible = false; PicT2_S1_DC3_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_S1_DC3_Stop.Visible = true; PicT2_S1_DC3_Stop.Visible = true; Pic_S1_DC3_Running.Visible = false; PicT2_S1_DC3_Running.Visible = false; });
        }
        private void S1_DC2_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_S1_DC2_Running.Visible = true; PicT2_S1_DC2_Running.Visible = true; Pic_S1_DC2_Stop.Visible = false; PicT2_S1_DC2_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_S1_DC2_Stop.Visible = true; PicT2_S1_DC2_Stop.Visible = true; Pic_S1_DC2_Running.Visible = false; PicT2_S1_DC2_Running.Visible = false; });
        }
        private void S1_DC1_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_S1_DC1_Running.Visible = true;PicT2_S1_DC1_Running.Visible = true ; Pic_S1_DC1_Stop.Visible = false;PicT2_S1_DC1_Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_S1_DC1_Stop.Visible = true; PicT2_S1_DC1_Stop.Visible = true; Pic_S1_DC1_Running.Visible = false; PicT2_S1_DC1_Running.Visible = false; });
        }

      
        

        private void bntLoad_Click(object sender, EventArgs e)
        {

        }
    }
}
