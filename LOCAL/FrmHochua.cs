
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient; // Thêm thư viện này để làm việc với SQL Server
using System.Drawing;
using System.Globalization; // Để sử dụng CultureInfo.InvariantCulture cho double.Parse
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading.Tasks;
using System.Data.Entity;


namespace RegistrationForm1
{
    public partial class FrmHochua : Form
    {
        private Chart chart;
        private ChartArea ca;
        private Label lblZ; // Label để hiển thị thông tin Z (mực nước) và W (dung tích) cố định
        private Label lblToaDo; // Label mới để hiển thị tọa độ X (Thời gian) và Z (Mực nước) khi rê chuột
        public string UrlToLoad { get; set; }


        // Bảng nội suy Z (Mực nước) và W (Giá trị tương ứng) được tải từ CSV
        private Dictionary<int, List<double>> interpolationLookupTable;
        private Dictionary<string, Series> chartSeries;
        private Dictionary<string, int> originalBorderWidths;
        private Dictionary<string, int> originalMarkerSizes;
        private Dictionary<string, Color> originalColors; // New: To store original colors for fading     
                                                          // Bộ đệm lưu trữ danh sách điểm dữ liệu (X: Ngày, Y: Giá trị) của các đường SQL
        private Dictionary<string, (List<DateTime> Dates, List<double> Values)> sqlDataCache = new Dictionary<string, (List<DateTime> Dates, List<double> Values)>();

