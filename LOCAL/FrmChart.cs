using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;

namespace RegistrationForm1
{
    public partial class ChartForm : Form
    {
        private TabControl chartTabControl;
        private List<SensorReading> sensorData;
        private Chart tempChart, voltageChart, currentChart, powerChart;
        private bool isRealTimeMode = false;

        public ChartForm(List<SensorReading> data)
        {
            sensorData = data;
            InitializeComponent();
            CreateChartTabs();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(1200, 800);
            this.Text = "Biểu đồ phân tích dữ liệu cảm biến";
            this.StartPosition = FormStartPosition.CenterScreen;

            chartTabControl = new TabControl();
            chartTabControl.Dock = DockStyle.Fill;
            chartTabControl.Font = new Font("Arial", 10, FontStyle.Regular);

            this.Controls.Add(chartTabControl);
        }
        private static IEnumerable<T> TakeLastItems<T>(IEnumerable<T> source, int count)
        {
            var list = source.ToList();
            return list.Skip(Math.Max(0, list.Count - count));
        }
        public void UpdateChartData(List<SensorReading> newData)
        {
            this.sensorData = newData;

            if (isRealTimeMode && tempChart != null)
            {
                
                var recentData = TakeLastItems(sensorData, 50).ToList();

                // Cập nhật biểu đồ nhiệt độ
                tempChart.Series[0].Points.Clear();
                foreach (var reading in recentData)
                {
                    tempChart.Series[0].Points.AddXY(reading.Timestamp, reading.Temperature);
                }

                // Cập nhật biểu đồ điện áp
                voltageChart.Series[0].Points.Clear();
                foreach (var reading in recentData)
                {
                    voltageChart.Series[0].Points.AddXY(reading.Timestamp, reading.Voltage);
                }

                // Cập nhật biểu đồ dòng điện
                currentChart.Series[0].Points.Clear();
                foreach (var reading in recentData)
                {
                    currentChart.Series[0].Points.AddXY(reading.Timestamp, reading.Current);
                }

                // Cập nhật biểu đồ công suất
                powerChart.Series[0].Points.Clear();
                foreach (var reading in recentData)
                {
                    powerChart.Series[0].Points.AddXY(reading.Timestamp, reading.Power);
                }

                // Làm mới hiển thị
                tempChart.Invalidate();
                voltageChart.Invalidate();
                currentChart.Invalidate();
                powerChart.Invalidate();
            }
        }
        private void CreateChartTabs()
        {
            // Tab 1: Biểu đồ đường theo thời gian
            chartTabControl.TabPages.Add(CreateTimeSeriesChart());

            // Tab 2: Biểu đồ phân bố nhiệt độ
            chartTabControl.TabPages.Add(CreateTemperatureDistributionChart());

            // Tab 3: Biểu đồ so sánh các thông số
            chartTabControl.TabPages.Add(CreateComparisonChart());

            // Tab 4: Biểu đồ phân tích mức điện áp
            chartTabControl.TabPages.Add(CreateVoltageAnalysisChart());
        }

