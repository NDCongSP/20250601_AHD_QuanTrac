using Ahd.Core;
using Ahd.Winforms.Controls;
using Domain.Entities;
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
      

        private Timer _timer = new Timer();
        public FrmTran()
        {
            InitializeComponent();
            Load += FrmTran_Load;
              
        }
        private void FrmTran_Load(object sender, EventArgs e)
        {
            _timer.Interval = 1000;
            _timer.Tick += Timer_Tick;
            _timer.Start();


        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                _timer.Enabled = false; // Tạm dừng timer trong quá trình xử lý

                if (Globalvariable.RealtimeDisplays.Count == 0)
                    return; // Nếu không có dữ liệu, thoát khỏi hàm

                //using var dbContext = new ApplicationDbContext();

                //var dataAlarm = dbContext.FT04s
                //    .Where(x => x.IsDeleted == false)
                //    .GroupBy(a => new { a.LocationId, a.StationId })
                //    .Select(g => new
                //    {
                //        LocationId = g.Key.LocationId,
                //        StationId = g.Key.StationId,
                //        LocationName = g.FirstOrDefault().LocationName,
                //        StationName = g.FirstOrDefault().StationName,
                //        Items = g.ToList(),
                //    })
                //    .ToList();

                //Globalvariable.InvokeIfRequired(this, () =>
                //{
                //    var result = dataAlarm.FirstOrDefault(x => x.StationName == "Station_1")
                //        .Items
                //        .Where(x => x.TagName.Contains("1")
                //    //  .Where(x => x.TagName==("Al_Door1")
                //           || ((x.TagName == "DC1_Running") || (x.TagName == "DC2_Running") || (x.TagName == "DC3_Running"))
                //                   || ((x.TagName == "DC1_Over") || (x.TagName == "DC2_Over") || (x.TagName == "DC3_Over"))
                //                   || ((x.TagName == "Door1_Opening") || (x.TagName == "Door1_Closing") || (x.TagName == "Door1_Open"))
                //                     || ((x.TagName == "Door1_Close") || (x.TagName == "Door1_PressureHigh") || (x.TagName == "Door1_PressureLow"))
                //        )
                //    .GroupBy(x => x.TagName) // group theo Tên Thiết Bị
                //    .SelectMany(g => g
                //        .OrderByDescending(x => x.CreateAt) // sắp xếp theo Thời Gian giảm dần
                //        .Take(2)) // lấy 2 bản ghi mới nhất
                //    .ToList();


                //    _dataGridViewT1.DataSource = null;
                //    _dataGridViewT1.DataSource = dataAlarm.FirstOrDefault(x => x.StationName == "Station_1")
                //        .Items
                //        .Where(x => x.TagName == ("Al_Door1")
                //           || ((x.TagName == "DC1_Running") || (x.TagName == "DC2_Running") || (x.TagName == "DC3_Running"))
                //                   || ((x.TagName == "DC1_Over") || (x.TagName == "DC2_Over") || (x.TagName == "DC3_Over"))
                //                   || ((x.TagName == "Door1_Opening") || (x.TagName == "Door1_Closing") || (x.TagName == "Door1_Open"))
                //                     || ((x.TagName == "Door1_Close") || (x.TagName == "Door1_PressureHigh") || (x.TagName == "Door1_PressureLow"))
                //        )
                //        .GroupBy(x => x.TagName) // group theo Tên Thiết Bị
                //        .SelectMany(g => g
                //                    .OrderByDescending(x => x.CreateAt) // sắp xếp theo Thời Gian giảm dần
                //                    .Take(1)
                //                   ) // lấy 2 bản ghi mới nhất
                //        .OrderByDescending(x=>x.CreateAt)
                //        .ToList();

                //    _dataGridViewT1.Columns[nameof(FT04.CreateAt)].DisplayIndex = 0;
                //    _dataGridViewT1.AutoResizeColumns();
                //    _dataGridViewT1.Refresh();

                //    _dataGridViewT2.DataSource = null;
                //    _dataGridViewT2.DataSource = dataAlarm.FirstOrDefault(x => x.StationName == "Station_1")
                //        .Items
                //    //   .Where(x => x.TagName.Contains("2")
                //    .Where(x => x.TagName == ("Al_Door2")
                //           || ((x.TagName == "DC1_Running") || (x.TagName == "DC2_Running") || (x.TagName == "DC3_Running"))
                //                   || ((x.TagName == "DC1_Over") || (x.TagName == "DC2_Over") || (x.TagName == "DC3_Over"))
                //                   || ((x.TagName == "Door2_Opening") || (x.TagName == "Door2_Closing") || (x.TagName == "Door2_Open") || ((x.TagName == "Door2_Close"))
                //                   || ((x.TagName == "Doorlock2_Opening") || (x.TagName == "Doorlock2_Closing") || (x.TagName == "Doorlock2_1Open") || ((x.TagName == "Doorlock2_1Close"))
                //                     || (x.TagName == "Door2_PressureHigh") || (x.TagName == "Door2_PressureLow")) || (x.TagName == "Doorlock2_2Open") || (x.TagName == "Doorlock2_2Close"))
                //        )

                //        .GroupBy(x => x.TagName) // group theo Tên Thiết Bị
                //        .SelectMany(g => g
                //                    .OrderByDescending(x => x.CreateAt) // sắp xếp theo Thời Gian giảm dần
                //                    .Take(1)
                //                   ) // lấy 2 bản ghi mới nhất
                //        .OrderByDescending(x => x.CreateAt)
                //        .ToList();

                //    _dataGridViewT2.Columns[nameof(FT04.CreateAt)].DisplayIndex = 0;
                //    _dataGridViewT2.AutoResizeColumns();
                //    _dataGridViewT2.Refresh();

                //    // Station 2
                //    _dataGridViewT3.DataSource = null;
                //    _dataGridViewT3.DataSource = dataAlarm.FirstOrDefault(x => x.StationName == "Station_2")
                //        .Items
                //        .Where(x => x.TagName == ("Al_Door1")
                //           || ((x.TagName == "DC1_Running") || (x.TagName == "DC2_Running") || (x.TagName == "DC3_Running"))
                //                   || ((x.TagName == "DC1_Over") || (x.TagName == "DC2_Over") || (x.TagName == "DC3_Over"))
                //                   || ((x.TagName == "Door1_Opening") || (x.TagName == "Door1_Closing") || (x.TagName == "Door1_Open"))
                //                     || ((x.TagName == "Door1_Close") || (x.TagName == "Door1_PressureHigh") || (x.TagName == "Door1_PressureLow"))
                //        )
                //        .GroupBy(x => x.TagName) // group theo Tên Thiết Bị
                //        .SelectMany(g => g
                //                    .OrderByDescending(x => x.CreateAt) // sắp xếp theo Thời Gian giảm dần
                //                    .Take(1)
                //                   ) // lấy 2 bản ghi mới nhất
                //        .OrderByDescending(x => x.CreateAt)
                //        .ToList();
                //    _dataGridViewT3.Columns[nameof(FT04.CreateAt)].DisplayIndex = 0;
                //    _dataGridViewT3.AutoResizeColumns();
                //    _dataGridViewT3.Refresh();


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



                // });
            }
            catch (Exception ex)
            {

            }
            finally
            {
                _timer.Enabled = true;
            }
        }
    }
}


       
       





























    