        // Dữ liệu CSV thô được nhúng trực tiếp vào code
        private readonly string csvInterpolationData = @"Z,0,   1,  2,  3,  4,  5,  6,  7,  8,  9,  10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90,91,92,93,94,95,96,97,98,99
                                                13,203,203.4,203.8,204.2,204.6,205,205.4,205.8,206.2,206.6,207,207.6,208.2,208.8,209.4,210,210.6,211.2,211.8,212.4,213,213.4,213.8,214.2,214.6,215,215.4,215.8,216.2,216.6,217,217.6,218.2,218.8,219.4,220,220.6,221.2,221.8,222.4,223,223.45,223.9,224.35,224.8,225.25,225.7,226.15,226.6,227.05,227.5,228.05,228.6,229.15,229.7,230.25,230.8,231.35,231.9,232.45,233,233.6,234.2,234.8,235.4,236,236.6,237.2,237.8,238.4,239,239.6,240.2,240.8,241.4,242,242.6,243.2,243.8,244.4,245,245.5,246,246.5,247,247.5,248,248.5,249,249.5,250,250.2,250.4,250.6,250.8,251,251.2,251.4,251.6,251.8
                                                14,252,253,254,255,256,257,258,259,260,261,262,262.5,263,263.5,264,264.5,265,265.5,266,266.5,267,267.65,268.3,268.95,269.6,270.25,270.9,271.55,272.2,272.85,273.5,274.15,274.8,275.45,276.1,276.75,277.4,278.05,278.7,279.35,280,280.5,281,281.5,282,282.5,283,283.5,284,284.5,285,285.7,286.4,287.1,287.8,288.5,289.2,289.9,290.6,291.3,292,292.7,293.4,294.1,294.8,295.5,296.2,296.9,297.6,298.3,299,299.7,300.4,301.1,301.8,302.5,303.2,303.9,304.6,305.3,306,306.7,307.4,308.1,308.8,309.5,310.2,310.9,311.6,312.3,313,313.6,314.2,314.8,315.4,316,316.6,317.2,317.8,318.4
                                                15,319,319.66,320.32,320.98,321.64,322.3,322.96,323.62,324.28,324.94,325,326.26,326.92,327.58,328.24,328.9,329.56,330.22,330.88,331.54,332.2,332.83,333.46,334.09,334.72,335.35,335.98,336.61,337.24,337.87,338.5,339.19,339.88,340.57,341.26,341.95,342.64,343.33,344.02,344.71,345.4,346.06,346.72,347.38,348.04,348.7,349.36,350.02,350.68,351.34,352,352.66,353.32,353.98,354.64,355.3,355.96,356.62,357.28,357.94,358.6,359.26,359.92,360.58,361.24,361.9,362.56,363.22,363.88,364.54,365.2,365.78,366.36,366.94,367.52,368.1,368.68,369.26,369.84,370.42,371,371.74,372.48,373.22,373.96,374.7,375.44,376.18,376.92,377.66,378.4,379.06,379.72,380.38,381.04,381.7,382.36,383.02,383.68,384.34
                                                16,385,385.85,386.7,387.55,388.4,389.25,390.1,390.95,391.8,392.65,393.5,394.35,395.2,396.05,396.9,397.75,398.6,399.45,400.3,401.15,402,402.85,403.7,404.55,405.4,406.25,407.1,407.95,408.8,409.65,410.5,410.95,411.4,411.85,412.3,412.75,413.2,413.65,414.1,414.55,415,416.25,417.5,418.75,420,421.25,422.5,423.75,425,426.25,427.5,428.35,429.2,430.05,430.9,431.75,432.6,433.45,434.3,435.15,436,436.85,437.7,438.55,439.4,440.25,441.1,441.95,442.8,443.65,444.5,445.35,446.2,447.05,447.9,448.75,449.6,450.45,451.3,452.15,453,453.85,454.7,455.55,456.4,457.25,458.1,458.95,459.8,460.65,461.5,462.35,463.2,464.05,464.9,465.75,466.6,467.45,468.3,469.15
                                                17,470,470.97,471.94,472.91,473.88,474.85,475.82,476.79,477.76,478.73,479.7,480.67,481.64,482.61,483.58,484.55,485.52,486.49,487.46,488.43,489.4,490.37,491.34,492.31,493.28,494.25,495.22,496.19,497.16,498.13,499.1,500.07,501.04,502.01,502.98,503.95,504.92,505.89,506.86,507.83,508.8,509.77,510.74,511.71,512.68,513.65,514.62,515.59,516.56,517.53,518.5,519.47,520.44,521.41,522.38,523.35,524.32,525.29,526.26,527.23,528.2,529.17,530.14,531.11,532.08,533.05,534.02,534.99,535.96,536.93,537.9,538.87,539.84,540.81,541.78,542.75,543.72,544.69,545.66,546.63,547.6,548.57,549.54,550.51,551.48,552.45,553.42,554.39,555.36,556.33,557.3,558.27,559.24,560.21,561.18,562.15,563.12,564.09,565.06,566.03
                                                18,567,568.12,569.24,570.36,571.48,572.6,573.72,574.84,575.96,577.08,578.2,579.32,580.44,581.56,582.68,583.8,584.92,586.04,587.16,588.28,589.4,590.52,591.64,592.76,593.88,595,596.12,597.24,598.36,599.48,600.6,601.72,602.84,603.96,605.08,606.2,607.32,608.44,609.56,610.68,611.8,612.92,614.04,615.16,616.28,617.4,618.52,619.64,620.76,621.88,623,624.12,625.24,626.36,627.48,628.6,629.72,630.84,631.96,633.08,634.2,635.32,636.44,637.56,638.68,639.8,640.92,642.04,643.16,644.28,645.4,646.52,647.64,648.76,649.88,651,652.12,653.24,654.36,655.48,656.6,657.72,658.84,659.96,661.08,662.2,663.32,664.44,665.56,666.68,667.8,668.92,670.04,671.16,672.28,673.4,674.52,675.64,676.76,677.88
                                                19,679,680.19,681.38,682.57,683.76,684.95,686.14,687.33,688.52,689.71,690.9,692.09,693.28,694.47,695.66,696.85,698.04,699.23,700.42,701.61,702.8,703.99,705.18,706.37,707.56,708.75,709.94,711.13,712.32,713.51,714.7,715.89,717.08,718.27,719.46,720.65,721.84,723.03,724.22,725.41,726.6,727.79,728.98,730.17,731.36,732.55,733.74,734.93,736.12,737.31,738.5,739.69,740.88,742.07,743.26,744.45,745.64,746.83,748.02,749.21,750.4,751.59,752.78,753.97,755.16,756.35,757.54,758.73,759.92,761.11,762.3,763.49,764.68,765.87,767.06,768.25,769.44,770.63,771.82,773.01,774.2,775.39,776.58,777.77,778.96,780.15,781.34,782.53,783.72,784.91,786.1,787.29,788.48,789.67,790.86,792.05,793.24,794.43,795.62,796.81
                                                20,798,799.4,800.8,802.2,803.6,805,806.4,807.8,809.2,810.6,812,813.4,814.8,816.2,817.6,819,820.4,821.8,823.2,824.6,826,827.4,828.8,830.2,831.6,833,834.4,835.8,837.2,838.6,840,841.4,842.8,844.2,845.6,847,848.4,849.8,851.2,852.6,854,855.4,856.8,858.2,859.6,861,862.4,863.8,865.2,866.6,868,869.4,870.8,872.2,873.6,875,876.4,877.8,879.2,880.6,882,883.4,884.8,886.2,887.6,889,890.4,891.8,893.2,894.6,896,897.4,898.8,900.2,901.6,903,904.4,905.8,907.2,908.6,910,911.4,912.8,914.2,915.6,917,918.4,919.8,921.2,922.6,924,925.4,926.8,928.2,929.6,931,932.4,933.8,935.2,936.6
                                                21,938,939.63,941.26,942.89,944.52,946.15,947.78,949.41,951.04,952.67,954.3,955.93,957.56,959.19,960.82,962.45,964.08,965.71,967.34,968.97,970.6,972.23,973.86,975.49,977.12,978.75,980.38,982.01,983.64,985.27,986.9,988.53,990.16,991.79,993.42,995.05,996.68,998.31,999.94,1001.57,1003.2,1004.83,1006.46,1008.09,1009.72,1011.35,1012.98,1014.61,1016.24,1017.87,1019.5,1021.13,1022.76,1024.39,1026.02,1027.65,1029.28,1030.91,1032.54,1034.17,1035.8,1037.43,1039.06,1040.69,1042.32,1043.95,1045.58,1047.21,1048.84,1050.47,1052.1,1053.73,1055.36,1056.99,1058.62,1060.25,1061.88,1063.51,1065.14,1066.77,1068.4,1070.03,1071.66,1073.29,1074.92,1076.55,1078.18,1079.81,1081.44,1083.07,1084.7,1086.33,1087.96,1089.59,1091.22,1092.85,1094.48,1096.11,1097.74,1099.37
                                                22,1101,1102.78,1104.56,1106.34,1108.12,1109.9,1111.68,1113.46,1115.24,1117.02,1118.8,1120.58,1122.36,1124.14,1125.92,1127.7,1129.48,1131.26,1133.04,1134.82,1136.6,1138.38,1140.16,1141.94,1143.72,1145.5,1147.28,1149.06,1150.84,1152.62,1154.4,1156.18,1157.96,1159.74,1161.52,1163.3,1165.08,1166.86,1168.64,1170.42,1172.2,1173.98,1175.76,1177.54,1179.32,1181.1,1182.88,1184.66,1186.44,1188.22,1190,1191.78,1193.56,1195.34,1197.12,1198.9,1200.68,1202.46,1204.24,1206.02,1207.8,1209.58,1211.36,1213.14,1214.92,1216.7,1218.48,1220.26,1222.04,1223.82,1225.6,1227.38,1229.16,1230.94,1232.72,1234.5,1236.28,1238.06,1239.84,1241.62,1243.4,1245.18,1246.96,1248.74,1250.52,1252.3,1254.08,1255.86,1257.64,1259.42,1261.2,1263.28,1265.36,1267.44,1269.52,1271.6,1273.68,1275.76,1277.84,1279.92
                                                23,1282,1284.06,1286.12,1288.18,1290.24,1292.3,1294.36,1296.42,1298.48,1300.54,1302.6,1304.71,1306.82,1308.93,1311.04,1313.15,1315.26,1317.37,1319.48,1321.59,1323.7,1325.71,1327.72,1329.73,1331.74,1333.75,1335.76,1337.77,1339.78,1341.79,1343.8,1345.86,1347.92,1349.98,1352.04,1354.1,1356.16,1358.22,1360.28,1362.34,1364.4,1366.46,1368.52,1370.58,1372.64,1374.7,1376.76,1378.82,1380.88,1382.94,1385,1387.06,1389.12,1391.18,1393.24,1395.3,1397.36,1399.42,1401.48,1403.54,1405.6,1407.66,1409.72,1411.78,1413.84,1415.9,1417.96,1420.02,1422.08,1424.14,1426.2,1428.26,1430.32,1432.38,1434.44,1436.5,1438.56,1440.62,1442.68,1444.74,1446.8,1448.86,1450.92,1452.98,1455.04,1457.1,1459.16,1461.22,1463.28,1465.34,1467.4,1469.46,1471.52,1473.58,1475.64,1477.7,1479.76,1481.82,1483.88,1485.94
                                                24,1488,1490.32,1492.64,1494.96,1497.28,1499.6,1501.92,1504.24,1506.56,1508.88,1511.2,1513.52,1515.84,1518.16,1520.48,1522.8,1525.12,1527.44,1529.76,1532.08,1534.4,1536.72,1539.04,1541.36,1543.68,1546,1548.32,1550.64,1552.96,1555.28,1557.6,1559.92,1562.24,1564.56,1566.88,1569.2,1571.52,1573.84,1576.16,1578.48,1580.8,1583.12,1585.44,1587.76,1590.08,1592.4,1594.72,1597.04,1599.36,1601.68,1604,1606.32,1608.64,1610.96,1613.28,1615.6,1617.92,1620.24,1622.56,1624.88,1627.2,1629.52,1631.84,1634.16,1636.48,1638.8,1641.12,1643.44,1645.76,1648.08,1650.4,1652.72,1655.04,1657.36,1659.68,1662,1664.32,1666.64,1668.96,1671.28,1673.6,1675.92,1678.24,1680.56,1682.88,1685.2,1687.52,1689.84,1692.16,1694.48,1696.8,1699.12,1701.44,1703.76,1706.08,1708.4,1710.72,1713.04,1715.36,1717.68
                                                25,1720,1722.32,1724.64,1726.96,1729.28,1731.6,1733.92,1736.24,1738.56,1740.88,1743.2,1745.52,1747.84,1750.16,1752.48,1754.8,1757.12,1759.44,1761.76,1764.08,1766.4,1768.72,1771.04,1773.36,1775.68,1778,1780.32,1782.64,1784.96,1787.28,1789.6,1791.92,1794.24,1796.56,1798.88,1801.2,1803.52,1805.84,1808.16,1810.48,1812.8,1815.12,1817.44,1819.76,1822.08,1824.4,1826.72,1829.04,1831.36,1833.68,1836,1838.32,1840.64,1842.96,1845.28,1847.6,1849.92,1852.24,1854.56,1856.88,1859.2,1861.52,1863.84,1866.16,1868.48,1870.8,1873.12,1875.44,1877.76,1880.08,1882.4,1884.72,1887.04,1889.36,1891.68,1894,1896.32,1898.64,1900.96,1903.28,1905.6,1907.92,1910.24,1912.56,1914.88,1917.2,1919.52,1921.84,1924.16,1926.48,1928.8,1931.12,1933.44,1935.76,1938.08,1940.4,1942.72,1945.04,1947.36,1949.68
                                                26,1952,1954.32,1956.64,1958.96,1961.28,1963.6,1965.92,1968.24,1970.56,1972.88,1975.2,1977.52,1979.84,1982.16,1984.48,1986.8,1989.12,1991.44,1993.76,1996.08,1998.4,2000.72,2003.04,2005.36,2007.68,2010,2012.32,2014.64,2016.96,2019.28,2021.6,2023.92,2026.24,2028.56,2030.88,2033.2,2035.52,2037.84,2040.16,2042.48,2044.8,2047.12,2049.44,2051.76,2054.08,2056.4,2058.72,2061.04,2063.36,2065.68,2068,2070.32,2072.64,2074.96,2077.28,2079.6,2081.92,2084.24,2086.56,2088.88,2091.2,2093.56,2095.92,2098.28,2100.64,2103,2105.36,2107.72,2110.08,2112.44,2114.8,2117.12,2119.44,2121.76,2124.08,2126.4,2128.72,2131.04,2133.36,2135.68,2138,2140.32,2142.64,2144.96,2147.28,2149.6,2151.92,2154.24,2156.56,2158.88,2161.2,2163.52,2165.84,2168.16,2170.48,2172.8,2175.12,2177.44,2179.76,2182.08";


