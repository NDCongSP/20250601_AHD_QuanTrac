using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace RegistrationForm1
{
    public partial class FrmNhaplieu : Form
    {
        // Bảng tra cứu α theo a/H
        private readonly Dictionary<double, double> alphaTable = new Dictionary<double, double>
        {
            { 0.00, 0.611 }, { 0.10, 0.615 }, { 0.15, 0.618 }, { 0.20, 0.620 }, { 0.25, 0.622 },
            { 0.30, 0.625 }, { 0.35, 0.628 }, { 0.40, 0.632 }, { 0.45, 0.638 }, { 0.50, 0.645 },
            { 0.55, 0.650 }, { 0.60, 0.660 }, { 0.65, 0.672 }, { 0.70, 0.690 }, { 0.75, 0.705 }
        };
        public FrmNhaplieu()
        {
            InitializeComponent();
            InitializeDefaultValues();
            SetupKeyPressEvents(); // Thêm dòng này để thiết lập sự kiện KeyPress
            SetupTextChangeEvents(); // Thêm sự kiện để tự động cập nhật Alpha
        }
        private void InitializeDefaultValues()
        {
            // Thiết lập giá trị mặc định cho các textbox
            txtPhi.Text = "0.95";     // Hệ số lưu tốc
            txtG.Text = "9.81";       // Gia tốc trọng trường
            txtZn.Text = "14";        // Cao trình ngưỡng trần (m)


        }
        private double GetAlphaFromTable(double aOverH)
        {
            // Làm tròn a/H đến 2 chữ số thập phân để so sánh chính xác hơn
            aOverH = Math.Round(aOverH, 2);

            // Nếu giá trị a/H có trong bảng, trả về α tương ứng
            if (alphaTable.ContainsKey(aOverH))
            {
                return alphaTable[aOverH];
            }

            // Nếu không có, thực hiện nội suy tuyến tính
            var keys = alphaTable.Keys.OrderBy(x => x).ToList();

            // Nếu a/H nhỏ hơn hoặc bằng giá trị nhỏ nhất trong bảng
            if (aOverH <= keys.First())
            {
                return alphaTable[keys.First()];
            }

            // Nếu a/H lớn hơn hoặc bằng giá trị lớn nhất trong bảng
            if (aOverH >= keys.Last())
            {
                return alphaTable[keys.Last()];
            }

            // Tìm hai giá trị gần nhất để nội suy
            double lowerKey = keys.Where(x => x <= aOverH).Max();
            double upperKey = keys.Where(x => x >= aOverH).Min();

            // Nội suy tuyến tính
            double lowerAlpha = alphaTable[lowerKey];
            double upperAlpha = alphaTable[upperKey];

            double interpolatedAlpha = lowerAlpha + (upperAlpha - lowerAlpha) * (aOverH - lowerKey) / (upperKey - lowerKey);
            return interpolatedAlpha;
        }
        private void UpdateAlpha()
        {
            if (!string.IsNullOrWhiteSpace(txtH.Text) && !string.IsNullOrWhiteSpace(txtHo.Text))
            {
                try
                {
                    double h = Convert.ToDouble(txtH.Text);
                    double Ho = Convert.ToDouble(txtHo.Text);

                    if (Ho != 0)
                    {
                        double aOverH = h / Ho;
                        double alpha = GetAlphaFromTable(aOverH);
                        alpha = Math.Round(alpha, 3); // Làm tròn đến 2 chữ số thập phân
                        txtAlpha.Text = alpha.ToString("F3");
                    }
                    else
                    {
                        txtAlpha.Text = "";
                    }
                }
                catch
                {
                    txtAlpha.Text = "";
                }
            }
            else
            {
                txtAlpha.Text = "";
            }
        }
        private void UpdateSumB()
        {
            if (!string.IsNullOrWhiteSpace(txtDoorOpen.Text))
            {
                try
                {
                    double c = Convert.ToDouble(txtDoorOpen.Text);
                    if (c != 0)
                    {
                        double sumB = c * 10;
                        sumB = Math.Round(sumB, 2); // Làm tròn đến 2 chữ số thập phân
                        txtSumB.Text = sumB.ToString("F2");
                    }
                    else
                    {
                        txtSumB.Text = "";
                    }
                }
                catch
                {
                    txtSumB.Text = "";
                }
            }
            else
            {
                txtSumB.Text = "";
            }
        }
        private void UpdateHo()
        {
            if (!string.IsNullOrWhiteSpace(txtMNTL.Text) && !string.IsNullOrWhiteSpace(txtZn.Text))
            {
                try
                {
                    double MNTL = Convert.ToDouble(txtMNTL.Text);
                    double Zn = Convert.ToDouble(txtZn.Text);

                    if (MNTL != 0)
                    {
                        double Ho = MNTL - Zn;
                        Ho = Math.Round(Ho, 2); // Làm tròn đến 2 chữ số thập phân
                        txtHo.Text = Ho.ToString("F2");
                    }
                    else
                    {
                        txtHo.Text = "";
                    }
                }
                catch
                {
                    txtHo.Text = "";
                }
            }
            else
            {
                txtHo.Text = "";
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra và lấy giá trị từ các textbox
                if (!ValidateInputs())
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ và chính xác các thông số cần thiết!\n\n" +
                                  "Các trường bắt buộc:\n" +
                                  "- φ (Hệ số lưu tốc)\n" +
                                  "- h (Chiều cao mở thực tế)\n" +
                                  "- Σb (Tổng chiều rộng khoang trần)\n" +
                                  "- g (Gia tốc trọng trường)\n" +
                                  "- Ho (Cột nước trên ngưỡng)",
                                  "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy giá trị từ các textbox
                double phi = Convert.ToDouble(txtPhi.Text);
                double alpha = Convert.ToDouble(txtAlpha.Text);
                double h = Convert.ToDouble(txtH.Text);
                double sumB = Convert.ToDouble(txtSumB.Text);
                double c = Convert.ToDouble(txtDoorOpen.Text);
                double g = Convert.ToDouble(txtG.Text);
                double Ho = Convert.ToDouble(txtHo.Text);

                // Kiểm tra điều kiện logic
                if (phi < 0.95 || phi > 1.0)
                {
                    MessageBox.Show("Hệ số lưu tốc φ phải nằm trong khoảng 0.95 - 1.0!",
                                  "Lỗi giá trị", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (Ho <= 0)
                {
                    MessageBox.Show("Cột nước trên ngưỡng Ho phải lớn hơn 0!",
                                  "Lỗi giá trị", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (h <= 0)
                {
                    MessageBox.Show("Chiều cao mở thực tế h phải lớn hơn 0!",
                                  "Lỗi giá trị", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (c <= 0)
                {
                    MessageBox.Show("Số cửa mở phải lớn hơn 0!",
                                  "Lỗi giá trị", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                double alphaTimesH = alpha * h;
                if (Ho <= alphaTimesH)
                {
                    MessageBox.Show($"Giá trị (Ho - α × h) phải lớn hơn 0 để tính căn bậc hai!\n" +
                                  $"Hiện tại: Ho = {Ho:F2}, α × h = {alphaTimesH:F2}\n" + // Thay đổi từ F3 thành F2
                                  $"Vui lòng kiểm tra lại các giá trị đầu vào.",
                                  "Lỗi tính toán", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tính toán theo công thức: Qu = φ × α × h × Σb × √(2 × g × (Ho - α × h))
                double insideSqrt = 2 * g * (Ho - alphaTimesH);
                double sqrt = Math.Sqrt(insideSqrt);
                double Qu = phi * alpha * h * sumB * sqrt;
                double Qu1 = Qu * c;
                double Qu2 = Qu / c;

                // Làm tròn tất cả kết quả đến 2 chữ số thập phân
                Qu = Math.Round(Qu, 2);
                Qu1 = Math.Round(Qu1, 2);
                Qu2 = Math.Round(Qu2, 2);

                // Hiển thị kết quả với 2 chữ số thập phân
                txtResult.Text = Qu.ToString("F2") + " m³/s";
                txtResult_1door.Text = Qu2.ToString("F2") + " m³/s";
                txtResult_Total.Text = Qu1.ToString("F2") + " m³/s";
            }
            catch (FormatException)
            {
                MessageBox.Show("Vui lòng nhập số hợp lệ cho tất cả các trường!",
                              "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra trong quá trình tính toán:\n{ex.Message}",
                              "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ValidateInputs()
        {
            // Kiểm tra các trường bắt buộc 
            if (string.IsNullOrWhiteSpace(txtPhi.Text) ||
                string.IsNullOrWhiteSpace(txtAlpha.Text) ||
                string.IsNullOrWhiteSpace(txtH.Text) ||
                string.IsNullOrWhiteSpace(txtSumB.Text) ||
                string.IsNullOrWhiteSpace(txtG.Text) ||
                string.IsNullOrWhiteSpace(txtMNTL.Text) ||
                string.IsNullOrWhiteSpace(txtZn.Text) ||
                string.IsNullOrWhiteSpace(txtHo.Text))
            {
                return false;
            }

            // Kiểm tra xem có thể chuyển đổi thành số không
            double temp;
            return double.TryParse(txtPhi.Text, out temp) &&
                   double.TryParse(txtAlpha.Text, out temp) &&
                   double.TryParse(txtH.Text, out temp) &&
                   double.TryParse(txtSumB.Text, out temp) &&
                   double.TryParse(txtG.Text, out temp) &&
                   double.TryParse(txtHo.Text, out temp) &&
                    double.TryParse(txtMNTL.Text, out temp) &&
                   (string.IsNullOrWhiteSpace(txtZn.Text) || double.TryParse(txtZn.Text, out temp));
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // Xóa các textbox input và output
            txtAlpha.Clear();
            txtH.Clear();
            txtHo.Clear();
            txtMNTL.Clear();
            txtResult.Clear();

            // Khôi phục giá trị mặc định
            txtPhi.Text = "0.95";
            txtG.Text = "9.81";
            txtSumB.Text = "60";
            txtZn.Text = "14";
        }
        // Sự kiện để tự động tính toán khi nhấn Enter
        private void txtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnCalculate_Click(sender, e);
                e.Handled = true; // Ngăn chặn tiếng beep khi nhấn Enter
            }
        }
        // Thiết lập sự kiện KeyPress cho các textbox chính
        private void SetupKeyPressEvents()
        {
            txtPhi.KeyPress += txtBox_KeyPress;
            txtH.KeyPress += txtBox_KeyPress;
            txtSumB.KeyPress += txtBox_KeyPress;
            txtG.KeyPress += txtBox_KeyPress;
            txtHo.KeyPress += txtBox_KeyPress;
            txtZn.KeyPress += txtBox_KeyPress;
        }
        // Thiết lập sự kiện TextChanged để tự động cập nhật Alpha
        private void SetupTextChangeEvents()
        {
            txtH.TextChanged += (s, e) => UpdateAlpha();
            txtHo.TextChanged += (s, e) => UpdateAlpha();
            txtDoorOpen.TextChanged += (s, e) => UpdateSumB();
            txtMNTL.TextChanged += (s, e) => UpdateHo();
            txtZn.TextChanged += (s, e) => UpdateHo();
        }
    }
}
