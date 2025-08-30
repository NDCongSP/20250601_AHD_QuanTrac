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

                Globalvariable.InvokeIfRequired(this, () =>
                {
                    var dataDisplayStation1 = Globalvariable.RealtimeDisplays.FirstOrDefault()?.Stations.FirstOrDefault(x => x.Path == "Local Station/DauTieng/S71500/Station_1");

                    if (dataDisplayStation1?.Al_Door1 == true)
                    {
                        Pic_Door1_Opening_Stop.Visible = false; Pic_Door1_Opening.Visible = true;
                    }
                    else { Pic_Door1_Opening_Stop.Visible = true; Pic_Door1_Opening.Visible = false; }
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
                this.Invoke((MethodInvoker)delegate { bnt_Remote_T1.BackColor = Color.GreenYellow; bnt_Remote_T2.BackColor = Color.GreenYellow; });
            }
            else
                this.Invoke((MethodInvoker)delegate { bnt_Remote_T1.BackColor = DefaultBackColor; bnt_Remote_T2.BackColor = DefaultBackColor; });
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