        // Mảng các ngày cố định đã sử dụng để vẽ các đường.
        private DateTime[] fixedDatesForCurves;
        // Dữ liệu tương ứng cho HCCN và DPPH
        private double[] hccnValuesForCurves;
        private double[] dpphValuesForCurves;
        private double[] dplValuesForCurves; // New member variable for DPL values
        private Timer dataRefreshTimer; // Khai báo một đối tượng Timer
        private Timer _timer = new Timer();

        public FrmHochua()
        {
            InitializeComponent();
            InitializeLabels(); // Khởi tạo và thêm các label vào Form
            LoadInterpolationTable(); // Tải bảng nội suy khi khởi tạo Form
            LoadChart(); // Tải biểu đồ
            LoadAllRainDataWithTimestampAsync();



            // Gắn sự kiện SizeChanged cho Form để cập nhật vị trí các controls khi Form thay đổi kích thước
            this.SizeChanged += (sender, e) => AdjustLayout();
            AdjustLayout(); // Cập nhật vị trí ban đầu sau khi controls đã được thêm vào form

            InitializeTimer(); // Khởi tạo và cấu hình Timer
            this.Shown += FrmHochua_Shown;
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
                    var location = Globalvariable.RealtimeDisplays?.FirstOrDefault(loc => loc.LocationId == 1);
                    if (location != null)
                    {
                        foreach (var item in location.Stations)
                        {
                            if (item.Path == "Local Station/DauTieng/S71500/Station_1")
                            {


                            }
                            else if (item.Path == "Local Station/DauTieng/S71500/Station_2")
                            {
                                //_labALDoor1_Station2.Text = item.Al_Door1.ToString();
                                //_labALDoor2_Station2.Text = item.Al_Door2.ToString();
                            }
                            else if (item.Path == "Local Station/DauTieng/S71500/Station_3")
                            {
                                //_labALDoor1_Station3.Text = item.Al_Door1.ToString();
                                //_labALDoor2_Station3.Text = item.Al_Door2.ToString();
                            }

                            _labFllow_ho.Text = location.Stations.FirstOrDefault(x => x.Path.Contains("Location_Info"))?.Fllow_Ho.ToString();
                            _labW1_ho.Text = location.CalculatorValue.W1_ho.ToString();
                            _lab_Q_tr.Text = location.CalculatorValue.Q_tr.ToString();
                            _labQ_cs1.Text = location.CalculatorValue.Qc_CS1.ToString();

                            // _labQ_cs1.Text = location.CalculatorValue.Q_cs1.ToString();
                            _labQ_cs2.Text = location.CalculatorValue.Qc_CS2.ToString();
                            _labQ_cs3.Text = location.CalculatorValue.Q_cs3.ToString();
                            _labQ_den.Text = location.CalculatorValue.Q_den.ToString();
                            _labQ_di.Text = location.CalculatorValue.Q_di.ToString();


                        }

                    }


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


        private void InitializeTimer()
        {
            dataRefreshTimer = new Timer();
            dataRefreshTimer.Interval = 10 * 60 * 1000; // 10 phút (10 * 60 giây * 1000 mili giây)
                                                        // dataRefreshTimer.Interval = 1000; // 10 phút (10 * 60 giây * 1000 mili giây)
            dataRefreshTimer.Tick += DataRefreshTimer_Tick; // Gán sự kiện Tick
            dataRefreshTimer.Start(); // Bắt đầu Timer
        }
        private void DataRefreshTimer_Tick(object sender, EventArgs e)
        {
            LoadAllRainDataWithTimestampAsync(); // Tải lại dữ liệu khi Timer Tick
        }



        private async Task LoadAllRainDataWithTimestampAsync()
        {
            try
            {
                //if (Globalvariable.RealtimeDisplays.Count == 0)
                //    return;

                using var dbContext = new ApplicationDbContext();



                // Lấy bản ghi mới nhất 
                var latest = await Task.Run(() => dbContext.FT03s
                    .Where(x => x.IsDeleted == false)
                    .OrderByDescending(x => x.CreateAt)
                    .FirstOrDefault());

                if (latest == null)
                {
                    UpdateUI(() => dataGridViewRainData.DataSource = null);
                    return;
                }

                // 2. Helper for safe conversion
                double ToDouble(object? value) =>
                    (value == null || value == DBNull.Value) ? 0 : Convert.ToDouble(value);

                // 3. Define the data structure
                DataTable dt = new DataTable();
                dt.Columns.Add("Tên", typeof(string));
                dt.Columns.Add("Tức thời", typeof(double));
                dt.Columns.Add("Tổng tích luỹ", typeof(double));
                dt.Columns.Add("Thời gian", typeof(DateTime));

                // 4. Populate rows (Mapping the properties)
                var stations = new[] {
            ("Đầu mối HDT", latest.API_D_DM_HoDT, latest.API_D_DM_HoDT_Total),
            ("Minh hòa",    latest.API_D_MinhHoa, latest.API_D_MinhHoa_Total),
            ("Minh tâm",    latest.API_D_MinhTam, latest.API_D_MinhTam_Total),
            ("Lộc thiện",   latest.API_D_LocThien, latest.API_D_LocThien_Total),
            ("Lộc ninh",    latest.API_D_LocNinh, latest.API_D_LocNinh_Total),
            ("Lộc thành",   latest.API_D_LocThanh, latest.API_D_LocThanh_Total),
            ("Thanh lương", latest.API_D_ThanhLuong, latest.API_D_ThanhLuong_Total),
            ("Tân hoà 1",   latest.API_D_TanHoa1, latest.API_D_TanHoa1_Total),
            ("Tân hoà 2",   latest.API_D_TanHoa2, latest.API_D_TanHoa2_Total),
            ("Kà tum",      latest.API_D_KaTum, latest.API_D_KaTum_Total),
            ("Tân thành",   latest.API_D_TanThanh, latest.API_D_TanThanh_Total),
            ("Đồng ban",    latest.API_D_DongBan, latest.API_D_DongBan_Total),
            ("Tân hà",      latest.API_D_TanHa, latest.API_D_TanHa_Total)
        };

                foreach (var (name, instant, total) in stations)
                {
                    dt.Rows.Add(name, ToDouble(instant), ToDouble(total), latest.CreateAt);
                }

                // 5. Unified UI Update
                UpdateUI(() =>
                {
                    dataGridViewRainData.DataSource = dt;

                    if (dataGridViewRainData.Columns["Tức thời"] != null)
                        dataGridViewRainData.Columns["Tức thời"].DefaultCellStyle.Format = "N2";

                    if (dataGridViewRainData.Columns["Tổng tích luỹ"] != null)
                        dataGridViewRainData.Columns["Tổng tích luỹ"].DefaultCellStyle.Format = "N2";

                    dataGridViewRainData.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                });
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Lỗi kết nối CSDL: {ex.Message}", "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hệ thống: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Simple helper to handle Cross-thread calls
        private void UpdateUI(Action action)
        {
            if (this.InvokeRequired) this.Invoke(action);
            else action();
        }




        private void InitializeLabels()
        {
            // KHỞI TẠO VÀ CẤU HÌNH LABEL lblZ
            lblZ = new Label();
            lblZ.AutoSize = true;
            lblZ.Font = new Font("Arial", 10, FontStyle.Bold);
            lblZ.ForeColor = Color.Black;
            this.Controls.Add(lblZ); // Thêm Label trực tiếp vào Form

            // KHỞI TẠO VÀ CẤU HÌNH LABEL lblToaDo
            lblToaDo = new Label();
            lblToaDo.Text = "Rê chuột để xem tọa độ và dung tích";
            lblToaDo.AutoSize = true;
            lblToaDo.Font = new Font("Arial", 10, FontStyle.Bold);
            lblToaDo.ForeColor = Color.Black;
            this.Controls.Add(lblToaDo); // Thêm Label trực tiếp vào Form
        }
        private void AdjustLayout()
        {
            // Một khoảng cách nhỏ từ phía trên Form để các label không bị dính sát
            int topPadding = 365;
            int labelMargin = 17; // Khoảng cách giữa các labels

            // Cập nhật vị trí của lblZ (trên cùng), căn giữa theo chiều ngang của Form
            lblZ.Location = new Point((this.ClientSize.Width - lblZ.Width) / 2, topPadding);
            lblZ.BringToFront(); // Đảm bảo lblZ luôn ở trên cùng

            // Cập nhật vị trí của lblToaDo (ngay dưới lblZ), cũng được căn giữa
            lblToaDo.Location = new Point((this.ClientSize.Width - lblToaDo.Width) / 2, lblZ.Bottom + labelMargin);
            lblToaDo.BringToFront(); // Đảm bảo lblToaDo luôn ở trên cùng

            // Cấu hình vị trí và kích thước của Chart
            if (chart != null)
            {
                // Chiều cao toàn bộ form
                int totalFormHeight = this.ClientSize.Height;

                // Chiều cao biểu đồ là 2/3 tổng chiều cao form
                int chartHeight = (int)(totalFormHeight * (2.0 / 3.0));

                // Vị trí top của biểu đồ để nó nằm dưới cùng và chiếm 2/3 tổng chiều cao
                int chartTop = totalFormHeight - chartHeight;

                chart.Location = new Point(0, chartTop);
                chart.Size = new Size(this.ClientSize.Width, chartHeight);
                chart.SendToBack(); // Đảm bảo biểu đồ nằm dưới các label
            }
        }

        private void LoadInterpolationTable()
        {
            interpolationLookupTable = new Dictionary<int, List<double>>();

            // Tách các dòng từ dữ liệu CSV
            string[] lines = csvInterpolationData.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            // Bỏ qua các dòng tiêu đề (ví dụ 3 dòng đầu tiên, hoặc tìm dòng bắt đầu bằng "Z,").
            int dataStartLine = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("Z,"))
                {
                    dataStartLine = i + 1; // Dòng dữ liệu bắt đầu sau dòng tiêu đề "Z,"
                    break;
                }
            }

            for (int i = dataStartLine; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] parts = line.Split(',');

                if (parts.Length > 0 && int.TryParse(parts[0], out int zIntegerPart))
                {
                    List<double> wValuesForDecimals = new List<double>();
                    // Bắt đầu từ index 1 vì index 0 là phần nguyên của Z
                    for (int j = 1; j < parts.Length; j++)
                    {
                        if (double.TryParse(parts[j], NumberStyles.Any, CultureInfo.InvariantCulture, out double wValue))
                        {
                            wValuesForDecimals.Add(wValue);
                        }
                        else
                        {
                            // Nếu không thể phân tích, thêm NaN hoặc giá trị mặc định
                            wValuesForDecimals.Add(double.NaN);
                        }
                    }
                    interpolationLookupTable[zIntegerPart] = wValuesForDecimals;
                }
            }
        }



