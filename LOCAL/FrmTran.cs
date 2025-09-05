using Ahd.Core;
using Ahd.Winforms.Controls;
using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
namespace RegistrationForm1

{
    public partial class FrmTran : Form
    {
        private FrmMain _mainForm;
        private BindingList<Tran1Model> _tran1List = new BindingList<Tran1Model>();
        private BindingList<Tran2Model> tran2List = new BindingList<Tran2Model>();
        private BindingList<Tran3Model> tran3List = new BindingList<Tran3Model>();
        private BindingList<Tran4Model> tran4List = new BindingList<Tran4Model>();
        private BindingList<Tran5Model> tran5List = new BindingList<Tran5Model>();
        private BindingList<Tran6Model> tran6List = new BindingList<Tran6Model>();
        private Timer _timer = new Timer();
        public FrmTran(FrmMain frmMain)
        {
            InitializeComponent();
            Load += FrmTran_Load;
            //        _mainForm = frmMain; // ✅ Gán trước khi sử dụng           
        }
        private void FrmTran_Load(object sender, EventArgs e)
        {
            _timer.Interval = 1000;
            _timer.Tick += Timer_Tick;
            _timer.Start();

            InitializeDataGridView();

        }
        private void InitializeDataGridView()
        {
            _tran1List = new BindingList<Tran1Model>();

            // Thêm một số dữ liệu ban đầu
            //_tran1List.Add(new Tran1Model { Id = 1, Device = "DC1_Running", Status = "0", CreateAt = DateTime.Now.ToString() });
            //_tran1List.Add(new Tran1Model { Id = 2, Device = "DC2_Running", Status = "0", CreateAt = DateTime.Now.ToString() });
            //_tran1List.Add(new Tran1Model { Id = 3, Device = "DC3_Running", Status = "1", CreateAt = DateTime.Now.ToString() });


            // Gán BindingList làm DataSource
            _dataGridViewT1.DataSource = _tran1List;

            // Đặt tiêu đề cột sau khi binding dữ liệu
            FormatColumnHeaders();

            // Gắn sự kiện CellFormatting
            _dataGridViewT1.CellFormatting += dataGridViewT1_CellFormatting;
        }
        private void FormatColumnHeaders()
        {
            if (_dataGridViewT1.Columns.Contains("Id"))
                _dataGridViewT1.Columns["Id"].HeaderText = "STT";

            if (_dataGridViewT1.Columns.Contains("Device"))
                _dataGridViewT1.Columns["Device"].HeaderText = "Thiết Bị";

            if (_dataGridViewT1.Columns.Contains("Status"))
                _dataGridViewT1.Columns["Status"].HeaderText = "Trạng Thái";

            if (_dataGridViewT1.Columns.Contains("CreateAt"))
                _dataGridViewT1.Columns["CreateAt"].HeaderText = "Thời Gian";
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                _timer.Enabled = false; // Tạm dừng timer trong quá trình xử lý

                if (Globalvariable.RealtimeDisplays.Count == 0)
                    return; // Nếu không có dữ liệu, thoát khỏi hàm

                using var dbContext = new ApplicationDbContext();

                var dataAlarm = dbContext.FT04s
                    .Where(x => x.IsDeleted == false)
                    .GroupBy(a => new { a.LocationId, a.StationId })
                    .Select(g => new
                    {
                        LocationId = g.Key.LocationId,
                        StationId = g.Key.StationId,
                        LocationName = g.FirstOrDefault().LocationName,
                        StationName = g.FirstOrDefault().StationName,
                        LatestEntry = g.OrderByDescending(a => a.CreateAt).FirstOrDefault(),
                        Items = g.ToList(),
                    })
                    .ToList();

                Globalvariable.InvokeIfRequired(this, () =>
                {
                    _dataGridViewT1.DataSource = null;
                     _dataGridViewT1.DataSource = dataAlarm.FirstOrDefault().Items;
                  

                    _dataGridViewT1.Refresh();

                    var dataDisplayStation1 = Globalvariable.RealtimeDisplays.FirstOrDefault()?.Stations.FirstOrDefault(x => x.Path == "Local Station/DauTieng/S71500/Station_1");

                    if (dataDisplayStation1?.DC1_Running == true)
                    {

                        Pic_S1_DC1_Running.Visible = true; PicT2_S1_DC1_Running.Visible = true; Pic_S1_DC1_Stop.Visible = false; PicT2_S1_DC1_Stop.Visible = false;



                    }
                    else { Pic_S1_DC1_Stop.Visible = true; PicT2_S1_DC1_Stop.Visible = true; Pic_S1_DC1_Running.Visible = false; PicT2_S1_DC1_Running.Visible = false; }
                    if (dataDisplayStation1?.DC2_Running == true)
                    {
                        Pic_S1_DC2_Running.Visible = true; PicT2_S1_DC2_Running.Visible = true; Pic_S1_DC2_Stop.Visible = false; PicT2_S1_DC2_Stop.Visible = false;
                    }
                    else { Pic_S1_DC2_Stop.Visible = true; PicT2_S1_DC2_Stop.Visible = true; Pic_S1_DC2_Running.Visible = false; PicT2_S1_DC2_Running.Visible = false; }
                    if (dataDisplayStation1?.DC3_Running == true)
                    {
                        Pic_S1_DC3_Running.Visible = true; PicT2_S1_DC3_Running.Visible = true; Pic_S1_DC3_Stop.Visible = false; PicT2_S1_DC3_Stop.Visible = false;
                    }
                    else { Pic_S1_DC3_Stop.Visible = true; PicT2_S1_DC3_Stop.Visible = true; Pic_S1_DC3_Running.Visible = false; PicT2_S1_DC3_Running.Visible = false; }

                    if (dataDisplayStation1?.DC1_Over == true)
                    {
                        Pic_S1_DC1_Over.Visible = true; PicT2_S1_DC1_Over.Visible = true; Pic_S1_DC1_Over_Stop.Visible = false; PicT2_S1_DC1_Over_Stop.Visible = false;
                    }
                    else { Pic_S1_DC1_Over_Stop.Visible = true; PicT2_S1_DC1_Over_Stop.Visible = true; Pic_S1_DC1_Over.Visible = false; PicT2_S1_DC1_Over.Visible = false; }
                    if (dataDisplayStation1?.DC2_Over == true)
                    {
                        Pic_S1_DC2_Over.Visible = true; PicT2_S1_DC2_Over.Visible = true; Pic_S1_DC2_Over_Stop.Visible = false; PicT2_S1_DC2_Over_Stop.Visible = false;
                    }
                    else { Pic_S1_DC2_Over_Stop.Visible = true; PicT2_S1_DC2_Over_Stop.Visible = true; Pic_S1_DC2_Over.Visible = false; PicT2_S1_DC2_Over.Visible = false; }
                    if (dataDisplayStation1?.DC3_Over == true)
                    {
                        Pic_S1_DC3_Over.Visible = true; PicT2_S1_DC3_Over.Visible = true; Pic_S1_DC3_Over_Stop.Visible = false; PicT2_S1_DC3_Over_Stop.Visible = false;
                    }
                    else { Pic_S1_DC3_Over_Stop.Visible = true; PicT2_S1_DC3_Over_Stop.Visible = true; Pic_S1_DC3_Over.Visible = false; PicT2_S1_DC3_Over.Visible = false; }

                    if (dataDisplayStation1?.Door1_Opening == true)
                    {
                        Pic_Door1_Opening.Visible = true; Pic_Door1_Opening_Stop.Visible = false;
                    }
                    else { Pic_Door1_Opening_Stop.Visible = true; Pic_Door1_Opening.Visible = false; }
                    if (dataDisplayStation1?.Door1_Closing == true)
                    {
                        Pic_Door1_Closing.Visible = true; Pic_Door1_Closing_Stop.Visible = false;
                    }
                    else { Pic_Door1_Closing_Stop.Visible = true; Pic_Door1_Closing.Visible = false; }
                    if (dataDisplayStation1?.Door2_Opening == true)
                    {
                        Pic_Door2_Opening.Visible = true; Pic_Door2_Opening_Stop.Visible = false;
                    }
                    else { Pic_Door2_Opening_Stop.Visible = true; Pic_Door2_Opening.Visible = false; }
                    if (dataDisplayStation1?.Door2_Closing == true)
                    {
                        Pic_Door2_Closing.Visible = true; Pic_Door2_Closing_Stop.Visible = false;
                    }
                    else { Pic_Door2_Closing_Stop.Visible = true; Pic_Door2_Closing.Visible = false; }
                    if (dataDisplayStation1?.Door1_Open == true)
                    {
                        Pic_Door1_Open.Visible = true; Pic_Door1_Open_Stop.Visible = false;
                    }
                    else { Pic_Door1_Open_Stop.Visible = true; Pic_Door1_Open.Visible = false; }
                    if (dataDisplayStation1?.Door1_Close == true)
                    {
                        Pic_Door1_Close.Visible = true; Pic_Door1_Close_Stop.Visible = false;
                    }
                    else { Pic_Door1_Close_Stop.Visible = true; Pic_Door1_Close.Visible = false; }
                    if (dataDisplayStation1?.Door2_Open == true)
                    {
                        Pic_Door2_Open.Visible = true; Pic_Door2_Open_Stop.Visible = false;
                    }
                    else { Pic_Door2_Open_Stop.Visible = true; Pic_Door2_Open.Visible = false; }
                    if (dataDisplayStation1?.Door2_Close == true)
                    {
                        Pic_Door2_Close.Visible = true; Pic_Door2_Close_Stop.Visible = false;
                    }
                    else { Pic_Door2_Close_Stop.Visible = true; Pic_Door2_Close.Visible = false; }
                    if (dataDisplayStation1?.Doorlock2_1Open == true)
                    {
                        Pic_Doorlock2_1Open.Visible = true; Pic_Doorlock2_1Open_Stop.Visible = false;
                    }
                    else { Pic_Doorlock2_1Open_Stop.Visible = true; Pic_Doorlock2_1Open.Visible = false; }
                    if (dataDisplayStation1?.Doorlock2_1Close == true)
                    {
                        Pic_Doorlock2_1Close.Visible = true; Pic_Doorlock2_1Close_Stop.Visible = false;
                    }
                    else { Pic_Doorlock2_1Close_Stop.Visible = true; Pic_Doorlock2_1Close.Visible = false; }
                    if (dataDisplayStation1?.Doorlock2_2Open == true)
                    {
                        Pic_Doorlock2_2Open.Visible = true; Pic_Doorlock2_2Open_Stop.Visible = false;
                    }
                    else { Pic_Doorlock2_2Open_Stop.Visible = true; Pic_Doorlock2_2Open.Visible = false; }
                    if (dataDisplayStation1?.Doorlock2_2Close == true)
                    {
                        Pic_Doorlock2_2Close.Visible = true; Pic_Doorlock2_2Close_Stop.Visible = false;
                    }
                    else { Pic_Doorlock2_2Close_Stop.Visible = true; Pic_Doorlock2_2Close.Visible = false; }

                    if (dataDisplayStation1?.Al_Door1 == true)
                    {
                        Pic_Al_Door1.Visible = true; Pic_Al_Door1_Stop.Visible = false;
                    }
                    else { Pic_Al_Door1_Stop.Visible = true; Pic_Al_Door1.Visible = false; }
                    if (dataDisplayStation1?.Al_Door2 == true)
                    {
                        Pic_Al_Door2.Visible = true; Pic_Al_Door2_Stop.Visible = false;
                    }
                    else { Pic_Al_Door2_Stop.Visible = true; Pic_Al_Door2.Visible = false; }

                    if (dataDisplayStation1?.Door1_PressureHigh == true)
                    {
                        Pic_Door1_PressureHigh.Visible = true; Pic_Door1_PressureHigh_Stop.Visible = false;
                    }
                    else { Pic_Door1_PressureHigh_Stop.Visible = true; Pic_Door1_PressureHigh.Visible = false; }
                    if (dataDisplayStation1?.Door1_PressureLow == true)
                    {
                        Pic_Door1_PressureLow.Visible = true; Pic_Door1_PressureLow_Stop.Visible = false;
                    }
                    else { Pic_Door1_PressureLow_Stop.Visible = true; Pic_Door1_PressureLow.Visible = false; }
                    if (dataDisplayStation1?.Door2_PressureHigh == true)
                    {
                        Pic_Door2_PressureHigh.Visible = true; Pic_Door2_PressureHigh_Stop.Visible = false;
                    }
                    else { Pic_Door2_PressureHigh_Stop.Visible = true; Pic_Door2_PressureHigh.Visible = false; }
                    if (dataDisplayStation1?.Door2_PressureLow == true)
                    {
                        Pic_Door2_PressureLow.Visible = true; Pic_Door2_PressureLow_Stop.Visible = false;
                    }
                    else { Pic_Door2_PressureLow_Stop.Visible = true; Pic_Door2_PressureLow.Visible = false; }

                    if (dataDisplayStation1?.Remote == true)
                    {
                        bnt_Remote_T1.BackColor = Color.GreenYellow; bnt_Remote_T2.BackColor = Color.GreenYellow;
                    }
                    else { bnt_Remote_T1.BackColor = DefaultBackColor; bnt_Remote_T2.BackColor = DefaultBackColor; }
                    if (dataDisplayStation1?.Local == true)
                    {
                        bnt_Local_T1.BackColor = Color.GreenYellow; bnt_Local_T2.BackColor = Color.GreenYellow;
                    }
                    else { bnt_Local_T1.BackColor = DefaultBackColor; bnt_Local_T2.BackColor = DefaultBackColor; }
                    if (dataDisplayStation1?.Auto == true)
                    {
                        bnt_Auto_T1.BackColor = Color.GreenYellow; bnt_Auto_T2.BackColor = Color.GreenYellow;
                    }
                    else { bnt_Auto_T1.BackColor = DefaultBackColor; bnt_Auto_T2.BackColor = DefaultBackColor; }
                    if (dataDisplayStation1?.Man == true)
                    {
                        bnt_Hand_T1.BackColor = Color.GreenYellow; bnt_Hand_T2.BackColor = Color.GreenYellow;
                    }
                    else { bnt_Hand_T1.BackColor = DefaultBackColor; bnt_Hand_T2.BackColor = DefaultBackColor; }
                    if (dataDisplayStation1?.Local_Stop == true)
                    {
                        bnt_Estop_T1.BackColor = Color.GreenYellow; bnt_Estop_T2.BackColor = Color.GreenYellow;
                    }
                    else { bnt_Estop_T1.BackColor = DefaultBackColor; bnt_Estop_T2.BackColor = DefaultBackColor; }
                    if (dataDisplayStation1?.Doorlock2_Opening == true)
                    {
                        Pic_Doorlock2_Opening.Visible = true; Pic_Doorlock2_Opening_Stop.Visible = false;
                    }
                    else { Pic_Doorlock2_Opening_Stop.Visible = true; Pic_Doorlock2_Opening.Visible = false; }
                    if (dataDisplayStation1?.Doorlock2_Closing == true)
                    {
                        Pic_Doorlock2_Closing.Visible = true; Pic_Doorlock2_Closing_Stop.Visible = false;
                    }
                    else { Pic_Doorlock2_Closing_Stop.Visible = true; Pic_Doorlock2_Closing.Visible = false; }

                    // Station 2
                    var dataDisplayStation2 = Globalvariable.RealtimeDisplays.FirstOrDefault()?.Stations.FirstOrDefault(x => x.Path == "Local Station/DauTieng/S71500/Station_2");
                    if (dataDisplayStation2?.DC1_Running == true)
                    {
                        Pic_S2_DC1_Running.Visible = true; PicT4_S2_DC1_Running.Visible = true; Pic_S2_DC1_Stop.Visible = false; PicT4_S2_DC1_Stop.Visible = false;
                    }
                    else { Pic_S2_DC1_Stop.Visible = true; PicT4_S2_DC1_Stop.Visible = true; Pic_S2_DC1_Running.Visible = false; PicT4_S2_DC1_Running.Visible = false; }
                    if (dataDisplayStation2?.DC2_Running == true)
                    {
                        Pic_S2_DC2_Running.Visible = true; PicT4_S2_DC2_Running.Visible = true; Pic_S2_DC2_Stop.Visible = false; PicT4_S2_DC2_Stop.Visible = false;
                    }
                    else { Pic_S2_DC2_Stop.Visible = true; PicT4_S2_DC2_Stop.Visible = true; Pic_S2_DC2_Running.Visible = false; PicT4_S2_DC2_Running.Visible = false; }
                    if (dataDisplayStation2?.DC3_Running == true)
                    {
                        Pic_S2_DC3_Running.Visible = true; PicT4_S2_DC3_Running.Visible = true; Pic_S2_DC3_Stop.Visible = false; PicT4_S2_DC3_Stop.Visible = false;
                    }
                    else { Pic_S2_DC3_Stop.Visible = true; PicT4_S2_DC3_Stop.Visible = true; Pic_S2_DC3_Running.Visible = false; PicT4_S2_DC3_Running.Visible = false; }
                    if (dataDisplayStation2?.DC1_Over == true)
                    {
                        Pic_S2_DC1_Over.Visible = true; PicT4_S2_DC1_Over.Visible = true; Pic_S2_DC1_Over_Stop.Visible = false; PicT4_S2_DC1_Over_Stop.Visible = false;
                    }
                    else { Pic_S2_DC1_Over_Stop.Visible = true; PicT4_S2_DC1_Over_Stop.Visible = true; Pic_S2_DC1_Over.Visible = false; PicT4_S2_DC1_Over.Visible = false; }
                    if (dataDisplayStation2?.DC2_Over == true)
                    {
                        Pic_S2_DC2_Over.Visible = true; PicT4_S2_DC2_Over.Visible = true; Pic_S2_DC2_Over_Stop.Visible = false; PicT4_S2_DC2_Over_Stop.Visible = false;
                    }
                    else { Pic_S2_DC2_Over_Stop.Visible = true; PicT4_S2_DC2_Over_Stop.Visible = true; Pic_S2_DC2_Over.Visible = false; PicT4_S2_DC2_Over.Visible = false; }
                    if (dataDisplayStation2?.DC3_Over == true)
                    {
                        Pic_S2_DC3_Over.Visible = true; PicT4_S2_DC3_Over.Visible = true; Pic_S2_DC3_Over_Stop.Visible = false; PicT4_S2_DC3_Over_Stop.Visible = false;
                    }
                    else { Pic_S2_DC3_Over_Stop.Visible = true; PicT4_S2_DC3_Over_Stop.Visible = true; Pic_S2_DC3_Over.Visible = false; PicT4_S2_DC3_Over.Visible = false; }
                    if (dataDisplayStation2?.Door1_Opening == true)
                    {
                        Pic_Door3_Opening.Visible = true; Pic_Door3_Opening_Stop.Visible = false;
                    }
                    else { Pic_Door3_Opening_Stop.Visible = true; Pic_Door3_Opening.Visible = false; }
                    if (dataDisplayStation2?.Door1_Closing == true)
                    {
                        Pic_Door3_Closing.Visible = true; Pic_Door3_Closing_Stop.Visible = false;
                    }
                    else { Pic_Door3_Closing_Stop.Visible = true; Pic_Door3_Closing.Visible = false; }
                    if (dataDisplayStation2?.Door2_Opening == true)
                    {
                        Pic_Door4_Opening.Visible = true; Pic_Door4_Opening_Stop.Visible = false;
                    }
                    else { Pic_Door4_Opening_Stop.Visible = true; Pic_Door4_Opening.Visible = false; }
                    if (dataDisplayStation2?.Door2_Closing == true)
                    {
                        Pic_Door4_Closing.Visible = true; Pic_Door4_Closing_Stop.Visible = false;
                    }
                    else { Pic_Door4_Closing_Stop.Visible = true; Pic_Door4_Closing.Visible = false; }
                    if (dataDisplayStation2?.Door1_Open == true)
                    {
                        Pic_Door3_Open.Visible = true; Pic_Door3_Open_Stop.Visible = false;
                    }
                    else { Pic_Door3_Open_Stop.Visible = true; Pic_Door3_Open.Visible = false; }
                    if (dataDisplayStation2?.Door1_Close == true)
                    {
                        Pic_Door3_Close.Visible = true; Pic_Door3_Close_Stop.Visible = false;
                    }
                    else { Pic_Door3_Close_Stop.Visible = true; Pic_Door3_Close.Visible = false; }
                    if (dataDisplayStation2?.Door2_Open == true)
                    {
                        Pic_Door4_Open.Visible = true; Pic_Door4_Open_Stop.Visible = false;
                    }
                    else { Pic_Door4_Open_Stop.Visible = true; Pic_Door4_Open.Visible = false; }
                    if (dataDisplayStation2?.Door2_Close == true)
                    {
                        Pic_Door4_Close.Visible = true; Pic_Door4_Close_Stop.Visible = false;
                    }
                    else { Pic_Door4_Close_Stop.Visible = true; Pic_Door4_Close.Visible = false; }

                    if (dataDisplayStation2?.Doorlock1_1Open == true)
                    {
                        Pic_Doorlock3_1Open.Visible = true; Pic_Doorlock3_1Open_Stop.Visible = false;
                    }
                    else { Pic_Doorlock3_1Open_Stop.Visible = true; Pic_Doorlock3_1Open.Visible = false; }

                    if (dataDisplayStation2?.Doorlock1_1Close == true)
                    {
                        Pic_Doorlock3_1Close.Visible = true; Pic_Doorlock3_1Close_Stop.Visible = false;
                    }
                    else { Pic_Doorlock3_1Close_Stop.Visible = true; Pic_Doorlock3_1Close.Visible = false; }

                    if (dataDisplayStation2?.Doorlock1_2Open == true)
                    {
                        Pic_Doorlock3_2Open.Visible = true; Pic_Doorlock3_2Open_Stop.Visible = false;
                    }
                    else { Pic_Doorlock3_2Open_Stop.Visible = true; Pic_Doorlock3_2Open.Visible = false; }
                    if (dataDisplayStation2?.Doorlock1_2Close == true)
                    {
                        Pic_Doorlock3_2Close.Visible = true; Pic_Doorlock3_2Close_Stop.Visible = false;
                    }
                    else { Pic_Doorlock3_2Close_Stop.Visible = true; Pic_Doorlock3_2Close.Visible = false; }

                    if (dataDisplayStation2?.Doorlock2_1Open == true)
                    {
                        Pic_Doorlock4_1Open.Visible = true; Pic_Doorlock4_1Open_Stop.Visible = false;
                    }
                    else { Pic_Doorlock4_1Open_Stop.Visible = true; Pic_Doorlock4_1Open.Visible = false; }
                    if (dataDisplayStation2?.Doorlock2_1Close == true)
                    {
                        Pic_Doorlock4_1Close.Visible = true; Pic_Doorlock4_1Close_Stop.Visible = false;
                    }
                    else { Pic_Doorlock4_1Close_Stop.Visible = true; Pic_Doorlock4_1Close.Visible = false; }
                    if (dataDisplayStation2?.Doorlock2_2Open == true)
                    {
                        Pic_Doorlock4_2Open.Visible = true; Pic_Doorlock4_2Open_Stop.Visible = false;
                    }
                    else { Pic_Doorlock4_2Open_Stop.Visible = true; Pic_Doorlock4_2Open.Visible = false; }
                    if (dataDisplayStation2?.Doorlock2_2Close == true)
                    {
                        Pic_Doorlock4_2Close.Visible = true; Pic_Doorlock4_2Close_Stop.Visible = false;
                    }
                    else { Pic_Doorlock4_2Close_Stop.Visible = true; Pic_Doorlock4_2Close.Visible = false; }
                    if (dataDisplayStation2?.Al_Door1 == true)
                    {
                        Pic_Al_Door3.Visible = true; Pic_Al_Door3_Stop.Visible = false;
                    }
                    else { Pic_Al_Door3_Stop.Visible = true; Pic_Al_Door3.Visible = false; }
                    if (dataDisplayStation2?.Al_Door2 == true)
                    {
                        Pic_Al_Door4.Visible = true; Pic_Al_Door4_Stop.Visible = false;
                    }
                    else { Pic_Al_Door4_Stop.Visible = true; Pic_Al_Door4.Visible = false; }
                    if (dataDisplayStation2?.Door1_PressureHigh == true)
                    {
                        Pic_Door3_PressureHigh.Visible = true; Pic_Door3_PressureHigh_Stop.Visible = false;
                    }
                    else { Pic_Door3_PressureHigh_Stop.Visible = true; Pic_Door3_PressureHigh.Visible = false; }
                    if (dataDisplayStation2?.Door1_PressureLow == true)
                    {
                        Pic_Door3_PressureLow.Visible = true; Pic_Door3_PressureLow_Stop.Visible = false;
                    }
                    else { Pic_Door3_PressureLow_Stop.Visible = true; Pic_Door3_PressureLow.Visible = false; }
                    if (dataDisplayStation2?.Door2_PressureHigh == true)
                    {
                        Pic_Door4_PressureHigh.Visible = true; Pic_Door4_PressureHigh_Stop.Visible = false;
                    }
                    else { Pic_Door4_PressureHigh_Stop.Visible = true; Pic_Door4_PressureHigh.Visible = false; }
                    if (dataDisplayStation2?.Door2_PressureLow == true)
                    {
                        Pic_Door4_PressureLow.Visible = true; Pic_Door4_PressureLow_Stop.Visible = false;
                    }
                    else { Pic_Door4_PressureLow_Stop.Visible = true; Pic_Door4_PressureLow.Visible = false; }
                    if (dataDisplayStation2?.Remote == true)
                    {
                        bnt_Remote_T3.BackColor = Color.GreenYellow; bnt_Remote_T4.BackColor = Color.GreenYellow;
                    }
                    else { bnt_Remote_T3.BackColor = DefaultBackColor; bnt_Remote_T4.BackColor = DefaultBackColor; }
                    if (dataDisplayStation2?.Local == true)
                    {
                        bnt_Local_T3.BackColor = Color.GreenYellow; bnt_Local_T4.BackColor = Color.GreenYellow;
                    }
                    else { bnt_Local_T3.BackColor = DefaultBackColor; bnt_Local_T4.BackColor = DefaultBackColor; }
                    if (dataDisplayStation2?.Auto == true)
                    {
                        bnt_Auto_T3.BackColor = Color.GreenYellow; bnt_Auto_T4.BackColor = Color.GreenYellow;
                    }
                    else { bnt_Auto_T3.BackColor = DefaultBackColor; bnt_Auto_T4.BackColor = DefaultBackColor; }
                    if (dataDisplayStation2?.Man == true)
                    {
                        bnt_Hand_T3.BackColor = Color.GreenYellow; bnt_Hand_T4.BackColor = Color.GreenYellow;
                    }
                    else { bnt_Hand_T3.BackColor = DefaultBackColor; bnt_Hand_T4.BackColor = DefaultBackColor; }
                    if (dataDisplayStation2?.Local_Stop == true)
                    {
                        bnt_Estop_T3.BackColor = Color.GreenYellow; bnt_Estop_T4.BackColor = Color.GreenYellow;
                    }
                    else { bnt_Estop_T3.BackColor = DefaultBackColor; bnt_Estop_T4.BackColor = DefaultBackColor; }
                    if (dataDisplayStation2?.Doorlock1_Opening == true)
                    {
                        Pic_Doorlock3_Opening.Visible = true; Pic_Doorlock3_Opening_Stop.Visible = false;
                    }
                    else { Pic_Doorlock3_Opening_Stop.Visible = true; Pic_Doorlock3_Opening.Visible = false; }
                    if (dataDisplayStation2?.Doorlock1_Closing == true)
                    {
                        Pic_Doorlock3_Closing.Visible = true; Pic_Doorlock3_Closing_Stop.Visible = false;
                    }
                    else { Pic_Doorlock3_Closing_Stop.Visible = true; Pic_Doorlock3_Closing.Visible = false; }
                    if (dataDisplayStation2?.Doorlock2_Opening == true)
                    {
                        Pic_Doorlock4_Opening.Visible = true; Pic_Doorlock4_Opening_Stop.Visible = false;
                    }
                    else { Pic_Doorlock4_Opening_Stop.Visible = true; Pic_Doorlock4_Opening.Visible = false; }
                    if (dataDisplayStation2?.Doorlock2_Closing == true)
                    {
                        Pic_Doorlock4_Closing.Visible = true; Pic_Doorlock4_Closing_Stop.Visible = false;
                    }
                    else { Pic_Doorlock4_Closing_Stop.Visible = true; Pic_Doorlock4_Closing.Visible = false; }
                    // Station 3

                    var dataDisplayStation3 = Globalvariable.RealtimeDisplays.FirstOrDefault()?.Stations.FirstOrDefault(x => x.Path == "Local Station/DauTieng/S71500/Station_3");
                    if (dataDisplayStation3?.DC1_Running == true)
                    {
                        Pic_S3_DC1_Running.Visible = true; PicT6_S3_DC1_Running.Visible = true; Pic_S3_DC1_Stop.Visible = false; PicT6_S3_DC1_Stop.Visible = false;
                    }
                    else { Pic_S3_DC1_Stop.Visible = true; PicT6_S3_DC1_Stop.Visible = true; Pic_S3_DC1_Running.Visible = false; PicT6_S3_DC1_Running.Visible = false; }
                    if (dataDisplayStation3?.DC2_Running == true)
                    {
                        Pic_S3_DC2_Running.Visible = true; PicT6_S3_DC2_Running.Visible = true; Pic_S3_DC2_Stop.Visible = false; PicT6_S3_DC2_Stop.Visible = false;
                    }
                    else { Pic_S3_DC2_Stop.Visible = true; PicT6_S3_DC2_Stop.Visible = true; Pic_S3_DC2_Running.Visible = false; PicT6_S3_DC2_Running.Visible = false; }
                    if (dataDisplayStation3?.DC3_Running == true)
                    {
                        Pic_S3_DC3_Running.Visible = true; PicT6_S3_DC3_Running.Visible = true; Pic_S3_DC3_Stop.Visible = false; PicT6_S3_DC3_Stop.Visible = false;
                    }
                    else { Pic_S3_DC3_Stop.Visible = true; PicT6_S3_DC3_Stop.Visible = true; Pic_S3_DC3_Running.Visible = false; PicT6_S3_DC3_Running.Visible = false; }
                    if (dataDisplayStation3?.DC1_Over == true)
                    {
                        Pic_S3_DC1_Over.Visible = true; PicT6_S3_DC1_Over.Visible = true; Pic_S3_DC1_Over_Stop.Visible = false; PicT6_S3_DC1_Over_Stop.Visible = false;
                    }
                    else { Pic_S3_DC1_Over_Stop.Visible = true; PicT6_S3_DC1_Over_Stop.Visible = true; Pic_S3_DC1_Over.Visible = false; PicT6_S3_DC1_Over.Visible = false; }
                    if (dataDisplayStation3?.DC2_Over == true)
                    {
                        Pic_S3_DC2_Over.Visible = true; PicT6_S3_DC2_Over.Visible = true; Pic_S3_DC2_Over_Stop.Visible = false; PicT6_S3_DC2_Over_Stop.Visible = false;
                    }
                    else { Pic_S3_DC2_Over_Stop.Visible = true; PicT6_S3_DC2_Over_Stop.Visible = true; Pic_S3_DC2_Over.Visible = false; PicT6_S3_DC2_Over.Visible = false; }
                    if (dataDisplayStation3?.DC3_Over == true)
                    {
                        Pic_S3_DC3_Over.Visible = true; PicT6_S3_DC3_Over.Visible = true; Pic_S3_DC3_Over_Stop.Visible = false; PicT6_S3_DC3_Over_Stop.Visible = false;
                    }
                    else { Pic_S3_DC3_Over_Stop.Visible = true; PicT6_S3_DC3_Over_Stop.Visible = true; Pic_S3_DC3_Over.Visible = false; PicT6_S3_DC3_Over.Visible = false; }
                    if (dataDisplayStation3?.Remote == true)
                    {
                        bnt_Remote_T5.BackColor = Color.GreenYellow; bnt_Remote_T6.BackColor = Color.GreenYellow;
                    }
                    else { bnt_Remote_T5.BackColor = DefaultBackColor; bnt_Remote_T6.BackColor = DefaultBackColor; }
                    if (dataDisplayStation3?.Local == true)
                    {
                        bnt_Local_T5.BackColor = Color.GreenYellow; bnt_Local_T6.BackColor = Color.GreenYellow;
                    }
                    else { bnt_Local_T5.BackColor = DefaultBackColor; bnt_Local_T6.BackColor = DefaultBackColor; }
                    if (dataDisplayStation3?.Auto == true)
                    {
                        bnt_Auto_T5.BackColor = Color.GreenYellow; bnt_Auto_T6.BackColor = Color.GreenYellow;
                    }
                    else { bnt_Auto_T5.BackColor = DefaultBackColor; bnt_Auto_T6.BackColor = DefaultBackColor; }
                    if (dataDisplayStation3?.Man == true)
                    {
                        bnt_Hand_T5.BackColor = Color.GreenYellow; bnt_Hand_T6.BackColor = Color.GreenYellow;
                    }
                    else { bnt_Hand_T5.BackColor = DefaultBackColor; bnt_Hand_T6.BackColor = DefaultBackColor; }
                    if (dataDisplayStation3?.Local_Stop == true)
                    {
                        bnt_Estop_T5.BackColor = Color.GreenYellow; bnt_Estop_T6.BackColor = Color.GreenYellow;
                    }
                    else { bnt_Estop_T5.BackColor = DefaultBackColor; bnt_Estop_T6.BackColor = DefaultBackColor; }
                    if (dataDisplayStation3?.Door1_Opening == true)
                    {
                        Pic_Door5_Opening.Visible = true; Pic_Door5_Opening_Stop.Visible = false;
                    }
                    else { Pic_Door5_Opening_Stop.Visible = true; Pic_Door5_Opening.Visible = false; }
                    if (dataDisplayStation3?.Door1_Closing == true)
                    {
                        Pic_Door5_Closing.Visible = true; Pic_Door5_Closing_Stop.Visible = false;
                    }
                    else { Pic_Door5_Closing_Stop.Visible = true; Pic_Door5_Closing.Visible = false; }
                    if (dataDisplayStation3?.Door2_Opening == true)
                    {
                        Pic_Door6_Opening.Visible = true; Pic_Door6_Opening_Stop.Visible = false;
                    }
                    else { Pic_Door6_Opening_Stop.Visible = true; Pic_Door6_Opening.Visible = false; }
                    if (dataDisplayStation3?.Door2_Closing == true)
                    {
                        Pic_Door6_Closing.Visible = true; Pic_Door6_Closing_Stop.Visible = false;
                    }
                    else { Pic_Door6_Closing_Stop.Visible = true; Pic_Door6_Closing.Visible = false; }
                    if (dataDisplayStation3?.Door1_Open == true)
                    {
                        Pic_Door5_Open.Visible = true; Pic_Door5_Open_Stop.Visible = false;
                    }
                    else { Pic_Door5_Open_Stop.Visible = true; Pic_Door5_Open.Visible = false; }
                    if (dataDisplayStation3?.Door1_Close == true)
                    {
                        Pic_Door5_Close.Visible = true; Pic_Door5_Close_Stop.Visible = false;
                    }
                    else { Pic_Door5_Close_Stop.Visible = true; Pic_Door5_Close.Visible = false; }
                    if (dataDisplayStation3?.Door2_Open == true)
                    {
                        Pic_Door6_Open.Visible = true; Pic_Door6_Open_Stop.Visible = false;
                    }
                    else { Pic_Door6_Open_Stop.Visible = true; Pic_Door6_Open.Visible = false; }
                    if (dataDisplayStation3?.Door2_Close == true)
                    {
                        Pic_Door6_Close.Visible = true; Pic_Door6_Close_Stop.Visible = false;
                    }
                    else { Pic_Door6_Close_Stop.Visible = true; Pic_Door6_Close.Visible = false; }

                    if (dataDisplayStation3?.Doorlock1_1Open == true)
                    {
                        Pic_Doorlock5_1Open.Visible = true; Pic_Doorlock5_1Open_Stop.Visible = false;
                    }
                    else { Pic_Doorlock5_1Open_Stop.Visible = true; Pic_Doorlock5_1Open.Visible = false; }
                    if (dataDisplayStation3?.Doorlock1_1Close == true)
                    {
                        Pic_Doorlock5_1Close.Visible = true; Pic_Doorlock5_1Close_Stop.Visible = false;
                    }
                    else { Pic_Doorlock5_1Close_Stop.Visible = true; Pic_Doorlock5_1Close.Visible = false; }
                    if (dataDisplayStation3?.Doorlock1_2Open == true)
                    {
                        Pic_Doorlock5_2Open.Visible = true; Pic_Doorlock5_2Open_Stop.Visible = false;
                    }
                    else { Pic_Doorlock5_2Open_Stop.Visible = true; Pic_Doorlock5_2Open.Visible = false; }
                    if (dataDisplayStation3?.Doorlock1_2Close == true)
                    {
                        Pic_Doorlock5_2Close.Visible = true; Pic_Doorlock5_2Close_Stop.Visible = false;
                    }
                    else { Pic_Doorlock5_2Close_Stop.Visible = true; Pic_Doorlock5_2Close.Visible = false; }
                    if (dataDisplayStation3?.Door1_PressureHigh == true)
                    {
                        Pic_Door5_PressureHigh.Visible = true; Pic_Door5_PressureHigh_Stop.Visible = false;
                    }
                    else { Pic_Door5_PressureHigh_Stop.Visible = true; Pic_Door5_PressureHigh.Visible = false; }
                    if (dataDisplayStation3?.Door1_PressureLow == true)
                    {
                        Pic_Door5_PressureLow.Visible = true; Pic_Door5_PressureLow_Stop.Visible = false;
                    }
                    else { Pic_Door5_PressureLow_Stop.Visible = true; Pic_Door5_PressureLow.Visible = false; }
                    if (dataDisplayStation3?.Door2_PressureHigh == true)
                    {
                        Pic_Door6_PressureHigh.Visible = true; Pic_Door6_PressureHigh_Stop.Visible = false;
                    }
                    else { Pic_Door6_PressureHigh_Stop.Visible = true; Pic_Door6_PressureHigh.Visible = false; }
                    if (dataDisplayStation3?.Door2_PressureLow == true)
                    {
                        Pic_Door6_PressureLow.Visible = true; Pic_Door6_PressureLow_Stop.Visible = false;
                    }
                    else { Pic_Door6_PressureLow_Stop.Visible = true; Pic_Door6_PressureLow.Visible = false; }
                    if (dataDisplayStation3?.Al_Door1 == true)
                    {
                        Pic_Al_Door5.Visible = true; Pic_Al_Door5_Stop.Visible = false;
                    }
                    else { Pic_Al_Door5_Stop.Visible = true; Pic_Al_Door5.Visible = false; }
                    if (dataDisplayStation3?.Al_Door2 == true)
                    {
                        Pic_Al_Door6.Visible = true; Pic_Al_Door6_Stop.Visible = false;
                    }
                    else { Pic_Al_Door6_Stop.Visible = true; Pic_Al_Door6.Visible = false; }
                    if (dataDisplayStation3?.Doorlock1_Opening == true)
                    {
                        Pic_Doorlock5_Opening.Visible = true; Pic_Doorlock5_Opening_Stop.Visible = false;
                    }
                    else { Pic_Doorlock5_Opening_Stop.Visible = true; Pic_Doorlock5_Opening.Visible = false; }
                    if (dataDisplayStation3?.Doorlock1_Closing == true)
                    {
                        Pic_Doorlock5_Closing.Visible = true; Pic_Doorlock5_Closing_Stop.Visible = false;
                    }
                    else { Pic_Doorlock5_Closing_Stop.Visible = true; Pic_Doorlock5_Closing.Visible = false; }









                });
            }
            catch (Exception ex)
            {

            }
            finally
            {
                _timer.Enabled = true;
            }
        }