        private TabPage CreateTimeSeriesChart()
        {
            TabPage timeTab = new TabPage("Biểu đồ theo thời gian");

            // Thêm panel điều khiển real-time
            Panel controlPanel = new Panel();
            controlPanel.Height = 40;
            controlPanel.Dock = DockStyle.Top;
            controlPanel.BackColor = Color.LightGray;

            CheckBox chkRealTime = new CheckBox();
            chkRealTime.Text = "Chế độ real-time";
            chkRealTime.Location = new Point(10, 10);
            chkRealTime.AutoSize = true;
            chkRealTime.CheckedChanged += (s, e) => {
                isRealTimeMode = chkRealTime.Checked;
            };

            Label lblInfo = new Label();
            lblInfo.Text = "Real-time mode: Hiển thị 50 điểm dữ liệu mới nhất";
            lblInfo.Location = new Point(150, 12);
            lblInfo.AutoSize = true;
            lblInfo.ForeColor = Color.Blue;

            controlPanel.Controls.Add(chkRealTime);
            controlPanel.Controls.Add(lblInfo);

            TableLayoutPanel layout = new TableLayoutPanel();
            layout.Dock = DockStyle.Fill;
            layout.RowCount = 2;
            layout.ColumnCount = 2;
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            // Tạo và lưu trữ reference của các chart
            tempChart = CreateTimeSeriesChartBase("Nhiệt độ (°C)", Color.Red);
            foreach (var reading in sensorData)
            {
                tempChart.Series[0].Points.AddXY(reading.Timestamp, reading.Temperature);
            }
            layout.Controls.Add(tempChart, 0, 0);

            voltageChart = CreateTimeSeriesChartBase("Điện áp (V)", Color.Blue);
            foreach (var reading in sensorData)
            {
                voltageChart.Series[0].Points.AddXY(reading.Timestamp, reading.Voltage);
            }
            layout.Controls.Add(voltageChart, 1, 0);

            currentChart = CreateTimeSeriesChartBase("Dòng điện (A)", Color.Green);
            foreach (var reading in sensorData)
            {
                currentChart.Series[0].Points.AddXY(reading.Timestamp, reading.Current);
            }
            layout.Controls.Add(currentChart, 0, 1);

            powerChart = CreateTimeSeriesChartBase("Công suất (W)", Color.Orange);
            foreach (var reading in sensorData)
            {
                powerChart.Series[0].Points.AddXY(reading.Timestamp, reading.Power);
            }
            layout.Controls.Add(powerChart, 1, 1);

            // Thêm control panel và layout vào tab
            timeTab.Controls.Add(layout);
            timeTab.Controls.Add(controlPanel);

            return timeTab;
        }
        private Chart CreateTimeSeriesChartBase(string title, Color color)
        {
            Chart chart = new Chart();
            chart.Dock = DockStyle.Fill;

            ChartArea chartArea = new ChartArea();
            chartArea.AxisX.Title = "Thời gian";
            chartArea.AxisY.Title = title;
            chartArea.AxisX.LabelStyle.Format = "HH:mm:ss";
            chartArea.AxisX.IntervalType = DateTimeIntervalType.Minutes;

            // Thiết lập auto-scroll cho real-time
            chartArea.AxisX.ScaleView.Zoomable = true;
            chartArea.AxisX.ScrollBar.IsPositionedInside = false;

            chart.ChartAreas.Add(chartArea);

            Series series = new Series();
            series.ChartType = SeriesChartType.Line;
            series.Color = color;
            series.BorderWidth = 2;
            series.MarkerStyle = MarkerStyle.Circle;
            series.MarkerColor = color;
            series.MarkerSize = 3;
            chart.Series.Add(series);

            chart.Titles.Add(title);
            chart.Titles[0].Font = new Font("Arial", 12, FontStyle.Bold);

            return chart;
        }
    

private TabPage CreateTemperatureDistributionChart()
        {
            TabPage tempTab = new TabPage("Phân bố nhiệt độ");

            Chart tempDistChart = new Chart();
            tempDistChart.Dock = DockStyle.Fill;

            ChartArea area = new ChartArea("TempDistArea");
            area.AxisX.Title = "Khoảng nhiệt độ (°C)";
            area.AxisY.Title = "Số lượng";
            tempDistChart.ChartAreas.Add(area);

            // Tạo histogram nhiệt độ
            Series tempDistSeries = new Series("Phân bố nhiệt độ");
            tempDistSeries.ChartType = SeriesChartType.Column;
            tempDistSeries.Color = Color.Red;

            // Phân chia khoảng nhiệt độ
            var tempGroups = sensorData
                .GroupBy(x => Math.Floor((double)x.Temperature))
                .OrderBy(g => g.Key)
                .ToList();

            foreach (var group in tempGroups)
            {
                tempDistSeries.Points.AddXY($"{group.Key}°C", group.Count());
            }

            tempDistChart.Series.Add(tempDistSeries);

            // Thêm tiêu đề
            Title title = new Title("Phân bố tần suất nhiệt độ");
            title.Font = new Font("Arial", 14, FontStyle.Bold);
            tempDistChart.Titles.Add(title);

            tempTab.Controls.Add(tempDistChart);
            return tempTab;
        }