        private async void LoadChart()
        {
            // 1. Khởi tạo và thiết lập sự kiện nền cho Chart
            InitChartComponents();

            // 2. Tính toán khung thời gian (Tháng 7 năm trước -> Tháng 6 năm sau)
            int baseYear = DateTime.Now.Month >= 7 ? DateTime.Now.Year : DateTime.Now.Year - 1;
            DateTime startDate = new DateTime(baseYear, 7, 1);
            DateTime endDate = new DateTime(baseYear + 1, 6, 30);

            ConfigureChartAxes(startDate, endDate);

            // 3. Khởi tạo dữ liệu trục X cố định cho các đường đặc tính hồ chứa
            DateTime[] fixedDates = GenerateFixedDates(baseYear);
            fixedDatesForCurves = fixedDates;

            // 4. Nạp dữ liệu mực nước tĩnh (Tối ưu hóa bộ nhớ & khởi tạo)
            LoadStaticCurveValues();

            // Khởi tạo nhanh mảng giá trị bằng Enumerable (39 phần tử tương ứng với fixedDates)
            double[] mndbtValues = Enumerable.Repeat(24.4, fixedDates.Length).ToArray();
            double[] mntkValues = Enumerable.Repeat(25.1, fixedDates.Length).ToArray();
            double[] mnktValues = Enumerable.Repeat(26.92, fixedDates.Length).ToArray();
            double[] ctddValues = Enumerable.Repeat(28.0, fixedDates.Length).ToArray();
            double[] mncValues = Enumerable.Repeat(17.0, fixedDates.Length).ToArray();

            double minHCCNValue = hccnValuesForCurves.Min();

            // 5. Làm sạch dữ liệu cũ
            chart.Series.Clear();
            chart.Annotations.Clear();

            // 6. Dựng các vùng Area (A, B, C)
            BuildAreaRegions(fixedDates, minHCCNValue);

            // 7. Thêm các đường giới hạn mực nước thiết kế vào biểu đồ
            AddStaticSeriesToChart(fixedDates, mncValues, ctddValues, mnktValues, mntkValues, mndbtValues);

            // TẠM THỜI THÔNG BÁO TRÊN UI KHI ĐANG TẢI NGẦM
            lblZ.Text = "⏳ Đang tải dữ liệu thủy văn từ SQL Server...";

            // 8. TỐI ƯU: Đợi tải và vẽ dữ liệu thực tế (SQL) bất đồng bộ mà không làm đơ Form
            await LoadAndRenderSqlDataAsync(startDate, endDate);

            // 9. Thêm các nhãn Text (Vùng A, B, C) lên biểu đồ (Chạy sau khi dữ liệu SQL đã sẵn sàng)
            AddChartAnnotations(fixedDates);
        }

        private void InitChartComponents()
        {
            chart = new Chart();
            this.Controls.Add(chart);

            ca = new ChartArea { BackColor = Color.GhostWhite };
            ca.InnerPlotPosition.Auto = false;
            ca.InnerPlotPosition.Width = 95;
            ca.InnerPlotPosition.Height = 80;
            ca.InnerPlotPosition.X = 3;
            ca.InnerPlotPosition.Y = 8;
            chart.ChartAreas.Add(ca);

            chart.MouseMove += chart_MouseMove;
            chart.MouseLeave += chart_MouseLeave;
            chart.MouseClick += chart_MouseClick;

            Legend legend = new Legend
            {
                Docking = Docking.Bottom,
                Alignment = StringAlignment.Center
            };
            chart.Legends.Add(legend);

            chartSeries = new Dictionary<string, Series>();
            originalBorderWidths = new Dictionary<string, int>();
            originalMarkerSizes = new Dictionary<string, int>();
            originalColors = new Dictionary<string, Color>();
        }

        private void ConfigureChartAxes(DateTime startDate, DateTime endDate)
        {
            // Trục X
            ca.AxisX.Minimum = startDate.ToOADate();
            ca.AxisX.Maximum = endDate.ToOADate();
            ca.AxisX.LabelStyle.Enabled = true;
            ca.AxisX.Title = "Thời gian";

            ca.AxisX.MajorGrid.Enabled = true;
            ca.AxisX.MajorGrid.LineColor = Color.LightGray;
            ca.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            ca.AxisX.MajorGrid.IntervalType = DateTimeIntervalType.Months;
            ca.AxisX.MajorGrid.Interval = 1;

            ca.AxisX.LineColor = Color.Gainsboro;
            ca.AxisX.LineDashStyle = ChartDashStyle.Dash;

            ca.AxisX.MajorTickMark.Enabled = true;
            ca.AxisX.MajorTickMark.IntervalType = DateTimeIntervalType.Months;
            ca.AxisX.MajorTickMark.Interval = 1;

            ca.AxisX.LabelStyle.IntervalType = DateTimeIntervalType.Months;
            ca.AxisX.LabelStyle.Interval = 1;
            ca.AxisX.LabelStyle.IsStaggered = true;
            ca.AxisX.LabelStyle.Angle = -45;
            ca.AxisX.LabelStyle.Font = new Font("Arial", 8);
            ca.AxisX.LabelStyle.ForeColor = Color.Blue;
            ca.AxisX.LabelStyle.Format = "dd/MM";

            // Trục Y
            ca.AxisY.Minimum = 17;
            ca.AxisY.Maximum = 28;
            ca.AxisY.Interval = 1;
            ca.AxisY.Title = "Mực nước (m)";

            ca.AxisY.MajorGrid.LineColor = Color.LightGray;
            ca.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            ca.AxisY.LineColor = Color.Gainsboro;
            ca.AxisY.LineDashStyle = ChartDashStyle.Dash;
        }

