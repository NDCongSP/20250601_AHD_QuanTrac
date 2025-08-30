using Ahd.Core;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
namespace RegistrationForm1
{
    public partial class FrmTran : Form
    {      
       private FrmMain _mainForm;          
        private BindingList<Tran1Model> tran1List = new BindingList<Tran1Model>();
        private BindingList<Tran2Model> tran2List = new BindingList<Tran2Model>();
        private BindingList<Tran3Model> tran3List = new BindingList<Tran3Model>();
        private BindingList<Tran4Model> tran4List = new BindingList<Tran4Model>();
        private BindingList<Tran5Model> tran5List = new BindingList<Tran5Model>();
        private BindingList<Tran6Model> tran6List = new BindingList<Tran6Model>();
        public FrmTran(FrmMain frmMain)
        {        
            InitializeComponent();          
              Load += FrmTran_Load;
            _mainForm = frmMain; // ✅ Gán trước khi sử dụng           
        }
        private void FrmTran_Load(object sender, EventArgs e)
        {
            if (_mainForm != null)
            {
                // Subscribe to events
                //_mainForm.S1RemoteChanged += S1_Remote_ValueChanged;
                //_mainForm.S1LocalChanged += S1_Local_ValueChanged;
                //_mainForm.S1AutoChanged += S1_Auto_ValueChanged;
                //_mainForm.S1ManChanged += S1_Man_ValueChanged;
                //_mainForm.S1LocalStopChanged += S1_Local_Stop_ValueChanged;

                //_mainForm.S2RemoteChanged += S2_Remote_ValueChanged;
                //_mainForm.S2LocalChanged += S2_Local_ValueChanged;
                //_mainForm.S2AutoChanged += S2_Auto_ValueChanged;
                //_mainForm.S2ManChanged += S2_Man_ValueChanged;
                //_mainForm.S2LocalStopChanged += S2_Local_Stop_ValueChanged;

                //_mainForm.S3RemoteChanged += S3_Remote_ValueChanged;
                //_mainForm.S3LocalChanged += S3_Local_ValueChanged;
                //_mainForm.S3AutoChanged += S3_Auto_ValueChanged;
                //_mainForm.S3ManChanged += S3_Man_ValueChanged;
                //_mainForm.S3LocalStopChanged += S3_Local_Stop_ValueChanged;
                //// Subscribe to DC Running events
                //_mainForm.S1_DC1_RunningChanged += S1_DC1_Running_ValueChanged;
                //_mainForm.S1_DC2_RunningChanged += S1_DC2_Running_ValueChanged;
                //_mainForm.S1_DC3_RunningChanged += S1_DC3_Running_ValueChanged;
                //_mainForm.S2_DC1_RunningChanged += S2_DC1_Running_ValueChanged;
                //_mainForm.S2_DC2_RunningChanged += S2_DC2_Running_ValueChanged;
                //_mainForm.S2_DC3_RunningChanged += S2_DC3_Running_ValueChanged;
                //_mainForm.S3_DC1_RunningChanged += S3_DC1_Running_ValueChanged;
                //_mainForm.S3_DC2_RunningChanged += S3_DC2_Running_ValueChanged;
                //_mainForm.S3_DC3_RunningChanged += S3_DC3_Running_ValueChanged;
                //// Subscribe to Door events
                //_mainForm.Door1_OpeningChanged += Door1_Opening_ValueChanged;
                //_mainForm.Door1_ClosingChanged += Door1_Closing_ValueChanged;
                //_mainForm.Door2_OpeningChanged += Door2_Opening_ValueChanged;
                //_mainForm.Door2_ClosingChanged += Door2_Closing_ValueChanged;
                //_mainForm.Door3_OpeningChanged += Door3_Opening_ValueChanged;
                //_mainForm.Door3_ClosingChanged += Door3_Closing_ValueChanged;
                //_mainForm.Door4_OpeningChanged += Door4_Opening_ValueChanged;
                //_mainForm.Door4_ClosingChanged += Door4_Closing_ValueChanged;
                //_mainForm.Door5_OpeningChanged += Door5_Opening_ValueChanged;
                //_mainForm.Door5_ClosingChanged += Door5_Closing_ValueChanged;
                //_mainForm.Door6_OpeningChanged += Door6_Opening_ValueChanged;
                //_mainForm.Door6_ClosingChanged += Door6_Closing_ValueChanged;
                //_mainForm.Door1_OpenChanged += Door1_Open_ValueChanged;
                //_mainForm.Door1_CloseChanged += Door1_Close_ValueChanged;
                //_mainForm.Door2_OpenChanged += Door2_Open_ValueChanged;
                //_mainForm.Door2_CloseChanged += Door2_Close_ValueChanged;
                //_mainForm.Door3_OpenChanged += Door3_Open_ValueChanged;
                //_mainForm.Door3_CloseChanged += Door3_Close_ValueChanged;
                //_mainForm.Door4_OpenChanged += Door4_Open_ValueChanged;
                //_mainForm.Door4_CloseChanged += Door4_Close_ValueChanged;
                //_mainForm.Door5_OpenChanged += Door5_Open_ValueChanged;
                //_mainForm.Door5_CloseChanged += Door5_Close_ValueChanged;
                //_mainForm.Door6_OpenChanged += Door6_Open_ValueChanged;
                //_mainForm.Door6_CloseChanged += Door6_Close_ValueChanged;
                //_mainForm.Doorlock2_1OpenChanged += Doorlock2_1Open_ValueChanged;
                //_mainForm.Doorlock2_1CloseChanged += Doorlock2_1Close_ValueChanged;
                //_mainForm.Doorlock2_2OpenChanged += Doorlock2_2Open_ValueChanged;
                //_mainForm.Doorlock2_2CloseChanged += Doorlock2_2Close_ValueChanged;
                //_mainForm.Doorlock3_1OpenChanged += Doorlock3_1Open_ValueChanged;
                //_mainForm.Doorlock3_1CloseChanged += Doorlock3_1Close_ValueChanged;
                //_mainForm.Doorlock3_2OpenChanged += Doorlock3_2Open_ValueChanged;
                //_mainForm.Doorlock3_2CloseChanged += Doorlock3_2Close_ValueChanged;
                //_mainForm.Doorlock4_1OpenChanged += Doorlock4_1Open_ValueChanged;
                //_mainForm.Doorlock4_1CloseChanged += Doorlock4_1Close_ValueChanged;
                //_mainForm.Doorlock4_2OpenChanged += Doorlock4_2Open_ValueChanged;
                //_mainForm.Doorlock4_2CloseChanged += Doorlock4_2Close_ValueChanged;
                //_mainForm.Doorlock5_1OpenChanged += Doorlock5_1Open_ValueChanged;
                //_mainForm.Doorlock5_1CloseChanged += Doorlock5_1Close_ValueChanged;
                //_mainForm.Doorlock5_2OpenChanged += Doorlock5_2Open_ValueChanged;
                //_mainForm.Doorlock5_2CloseChanged += Doorlock5_2Close_ValueChanged;
                //_mainForm.S1_DC1_OverChanged += S1_DC1_Over_ValueChanged;
                //_mainForm.S1_DC2_OverChanged += S1_DC2_Over_ValueChanged;
                //_mainForm.S1_DC3_OverChanged += S1_DC3_Over_ValueChanged;
                //_mainForm.S2_DC1_OverChanged += S2_DC1_Over_ValueChanged;
                //_mainForm.S2_DC2_OverChanged += S2_DC2_Over_ValueChanged;
                //_mainForm.S2_DC3_OverChanged += S2_DC3_Over_ValueChanged;
                //_mainForm.S3_DC1_OverChanged += S3_DC1_Over_ValueChanged;
                //_mainForm.S3_DC2_OverChanged += S3_DC2_Over_ValueChanged;
                //_mainForm.S3_DC3_OverChanged += S3_DC3_Over_ValueChanged;
                //_mainForm.Door1_PressureHighChanged += Door1_PressureHigh_ValueChanged;
                //_mainForm.Door1_PressureLowChanged += Door1_PressureLow_ValueChanged;
                //_mainForm.Door2_PressureHighChanged += Door2_PressureHigh_ValueChanged;
                //_mainForm.Door2_PressureLowChanged += Door2_PressureLow_ValueChanged;
                //_mainForm.Door3_PressureHighChanged += Door3_PressureHigh_ValueChanged;
                //_mainForm.Door3_PressureLowChanged += Door3_PressureLow_ValueChanged;
                //_mainForm.Door4_PressureHighChanged += Door4_PressureHigh_ValueChanged;
                //_mainForm.Door4_PressureLowChanged += Door4_PressureLow_ValueChanged;
                //_mainForm.Door5_PressureHighChanged += Door5_PressureHigh_ValueChanged;
                //_mainForm.Door5_PressureLowChanged += Door5_PressureLow_ValueChanged;
                //_mainForm.Door6_PressureHighChanged += Door6_PressureHigh_ValueChanged;
                //_mainForm.Door6_PressureLowChanged += Door6_PressureLow_ValueChanged;
                //_mainForm.Al_Door1Changed += Al_Door1_ValueChanged;
                //_mainForm.Al_Door2Changed += Al_Door2_ValueChanged;
                //_mainForm.Al_Door3Changed += Al_Door3_ValueChanged;
                //_mainForm.Al_Door4Changed += Al_Door4_ValueChanged;
                //_mainForm.Al_Door5Changed += Al_Door5_ValueChanged;
                //_mainForm.Al_Door6Changed += Al_Door6_ValueChanged;
            }
            else
            {
                MessageBox.Show("FrmMain instance is null. Please check.");
            }
            // ✅ Load trạng thái ban đầu ngay khi khởi tạo
            LoadInitialValues();
            dataGridViewT1.CellFormatting += dataGridViewT1_CellFormatting;
            dataGridViewT2.CellFormatting += dataGridViewT2_CellFormatting;
            dataGridViewT3.CellFormatting += dataGridViewT3_CellFormatting;
            dataGridViewT4.CellFormatting += dataGridViewT4_CellFormatting;
            dataGridViewT5.CellFormatting += dataGridViewT5_CellFormatting;
            dataGridViewT6.CellFormatting += dataGridViewT6_CellFormatting;

            LoadAllTags1();
            ReadAllTagStatus1();
            LoadAllTags2();
            ReadAllTagStatus2();
            LoadAllTags3();
            ReadAllTagStatus3();
            LoadAllTags4();
            ReadAllTagStatus4();
            LoadAllTags5();
            ReadAllTagStatus5();
            LoadAllTags6();
            ReadAllTagStatus6();
        }
        private void FormatGridT6()
        {
            var dgv = dataGridViewT6;

            // ✅ Font và header style
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgv.EnableHeadersVisualStyles = false;
            // ✅ Font cell
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            // ✅ Canh giữa header và cell
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            // ✅ Auto size columns
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv.AutoResizeColumns();
            // ✅ Height dòng
            dgv.RowTemplate.Height = 30;
            // ✅ Màu alternating rows
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            // ✅ Border style
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            // ✅ Không cho user thêm hàng mới
            dgv.AllowUserToAddRows = false;
            // ✅ ReadOnly nếu chỉ hiển thị
            dgv.ReadOnly = true;
            // ✅ Full row select
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            // ✅ Đổi màu theo Status
            
        }
        private void FormatGridT5()
        {
            var dgv = dataGridViewT5;

            // ✅ Font và header style
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgv.EnableHeadersVisualStyles = false;
            // ✅ Font cell
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            // ✅ Canh giữa header và cell
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            // ✅ Auto size columns
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv.AutoResizeColumns();
            // ✅ Height dòng
            dgv.RowTemplate.Height = 30;
            // ✅ Màu alternating rows
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            // ✅ Border style
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            // ✅ Không cho user thêm hàng mới
            dgv.AllowUserToAddRows = false;
            // ✅ ReadOnly nếu chỉ hiển thị
            dgv.ReadOnly = true;
            // ✅ Full row select
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            // ✅ Đổi màu theo Status
            
        }
        private void FormatGridT4()
        {
            var dgv = dataGridViewT4;

            // ✅ Font và header style
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgv.EnableHeadersVisualStyles = false;
            // ✅ Font cell
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            // ✅ Canh giữa header và cell
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            // ✅ Auto size columns
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv.AutoResizeColumns();
            // ✅ Height dòng
            dgv.RowTemplate.Height = 30;
            // ✅ Màu alternating rows
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            // ✅ Border style
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            // ✅ Không cho user thêm hàng mới
            dgv.AllowUserToAddRows = false;
            // ✅ ReadOnly nếu chỉ hiển thị
            dgv.ReadOnly = true;
            // ✅ Full row select
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            // ✅ Đổi màu theo Status
            
        }
        private void FormatGridT3()
        {
            var dgv = dataGridViewT3;

            // ✅ Font và header style
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgv.EnableHeadersVisualStyles = false;
            // ✅ Font cell
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            // ✅ Canh giữa header và cell
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            // ✅ Auto size columns
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv.AutoResizeColumns();
            // ✅ Height dòng
            dgv.RowTemplate.Height = 30;
            // ✅ Màu alternating rows
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            // ✅ Border style
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            // ✅ Không cho user thêm hàng mới
            dgv.AllowUserToAddRows = false;
            // ✅ ReadOnly nếu chỉ hiển thị
            dgv.ReadOnly = true;
            // ✅ Full row select
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            
        }
        private void FormatGridT2()
        {
            var dgv = dataGridViewT2;

            // ✅ Font và header style
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgv.EnableHeadersVisualStyles = false;
            // ✅ Font cell
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            // ✅ Canh giữa header và cell
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            // ✅ Auto size columns
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv.AutoResizeColumns();
            // ✅ Height dòng
            dgv.RowTemplate.Height = 30;
            // ✅ Màu alternating rows
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            // ✅ Border style
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            // ✅ Không cho user thêm hàng mới
            dgv.AllowUserToAddRows = false;
            // ✅ ReadOnly nếu chỉ hiển thị
            dgv.ReadOnly = true;
            // ✅ Full row select
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            // ✅ Đổi màu theo Status
        }
        private void FormatGridT1()
        {
            var dgv = dataGridViewT1;

            // ✅ Font và header style
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgv.EnableHeadersVisualStyles = false;
            // ✅ Font cell
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            // ✅ Canh giữa header và cell
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            // ✅ Auto size columns
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv.AutoResizeColumns();
            // ✅ Height dòng
            dgv.RowTemplate.Height = 30;
            // ✅ Màu alternating rows
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            // ✅ Border style
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            // ✅ Không cho user thêm hàng mới
            dgv.AllowUserToAddRows = false;
            // ✅ ReadOnly nếu chỉ hiển thị
            dgv.ReadOnly = true;
            // ✅ Full row select
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            // ✅ Đổi màu theo Status
            
        }
        private void LoadInitialValues() // Load giá trị ban đầu từ FrmMain (được gọi trong constructor)
        {

            // Load trạng thái ban đầu từ FrmMain của các nút và nhãn
            //  label1.Text = $"S1_Remote: {_mainForm.GetS1RemoteValue()}"; bnt_Remote_T1.BackColor = _mainForm.GetS1RemoteValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Remote_T1.BackColor = _mainForm.GetS1RemoteValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Remote_T2.BackColor = _mainForm.GetS1RemoteValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Remote_T3.BackColor = _mainForm.GetS2RemoteValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Remote_T4.BackColor = _mainForm.GetS2RemoteValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Remote_T5.BackColor = _mainForm.GetS3RemoteValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Remote_T6.BackColor = _mainForm.GetS3RemoteValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            //   label2.Text = $"S1_Local: {_mainForm.GetS1LocalValue()}"; bnt_Local_T1.BackColor = _mainForm.GetS1LocalValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Local_T1.BackColor = _mainForm.GetS1LocalValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Local_T2.BackColor = _mainForm.GetS1LocalValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Local_T3.BackColor = _mainForm.GetS2LocalValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Local_T4.BackColor = _mainForm.GetS2LocalValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Local_T5.BackColor = _mainForm.GetS3LocalValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Local_T6.BackColor = _mainForm.GetS3LocalValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            //   label3.Text = $"S1_Auto: {_mainForm.GetS1AutoValue()}"; bnt_Auto_T1.BackColor = _mainForm.GetS1AutoValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Auto_T1.BackColor = _mainForm.GetS1AutoValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Auto_T2.BackColor = _mainForm.GetS1AutoValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Auto_T3.BackColor = _mainForm.GetS2AutoValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Auto_T4.BackColor = _mainForm.GetS2AutoValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Auto_T5.BackColor = _mainForm.GetS3AutoValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Auto_T6.BackColor = _mainForm.GetS3AutoValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            //    label4.Text = $"S1_Man: {_mainForm.GetS1ManValue()}"; bnt_Hand_T1.BackColor = _mainForm.GetS1ManValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Hand_T1.BackColor = _mainForm.GetS1ManValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Hand_T2.BackColor = _mainForm.GetS1ManValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Hand_T3.BackColor = _mainForm.GetS2ManValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Hand_T4.BackColor = _mainForm.GetS2ManValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Hand_T5.BackColor = _mainForm.GetS3ManValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Hand_T6.BackColor = _mainForm.GetS3ManValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            //       label5.Text = $"S1_Local_Stop: {_mainForm.GetS1LocalStopValue()}"; bnt_Estop_T1.BackColor = _mainForm.GetS1LocalStopValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Estop_T1.BackColor = _mainForm.GetS1LocalStopValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Estop_T2.BackColor = _mainForm.GetS1LocalStopValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Estop_T3.BackColor = _mainForm.GetS2LocalStopValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Estop_T4.BackColor = _mainForm.GetS2LocalStopValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Estop_T5.BackColor = _mainForm.GetS3LocalStopValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            bnt_Estop_T6.BackColor = _mainForm.GetS3LocalStopValue() == "1" ? Color.GreenYellow : DefaultBackColor;
            // Load trạng thái của các nút DC Running
            Pic_S1_DC1_Running.Visible = _mainForm.GetS1_DC1_RunningValue() == "1";
            PicT2_S1_DC1_Running.Visible = _mainForm.GetS1_DC1_RunningValue() == "1";
            Pic_S1_DC2_Running.Visible = _mainForm.GetS1_DC2_RunningValue() == "1";
            PicT2_S1_DC2_Running.Visible = _mainForm.GetS1_DC2_RunningValue() == "1";
            Pic_S1_DC3_Running.Visible = _mainForm.GetS1_DC3_RunningValue() == "1";
            PicT2_S1_DC3_Running.Visible = _mainForm.GetS1_DC3_RunningValue() == "1";
            Pic_S2_DC1_Running.Visible = _mainForm.GetS2_DC1_RunningValue() == "1";
            PicT4_S2_DC1_Running.Visible = _mainForm.GetS2_DC1_RunningValue() == "1";
            Pic_S2_DC2_Running.Visible = _mainForm.GetS2_DC2_RunningValue() == "1";
            PicT4_S2_DC2_Running.Visible = _mainForm.GetS2_DC2_RunningValue() == "1";
            Pic_S2_DC3_Running.Visible = _mainForm.GetS2_DC3_RunningValue() == "1";
            PicT4_S2_DC3_Running.Visible = _mainForm.GetS2_DC3_RunningValue() == "1";
            Pic_S3_DC1_Running.Visible = _mainForm.GetS3_DC1_RunningValue() == "1";
            PicT6_S3_DC1_Running.Visible = _mainForm.GetS3_DC1_RunningValue() == "1";
            Pic_S3_DC2_Running.Visible = _mainForm.GetS3_DC2_RunningValue() == "1";
            PicT6_S3_DC2_Running.Visible = _mainForm.GetS3_DC2_RunningValue() == "1";
            Pic_S3_DC3_Running.Visible = _mainForm.GetS3_DC3_RunningValue() == "1";
            PicT6_S3_DC3_Running.Visible = _mainForm.GetS3_DC3_RunningValue() == "1";
            // Load trạng thái của các cửa
            Pic_Door1_Opening.Visible = _mainForm.GetDoor1_OpeningValue() == "1";
            Pic_Door1_Opening_Stop.Visible = _mainForm.GetDoor1_OpeningValue() == "0";
            Pic_Door1_Closing.Visible = _mainForm.GetDoor1_ClosingValue() == "1";
            Pic_Door1_Closing_Stop.Visible = _mainForm.GetDoor1_ClosingValue() == "0";
            Pic_Door2_Opening.Visible = _mainForm.GetDoor2_OpeningValue() == "1";
            Pic_Door2_Opening_Stop.Visible = _mainForm.GetDoor2_OpeningValue() == "0";
            Pic_Door2_Closing.Visible = _mainForm.GetDoor2_ClosingValue() == "1";
            Pic_Door2_Closing_Stop.Visible = _mainForm.GetDoor2_ClosingValue() == "0";
            Pic_Door3_Opening.Visible = _mainForm.GetDoor3_OpeningValue() == "1";
            Pic_Door3_Opening_Stop.Visible = _mainForm.GetDoor3_OpeningValue() == "0";
            Pic_Door3_Closing.Visible = _mainForm.GetDoor3_ClosingValue() == "1";
            Pic_Door3_Closing_Stop.Visible = _mainForm.GetDoor3_ClosingValue() == "0";
            Pic_Door4_Opening.Visible = _mainForm.GetDoor4_OpeningValue() == "1";
            Pic_Door4_Opening_Stop.Visible = _mainForm.GetDoor4_OpeningValue() == "0";
            Pic_Door4_Closing.Visible = _mainForm.GetDoor4_ClosingValue() == "1";
            Pic_Door4_Closing_Stop.Visible = _mainForm.GetDoor4_ClosingValue() == "0";
            Pic_Door5_Opening.Visible = _mainForm.GetDoor5_OpeningValue() == "1";
            Pic_Door5_Opening_Stop.Visible = _mainForm.GetDoor5_OpeningValue() == "0";
            Pic_Door5_Closing.Visible = _mainForm.GetDoor5_ClosingValue() == "1";
            Pic_Door5_Closing_Stop.Visible = _mainForm.GetDoor5_ClosingValue() == "0";
            Pic_Door6_Opening.Visible = _mainForm.GetDoor6_OpeningValue() == "1";
            Pic_Door6_Opening_Stop.Visible = _mainForm.GetDoor6_OpeningValue() == "0";
            Pic_Door6_Closing.Visible = _mainForm.GetDoor6_ClosingValue() == "1";
            Pic_Door6_Closing_Stop.Visible = _mainForm.GetDoor6_ClosingValue() == "0";
            Pic_Door1_Open.Visible = _mainForm.GetDoor1_OpenValue() == "1";
            Pic_Door1_Open_Stop.Visible = _mainForm.GetDoor1_OpenValue() == "0";
            Pic_Door1_Close.Visible = _mainForm.GetDoor1_CloseValue() == "1";
            Pic_Door1_Close_Stop.Visible = _mainForm.GetDoor1_CloseValue() == "0";
            Pic_Door2_Open.Visible = _mainForm.GetDoor2_OpenValue() == "1";
            Pic_Door2_Open_Stop.Visible = _mainForm.GetDoor2_OpenValue() == "0";
            Pic_Door2_Close.Visible = _mainForm.GetDoor2_CloseValue() == "1";
            Pic_Door2_Close_Stop.Visible = _mainForm.GetDoor2_CloseValue() == "0";
            Pic_Door3_Open.Visible = _mainForm.GetDoor3_OpenValue() == "1";
            Pic_Door3_Open_Stop.Visible = _mainForm.GetDoor3_OpenValue() == "0";
            Pic_Door3_Close.Visible = _mainForm.GetDoor3_CloseValue() == "1";
            Pic_Door3_Close_Stop.Visible = _mainForm.GetDoor3_CloseValue() == "0";
            Pic_Door4_Open.Visible = _mainForm.GetDoor4_OpenValue() == "1";
            Pic_Door4_Open_Stop.Visible = _mainForm.GetDoor4_OpenValue() == "0";
            Pic_Door4_Close.Visible = _mainForm.GetDoor4_CloseValue() == "1";
            Pic_Door4_Close_Stop.Visible = _mainForm.GetDoor4_CloseValue() == "0";
            Pic_Door5_Open.Visible = _mainForm.GetDoor5_OpenValue() == "1";
            Pic_Door5_Open_Stop.Visible = _mainForm.GetDoor5_OpenValue() == "0";
            Pic_Door5_Close.Visible = _mainForm.GetDoor5_CloseValue() == "1";
            Pic_Door5_Close_Stop.Visible = _mainForm.GetDoor5_CloseValue() == "0";
            Pic_Door6_Open.Visible = _mainForm.GetDoor6_OpenValue() == "1";
            Pic_Door6_Open_Stop.Visible = _mainForm.GetDoor6_OpenValue() == "0";
            Pic_Door6_Close.Visible = _mainForm.GetDoor6_CloseValue() == "1";
            Pic_Door6_Close_Stop.Visible = _mainForm.GetDoor6_CloseValue() == "0";
            Pic_Doorlock2_1Open.Visible = _mainForm.GetDoorlock2_1OpenValue() == "1";
            Pic_Doorlock2_1Open_Stop.Visible = _mainForm.GetDoorlock2_1OpenValue() == "0";
            Pic_Doorlock2_1Close.Visible = _mainForm.GetDoorlock2_1CloseValue() == "1";
            Pic_Doorlock2_1Close_Stop.Visible = _mainForm.GetDoorlock2_1CloseValue() == "0";
            Pic_Doorlock2_2Open.Visible = _mainForm.GetDoorlock2_2OpenValue() == "1";
            Pic_Doorlock2_2Open_Stop.Visible = _mainForm.GetDoorlock2_2OpenValue() == "0";
            Pic_Doorlock2_2Close.Visible = _mainForm.GetDoorlock2_2CloseValue() == "1";
            Pic_Doorlock2_2Close_Stop.Visible = _mainForm.GetDoorlock2_2CloseValue() == "0";
            Pic_Doorlock3_1Open.Visible = _mainForm.GetDoorlock3_1OpenValue() == "1";
            Pic_Doorlock3_1Open_Stop.Visible = _mainForm.GetDoorlock3_1OpenValue() == "0";
            Pic_Doorlock3_1Close.Visible = _mainForm.GetDoorlock3_1CloseValue() == "1";
            Pic_Doorlock3_1Close_Stop.Visible = _mainForm.GetDoorlock3_1CloseValue() == "0";
            Pic_Doorlock3_2Open.Visible = _mainForm.GetDoorlock3_2OpenValue() == "1";
            Pic_Doorlock3_2Open_Stop.Visible = _mainForm.GetDoorlock3_2OpenValue() == "0";
            Pic_Doorlock3_2Close.Visible = _mainForm.GetDoorlock3_2CloseValue() == "1";
            Pic_Doorlock3_2Close_Stop.Visible = _mainForm.GetDoorlock3_2CloseValue() == "0";
            Pic_Doorlock4_1Open.Visible = _mainForm.GetDoorlock4_1OpenValue() == "1";
            Pic_Doorlock4_1Open_Stop.Visible = _mainForm.GetDoorlock4_1OpenValue() == "0";
            Pic_Doorlock4_1Close.Visible = _mainForm.GetDoorlock4_1CloseValue() == "1";
            Pic_Doorlock4_1Close_Stop.Visible = _mainForm.GetDoorlock4_1CloseValue() == "0";
            Pic_Doorlock4_2Open.Visible = _mainForm.GetDoorlock4_2OpenValue() == "1";
            Pic_Doorlock4_2Open_Stop.Visible = _mainForm.GetDoorlock4_2OpenValue() == "0";
            Pic_Doorlock4_2Close.Visible = _mainForm.GetDoorlock4_2CloseValue() == "1";
            Pic_Doorlock4_2Close_Stop.Visible = _mainForm.GetDoorlock4_2CloseValue() == "0";
            Pic_Doorlock5_1Open.Visible = _mainForm.GetDoorlock5_1OpenValue() == "1";
            Pic_Doorlock5_1Open_Stop.Visible = _mainForm.GetDoorlock5_1OpenValue() == "0";
            Pic_Doorlock5_1Close.Visible = _mainForm.GetDoorlock5_1CloseValue() == "1";
            Pic_Doorlock5_1Close_Stop.Visible = _mainForm.GetDoorlock5_1CloseValue() == "0";
            Pic_Doorlock5_2Open.Visible = _mainForm.GetDoorlock5_2OpenValue() == "1";
            Pic_Doorlock5_2Open_Stop.Visible = _mainForm.GetDoorlock5_2OpenValue() == "0";
            Pic_Doorlock5_2Close.Visible = _mainForm.GetDoorlock5_2CloseValue() == "1";
            Pic_Doorlock5_2Close_Stop.Visible = _mainForm.GetDoorlock5_2CloseValue() == "0";
            Pic_S1_DC1_Over.Visible = _mainForm.GetS1_DC1_OverValue() == "1";PicT2_S1_DC1_Over.Visible = _mainForm.GetS1_DC1_OverValue() == "1";
            Pic_S1_DC1_Over_Stop.Visible = _mainForm.GetS1_DC1_OverValue() == "0";PicT2_S1_DC1_Over_Stop.Visible = _mainForm.GetS1_DC1_OverValue() == "0";
            Pic_S1_DC2_Over.Visible = _mainForm.GetS1_DC2_OverValue() == "1";PicT2_S1_DC2_Over.Visible = _mainForm.GetS1_DC2_OverValue() == "1";
            Pic_S1_DC2_Over_Stop.Visible = _mainForm.GetS1_DC2_OverValue() == "0";PicT2_S1_DC2_Over_Stop.Visible = _mainForm.GetS1_DC2_OverValue() == "0";
            Pic_S1_DC3_Over.Visible = _mainForm.GetS1_DC3_OverValue() == "1";PicT2_S1_DC3_Over.Visible = _mainForm.GetS1_DC3_OverValue() == "1";
            Pic_S1_DC3_Over_Stop.Visible = _mainForm.GetS1_DC3_OverValue() == "0";PicT2_S1_DC3_Over_Stop.Visible = _mainForm.GetS1_DC3_OverValue() == "0";

            Pic_S2_DC1_Over.Visible = _mainForm.GetS2_DC1_OverValue() == "1";PicT4_S2_DC1_Over.Visible = _mainForm.GetS2_DC1_OverValue() == "1";
            Pic_S2_DC1_Over_Stop.Visible = _mainForm.GetS2_DC1_OverValue() == "0";PicT4_S2_DC1_Over_Stop.Visible = _mainForm.GetS2_DC1_OverValue() == "0";
            Pic_S2_DC2_Over.Visible = _mainForm.GetS2_DC2_OverValue() == "1";PicT4_S2_DC2_Over.Visible = _mainForm.GetS2_DC2_OverValue() == "1";
            Pic_S2_DC2_Over_Stop.Visible = _mainForm.GetS2_DC2_OverValue() == "0";PicT4_S2_DC2_Over_Stop.Visible = _mainForm.GetS2_DC2_OverValue() == "0";
            Pic_S2_DC3_Over.Visible = _mainForm.GetS2_DC3_OverValue() == "1";PicT4_S2_DC3_Over.Visible = _mainForm.GetS2_DC3_OverValue() == "1";
            Pic_S2_DC3_Over_Stop.Visible = _mainForm.GetS2_DC3_OverValue() == "0";Pic_S2_DC3_Over_Stop.Visible = _mainForm.GetS2_DC3_OverValue() == "0";

            Pic_S3_DC1_Over.Visible = _mainForm.GetS3_DC1_OverValue() == "1";PicT6_S3_DC1_Over.Visible = _mainForm.GetS3_DC1_OverValue() == "1";
            Pic_S3_DC1_Over_Stop.Visible = _mainForm.GetS3_DC1_OverValue() == "0";PicT6_S3_DC1_Over_Stop.Visible = _mainForm.GetS3_DC1_OverValue() == "0";
            Pic_S3_DC2_Over.Visible = _mainForm.GetS3_DC2_OverValue() == "1";PicT6_S3_DC2_Over.Visible = _mainForm.GetS3_DC2_OverValue() == "1";
            Pic_S3_DC2_Over_Stop.Visible = _mainForm.GetS3_DC2_OverValue() == "0";PicT6_S3_DC2_Over_Stop.Visible = _mainForm.GetS3_DC2_OverValue() == "0";
            Pic_S3_DC3_Over.Visible = _mainForm.GetS3_DC3_OverValue() == "1";PicT6_S3_DC3_Over.Visible = _mainForm.GetS3_DC3_OverValue() == "1";
            Pic_S3_DC3_Over_Stop.Visible = _mainForm.GetS3_DC3_OverValue() == "0";PicT6_S3_DC3_Over_Stop.Visible = _mainForm.GetS3_DC3_OverValue() == "0";

            Pic_Door1_PressureHigh.Visible = _mainForm.GetDoor1_PressureHighValue() == "1";
            Pic_Door1_PressureHigh_Stop.Visible = _mainForm.GetDoor1_PressureHighValue() == "0";
            Pic_Door1_PressureLow.Visible = _mainForm.GetDoor1_PressureLowValue() == "1";
            Pic_Door1_PressureLow_Stop.Visible = _mainForm.GetDoor1_PressureLowValue() == "0";
            Pic_Door2_PressureHigh.Visible = _mainForm.GetDoor2_PressureHighValue() == "1";
            Pic_Door2_PressureHigh_Stop.Visible = _mainForm.GetDoor2_PressureHighValue() == "0";
            Pic_Door2_PressureLow.Visible = _mainForm.GetDoor2_PressureLowValue() == "1";
            Pic_Door2_PressureLow_Stop.Visible = _mainForm.GetDoor2_PressureLowValue() == "0";
            Pic_Door3_PressureHigh.Visible = _mainForm.GetDoor3_PressureHighValue() == "1";
            Pic_Door3_PressureHigh_Stop.Visible = _mainForm.GetDoor3_PressureHighValue() == "0";
            Pic_Door3_PressureLow.Visible = _mainForm.GetDoor3_PressureLowValue() == "1";
            Pic_Door3_PressureLow_Stop.Visible = _mainForm.GetDoor3_PressureLowValue() == "0";
            Pic_Door4_PressureHigh.Visible = _mainForm.GetDoor4_PressureHighValue() == "1";
            Pic_Door4_PressureHigh_Stop.Visible = _mainForm.GetDoor4_PressureHighValue() == "0";
            Pic_Door4_PressureLow.Visible = _mainForm.GetDoor4_PressureLowValue() == "1";
            Pic_Door4_PressureLow_Stop.Visible = _mainForm.GetDoor4_PressureLowValue() == "0";
            Pic_Door5_PressureHigh.Visible = _mainForm.GetDoor5_PressureHighValue() == "1";
            Pic_Door5_PressureHigh_Stop.Visible = _mainForm.GetDoor5_PressureHighValue() == "0";
            Pic_Door5_PressureLow.Visible = _mainForm.GetDoor5_PressureLowValue() == "1";
            Pic_Door5_PressureLow_Stop.Visible = _mainForm.GetDoor5_PressureLowValue() == "0";
            Pic_Door6_PressureHigh.Visible = _mainForm.GetDoor6_PressureHighValue() == "1";
            Pic_Door6_PressureHigh_Stop.Visible = _mainForm.GetDoor6_PressureHighValue() == "0";
            Pic_Door6_PressureLow.Visible = _mainForm.GetDoor6_PressureLowValue() == "1";
            Pic_Door6_PressureLow_Stop.Visible = _mainForm.GetDoor6_PressureLowValue() == "0";
            // Load trạng thái các Alarm lệch cửa











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
        private void S1_Remote_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { bnt_Remote_T1.BackColor = Color.GreenYellow;bnt_Remote_T2.BackColor = Color.GreenYellow;  });
            }
            else
                this.Invoke((MethodInvoker)delegate { bnt_Remote_T1.BackColor = DefaultBackColor;bnt_Remote_T2.BackColor = DefaultBackColor; });
        }
        private void S1_Local_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { bnt_Local_T1.BackColor = Color.GreenYellow; bnt_Local_T2.BackColor = Color.GreenYellow; });
            }
            else
                this.Invoke((MethodInvoker)delegate { bnt_Local_T1.BackColor = DefaultBackColor; bnt_Local_T2.BackColor = DefaultBackColor; });
        }
        private void S1_Auto_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { bnt_Auto_T1.BackColor = Color.GreenYellow; bnt_Auto_T2.BackColor = Color.GreenYellow; });
            }
            else
                this.Invoke((MethodInvoker)delegate { bnt_Auto_T1.BackColor = DefaultBackColor; bnt_Auto_T2.BackColor = DefaultBackColor; });
        }
        private void S1_Man_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { bnt_Hand_T1.BackColor = Color.GreenYellow; bnt_Hand_T2.BackColor = Color.GreenYellow; });
            }
            else
                this.Invoke((MethodInvoker)delegate { bnt_Hand_T1.BackColor = DefaultBackColor; bnt_Hand_T2.BackColor = DefaultBackColor; });
        }
        private void S1_Local_Stop_ValueChanged(object sender, TagValueChangedEventArgs e)
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
        // Alam lệch cửa 1
        private void Al_Door1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Al_Door1.Visible = true; Pic_Al_Door1Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Al_Door1Stop.Visible = true; Pic_Al_Door1.Visible = false; });
        }
        // Alam lệch cửa 2
        private void Al_Door2_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Al_Door2.Visible = true; Pic_Al_Door2Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Al_Door2Stop.Visible = true; Pic_Al_Door2.Visible = false; });
        }
        // Alam lệch cửa 3
        private void Al_Door3_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Al_Door3.Visible = true; Pic_Al_Door3Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Al_Door3Stop.Visible = true; Pic_Al_Door3.Visible = false; });
        }
        // Alam lệch cửa 4
        private void Al_Door4_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Al_Door4.Visible = true; Pic_Al_Door4Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Al_Door4Stop.Visible = true; Pic_Al_Door4.Visible = false; });
        }
        // Alam lệch cửa 5
        private void Al_Door5_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Al_Door5.Visible = true; Pic_Al_Door5Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Al_Door5Stop.Visible = true; Pic_Al_Door5.Visible = false; });
        }
        // Alam lệch cửa 6
        private void Al_Door6_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            if (e.NewValue == "1")
            {
                this.Invoke((MethodInvoker)delegate { Pic_Al_Door6.Visible = true; Pic_Al_Door6Stop.Visible = false; });
            }
            else
                this.Invoke((MethodInvoker)delegate { Pic_Al_Door6Stop.Visible = true; Pic_Al_Door6.Visible = false; });
        }




        // Khu vực Tràn 1
        private void Door1_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue1("Cửa 1 Đang Đóng", newValue);
                // ✅ Update hình ảnh
                Pic_Door1_Closing.Visible = (newValue == "1");
                Pic_Door1_Closing_Stop.Visible = (newValue != "1");
            });
        }
        private void Door1_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue1("Cửa 1 Đang Mở", newValue);
                // ✅ Update hình ảnh
                Pic_Door1_Opening.Visible = (newValue == "1");
                Pic_Door1_Opening_Stop.Visible = (newValue != "1");
            });
        }
        private void Door1_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue1("Cửa 1 Đóng Hoàn Toàn", newValue);
                // ✅ Update hình ảnh
                Pic_Door1_Close.Visible = (newValue == "1");
                Pic_Door1_Close_Stop.Visible = (newValue != "1");
            });
        }
        private void Door1_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue1("Cửa 1 Mở Hoàn Toàn", newValue);
                // ✅ Update hình ảnh
                Pic_Door1_Open.Visible = (newValue == "1");
                Pic_Door1_Open_Stop.Visible = (newValue != "1");
            });
        }
        private void S1_DC3_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue1("Bơm 3 Đang Chạy", newValue);
                UpdateTagValue2("Bơm 3 Đang Chạy", newValue);
                // ✅ Update hình ảnh
                Pic_S1_DC3_Running.Visible = (newValue == "1");
                PicT2_S1_DC3_Running.Visible = (newValue == "1");
                Pic_S1_DC3_Stop.Visible = (newValue != "1");
               PicT2_S1_DC3_Stop.Visible = (newValue != "1");
            });
        }
        private void S1_DC2_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue1("Bơm 2 Đang Chạy", newValue);
                UpdateTagValue2("Bơm 2 Đang Chạy", newValue);
                // ✅ Update hình ảnh
                Pic_S1_DC2_Running.Visible = (newValue == "1");
                PicT2_S1_DC2_Running.Visible = (newValue == "1");
                Pic_S1_DC2_Stop.Visible = (newValue != "1");
                PicT2_S1_DC2_Stop.Visible = (newValue != "1");
            });
        }
        private void S1_DC1_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue1("Bơm 1 Đang Chạy", newValue);
                UpdateTagValue2("Bơm 1 Đang Chạy", newValue);
                // ✅ Update hình ảnh
                Pic_S1_DC1_Running.Visible = (newValue == "1");
                PicT2_S1_DC1_Running.Visible = (newValue == "1");
                Pic_S1_DC1_Stop.Visible = (newValue != "1");
                PicT2_S1_DC1_Stop.Visible = (newValue != "1");
            });
        }
        private void ReadAllTagStatus1()
        {
            if (_mainForm == null)
            {
                MessageBox.Show("_mainForm is null");
                return;
            }
            string pump1 = _mainForm.GetS1_DC1_RunningValue();
            string pump2 = _mainForm.GetS1_DC2_RunningValue();
            string pump3 = _mainForm.GetS1_DC3_RunningValue();
            string door1Open = _mainForm.GetDoor1_OpenValue();
            string door1Close = _mainForm.GetDoor1_CloseValue();
            string door1Opening = _mainForm.GetDoor1_OpeningValue();
            string door1Closing = _mainForm.GetDoor1_ClosingValue();

            UpdateTagValue1("Bơm 1 Đang Chạy", pump1 == "1" ? "1" : "0");
            UpdateTagValue1("Bơm 2 Đang Chạy", pump2 == "1" ? "1" : "0");
            UpdateTagValue1("Bơm 3 Đang Chạy", pump3 == "1" ? "1" : "0");
            UpdateTagValue1("Cửa 1 Mở Hoàn Toàn", door1Open == "1" ? "1" : "0");
            UpdateTagValue1("Cửa 1 Đóng Hoàn Toàn", door1Close == "1" ? "1" : "0");
            UpdateTagValue1("Cửa 1 Đang Mở", door1Opening == "1" ? "1" : "0");
            UpdateTagValue1("Cửa 1 Đang Đóng", door1Closing == "1" ? "1" : "0");
        }
        private void LoadAllTags1()
        {
            if (GlobalData.Tran1List.Count == 0)
            {
                int stt = 1;
                GlobalData.Tran1List.Add(new Tran1Model { Id = stt++, Device = "Bơm 1 Đang Chạy", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran1List.Add(new Tran1Model { Id = stt++, Device = "Bơm 2 Đang Chạy", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran1List.Add(new Tran1Model { Id = stt++, Device = "Bơm 3 Đang Chạy", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran1List.Add(new Tran1Model { Id = stt++, Device = "Cửa 1 Mở Hoàn Toàn", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran1List.Add(new Tran1Model { Id = stt++, Device = "Cửa 1 Đóng Hoàn Toàn", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran1List.Add(new Tran1Model { Id = stt++, Device = "Cửa 1 Đang Mở", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran1List.Add(new Tran1Model { Id = stt++, Device = "Cửa 1 Đang Đóng", Status = "0", CreateAt = DateTime.Now });
                // Thêm các Tag khác
            }
            tran1List = GlobalData.Tran1List;
            dataGridViewT1.DataSource = tran1List;
            FormatGridT1();
        }
        private void UpdateTagValue1(string device, string status)
        {
            if (dataGridViewT1.InvokeRequired)
            {
                dataGridViewT1.Invoke(new Action(() =>
                {
                    UpdateTagValue1(device, status); // Gọi lại chính hàm này trên UI thread
                }));
                return;
            }

            var item = tran1List.FirstOrDefault(x => x.Device == device);
            if (item != null)
            {
                item.Status = status;
                item.CreateAt = DateTime.Now;
                dataGridViewT1.Refresh(); // BindingList tự động notify nhưng Refresh để format
                FormatGridT1(); // Đổi màu hàng
            }
        }
        private void dataGridViewT1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (dataGridViewT1.Rows[e.RowIndex].DataBoundItem is Tran1Model item)
            {
                // Nếu cột đang format là cột Status
                if (dataGridViewT1.Columns[e.ColumnIndex].DataPropertyName == "Status")
                {
                    if (item.Status == "1")
                    {
                        // Tô màu cả hàng
                        dataGridViewT1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                        dataGridViewT1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        // Trả lại màu mặc định nếu không phải "1"
                        dataGridViewT1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                        dataGridViewT1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
                // ✅ Đặt header tiếng Việt
                if (dataGridViewT1.Columns.Contains("Id"))
                    dataGridViewT1.Columns["Id"].HeaderText = "STT";

                if (dataGridViewT1.Columns.Contains("Device"))
                    dataGridViewT1.Columns["Device"].HeaderText = "Thiết Bị";

                if (dataGridViewT1.Columns.Contains("Status"))
                    dataGridViewT1.Columns["Status"].HeaderText = "Trạng Thái";

                if (dataGridViewT1.Columns.Contains("CreateAt"))
                    dataGridViewT1.Columns["CreateAt"].HeaderText = "Thời Gian";
            }
        }
        // Kết thúc Tràn 1
        // Tràn 2
        private void LoadAllTags2()
        {
            if (GlobalData.Tran2List.Count == 0)
            {
                int stt = 1;
                GlobalData.Tran2List.Add(new Tran2Model { Id = stt++, Device = "Bơm 1 Đang Chạy", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran2List.Add(new Tran2Model { Id = stt++, Device = "Bơm 2 Đang Chạy", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran2List.Add(new Tran2Model { Id = stt++, Device = "Bơm 3 Đang Chạy", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran2List.Add(new Tran2Model { Id = stt++, Device = "Cửa 2 Mở Hoàn Toàn", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran2List.Add(new Tran2Model { Id = stt++, Device = "Cửa 2 Đóng Hoàn Toàn", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran2List.Add(new Tran2Model { Id = stt++, Device = "Cửa 2 Đang Mở", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran2List.Add(new Tran2Model { Id = stt++, Device = "Cửa 2 Đang Đóng", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran2List.Add(new Tran2Model { Id = stt++, Device = "Chốt 2_1 Mở Hết", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran2List.Add(new Tran2Model { Id = stt++, Device = "Chốt 2_1 Đóng Hết", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran2List.Add(new Tran2Model { Id = stt++, Device = "Chốt 2_2 Mở Hết", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran2List.Add(new Tran2Model { Id = stt++, Device = "Chốt 2_2 Đóng Hết", Status = "0", CreateAt = DateTime.Now });

                // Thêm các Tag khác
            }
            tran2List = GlobalData.Tran2List;

            if (dataGridViewT2.DataSource == null)
                dataGridViewT2.DataSource = tran2List;

            FormatGridT2();
        }
        private void ReadAllTagStatus2()
        {
            if (_mainForm == null)
            {
                MessageBox.Show("_mainForm is null");
                return;
            }
            string pump1 = _mainForm.GetS1_DC1_RunningValue();
            string pump2 = _mainForm.GetS1_DC2_RunningValue();
            string pump3 = _mainForm.GetS1_DC3_RunningValue();
            string door2Open = _mainForm.GetDoor2_OpenValue();
            string door2Close = _mainForm.GetDoor2_CloseValue();
            string door2Opening = _mainForm.GetDoor2_OpeningValue();
            string door2Closing = _mainForm.GetDoor2_ClosingValue();
            string doorlock2_1Open = _mainForm.GetDoorlock2_1OpenValue();
            string doorlock2_1Close = _mainForm.GetDoorlock2_1CloseValue();
            string doorlock2_2Open = _mainForm.GetDoorlock2_2OpenValue();
            string doorlock2_2Close = _mainForm.GetDoorlock2_2CloseValue();

            UpdateTagValue2("Bơm 1 Đang Chạy", pump1 == "1" ? "1" : "0");
            UpdateTagValue2("Bơm 2 Đang Chạy", pump2 == "1" ? "1" : "0");
            UpdateTagValue2("Bơm 3 Đang Chạy", pump3 == "1" ? "1" : "0");
            UpdateTagValue2("Cửa 2 Mở Hoàn Toàn", door2Open == "1" ? "1" : "0");
            UpdateTagValue2("Cửa 2 Đóng Hoàn Toàn", door2Close == "1" ? "1" : "0");
            UpdateTagValue2("Cửa 2 Đang Mở", door2Opening == "1" ? "1" : "0");
            UpdateTagValue2("Cửa 2 Đang Đóng", door2Closing == "1" ? "1" : "0");
            UpdateTagValue2("Chốt 2_1 Mở Hết", doorlock2_1Open == "1" ? "1" : "0");
            UpdateTagValue2("Chốt 2_1 Đóng Hết", doorlock2_1Close == "1" ? "1" : "0");
            UpdateTagValue2("Chốt 2_2 Mở Hết", doorlock2_2Open == "1" ? "1" : "0");
            UpdateTagValue2("Chốt 2_2 Đóng Hết", doorlock2_2Close == "1" ? "1" : "0");

            // Thêm các tag khác tương tự
        }
        private void dataGridViewT2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 &&
                dataGridViewT2.Rows[e.RowIndex].DataBoundItem is Tran2Model item)
            {
                if (dataGridViewT2.Columns[e.ColumnIndex].DataPropertyName == "Status")
                {
                    if (item.Status == "1")
                    {
                        dataGridViewT2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                        dataGridViewT2.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        dataGridViewT2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                        dataGridViewT2.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
                // ✅ Đặt header tiếng Việt
                if (dataGridViewT2.Columns.Contains("Id"))
                    dataGridViewT2.Columns["Id"].HeaderText = "STT";

                if (dataGridViewT2.Columns.Contains("Device"))
                    dataGridViewT2.Columns["Device"].HeaderText = "Thiết Bị";

                if (dataGridViewT2.Columns.Contains("Status"))
                    dataGridViewT2.Columns["Status"].HeaderText = "Trạng Thái";

                if (dataGridViewT2.Columns.Contains("CreateAt"))
                    dataGridViewT2.Columns["CreateAt"].HeaderText = "Thời Gian";
            }
        }
        private void UpdateTagValue2(string device, string status)
        {
            if (dataGridViewT2.InvokeRequired)
            {
                dataGridViewT2.Invoke(new Action(() => { UpdateTagValue2(device, status); }));
                return;
            }

            var item = tran2List.FirstOrDefault(x => x.Device == device);
            if (item != null)
            {
                item.Status = status;
                item.CreateAt = DateTime.Now;

                int index = tran2List.IndexOf(item);
                if (index >= 0)
                {
                    dataGridViewT2.InvalidateRow(index); // Force redraw row formatting
                }
            }
        }
        private void Door2_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue2("Cửa 2 Đang Mở", newValue);
                // ✅ Update hình ảnh
                Pic_Door2_Opening.Visible = (newValue == "1");
                Pic_Door2_Opening_Stop.Visible = (newValue != "1");
            });
        }
        private void Door2_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue2("Cửa 2 Đang Đóng", newValue);
                // ✅ Update hình ảnh
                Pic_Door2_Closing.Visible = (newValue == "1");
                Pic_Door2_Closing_Stop.Visible = (newValue != "1");
            });
        }

        private void Door2_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue2("Cửa 2 Mở Hoàn Toàn", newValue);
                // ✅ Update hình ảnh
                Pic_Door2_Open.Visible = (newValue == "1");
                Pic_Door2_Open_Stop.Visible = (newValue != "1");
            });
        }
        private void Door2_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue2("Cửa 2 Đóng Hoàn Toàn", newValue);
                // ✅ Update hình ảnh
                Pic_Door2_Close.Visible = (newValue == "1");
                Pic_Door2_Close_Stop.Visible = (newValue != "1");
            });
        }
        private void Doorlock2_2Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue2("Chốt 2_2 Mở Hết", newValue);
                // ✅ Update hình ảnh
                Pic_Doorlock2_2Open.Visible = (newValue == "1");
                Pic_Doorlock2_2Open_Stop.Visible = (newValue != "1");
            });
        }
        private void Doorlock2_2Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue2("Chốt 2_2 Đóng Hết", newValue);
                // ✅ Update hình ảnh
                Pic_Doorlock2_2Close.Visible = (newValue == "1");
                Pic_Doorlock2_2Close_Stop.Visible = (newValue != "1");
            });
        }
        private void Doorlock2_1Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue2("Chốt 2_1 Mở Hết", newValue);
                // ✅ Update hình ảnh
                Pic_Doorlock2_1Open.Visible = (newValue == "1");
                Pic_Doorlock2_1Open_Stop.Visible = (newValue != "1");
            });
        }
        private void Doorlock2_1Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue2("Chốt 2_1 Đóng Hết", newValue);
                // ✅ Update hình ảnh
                Pic_Doorlock2_1Close.Visible = (newValue == "1");
                Pic_Doorlock2_1Close_Stop.Visible = (newValue != "1");
            });
        }
        // Kết thúc Tràn 2
        // Tràn 3
        private void LoadAllTags3()
        {
            if (GlobalData.Tran3List.Count == 0)
            {
                int stt = 1;
                GlobalData.Tran3List.Add(new Tran3Model { Id = stt++, Device = "Bơm 1 Đang Chạy", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran3List.Add(new Tran3Model { Id = stt++, Device = "Bơm 2 Đang Chạy", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran3List.Add(new Tran3Model { Id = stt++, Device = "Bơm 3 Đang Chạy", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran3List.Add(new Tran3Model { Id = stt++, Device = "Cửa 3 Mở Hoàn Toàn", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran3List.Add(new Tran3Model { Id = stt++, Device = "Cửa 3 Đóng Hoàn Toàn", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran3List.Add(new Tran3Model { Id = stt++, Device = "Cửa 3 Đang Mở", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran3List.Add(new Tran3Model { Id = stt++, Device = "Cửa 3 Đang Đóng", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran3List.Add(new Tran3Model { Id = stt++, Device = "Chốt 3_1 Mở Hết", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran3List.Add(new Tran3Model { Id = stt++, Device = "Chốt 3_1 Đóng Hết", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran3List.Add(new Tran3Model { Id = stt++, Device = "Chốt 3_2 Mở Hết", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran3List.Add(new Tran3Model { Id = stt++, Device = "Chốt 3_2 Đóng Hết", Status = "0", CreateAt = DateTime.Now });



                // Thêm các Tag khác
            }
            tran3List = GlobalData.Tran3List;

            if (dataGridViewT3.DataSource == null)
                dataGridViewT3.DataSource = tran3List;

            FormatGridT3();
        }

        private void ReadAllTagStatus3()
        {
            if (_mainForm == null)
            {
                MessageBox.Show("_mainForm is null");
                return;
            }
            string pump1 = _mainForm.GetS2_DC1_RunningValue();
            string pump2 = _mainForm.GetS2_DC2_RunningValue();
            string pump3 = _mainForm.GetS2_DC3_RunningValue();
            string door3Open = _mainForm.GetDoor3_OpenValue();
            string door3Close = _mainForm.GetDoor3_CloseValue();
            string door3Opening = _mainForm.GetDoor3_OpeningValue();
            string door3Closing = _mainForm.GetDoor3_ClosingValue();
            string doorlock3_1Open = _mainForm.GetDoorlock3_1OpenValue();
            string doorlock3_1Close = _mainForm.GetDoorlock3_1CloseValue();
            string doorlock3_2Open = _mainForm.GetDoorlock3_2OpenValue();
            string doorlock3_2Close = _mainForm.GetDoorlock3_2CloseValue();

            UpdateTagValue3("Bơm 1 Đang Chạy", pump1 == "1" ? "1" : "0");
            UpdateTagValue3("Bơm 2 Đang Chạy", pump2 == "1" ? "1" : "0");
            UpdateTagValue3("Bơm 3 Đang Chạy", pump3 == "1" ? "1" : "0");
            UpdateTagValue3("Cửa 3 Mở Hoàn Toàn", door3Open == "1" ? "1" : "0");
            UpdateTagValue3("Cửa 3 Đóng Hoàn Toàn", door3Close == "1" ? "1" : "0");
            UpdateTagValue3("Cửa 3 Đang Mở", door3Opening == "1" ? "1" : "0");
            UpdateTagValue3("Cửa 3 Đang Đóng", door3Closing == "1" ? "1" : "0");
            UpdateTagValue3("Chốt 3_1 Mở Hết", doorlock3_1Open == "1" ? "1" : "0");
            UpdateTagValue3("Chốt 3_1 Đóng Hết", doorlock3_1Close == "1" ? "1" : "0");
            UpdateTagValue3("Chốt 3_2 Mở Hết", doorlock3_2Open == "1" ? "1" : "0");
            UpdateTagValue3("Chốt 3_2 Đóng Hết", doorlock3_2Close == "1" ? "1" : "0");

            // Thêm các tag khác tương tự
        }
        private void dataGridViewT3_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 &&
                dataGridViewT3.Rows[e.RowIndex].DataBoundItem is Tran3Model item)
            {
                if (dataGridViewT3.Columns[e.ColumnIndex].DataPropertyName == "Status")
                {
                    if (item.Status == "1")
                    {
                        dataGridViewT3.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                        dataGridViewT3.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        dataGridViewT3.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                        dataGridViewT3.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
                // ✅ Đặt header tiếng Việt
                if (dataGridViewT3.Columns.Contains("Id"))
                    dataGridViewT3.Columns["Id"].HeaderText = "STT";

                if (dataGridViewT3.Columns.Contains("Device"))
                    dataGridViewT3.Columns["Device"].HeaderText = "Thiết Bị";

                if (dataGridViewT3.Columns.Contains("Status"))
                    dataGridViewT3.Columns["Status"].HeaderText = "Trạng Thái";

                if (dataGridViewT3.Columns.Contains("CreateAt"))
                    dataGridViewT3.Columns["CreateAt"].HeaderText = "Thời Gian";
            }
        }
        private void UpdateTagValue3(string device, string status)
        {
            if (dataGridViewT3.InvokeRequired)
            {
                dataGridViewT3.Invoke(new Action(() => { UpdateTagValue3(device, status); }));
                return;
            }

            var item = tran3List.FirstOrDefault(x => x.Device == device);
            if (item != null)
            {
                item.Status = status;
                item.CreateAt = DateTime.Now;

                int index = tran3List.IndexOf(item);
                if (index >= 0)
                {
                    dataGridViewT3.InvalidateRow(index); // Force redraw row formatting
                }
            }
        }
        private void S2_DC1_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
               string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue3("Bơm 1 Đang Chạy", newValue);
                UpdateTagValue4("Bơm 1 Đang Chạy", newValue);
                // ✅ Update hình ảnh
                Pic_S2_DC1_Running.Visible = (newValue == "1");
               PicT4_S2_DC1_Running.Visible = (newValue == "1");
                Pic_S2_DC1_Stop.Visible = (newValue != "1");
                PicT4_S2_DC1_Stop.Visible = (newValue != "1");
            });
        }
        private void S2_DC2_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue3("Bơm 2 Đang Chạy", newValue);
                UpdateTagValue4("Bơm 2 Đang Chạy", newValue);
                // ✅ Update hình ảnh
                Pic_S2_DC2_Running.Visible = (newValue == "1");
                PicT4_S2_DC2_Running.Visible = (newValue == "1");
                Pic_S2_DC2_Stop.Visible = (newValue != "1");
                PicT4_S2_DC2_Stop.Visible = (newValue != "1");
            });
        }
        private void S2_DC3_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue3("Bơm 3 Đang Chạy", newValue);
                UpdateTagValue4("Bơm 3 Đang Chạy", newValue);
                // ✅ Update hình ảnh
                Pic_S2_DC3_Running.Visible = (newValue == "1");
                PicT4_S2_DC3_Running.Visible = (newValue == "1");
                Pic_S2_DC3_Stop.Visible = (newValue != "1");
                PicT4_S2_DC3_Stop.Visible = (newValue != "1");
            });
        }
        private void Door3_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue3("Cửa 3 Đang Mở", newValue);
                // ✅ Update hình ảnh
                Pic_Door3_Opening.Visible = (newValue == "1");
                Pic_Door3_Opening_Stop.Visible = (newValue != "1");
            });
        }
        private void Door3_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue3("Cửa 3 Đang Đóng", newValue);
                // ✅ Update hình ảnh
                Pic_Door3_Closing.Visible = (newValue == "1");
                Pic_Door3_Closing_Stop.Visible = (newValue != "1");
            });
        }
        private void Door3_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue3("Cửa 3 Mở Hoàn Toàn", newValue);
                // ✅ Update hình ảnh
                Pic_Door3_Open.Visible = (newValue == "1");
                Pic_Door3_Open_Stop.Visible = (newValue != "1");
            });
        }
        private void Door3_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue3("Cửa 3 Đóng Hoàn Toàn", newValue);
                // ✅ Update hình ảnh
                Pic_Door3_Close.Visible = (newValue == "1");
                Pic_Door3_Close_Stop.Visible = (newValue != "1");
            });
        }

        private void Doorlock3_2Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue3("Chốt 3_2 Mở Hết", newValue);
                // ✅ Update hình ảnh
                Pic_Doorlock3_2Open.Visible = (newValue == "1");
                Pic_Doorlock3_2Open_Stop.Visible = (newValue != "1");
            });
        }
        private void Doorlock3_2Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue3("Chốt 3_2 Đóng Hết", newValue);
                // ✅ Update hình ảnh
                Pic_Doorlock3_2Close.Visible = (newValue == "1");
                Pic_Doorlock3_2Close_Stop.Visible = (newValue != "1");
            });
        }
        private void Doorlock3_1Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue3("Chốt 3_1 Mở Hết", newValue);
                // ✅ Update hình ảnh
                Pic_Doorlock3_1Open.Visible = (newValue == "1");
                Pic_Doorlock3_1Open_Stop.Visible = (newValue != "1");
            });
        }
        private void Doorlock3_1Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue3("Chốt 3_1 Đóng Hết", newValue);
                // ✅ Update hình ảnh
                Pic_Doorlock3_1Close.Visible = (newValue == "1");
                Pic_Doorlock3_1Close_Stop.Visible = (newValue != "1");
            });
        }
        // Kết thúc Tràn 3
        // Tràn 4
        private void LoadAllTags4()
        {
            if (GlobalData.Tran4List.Count == 0)
            {
                int stt = 1;
                GlobalData.Tran4List.Add(new Tran4Model { Id = stt++, Device = "Bơm 1 Đang Chạy", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran4List.Add(new Tran4Model { Id = stt++, Device = "Bơm 2 Đang Chạy", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran4List.Add(new Tran4Model { Id = stt++, Device = "Bơm 3 Đang Chạy", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran4List.Add(new Tran4Model { Id = stt++, Device = "Cửa 4 Mở Hoàn Toàn", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran4List.Add(new Tran4Model { Id = stt++, Device = "Cửa 4 Đóng Hoàn Toàn", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran4List.Add(new Tran4Model { Id = stt++, Device = "Cửa 4 Đang Mở", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran4List.Add(new Tran4Model { Id = stt++, Device = "Cửa 4 Đang Đóng", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran4List.Add(new Tran4Model { Id = stt++, Device = "Chốt 4_1 Mở Hết", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran4List.Add(new Tran4Model { Id = stt++, Device = "Chốt 4_1 Đóng Hết", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran4List.Add(new Tran4Model { Id = stt++, Device = "Chốt 4_2 Mở Hết", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran4List.Add(new Tran4Model { Id = stt++, Device = "Chốt 4_2 Đóng Hết", Status = "0", CreateAt = DateTime.Now });



                // Thêm các Tag khác
            }
            tran4List = GlobalData.Tran4List;

            if (dataGridViewT4.DataSource == null)
                dataGridViewT4.DataSource = tran4List;

            FormatGridT4();
        }

        private void ReadAllTagStatus4()
        {
            if (_mainForm == null)
            {
                MessageBox.Show("_mainForm is null");
                return;
            }
            string pump1 = _mainForm.GetS2_DC1_RunningValue();
            string pump2 = _mainForm.GetS2_DC2_RunningValue();
            string pump3 = _mainForm.GetS2_DC3_RunningValue();
            string door4Open = _mainForm.GetDoor4_OpenValue();
            string door4Close = _mainForm.GetDoor4_CloseValue();
            string door4Opening = _mainForm.GetDoor4_OpeningValue();
            string door4Closing = _mainForm.GetDoor4_ClosingValue();
            string doorlock4_1Open = _mainForm.GetDoorlock4_1OpenValue();
            string doorlock4_1Close = _mainForm.GetDoorlock4_1CloseValue();
            string doorlock4_2Open = _mainForm.GetDoorlock4_2OpenValue();
            string doorlock4_2Close = _mainForm.GetDoorlock4_2CloseValue();

            UpdateTagValue4("Bơm 1 Đang Chạy", pump1 == "1" ? "1" : "0");
            UpdateTagValue4("Bơm 2 Đang Chạy", pump2 == "1" ? "1" : "0");
            UpdateTagValue4("Bơm 3 Đang Chạy", pump3 == "1" ? "1" : "0");
            UpdateTagValue4("Cửa 4 Mở Hoàn Toàn", door4Open == "1" ? "1" : "0");
            UpdateTagValue4("Cửa 4 Đóng Hoàn Toàn", door4Close == "1" ? "1" : "0");
            UpdateTagValue4("Cửa 4 Đang Mở", door4Opening == "1" ? "1" : "0");
            UpdateTagValue4("Cửa 4 Đang Đóng", door4Closing == "1" ? "1" : "0");
            UpdateTagValue4("Chốt 4_1 Mở Hết", doorlock4_1Open == "1" ? "1" : "0");
            UpdateTagValue4("Chốt 4_1 Đóng Hết", doorlock4_1Close == "1" ? "1" : "0");
            UpdateTagValue4("Chốt 4_2 Mở Hết", doorlock4_2Open == "1" ? "1" : "0");
            UpdateTagValue4("Chốt 4_2 Đóng Hết", doorlock4_2Close == "1" ? "1" : "0");

            // Thêm các tag khác tương tự
        }
        private void dataGridViewT4_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 &&
                dataGridViewT4.Rows[e.RowIndex].DataBoundItem is Tran4Model item)
            {
                if (dataGridViewT4.Columns[e.ColumnIndex].DataPropertyName == "Status")
                {
                    if (item.Status == "1")
                    {
                        dataGridViewT4.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                        dataGridViewT4.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        dataGridViewT4.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                        dataGridViewT4.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
                // ✅ Đặt header tiếng Việt
                if (dataGridViewT4.Columns.Contains("Id"))
                    dataGridViewT4.Columns["Id"].HeaderText = "STT";

                if (dataGridViewT4.Columns.Contains("Device"))
                    dataGridViewT4.Columns["Device"].HeaderText = "Thiết Bị";

                if (dataGridViewT4.Columns.Contains("Status"))
                    dataGridViewT4.Columns["Status"].HeaderText = "Trạng Thái";

                if (dataGridViewT4.Columns.Contains("CreateAt"))
                    dataGridViewT4.Columns["CreateAt"].HeaderText = "Thời Gian";
            }
        }
        private void UpdateTagValue4(string device, string status)
        {
            if (dataGridViewT4.InvokeRequired)
            {
                dataGridViewT4.Invoke(new Action(() => { UpdateTagValue4(device, status); }));
                return;
            }

            var item = tran4List.FirstOrDefault(x => x.Device == device);
            if (item != null)
            {
                item.Status = status;
                item.CreateAt = DateTime.Now;

                int index = tran4List.IndexOf(item);
                if (index >= 0)
                {
                    dataGridViewT4.InvalidateRow(index); // Force redraw row formatting
                }
            }
        }
        private void Door4_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue4("Cửa 4 Đang Mở", newValue);
                // ✅ Update hình ảnh
                Pic_Door4_Opening.Visible = (newValue == "1");
                Pic_Door4_Opening_Stop.Visible = (newValue != "1");
            });
        }
        private void Door4_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue4("Cửa 4 Đang Đóng", newValue);
                // ✅ Update hình ảnh
                Pic_Door4_Closing.Visible = (newValue == "1");
                Pic_Door4_Closing_Stop.Visible = (newValue != "1");
            });
        }
        private void Door4_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue4("Cửa 4 Mở Hoàn Toàn", newValue);
                // ✅ Update hình ảnh
                Pic_Door4_Open.Visible = (newValue == "1");
                Pic_Door4_Open_Stop.Visible = (newValue != "1");
            });
        }
        private void Door4_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue4("Cửa 4 Đóng Hoàn Toàn", newValue);
                // ✅ Update hình ảnh
                Pic_Door4_Close.Visible = (newValue == "1");
                Pic_Door4_Close_Stop.Visible = (newValue != "1");
            });
        }

        private void Doorlock4_2Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue4("Chốt 4_2 Mở Hết", newValue);
                // ✅ Update hình ảnh
                Pic_Doorlock4_2Open.Visible = (newValue == "1");
                Pic_Doorlock4_2Open_Stop.Visible = (newValue != "1");
            });
        }
        private void Doorlock4_2Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue4("Chốt 4_2 Đóng Hết", newValue);
                // ✅ Update hình ảnh
                Pic_Doorlock4_2Close.Visible = (newValue == "1");
                Pic_Doorlock4_2Close_Stop.Visible = (newValue != "1");
            });
        }
        private void Doorlock4_1Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue4("Chốt 4_1 Mở Hết", newValue);
                // ✅ Update hình ảnh
                Pic_Doorlock4_1Open.Visible = (newValue == "1");
                Pic_Doorlock4_1Open_Stop.Visible = (newValue != "1");
            });
        }
        private void Doorlock4_1Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue4("Chốt 4_1 Đóng Hết", newValue);
                // ✅ Update hình ảnh
                Pic_Doorlock4_1Close.Visible = (newValue == "1");
                Pic_Doorlock4_1Close_Stop.Visible = (newValue != "1");
            });
        }
        // Kết thúc Tràn 4
        // Tràn 5
        private void LoadAllTags5()
        {
            if (GlobalData.Tran5List.Count == 0)
            {
                int stt = 1;
                GlobalData.Tran5List.Add(new Tran5Model { Id = stt++, Device = "Bơm 1 Đang Chạy", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran5List.Add(new Tran5Model { Id = stt++, Device = "Bơm 2 Đang Chạy", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran5List.Add(new Tran5Model { Id = stt++, Device = "Bơm 3 Đang Chạy", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran5List.Add(new Tran5Model { Id = stt++, Device = "Cửa 5 Mở Hoàn Toàn", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran5List.Add(new Tran5Model { Id = stt++, Device = "Cửa 5 Đóng Hoàn Toàn", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran5List.Add(new Tran5Model { Id = stt++, Device = "Cửa 5 Đang Mở", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran5List.Add(new Tran5Model { Id = stt++, Device = "Cửa 5 Đang Đóng", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran5List.Add(new Tran5Model { Id = stt++, Device = "Chốt 5_1 Mở Hết", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran5List.Add(new Tran5Model { Id = stt++, Device = "Chốt 5_1 Đóng Hết", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran5List.Add(new Tran5Model { Id = stt++, Device = "Chốt 5_2 Mở Hết", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran5List.Add(new Tran5Model { Id = stt++, Device = "Chốt 5_2 Đóng Hết", Status = "0", CreateAt = DateTime.Now });



                // Thêm các Tag khác
            }
            tran5List = GlobalData.Tran5List;

            if (dataGridViewT5.DataSource == null)
                dataGridViewT5.DataSource = tran5List;

            FormatGridT5();
        }

        private void ReadAllTagStatus5()
        {
            if (_mainForm == null)
            {
                MessageBox.Show("_mainForm is null");
                return;
            }
            string pump1 = _mainForm.GetS3_DC1_RunningValue();
            string pump2 = _mainForm.GetS3_DC2_RunningValue();
            string pump3 = _mainForm.GetS3_DC3_RunningValue();
            string door5Open = _mainForm.GetDoor5_OpenValue();
            string door5Close = _mainForm.GetDoor5_CloseValue();
            string door5Opening = _mainForm.GetDoor5_OpeningValue();
            string door5Closing = _mainForm.GetDoor5_ClosingValue();
            string doorlock5_1Open = _mainForm.GetDoorlock5_1OpenValue();
            string doorlock5_1Close = _mainForm.GetDoorlock5_1CloseValue();
            string doorlock5_2Open = _mainForm.GetDoorlock5_2OpenValue();
            string doorlock5_2Close = _mainForm.GetDoorlock5_2CloseValue();

            UpdateTagValue5("Bơm 1 Đang Chạy", pump1 == "1" ? "1" : "0");
            UpdateTagValue5("Bơm 2 Đang Chạy", pump2 == "1" ? "1" : "0");
            UpdateTagValue5("Bơm 3 Đang Chạy", pump3 == "1" ? "1" : "0");
            UpdateTagValue5("Cửa 5 Mở Hoàn Toàn", door5Open == "1" ? "1" : "0");
            UpdateTagValue5("Cửa 5 Đóng Hoàn Toàn", door5Close == "1" ? "1" : "0");
            UpdateTagValue5("Cửa 5 Đang Mở", door5Opening == "1" ? "1" : "0");
            UpdateTagValue5("Cửa 5 Đang Đóng", door5Closing == "1" ? "1" : "0");
            UpdateTagValue5("Chốt 5_1 Mở Hết", doorlock5_1Open == "1" ? "1" : "0");
            UpdateTagValue5("Chốt 5_1 Đóng Hết", doorlock5_1Close == "1" ? "1" : "0");
            UpdateTagValue5("Chốt 5_2 Mở Hết", doorlock5_2Open == "1" ? "1" : "0");
            UpdateTagValue5("Chốt 5_2 Đóng Hết", doorlock5_2Close == "1" ? "1" : "0");

            // Thêm các tag khác tương tự
        }
        private void dataGridViewT5_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 &&
                dataGridViewT5.Rows[e.RowIndex].DataBoundItem is Tran5Model item)
            {
                if (dataGridViewT5.Columns[e.ColumnIndex].DataPropertyName == "Status")
                {
                    if (item.Status == "1")
                    {
                        dataGridViewT5.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                        dataGridViewT5.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        dataGridViewT5.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                        dataGridViewT5.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
                // ✅ Đặt header tiếng Việt
                if (dataGridViewT5.Columns.Contains("Id"))
                    dataGridViewT5.Columns["Id"].HeaderText = "STT";

                if (dataGridViewT5.Columns.Contains("Device"))
                    dataGridViewT5.Columns["Device"].HeaderText = "Thiết Bị";

                if (dataGridViewT5.Columns.Contains("Status"))
                    dataGridViewT5.Columns["Status"].HeaderText = "Trạng Thái";

                if (dataGridViewT5.Columns.Contains("CreateAt"))
                    dataGridViewT5.Columns["CreateAt"].HeaderText = "Thời Gian";
            }
        }
        private void UpdateTagValue5(string device, string status)
        {
            if (dataGridViewT5.InvokeRequired)
            {
                dataGridViewT5.Invoke(new Action(() => { UpdateTagValue5(device, status); }));
                return;
            }

            var item = tran5List.FirstOrDefault(x => x.Device == device);
            if (item != null)
            {
                item.Status = status;
                item.CreateAt = DateTime.Now;

                int index = tran5List.IndexOf(item);
                if (index >= 0)
                {
                    dataGridViewT5.InvalidateRow(index); // Force redraw row formatting
                }
            }
        }
        private void S3_DC3_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {

            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue5("Bơm 3 Đang Chạy", newValue);
                UpdateTagValue6("Bơm 3 Đang Chạy", newValue);
                // ✅ Update hình ảnh
                Pic_S3_DC3_Running.Visible = (newValue == "1");
                PicT6_S3_DC3_Running.Visible = (newValue == "1");
                Pic_S3_DC3_Stop.Visible = (newValue != "1");
                PicT6_S3_DC3_Stop.Visible = (newValue != "1");
            });
        }
        private void S3_DC2_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {

            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue5("Bơm 2 Đang Chạy", newValue);
                UpdateTagValue6("Bơm 2 Đang Chạy", newValue);
                // ✅ Update hình ảnh
                Pic_S3_DC2_Running.Visible = (newValue == "1");
                PicT6_S3_DC2_Running.Visible = (newValue == "1");
                Pic_S3_DC2_Stop.Visible = (newValue != "1");
                PicT6_S3_DC2_Stop.Visible = (newValue != "1");
            });
        }
        private void S3_DC1_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {

            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue5("Bơm 1 Đang Chạy", newValue);
               UpdateTagValue6("Bơm 1 Đang Chạy", newValue);
                // ✅ Update hình ảnh
                Pic_S3_DC1_Running.Visible = (newValue == "1");
                PicT6_S3_DC1_Running.Visible = (newValue == "1");
                Pic_S3_DC1_Stop.Visible = (newValue != "1");
                PicT6_S3_DC1_Stop.Visible = (newValue != "1");
            });
        }
        private void Door5_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue5("Cửa 5 Đang Mở", newValue);
                // ✅ Update hình ảnh
                Pic_Door5_Opening.Visible = (newValue == "1");
                Pic_Door5_Opening_Stop.Visible = (newValue != "1");
            });
        }
        private void Door5_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue5("Cửa 5 Đang Đóng", newValue);
                // ✅ Update hình ảnh
                Pic_Door5_Closing.Visible = (newValue == "1");
                Pic_Door5_Closing_Stop.Visible = (newValue != "1");
            });
        }
        private void Door5_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue5("Cửa 5 Mở Hoàn Toàn", newValue);
                // ✅ Update hình ảnh
                Pic_Door5_Open.Visible = (newValue == "1");
                Pic_Door5_Open_Stop.Visible = (newValue != "1");
            });
        }
        private void Door5_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue5("Cửa 5 Đóng Hoàn Toàn", newValue);
                // ✅ Update hình ảnh
                Pic_Door5_Close.Visible = (newValue == "1");
                Pic_Door5_Close_Stop.Visible = (newValue != "1");
            });
        }

        private void Doorlock5_2Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue5("Chốt 5_2 Mở Hết", newValue);
                // ✅ Update hình ảnh
                Pic_Doorlock5_2Open.Visible = (newValue == "1");
                Pic_Doorlock5_2Open_Stop.Visible = (newValue != "1");
            });
        }
        private void Doorlock5_2Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue5("Chốt 5_2 Đóng Hết", newValue);
                // ✅ Update hình ảnh
                Pic_Doorlock5_2Close.Visible = (newValue == "1");
                Pic_Doorlock5_2Close_Stop.Visible = (newValue != "1");
            });
        }
        private void Doorlock5_1Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue5("Chốt 5_1 Mở Hết", newValue);
                // ✅ Update hình ảnh
                Pic_Doorlock5_1Open.Visible = (newValue == "1");
                Pic_Doorlock5_1Open_Stop.Visible = (newValue != "1");
            });
        }
        private void Doorlock5_1Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue5("Chốt 5_1 Đóng Hết", newValue);
                // ✅ Update hình ảnh
                Pic_Doorlock5_1Close.Visible = (newValue == "1");
                Pic_Doorlock5_1Close_Stop.Visible = (newValue != "1");
            });
        }
        // Kết thúc Tràn 5
        // Tràn 6
        private void LoadAllTags6()
        {
            if (GlobalData.Tran6List.Count == 0)
            {
                int stt = 1;
                GlobalData.Tran6List.Add(new Tran6Model { Id = stt++, Device = "Bơm 1 Đang Chạy", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran6List.Add(new Tran6Model { Id = stt++, Device = "Bơm 2 Đang Chạy", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran6List.Add(new Tran6Model { Id = stt++, Device = "Bơm 3 Đang Chạy", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran6List.Add(new Tran6Model { Id = stt++, Device = "Cửa 6 Mở Hoàn Toàn", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran6List.Add(new Tran6Model { Id = stt++, Device = "Cửa 6 Đóng Hoàn Toàn", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran6List.Add(new Tran6Model { Id = stt++, Device = "Cửa 6 Đang Mở", Status = "0", CreateAt = DateTime.Now });
                GlobalData.Tran6List.Add(new Tran6Model { Id = stt++, Device = "Cửa 6 Đang Đóng", Status = "0", CreateAt = DateTime.Now });
                



                // Thêm các Tag khác
            }
            tran6List = GlobalData.Tran6List;

            if (dataGridViewT6.DataSource == null)
                dataGridViewT6.DataSource = tran6List;

            FormatGridT6();
        }

        private void ReadAllTagStatus6()
        {
            if (_mainForm == null)
            {
                MessageBox.Show("_mainForm is null");
                return;
            }
            string pump1 = _mainForm.GetS3_DC1_RunningValue();
            string pump2 = _mainForm.GetS3_DC2_RunningValue();
            string pump3 = _mainForm.GetS3_DC3_RunningValue();
            string door6Open = _mainForm.GetDoor6_OpenValue();
            string door6Close = _mainForm.GetDoor6_CloseValue();
            string door6Opening = _mainForm.GetDoor6_OpeningValue();
            string door6Closing = _mainForm.GetDoor6_ClosingValue();
            

            UpdateTagValue6("Bơm 1 Đang Chạy", pump1 == "1" ? "1" : "0");
            UpdateTagValue6("Bơm 2 Đang Chạy", pump2 == "1" ? "1" : "0");
            UpdateTagValue6("Bơm 3 Đang Chạy", pump3 == "1" ? "1" : "0");
            UpdateTagValue6("Cửa 6 Mở Hoàn Toàn", door6Open == "1" ? "1" : "0");
            UpdateTagValue6("Cửa 6 Đóng Hoàn Toàn", door6Close == "1" ? "1" : "0");
            UpdateTagValue6("Cửa 6 Đang Mở", door6Opening == "1" ? "1" : "0");
            UpdateTagValue6("Cửa 6 Đang Đóng", door6Closing == "1" ? "1" : "0");
            

            // Thêm các tag khác tương tự
        }
        private void dataGridViewT6_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 &&
                dataGridViewT6.Rows[e.RowIndex].DataBoundItem is Tran6Model item)
            {
                if (dataGridViewT6.Columns[e.ColumnIndex].DataPropertyName == "Status")
                {
                    if (item.Status == "1")
                    {
                        dataGridViewT6.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                        dataGridViewT6.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        dataGridViewT6.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                        dataGridViewT6.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
                // ✅ Đặt header tiếng Việt
                if (dataGridViewT6.Columns.Contains("Id"))
                    dataGridViewT6.Columns["Id"].HeaderText = "STT";

                if (dataGridViewT6.Columns.Contains("Device"))
                    dataGridViewT6.Columns["Device"].HeaderText = "Thiết Bị";

                if (dataGridViewT6.Columns.Contains("Status"))
                    dataGridViewT6.Columns["Status"].HeaderText = "Trạng Thái";

                if (dataGridViewT6.Columns.Contains("CreateAt"))
                    dataGridViewT6.Columns["CreateAt"].HeaderText = "Thời Gian";
            }
        }
        private void UpdateTagValue6(string device, string status)
        {
            if (dataGridViewT6.InvokeRequired)
            {
                dataGridViewT6.Invoke(new Action(() => { UpdateTagValue6(device, status); }));
                return;
            }

            var item = tran6List.FirstOrDefault(x => x.Device == device);
            if (item != null)
            {
                item.Status = status;
                item.CreateAt = DateTime.Now;

                int index = tran6List.IndexOf(item);
                if (index >= 0)
                {
                    dataGridViewT6.InvalidateRow(index); // Force redraw row formatting
                }
            }
        }
        private void Door6_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue6("Cửa 6 Đóng Hoàn Toàn", newValue);
                // ✅ Update hình ảnh
                Pic_Door6_Close.Visible = (newValue == "1");
                Pic_Door6_Close_Stop.Visible = (newValue != "1");
            });
        }
        private void Door6_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue6("Cửa 6 Mở Hoàn Toàn", newValue);
                // ✅ Update hình ảnh
                Pic_Door6_Open.Visible = (newValue == "1");
                Pic_Door6_Open_Stop.Visible = (newValue != "1");
            });
        }
        private void Door6_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue6("Cửa 6 Đang Đóng", newValue);
                // ✅ Update hình ảnh
                Pic_Door6_Closing.Visible = (newValue == "1");
                Pic_Door6_Closing_Stop.Visible = (newValue != "1");
            });
        }
        private void Door6_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            string newValue = e.NewValue == "1" ? "1" : "0";
            this.Invoke((MethodInvoker)delegate
            {
                // ✅ Update giá trị BindingList
                UpdateTagValue6("Cửa 6 Đang Mở", newValue);
                // ✅ Update hình ảnh
                Pic_Door6_Opening.Visible = (newValue == "1");
                Pic_Door6_Opening_Stop.Visible = (newValue != "1");
            });
        }

        
    }
}