        private TabPage CreateComparisonChart()
        {
            TabPage compTab = new TabPage("So sánh thông số");

            Chart compChart = new Chart();
            compChart.Dock = DockStyle.Fill;

            ChartArea compArea = new ChartArea("ComparisonArea");
            compArea.AxisX.Title = "Thông số";
            compArea.AxisY.Title = "Giá trị";
            compChart.ChartAreas.Add(compArea);

            // Tính giá trị trung bình
            var avgTemp = sensorData.Average(x => (double)x.Temperature);
            var avgVoltage = sensorData.Average(x => (double)x.Voltage);
            var avgCurrent = sensorData.Average(x => (double)x.Current);
            var avgPower = sensorData.Average(x => (double)x.Power);

            // Series cho giá trị trung bình
            Series avgSeries = new Series("Giá trị trung bình");
            avgSeries.ChartType = SeriesChartType.Column;
            avgSeries.Color = Color.Blue;

            avgSeries.Points.AddXY("Nhiệt độ (°C)", avgTemp);
            avgSeries.Points.AddXY("Điện áp (V)", avgVoltage);
            avgSeries.Points.AddXY("Dòng điện (A)", avgCurrent);
            avgSeries.Points.AddXY("Công suất (W)", avgPower);

            // Series cho giá trị max
            Series maxSeries = new Series("Giá trị cao nhất");
            maxSeries.ChartType = SeriesChartType.Column;
            maxSeries.Color = Color.Red;

            maxSeries.Points.AddXY("Nhiệt độ (°C)", (double)sensorData.Max(x => x.Temperature));
            maxSeries.Points.AddXY("Điện áp (V)", (double)sensorData.Max(x => x.Voltage));
            maxSeries.Points.AddXY("Dòng điện (A)", (double)sensorData.Max(x => x.Current));
            maxSeries.Points.AddXY("Công suất (W)", (double)sensorData.Max(x => x.Power));

            // Series cho giá trị min
            Series minSeries = new Series("Giá trị thấp nhất");
            minSeries.ChartType = SeriesChartType.Column;
            minSeries.Color = Color.Green;

            minSeries.Points.AddXY("Nhiệt độ (°C)", (double)sensorData.Min(x => x.Temperature));
            minSeries.Points.AddXY("Điện áp (V)", (double)sensorData.Min(x => x.Voltage));
            minSeries.Points.AddXY("Dòng điện (A)", (double)sensorData.Min(x => x.Current));
            minSeries.Points.AddXY("Công suất (W)", (double)sensorData.Min(x => x.Power));

            compChart.Series.Add(avgSeries);
            compChart.Series.Add(maxSeries);
            compChart.Series.Add(minSeries);

            // Thêm tiêu đề
            Title compTitle = new Title("So sánh các thông số cảm biến");
            compTitle.Font = new Font("Arial", 14, FontStyle.Bold);
            compChart.Titles.Add(compTitle);

            // Thêm legend
            Legend compLegend = new Legend("CompLegend");
            compChart.Legends.Add(compLegend);

            compTab.Controls.Add(compChart);
            return compTab;
        }

        private TabPage CreateVoltageAnalysisChart()
        {
            TabPage voltageTab = new TabPage("Phân tích điện áp");

            Chart voltageChart = new Chart();
            voltageChart.Dock = DockStyle.Fill;

            ChartArea voltageArea = new ChartArea("VoltageArea");
            voltageArea.AxisX.Title = "Mức điện áp";
            voltageArea.AxisY.Title = "Số lượng";
            voltageChart.ChartAreas.Add(voltageArea);

            // Phân loại mức điện áp
            var voltageStats = GetVoltageStatistics();

            Series voltageSeries = new Series("Phân tích điện áp");
            voltageSeries.ChartType = SeriesChartType.Pie;

            voltageSeries.Points.AddXY("Thấp (< 11.8V)", voltageStats.LowCount);
            voltageSeries.Points.AddXY("Bình thường (11.8-12.2V)", voltageStats.NormalCount);
            voltageSeries.Points.AddXY("Cao (> 12.2V)", voltageStats.HighCount);

            // Tô màu cho từng phần
            voltageSeries.Points[0].Color = Color.Red;
            voltageSeries.Points[1].Color = Color.Green;
            voltageSeries.Points[2].Color = Color.Orange;

            // Hiển thị % trên biểu đồ
            voltageSeries.Label = "#PERCENT{P1}";
            voltageSeries.LegendText = "#VALX (#VALY)";

            voltageChart.Series.Add(voltageSeries);

            // Thêm tiêu đề
            Title voltageTitle = new Title("Phân tích mức điện áp");
            voltageTitle.Font = new Font("Arial", 14, FontStyle.Bold);
            voltageChart.Titles.Add(voltageTitle);

            // Thêm legend
            Legend voltageLegend = new Legend("VoltageLegend");
            voltageChart.Legends.Add(voltageLegend);

            voltageTab.Controls.Add(voltageChart);
            return voltageTab;
        }

        
        private VoltageStatistics GetVoltageStatistics()
        {
            var stats = new VoltageStatistics();

            foreach (var reading in sensorData)
            {
                double voltage = (double)reading.Voltage;
                if (voltage < 11.8)
                    stats.LowCount++;
                else if (voltage <= 12.2)
                    stats.NormalCount++;
                else
                    stats.HighCount++;
            }

            return stats;
        }
    }

    public class SensorReading
    {
        public DateTime Timestamp { get; set; }
        public decimal Temperature { get; set; }
        public decimal Voltage { get; set; }
        public decimal Current { get; set; }
        public decimal Power { get; set; }
    }

    public class VoltageStatistics
    {
        public int LowCount { get; set; }
        public int NormalCount { get; set; }
        public int HighCount { get; set; }
    }
}