        private DateTime[] GenerateFixedDates(int baseYear)
        {
            return new[] {
        new DateTime(baseYear,7,1), new DateTime(baseYear,7,15), new DateTime(baseYear,7,16), new DateTime(baseYear,7,31),
        new DateTime(baseYear,8,1), new DateTime(baseYear,8,15), new DateTime(baseYear,8,16), new DateTime(baseYear,8,31),
        new DateTime(baseYear,9,1), new DateTime(baseYear,9,15), new DateTime(baseYear,9,16), new DateTime(baseYear,9,30),
        new DateTime(baseYear,10,1), new DateTime(baseYear,10,15), new DateTime(baseYear,10,16), new DateTime(baseYear,10,31),
        new DateTime(baseYear,11,1), new DateTime(baseYear,12,1), new DateTime(baseYear,12,11), new DateTime(baseYear,12,21),
        new DateTime(baseYear+1,1,1), new DateTime(baseYear+1,1,11), new DateTime(baseYear+1,1,21), new DateTime(baseYear+1,2,1),
        new DateTime(baseYear+1,2,11), new DateTime(baseYear+1,2,21), new DateTime(baseYear+1,3,1), new DateTime(baseYear+1,3,11),
        new DateTime(baseYear+1,3,21), new DateTime(baseYear+1,4,1), new DateTime(baseYear+1,4,11), new DateTime(baseYear+1,4,21),
        new DateTime(baseYear+1,5,1), new DateTime(baseYear+1,5,11), new DateTime(baseYear+1,5,21), new DateTime(baseYear+1,6,1),
        new DateTime(baseYear+1,6,11), new DateTime(baseYear+1,6,21), new DateTime(baseYear+1,6,30)
    };
        }

        private void LoadStaticCurveValues()
        {
            hccnValuesForCurves = new double[] {
        17,17.10,17.17,17.25,17.33,17.60,17.57,17.70,17.8,18.60,18.68,19.50,19.56,20.25,20.38,21.10,21.2,21.67,21.6,21.55,21.5,
        21.21,21.03,20.81,20.51,20.21,19.97,19.46,19.03,18.59,18.28,17.98,17.7,17.48,17.45,17.4,17.38,17.35,17
    };
            dpphValuesForCurves = new double[] {
        19,19.20,19.24,19.40,19.47,19.90,19.96,20.40,20.45,21.30,21.35,22.15,22.24,23.00,23.09,23.80,23.93,24.4,24.4,24.4,24.4,
        24.19,23.99,23.8,23.34,22.92,22.62,22.12,21.66,21.21,20.84,20.51,20.21,20.03,19.86,19.69,19.44,19.21,19
    };
            // Sửa lỗi mảng thiếu phần tử: Fill mảng DPL tự động lặp lại cho đủ kích thước nếu cần, hoặc giữ nguyên logic của bạn
            dplValuesForCurves = new double[] {
        20.30,20.30,21.20,21.20,22.10,22.10,22.70,22.70,23.30,23.30,23.65,23.65,24.00,24.00,24.40,24.40,24.40,24.40
    };
        }

        private void BuildAreaRegions(DateTime[] fixedDates, double minHCCNValue)
        {
            // Vùng A
            Series areaA = new Series("Vùng A: Vùng hạn chế cấp nước") { ChartType = SeriesChartType.Area, Color = Color.FromArgb(80, Color.Red), BorderWidth = 0 };
            // Vùng B
            Series areaB = new Series("Vùng B: Cấp nước bình thường") { ChartType = SeriesChartType.Area, Color = Color.FromArgb(80, Color.Green), BorderWidth = 0 };
            // Vùng C
            Series areaC = new Series("Vùng C: Cấp nước gia tăng") { ChartType = SeriesChartType.Range, Color = Color.FromArgb(80, Color.LightBlue), BorderWidth = 0 };

            int minLength = Math.Min(fixedDates.Length, Math.Min(dplValuesForCurves.Length, dpphValuesForCurves.Length));

            for (int i = 0; i < fixedDates.Length; i++)
            {
                double oaDate = fixedDates[i].ToOADate();

                // Cấu hình điểm Vùng A
                double valA = hccnValuesForCurves[i] > minHCCNValue ? hccnValuesForCurves[i] : minHCCNValue;
                areaA.Points.Add(new DataPoint(oaDate, new[] { valA, minHCCNValue }));

                // Cấu hình điểm Vùng B
                double valB = dpphValuesForCurves[i] > hccnValuesForCurves[i] ? dpphValuesForCurves[i] : hccnValuesForCurves[i];
                areaB.Points.Add(new DataPoint(oaDate, new[] { valB, hccnValuesForCurves[i] }));

                // Cấu hình điểm Vùng C (Dựa theo độ dài tối thiểu của mảng dpl)
                if (i < minLength)
                {
                    if (dplValuesForCurves[i] >= dpphValuesForCurves[i])
                        areaC.Points.Add(new DataPoint(oaDate, new[] { dpphValuesForCurves[i], dplValuesForCurves[i] }));
                    else
                        areaC.Points.Add(new DataPoint(oaDate, new[] { double.NaN }));
                }
            }

            // Đăng ký Series vào Chart
            string[] names = { areaC.Name, areaB.Name, areaA.Name };
            Series[] series = { areaC, areaB, areaA };
            Color[] colors = { Color.FromArgb(80, Color.LightBlue), Color.FromArgb(80, Color.Green), Color.FromArgb(80, Color.Red) };

            for (int i = 0; i < series.Length; i++)
            {
                chart.Series.Add(series[i]);
                chartSeries.Add(names[i], series[i]);
                originalColors.Add(names[i], colors[i]);
            }
        }

        private void AddStaticSeriesToChart(DateTime[] dates, double[] mnc, double[] ctdd, double[] mnkt, double[] mntk, double[] mndbt)
        {
            var seriesData = new[]
            {
        new { Name = "MNC", Color = Color.Black, Style = MarkerStyle.Circle, Data = mnc },
        new { Name = "CTĐĐ", Color = Color.Red, Style = MarkerStyle.Star4, Data = ctdd },
        new { Name = "MNKT", Color = Color.Black, Style = MarkerStyle.Triangle, Data = mnkt },
        new { Name = "MNTK", Color = Color.Black, Style = MarkerStyle.Diamond, Data = mntk },
        new { Name = "MNDBT", Color = Color.Black, Style = MarkerStyle.Cross, Data = mndbt },
        new { Name = "DPL", Color = Color.Red, Style = MarkerStyle.Triangle, Data = dplValuesForCurves },
        new { Name = "HCCN", Color = Color.Red, Style = MarkerStyle.Circle, Data = hccnValuesForCurves },
        new { Name = "DPPH", Color = Color.Magenta, Style = MarkerStyle.Square, Data = dpphValuesForCurves }
    };

            foreach (var s in seriesData)
            {
                Series sObj = CreateSeries(s.Name, s.Color, s.Style, dates.Take(s.Data.Length).ToList(), s.Data);
                chart.Series.Add(sObj);
                chartSeries.Add(sObj.Name, sObj);
            }
        }