        private void FormatGridT1()
        {
            var dgv = _dataGridViewT1;

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



        private void UpdateTagValue1(string device, string status)
        {
            // Kiểm tra xem có cần Invoke lên UI thread không
            if (_dataGridViewT1.InvokeRequired)
            {
                _dataGridViewT1.Invoke(new Action(() => UpdateTagValue1(device, status)));
                return;
            }

            // Tìm và cập nhật item trong danh sách
            var dataDisplayStation1 = Globalvariable.RealtimeDisplays.FirstOrDefault()?.Stations.FirstOrDefault(x => x.Path == "Local Station/DauTieng/S71500/Station_1");
            //     var item = _tran1List.FirstOrDefault(x => x.Device == device);
            if (dataDisplayStation1 != null)
            {
                dataDisplayStation1.GetPropertyByName("Status")?.SetValue(dataDisplayStation1, status);
                dataDisplayStation1.StationName = status;
                dataDisplayStation1.DC1_Running = status == "1";
                //  dataDisplayStation1.StationId = device;
            }
        }
        private void dataGridViewT1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (_dataGridViewT1.Rows[e.RowIndex].DataBoundItem is Tran1Model item)
            {
                // Nếu cột đang format là cột Status
                if (_dataGridViewT1.Columns[e.ColumnIndex].DataPropertyName == "Status")
                {
                    if (item.Status == "1")
                    {
                        // Tô màu cả hàng
                        _dataGridViewT1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                        _dataGridViewT1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        // Trả lại màu mặc định nếu không phải "1"
                        _dataGridViewT1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                        _dataGridViewT1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
                // ✅ Đặt header tiếng Việt
                if (_dataGridViewT1.Columns.Contains("Id"))
                    _dataGridViewT1.Columns["Id"].HeaderText = "STT";

                if (_dataGridViewT1.Columns.Contains("Device"))
                    _dataGridViewT1.Columns["Device"].HeaderText = "Thiết Bị";

                if (_dataGridViewT1.Columns.Contains("Status"))
                    _dataGridViewT1.Columns["Status"].HeaderText = "Trạng Thái";

                if (_dataGridViewT1.Columns.Contains("CreateAt"))
                    _dataGridViewT1.Columns["CreateAt"].HeaderText = "Thời Gian";



            }
        }
        // Kết thúc Tràn 1
        // Tràn 2





























    }
}