        private async Task LoadAndRenderSqlDataAsync(DateTime startCurrent, DateTime endCurrent)
        {
            DateTime startOld = startCurrent.AddYears(-1);
            DateTime endOld = endCurrent.AddYears(-1);

            // 1. Kích hoạt song song cả 2 truy vấn SQL (Năm nay và Năm ngoái) để giảm nửa thời gian chờ
            var currentDataTask = GetFllow_Ho_FinalDataFromSqlAsync(startCurrent, endCurrent);
            var oldDataTask = GetFllow_Ho_FinalDataFromSqlAsync(startOld, endOld);

            // Giải phóng luồng UI tại đây, đợi database trả về dữ liệu
            await Task.WhenAll(currentDataTask, oldDataTask);

            var sqlData = currentDataTask.Result;
            var sqlDataOld = oldDataTask.Result;


            // --- Vẽ dữ liệu thực tế Năm nay ---
            Series fllowHoFinal = CreateSeries("Fllow_Ho_Final", Color.Blue, MarkerStyle.None, sqlData.Dates, sqlData.Values.ToArray()); // Tắt marker mặc định
            fllowHoFinal.MarkerStyle = MarkerStyle.None; // Đảm bảo tắt hẳn
                                                         // --- Tại hàm nạp dữ liệu SQL cũ của bạn, thêm 2 dòng này sau khi lấy được sqlData và sqlDataOld ---
            sqlDataCache["Fllow_Ho_Final"] = (sqlData.Dates, sqlData.Values);

            chart.Series.Add(fllowHoFinal);

            // --- Vẽ dữ liệu thực tế Năm ngoái ---
            List<DateTime> oldDatesShifted = sqlDataOld.Dates.Select(x => x.AddYears(1)).ToList();
            Series fllowHoFinalOld = CreateSeries("Fllow_Ho_Final_Old", Color.Brown, MarkerStyle.None, oldDatesShifted, sqlDataOld.Values.ToArray()); // Tắt marker mặc định
            fllowHoFinalOld.MarkerStyle = MarkerStyle.None; // Đảm bảo tắt hẳn
            sqlDataCache["Fllow_Ho_Final_Old"] = (sqlDataOld.Dates, sqlDataOld.Values);
            chart.Series.Add(fllowHoFinalOld);

            // 4. Kiểm tra dữ liệu và cập nhật nhãn trạng thái mực nước hiện tại
            bool hasValidData = sqlData.Values.Any(v => !double.IsNaN(v));
            fllowHoFinal.Enabled = hasValidData;
            fllowHoFinal.IsVisibleInLegend = hasValidData;

            if (hasValidData)
            {
                double latestZ = sqlData.Values.Last(v => !double.IsNaN(v));
                double interpolatedW = InterpolateW(latestZ);
                lblZ.Text = $"Mực nước: {latestZ:F2}m ⇄ Dung tích: {interpolatedW:F2}x10⁶m³";
            }
            else
            {
                lblZ.Text = "Không có dữ liệu Fllow_Ho_Final từ SQL.";
            }
        }

        private void AddChartAnnotations(DateTime[] fixedDates)
        {
            // Thêm Nhãn Vùng A
            int midA = fixedDates.Length / 2;
            double yPosA = (hccnValuesForCurves[midA] + 17.0) / 2.0;
            CreateTextAnnotation("Vùng A", fixedDates[midA].ToOADate(), yPosA);

            // Thêm Nhãn Vùng B
            double yPosB = (dpphValuesForCurves[midA] + hccnValuesForCurves[midA]) / 2.0;
            CreateTextAnnotation("Vùng B", fixedDates[midA].ToOADate(), yPosB);

            // Thêm Nhãn Vùng C (Vị trí 1)
            int idxC1 = fixedDates.Length / 5;
            if (idxC1 < dplValuesForCurves.Length)
            {
                double yPosC1 = (dplValuesForCurves[idxC1] + dpphValuesForCurves[idxC1]) / 2.0;
                CreateTextAnnotation("Vùng C", fixedDates[fixedDates.Length / 8].ToOADate(), yPosC1);
            }

            // Thêm Nhãn Vùng C (Vị trí 2) - Sửa lỗi index logic cũ (dùng idxC2 thay vì idxC1)
            int idxC2 = fixedDates.Length * 3 / 4;
            if (idxC2 < dplValuesForCurves.Length && idxC2 < dpphValuesForCurves.Length)
            {
                double yPosC2 = (dplValuesForCurves[idxC2] + dpphValuesForCurves[idxC2]) / 2.0;
                CreateTextAnnotation("Vùng C", fixedDates[idxC2].ToOADate(), yPosC2);
            }
        }

        private void CreateTextAnnotation(string text, double x, double y)
        {
            TextAnnotation anno = new TextAnnotation
            {
                Text = text,
                ForeColor = Color.Black,
                Font = new Font("Arial", 14, FontStyle.Bold),
                X = x,
                Y = y,
                AxisX = ca.AxisX,
                AxisY = ca.AxisY,
                AllowMoving = false,
                Visible = true
            };
            anno.SmartLabelStyle.Enabled = true;
            chart.Annotations.Add(anno);
        }





        private void chart_MouseClick(object sender, MouseEventArgs e)
        {
            // 1. Thực hiện HitTest nhanh
            HitTestResult result = chart.HitTest(e.X, e.Y);

            // Kiểm tra nếu phần tử được click không phải là LegendItem thì thoát sớm
            if (result.ChartElementType != ChartElementType.LegendItem) return;

            Series series = result.Series;
            if (series == null) return;

            // 2. Lọc bỏ các chuỗi vùng cố định không cho phép ẩn
            if (series.Name == "Vùng A: Vùng hạn chế cấp nước" ||
                series.Name == "Vùng B: Cấp nước bình thường" ||
                series.Name == "Vùng C: Cấp nước gia tăng")
            {
                return;
            }

            // 3. Kiểm tra xem Series này có nằm trong danh sách quản lý không
            if (chartSeries.ContainsKey(series.Name))
            {
                // TỐI ƯU GIAO DIỆN: Tạm dừng cập nhật đồ họa để tránh lag giật hình
                chart.Series.SuspendUpdates();

                // Kiểm tra trạng thái ẩn hiện dựa trên màu sắc (A = 0 nghĩa là suốt hoàn toàn -> đang ẩn)
                bool isCurrentlyHidden = (series.Color.A == 0);

                if (isCurrentlyHidden)
                {
                    // --- KHÔI PHỤC HIỂN THỊ ---
                    Color originalColor = Color.Blue; // Mặc định phòng hờ
                    originalColors.TryGetValue(series.Name, out originalColor);

                    series.Color = originalColor;

                    // Khôi phục lại độ dày đường (nếu là Line)
                    if (series.ChartType == SeriesChartType.Line || series.ChartType == SeriesChartType.FastLine)
                    {
                        int originalWidth = 2;
                        originalBorderWidths.TryGetValue(series.Name, out originalWidth);
                        series.BorderWidth = originalWidth;
                    }

                    series.LegendText = series.Name; // Trả lại tên cũ
                }
                else
                {
                    // --- ẨN ĐƯỜNG DỮ LIỆU ---
                    // TỐI ƯU CỐT LÕI: Chuyển màu sang suốt nhưng GIỮ NGUYÊN dòng trong Chú giải (Legend)
                    series.Color = Color.FromArgb(0, series.Color);

                    // Ép độ dày bằng 0 để bộ render đồ họa của MS Chart bỏ qua không vẽ đường này nữa -> Tăng tốc cực đem
                    if (series.ChartType == SeriesChartType.Line || series.ChartType == SeriesChartType.FastLine)
                    {
                        series.BorderWidth = 0;
                    }

                    series.LegendText = series.Name + " (ẩn)";
                }

                // Đảm bảo mục chú thích luôn hiển thị trên Legend cho dù ẩn hay hiện đường line
                series.IsVisibleInLegend = true;

                // Kích hoạt lại và vẽ lại một lần duy nhất
                chart.Series.ResumeUpdates();
                chart.Invalidate();
            }
        }

        /// <summary>
        /// Thiết lập ContextMenuStrip để bật/tắt hiển thị của các Series trên biểu đồ.
        /// </summary>
        private double InterpolateFixedValueForDate(DateTime targetDate, DateTime[] dates, double[] values)
        {
            if (dates.Length == 0 || values.Length == 0 || dates.Length != values.Length)
            {
                return double.NaN;
            }

            // Handle edge cases: targetDate is before or at the first date
            if (targetDate <= dates[0])
            {
                return values[0];
            }
            // Handle edge cases: targetDate is after or at the last date
            if (targetDate >= dates[dates.Length - 1])
            {
                return values[dates.Length - 1];
            }

            int lowerIndex = 0;
            for (int i = 0; i < dates.Length - 1; i++)
            {
                if (targetDate >= dates[i] && targetDate < dates[i + 1])
                {
                    lowerIndex = i;
                    break; // Found the segment, exit loop
                }
            }


            int upperIndex = lowerIndex + 1;


            DateTime x1Date = dates[lowerIndex];
            DateTime x2Date = dates[upperIndex];
            double y1 = values[lowerIndex];
            double y2 = values[upperIndex];

            // Perform linear interpolation
            long totalTicks = x2Date.Ticks - x1Date.Ticks;
            long targetTicks = targetDate.Ticks - x1Date.Ticks;

            if (totalTicks == 0)
            {
                // Should not happen if dates are distinct, but handle for safety
                return y1;
            }

            double ratio = (double)targetTicks / totalTicks;
            return y1 + (y2 - y1) * ratio;
        }


        // Khai báo 2 biến toàn cục này ở cấp Class (bên ngoài hàm MouseMove) để lưu vị trí chuột cũ
        private Point lastMousePosition = Point.Empty;
        private const int MOUSE_THRESHOLD_PIXELS = 3; // Chỉ tính toán lại nếu chuột di chuyển > 3 pixel

        private void chart_MouseMove(object sender, MouseEventArgs e)
        {
            // TỐI ƯU 1: Kiểm tra khoảng cách di chuyển của chuột để tránh tính toán thừa
            if (lastMousePosition != Point.Empty &&
                Math.Abs(e.X - lastMousePosition.X) < MOUSE_THRESHOLD_PIXELS &&
                Math.Abs(e.Y - lastMousePosition.Y) < MOUSE_THRESHOLD_PIXELS)
            {
                return; // Bỏ qua, không làm gì cả để tiết kiệm CPU
            }

            // Cập nhật lại vị trí chuột hiện tại
            lastMousePosition = e.Location;

            HitTestResult result = chart.HitTest(e.X, e.Y);

            // TỐI ƯU 2: Gộp nhóm điều kiện lọc nhanh bằng cấu trúc Switch-Case hoặc kiểm tra loại trừ
            bool isInActiveArea = result.ChartArea == ca &&
                                 (result.ChartElementType == ChartElementType.PlottingArea ||
                                  result.ChartElementType == ChartElementType.Gridlines ||
                                  result.ChartElementType == ChartElementType.DataPoint);

            if (!isInActiveArea)
            {
                lblToaDo.Text = "Rê chuột để xem tọa độ và dung tích";
                lblToaDo.BringToFront();
                return;
            }

            try
            {
                // Chuyển đổi pixel sang giá trị thực tế trục X, Y
                double zValue = ca.AxisY.PixelPositionToValue(e.Y);
                double xValueOADate = ca.AxisX.PixelPositionToValue(e.X);

                // Kiểm tra tính hợp lệ của phạm vi trục
                if (zValue < ca.AxisY.Minimum || zValue > ca.AxisY.Maximum ||
                    xValueOADate < ca.AxisX.Minimum || xValueOADate > ca.AxisX.Maximum)
                {
                    lblToaDo.Text = "Ngoài phạm vi dữ liệu biểu đồ";
                    return;
                }

                DateTime xDate = DateTime.FromOADate(xValueOADate);
                double interpolatedW = InterpolateW(zValue);

                // Nội suy một lần và tái sử dụng giá trị liên tục
                double interpolatedHCCN = InterpolateFixedValueForDate(xDate, fixedDatesForCurves, hccnValuesForCurves);
                double interpolatedDPPH = InterpolateFixedValueForDate(xDate, fixedDatesForCurves, dpphValuesForCurves);
                double interpolatedDPL = InterpolateFixedValueForDate(xDate, fixedDatesForCurves, dplValuesForCurves);

                string regionInfo = "";
                const double minHCCNValue = 17.00;

                // TỐI ƯU 3: Tối giản hóa biểu thức logic so sánh
                if (zValue <= interpolatedDPL && zValue >= interpolatedDPPH && !double.IsNaN(interpolatedDPL) && !double.IsNaN(interpolatedDPPH))
                {
                    // regionInfo = " (Vùng C:Cấp nước gia cường)";
                }
                else if (zValue <= interpolatedDPPH && zValue >= interpolatedHCCN && !double.IsNaN(interpolatedDPPH))
                {
                    // regionInfo = " (Vùng B:Cấp nước bình thường)";
                }
                else if (zValue <= interpolatedHCCN && zValue >= minHCCNValue && !double.IsNaN(interpolatedHCCN))
                {
                    //  regionInfo = " (Vùng A:Hạn chế cấp nước)";
                }

                lblToaDo.Text = $"Ngày: {xDate:dd/MM/yyyy HH:mm:ss} Z: {zValue:F2}m ⇄ W:{interpolatedW:F2}x10⁶m³{regionInfo}";
            }
            catch (Exception ex)
            {
                lblToaDo.Text = $"Lỗi khi đọc tọa độ: {ex.Message}";
            }
            finally
            {
                lblToaDo.BringToFront();
            }
        }
        private void chart_MouseLeave(object sender, EventArgs e)
        {
            lblToaDo.Text = "Rê chuột để xem tọa độ và dung tích";
            lblToaDo.BringToFront();
        }




        private async Task<(List<DateTime> Dates, List<double> Values)> GetFllow_Ho_FinalDataFromSqlAsync(
            DateTime startDate,
            DateTime endDate)
        {
            var dates = new List<DateTime>();
            var values = new List<double>();
            const double EPSILON = 0.000001;

            try
            {
                using (var dbContext = new ApplicationDbContext())
                {
                    // Sử dụng ToListAsync() để giải phóng UI Thread khi đang đợi SQL
                    var records = await dbContext.FT03s
                        .Where(x => x.IsDeleted == false // Ép kiểu so sánh rõ ràng
                                 && x.StationName == "Location_Info"
                                 && x.CreateAt >= startDate
                                 && x.CreateAt <= endDate
                                 && x.CreateAt != null)
                        .OrderBy(x => x.CreateAt)
                        .Select(x => new { x.CreateAt, x.Fllow_Ho_Final })
                        .ToListAsync();

                    foreach (var item in records)
                    {
                        dates.Add(item.CreateAt.Value);
                        if (item.Fllow_Ho_Final.HasValue && Math.Abs(item.Fllow_Ho_Final.Value) > EPSILON)
                        {
                            values.Add(item.Fllow_Ho_Final.Value);
                        }
                        else
                        {
                            values.Add(double.NaN);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi lấy dữ liệu: {ex.Message}");
            }

            return (dates, values);
        }
        private double InterpolateW(double zValue)
        {
            //   Console.WriteLine($"--- Bắt đầu InterpolateW cho Z = {zValue:F2} ---");

            if (interpolationLookupTable == null || !interpolationLookupTable.Any())
            {
                //       Console.WriteLine("Bảng nội suy trống hoặc chưa được tải.");
                return double.NaN;
            }

            int zIntegerPart = (int)Math.Floor(zValue);
            double zDecimalPart = zValue - zIntegerPart; // Phần thập phân (e.g., 0.56)
            int decimalIndex = (int)Math.Round(zDecimalPart * 100); // Index cột (e.g., 56)


            // Đảm bảo index cột nằm trong phạm vi hợp lệ [0, 99]
            decimalIndex = Math.Max(0, Math.Min(99, decimalIndex));
            //     Console.WriteLine($"Index thập phân sau khi kiểm tra: {decimalIndex}");


            // Trường hợp 1: Giá trị Z chính xác nằm trong bảng (Z nguyên + phần thập phân cụ thể)
            if (interpolationLookupTable.ContainsKey(zIntegerPart))
            {
                //        Console.WriteLine($"Tìm thấy phần nguyên Z ({zIntegerPart}) trong bảng.");
                List<double> wValues = interpolationLookupTable[zIntegerPart];
                if (decimalIndex < wValues.Count && !double.IsNaN(wValues[decimalIndex]))
                {
                    Console.WriteLine($"Tìm thấy giá trị trực tiếp tại Z={zIntegerPart}.{decimalIndex}: {wValues[decimalIndex]:F2}");
                    return wValues[decimalIndex];
                }

                // Tìm 2 điểm gần nhất trong hàng wValues
                int lowerBoundIndex = Math.Min(decimalIndex, wValues.Count - 1);
                int upperBoundIndex = Math.Min(decimalIndex + 1, wValues.Count - 1);

                //          Console.WriteLine($"Nội suy ngang: lowerBoundIndex={lowerBoundIndex}, upperBoundIndex={upperBoundIndex}");

                if (lowerBoundIndex == upperBoundIndex) // Chỉ có 1 điểm hoặc nằm ở cuối
                {
                    //            Console.WriteLine($"Chỉ có một điểm khả dụng ({wValues[lowerBoundIndex]:F2}) cho nội suy ngang.");
                    return wValues[lowerBoundIndex];
                }

                double x1 = (double)lowerBoundIndex / 100.0;
                double y1 = wValues[lowerBoundIndex];
                double x2 = (double)upperBoundIndex / 100.0;
                double y2 = wValues[upperBoundIndex];

                Console.WriteLine($"Điểm nội suy ngang: ({x1:F2}, {y1:F2}) và ({x2:F2}, {y2:F2})");

                // Nội suy tuyến tính theo phần thập phân
                if (Math.Abs(x2 - x1) > 1e-9) // Tránh chia cho 0
                {
                    double interpolatedHorizontalW = y1 + (y2 - y1) * ((zDecimalPart - x1) / (x2 - x1));
                    Console.WriteLine($"Giá trị nội suy ngang: {interpolatedHorizontalW:F2}");
                    return interpolatedHorizontalW;
                }
                Console.WriteLine($"x1 ({x1:F2}) bằng x2 ({x2:F2}), trả về y1 ({y1:F2}).");
                return y1; // Nếu x1 == x2, trả về y1
            }

            Console.WriteLine($"Không tìm thấy phần nguyên Z ({zIntegerPart}) trong bảng. Thử nội suy dọc.");

            // Trường hợp 2: Giá trị Z nằm giữa các hàng Z nguyên (ví dụ: 19.56 nhưng chỉ có hàng 19 và 20)
            // Cần tìm hai hàng Z nguyên gần nhất và nội suy dọc theo cột thập phân
            int floorZInteger = interpolationLookupTable.Keys.Where(k => k <= zValue).DefaultIfEmpty(-1).Max();
            int ceilZInteger = interpolationLookupTable.Keys.Where(k => k >= zValue).DefaultIfEmpty(-1).Min();

            Console.WriteLine($"Nội suy dọc: floorZInteger={floorZInteger}, ceilZInteger={ceilZInteger}");

            if (floorZInteger == -1 && ceilZInteger == -1)
            {
                Console.WriteLine("Không có dữ liệu để nội suy dọc (không tìm thấy Z nguyên phù hợp).");
                return double.NaN; // Không có dữ liệu để nội suy
            }

            // Lấy giá trị W tại phần thập phân tương ứng cho hai Z nguyên
            double wFloor = double.NaN;
            if (interpolationLookupTable.ContainsKey(floorZInteger) && decimalIndex < interpolationLookupTable[floorZInteger].Count)
            {
                wFloor = interpolationLookupTable[floorZInteger][decimalIndex];
                Console.WriteLine($"wFloor (tại {floorZInteger}.{decimalIndex}): {wFloor:F2}");
            }
            else
            {
                Console.WriteLine($"Không tìm thấy wFloor tại {floorZInteger}.{decimalIndex} hoặc là NaN.");
            }


            double wCeil = double.NaN;
            if (interpolationLookupTable.ContainsKey(ceilZInteger) && decimalIndex < interpolationLookupTable[ceilZInteger].Count)
            {
                wCeil = interpolationLookupTable[ceilZInteger][decimalIndex];
                Console.WriteLine($"wCeil (tại {ceilZInteger}.{decimalIndex}): {wCeil:F2}");
            }
            else
            {
                Console.WriteLine($"Không tìm thấy wCeil tại {ceilZInteger}.{decimalIndex} hoặc là NaN.");
            }

            // Nếu chỉ có một trong hai giá trị tồn tại hoặc hợp lệ, sử dụng giá trị đó
            if (double.IsNaN(wFloor) && !double.IsNaN(wCeil))
            {
                Console.WriteLine("Chỉ wCeil hợp lệ, trả về wCeil.");
                return wCeil;
            }
            if (!double.IsNaN(wFloor) && double.IsNaN(wCeil))
            {
                Console.WriteLine("Chỉ wFloor hợp lệ, trả về wFloor.");
                return wFloor;
            }
            if (double.IsNaN(wFloor) && double.IsNaN(wCeil))
            {
                Console.WriteLine("Cả wFloor và wCeil đều không hợp lệ, trả về NaN.");
                return double.NaN;
            }

            // Nội suy dọc giữa hai hàng Z nguyên
            if (Math.Abs(ceilZInteger - floorZInteger) > 1e-9) // Tránh chia cho 0
            {
                double interpolatedVerticalW = wFloor + (wCeil - wFloor) * ((zValue - floorZInteger) / (ceilZInteger - floorZInteger));
                Console.WriteLine($"Giá trị nội suy dọc: {interpolatedVerticalW:F2}");
                return interpolatedVerticalW;
            }
            Console.WriteLine($"floorZInteger ({floorZInteger}) bằng ceilZInteger ({ceilZInteger}), trả về wFloor ({wFloor:F2}).");
            return wFloor; // Nếu floorZInteger == ceilZInteger (zValue là số nguyên), trả về giá trị wFloor
        }


        // Phương thức tạo series
        private Series CreateSeries(string name, Color color, MarkerStyle marker, IEnumerable<DateTime> dates, double[] values)
        {
            Series series = new Series(name)
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 2, // Mặc định
                Color = color,
                MarkerStyle = marker,
                MarkerSize = 5, // Mặc định
                // Cập nhật ToolTip để hiển thị cả giá trị X (thời gian) và Y (mực nước)
                ToolTip = "Thời gian: #VALX{dd/MM/yyyy }\nMực nước: #VALY{F2} m",
                IsVisibleInLegend = true, // Luôn hiển thị trong chú giải
                LegendText = name // Thiết lập LegendText ban đầu là tên của series
            };

            // Lưu BorderWidth, MarkerSize và Color gốc khi tạo series
            originalBorderWidths.Add(series.Name, series.BorderWidth);
            originalMarkerSizes.Add(series.Name, series.MarkerSize);
            originalColors.Add(series.Name, series.Color); // Lưu màu sắc gốc
                                                           // Cấu hình Label hiển thị giá trị cho series DPPH
            if (name == "DPPH")
            {
                series.IsValueShownAsLabel = true;// hiển thị giá trị trên line
                series.LabelFormat = "{0:F2}";     // Định dạng giá trị với 2 chữ số thập phân
                series.LabelAngle = -90;           // Xoay nhãn để tiết kiệm không gian
                series.LabelBorderWidth = 0;       // Bỏ viền quanh nhãn
                series.Font = new Font("Arial", 7, FontStyle.Bold); // Phông chữ nhỏ hơn, in đậm
                series.LabelForeColor = Color.DarkMagenta; // Màu của nhãn
            }
            if (name == "DPL")
            {
                series.IsValueShownAsLabel = true;// hiển thị giá trị trên line
                series.LabelFormat = "{0:F2}";     // Định dạng giá trị với 2 chữ số thập phân
                series.LabelAngle = -90;           // Xoay nhãn để tiết kiệm không gian
                series.LabelBorderWidth = 0;       // Bỏ viền quanh nhãn
                series.Font = new Font("Arial", 7, FontStyle.Bold); // Phông chữ nhỏ hơn, in đậm
                series.LabelForeColor = Color.DarkMagenta; // Màu của nhãn
            }

            var dateList = dates.ToList();

            int count = Math.Min(dateList.Count, values.Length);

            for (int i = 0; i < count; i++)
            {
                series.Points.AddXY(dateList[i], values[i]);
            }


            return series;

        }



        private async void FrmHochua_Shown(object sender, EventArgs e)
        {
            await webView22.EnsureCoreWebView2Async();
            webView22.CoreWebView2.Navigate(UrlToLoad);
        }


    }
}
