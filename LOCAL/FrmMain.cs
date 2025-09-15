using Ahd.Core;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Office2010.Word;

using Domain;
using Domain.Entities;
using GemBox.Spreadsheet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RegistrationForm1
{
    public partial class FrmMain : Form
    {
        private Form currentChildForm = null;
        private Timer _timer; //. Timer để lấy dữ liệu từ Scada
        private Timer api_DTtimer; //. Timer lấy dữ liễu API dầu tiếng
        private Timer apiTimer;
        private Timer api_CDDTimer;
        private Timer _refreshTimer;


        private List<Station> _cachedStations; // Lưu trữ danh sách trạm đã tải
        private Dictionary<string, string> _stationIdToNameMap; // Ánh xạ StationId (uuid HOẶC code) sang Name

        private static readonly HttpClient client = new HttpClient();
        private const string API_STATIONS_URL = "https://kttv-open.vrain.vn/v1/stations";
        private const string API_STATS_URL = "https://kttv-open.vrain.vn/v1/stations/stats";
        private const string API_KEY = "4c81eccdb524441ba52c390d5b96e233";
        private int _logTime;
        private DateTime _startTime = DateTime.Now;
        private const string CONNECTION_STRING = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=DauTieng;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";


        private const double Z_OFFSET_FOR_RESULT2 = 0.01;// Offset cho giá trị Z của nội suy lần 2
      //  private double SelectedIndex = 0;
        private double z_ho_change_direction = 0.1; // Thay đổi 0.1 mỗi lần tick


        // Bảng tra cứu α theo a/H
        private static Dictionary<double, double> alphaTable = new Dictionary<double, double>
        {
            { 0.00, 0.611 }, { 0.10, 0.615 }, { 0.15, 0.618 }, { 0.20, 0.620 }, { 0.25, 0.622 },
            { 0.30, 0.625 }, { 0.35, 0.628 }, { 0.40, 0.632 }, { 0.45, 0.638 }, { 0.50, 0.645 },
            { 0.55, 0.650 }, { 0.60, 0.660 }, { 0.65, 0.672 }, { 0.70, 0.690 }, { 0.75, 0.705 }
        };
        // Bảng tra cứu 
        private readonly string csvInterpolationData = @"Z	0	1	2	3	4	5	6	7	8	9	10	11	12	13	14	15	16	17	18	19	20	21	22	23	24	25	26	27	28	29	30	31	32	33	34	35	36	37	38	39	40	41	42	43	44	45	46	47	48	49	50	51	52	53	54	55	56	57	58	59	60	61	62	63	64	65	66	67	68	69	70	71	72	73	74	75	76	77	78	79	80	81	82	83	84	85	86	87	88	89	90	91	92	93	94	95	96	97	98	99
13	203.00	203.40	203.80	204.20	204.60	205.00	205.40	205.80	206.20	206.60	207.00	207.60	208.20	208.80	209.40	210.00	210.60	211.20	211.80	212.40	213.00	213.40	213.80	214.20	214.60	215.00	215.40	215.80	216.20	216.60	217.00	217.60	218.20	218.80	219.40	220.00	220.60	221.20	221.80	222.40	223.00	223.45	223.90	224.35	224.80	225.25	225.70	226.15	226.60	227.05	227.50	228.05	228.60	229.15	229.70	230.25	230.80	231.35	231.90	232.45	233.00	233.60	234.20	234.80	235.40	236.00	236.60	237.20	237.80	238.40	239.00	239.60	240.20	240.80	241.40	242.00	242.60	243.20	243.80	244.40	245.00	245.50	246.00	246.50	247.00	247.50	248.00	248.50	249.00	249.50	250.00	250.20	250.40	250.60	250.80	251.00	251.20	251.40	251.60	251.80
14	252.00	253.00	254.00	255.00	256.00	257.00	258.00	259.00	260.00	261.00	262.00	262.50	263.00	263.50	264.00	264.50	265.00	265.50	266.00	266.50	267.00	267.65	268.30	268.95	269.60	270.25	270.90	271.55	272.20	272.85	273.50	274.15	274.80	275.45	276.10	276.75	277.40	278.05	278.70	279.35	280.00	280.50	281.00	281.50	282.00	282.50	283.00	283.50	284.00	284.50	285.00	285.70	286.40	287.10	287.80	288.50	289.20	289.90	290.60	291.30	292.00	292.70	293.40	294.10	294.80	295.50	296.20	296.90	297.60	298.30	299.00	299.70	300.40	301.10	301.80	302.50	303.20	303.90	304.60	305.30	306.00	306.70	307.40	308.10	308.80	309.50	310.20	310.90	311.60	312.30	313.00	313.60	314.20	314.80	315.40	316.00	316.60	317.20	317.80	318.40
15	319.00	319.60	320.20	320.80	321.40	322.00	322.60	323.20	323.80	324.40	325.00	325.72	326.44	327.16	327.88	328.60	329.32	330.04	330.76	331.48	332.20	332.83	333.46	334.09	334.72	335.35	335.98	336.61	337.24	337.87	338.50	339.19	339.88	340.57	341.26	341.95	342.64	343.33	344.02	344.71	345.40	346.06	346.72	347.38	348.04	348.70	349.36	350.02	350.68	351.34	352.00	352.66	353.32	353.98	354.64	355.30	355.96	356.62	357.28	357.94	358.60	359.26	359.92	360.58	361.24	361.90	362.56	363.22	363.88	364.54	365.20	365.78	366.36	366.94	367.52	368.10	368.68	369.26	369.84	370.42	371.00	371.74	372.48	373.22	373.96	374.70	375.44	376.18	376.92	377.66	378.40	379.06	379.72	380.38	381.04	381.70	382.36	383.02	383.68	384.34
16	385.00	385.85	386.70	387.55	388.40	389.25	390.10	390.95	391.80	392.65	393.50	394.35	395.20	396.05	396.90	397.75	398.60	399.45	400.30	401.15	402.00	402.85	403.70	404.55	405.40	406.25	407.10	407.95	408.80	409.65	410.50	410.95	411.40	411.85	412.30	412.75	413.20	413.65	414.10	414.55	415.00	416.25	417.50	418.75	420.00	421.25	422.50	423.75	425.00	426.25	427.50	428.35	429.20	430.05	430.90	431.75	432.60	433.45	434.30	435.15	436.00	436.85	437.70	438.55	439.40	440.25	441.10	441.95	442.80	443.65	444.50	445.35	446.20	447.05	447.90	448.75	449.60	450.45	451.30	452.15	453.00	453.85	454.70	455.55	456.40	457.25	458.10	458.95	459.80	460.65	461.50	462.35	463.20	464.05	464.90	465.75	466.60	467.45	468.30	469.15
17	470.00	470.97	471.94	472.91	473.88	474.85	475.82	476.79	477.76	478.73	479.70	480.67	481.64	482.61	483.58	484.55	485.52	486.49	487.46	488.43	489.40	490.37	491.34	492.31	493.28	494.25	495.22	496.19	497.16	498.13	499.10	500.07	501.04	502.01	502.98	503.95	504.92	505.89	506.86	507.83	508.80	509.77	510.74	511.71	512.68	513.65	514.62	515.59	516.56	517.53	518.50	519.47	520.44	521.41	522.38	523.35	524.32	525.29	526.26	527.23	528.20	529.17	530.14	531.11	532.08	533.05	534.02	534.99	535.96	536.93	537.90	538.87	539.84	540.81	541.78	542.75	543.72	544.69	545.66	546.63	547.60	548.57	549.54	550.51	551.48	552.45	553.42	554.39	555.36	556.33	557.30	558.27	559.24	560.21	561.18	562.15	563.12	564.09	565.06	566.03
18	567.00	568.12	569.24	570.36	571.48	572.60	573.72	574.84	575.96	577.08	578.20	579.32	580.44	581.56	582.68	583.80	584.92	586.04	587.16	588.28	589.40	590.52	591.64	592.76	593.88	595.00	596.12	597.24	598.36	599.48	600.60	601.72	602.84	603.96	605.08	606.20	607.32	608.44	609.56	610.68	611.80	612.92	614.04	615.16	616.28	617.40	618.52	619.64	620.76	621.88	623.00	624.12	625.24	626.36	627.48	628.60	629.72	630.84	631.96	633.08	634.20	635.32	636.44	637.56	638.68	639.80	640.92	642.04	643.16	644.28	645.40	646.52	647.64	648.76	649.88	651.00	652.12	653.24	654.36	655.48	656.60	657.72	658.84	659.96	661.08	662.20	663.32	664.44	665.56	666.68	667.80	668.92	670.04	671.16	672.28	673.40	674.52	675.64	676.76	677.88
19	679.00	680.19	681.38	682.57	683.76	684.95	686.14	687.33	688.52	689.71	690.90	692.09	693.28	694.47	695.66	696.85	698.04	699.23	700.42	701.61	702.80	703.99	705.18	706.37	707.56	708.75	709.94	711.13	712.32	713.51	714.70	715.89	717.08	718.27	719.46	720.65	721.84	723.03	724.22	725.41	726.60	727.79	728.98	730.17	731.36	732.55	733.74	734.93	736.12	737.31	738.50	739.69	740.88	742.07	743.26	744.45	745.64	746.83	748.02	749.21	750.40	751.59	752.78	753.97	755.16	756.35	757.54	758.73	759.92	761.11	762.30	763.49	764.68	765.87	767.06	768.25	769.44	770.63	771.82	773.01	774.20	775.39	776.58	777.77	778.96	780.15	781.34	782.53	783.72	784.91	786.10	787.29	788.48	789.67	790.86	792.05	793.24	794.43	795.62	796.81
20	798.00	799.40	800.80	802.20	803.60	805.00	806.40	807.80	809.20	810.60	812.00	813.40	814.80	816.20	817.60	819.00	820.40	821.80	823.20	824.60	826.00	827.40	828.80	830.20	831.60	833.00	834.40	835.80	837.20	838.60	840.00	841.40	842.80	844.20	845.60	847.00	848.40	849.80	851.20	852.60	854.00	855.40	856.80	858.20	859.60	861.00	862.40	863.80	865.20	866.60	868.00	869.40	870.80	872.20	873.60	875.00	876.40	877.80	879.20	880.60	882.00	883.40	884.80	886.20	887.60	889.00	890.40	891.80	893.20	894.60	896.00	897.40	898.80	900.20	901.60	903.00	904.40	905.80	907.20	908.60	910.00	911.40	912.80	914.20	915.60	917.00	918.40	919.80	921.20	922.60	924.00	925.40	926.80	928.20	929.60	931.00	932.40	933.80	935.20	936.60
21	938.00	939.63	941.26	942.89	944.52	946.15	947.78	949.41	951.04	952.67	954.30	955.93	957.56	959.19	960.82	962.45	964.08	965.71	967.34	968.97	970.60	972.23	973.86	975.49	977.12	978.75	980.38	982.01	983.64	985.27	986.90	988.53	990.16	991.79	993.42	995.05	996.68	998.31	999.94	1001.57	1003.20	1004.83	1006.46	1008.09	1009.72	1011.35	1012.98	1014.61	1016.24	1017.87	1019.50	1021.13	1022.76	1024.39	1026.02	1027.65	1029.28	1030.91	1032.54	1034.17	1035.80	1037.43	1039.06	1040.69	1042.32	1043.95	1045.58	1047.21	1048.84	1050.47	1052.10	1053.73	1055.36	1056.99	1058.62	1060.25	1061.88	1063.51	1065.14	1066.77	1068.40	1070.03	1071.66	1073.29	1074.92	1076.55	1078.18	1079.81	1081.44	1083.07	1084.70	1086.33	1087.96	1089.59	1091.22	1092.85	1094.48	1096.11	1097.74	1099.37
22	1101.00	1102.78	1104.56	1106.34	1108.12	1109.90	1111.68	1113.46	1115.24	1117.02	1118.80	1120.58	1122.36	1124.14	1125.92	1127.70	1129.48	1131.26	1133.04	1134.82	1136.60	1138.38	1140.16	1141.94	1143.72	1145.50	1147.28	1149.06	1150.84	1152.62	1154.40	1156.18	1157.96	1159.74	1161.52	1163.30	1165.08	1166.86	1168.64	1170.42	1172.20	1173.98	1175.76	1177.54	1179.32	1181.10	1182.88	1184.66	1186.44	1188.22	1190.00	1191.78	1193.56	1195.34	1197.12	1198.90	1200.68	1202.46	1204.24	1206.02	1207.80	1209.58	1211.36	1213.14	1214.92	1216.70	1218.48	1220.26	1222.04	1223.82	1225.60	1227.38	1229.16	1230.94	1232.72	1234.50	1236.28	1238.06	1239.84	1241.62	1243.40	1245.18	1246.96	1248.74	1250.52	1252.30	1254.08	1255.86	1257.64	1259.42	1261.20	1263.28	1265.36	1267.44	1269.52	1271.60	1273.68	1275.76	1277.84	1279.92
23	1282.00	1284.06	1286.12	1288.18	1290.24	1292.30	1294.36	1296.42	1298.48	1300.54	1302.60	1304.71	1306.82	1308.93	1311.04	1313.15	1315.26	1317.37	1319.48	1321.59	1323.70	1325.71	1327.72	1329.73	1331.74	1333.75	1335.76	1337.77	1339.78	1341.79	1343.80	1345.86	1347.92	1349.98	1352.04	1354.10	1356.16	1358.22	1360.28	1362.34	1364.40	1366.46	1368.52	1370.58	1372.64	1374.70	1376.76	1378.82	1380.88	1382.94	1385.00	1387.06	1389.12	1391.18	1393.24	1395.30	1397.36	1399.42	1401.48	1403.54	1405.60	1407.66	1409.72	1411.78	1413.84	1415.90	1417.96	1420.02	1422.08	1424.14	1426.20	1428.26	1430.32	1432.38	1434.44	1436.50	1438.56	1440.62	1442.68	1444.74	1446.80	1448.86	1450.92	1452.98	1455.04	1457.10	1459.16	1461.22	1463.28	1465.34	1467.40	1469.46	1471.52	1473.58	1475.64	1477.70	1479.76	1481.82	1483.88	1485.94
24	1488.00	1490.32	1492.64	1494.96	1497.28	1499.60	1501.92	1504.24	1506.56	1508.88	1511.20	1513.52	1515.84	1518.16	1520.48	1522.80	1525.12	1527.44	1529.76	1532.08	1534.40	1536.72	1539.04	1541.36	1543.68	1546.00	1548.32	1550.64	1552.96	1555.28	1557.60	1559.92	1562.24	1564.56	1566.88	1569.20	1571.52	1573.84	1576.16	1578.48	1580.80	1583.12	1585.44	1587.76	1590.08	1592.40	1594.72	1597.04	1599.36	1601.68	1604.00	1606.32	1608.64	1610.96	1613.28	1615.60	1617.92	1620.24	1622.56	1624.88	1627.20	1629.52	1631.84	1634.16	1636.48	1638.80	1641.12	1643.44	1645.76	1648.08	1650.40	1652.72	1655.04	1657.36	1659.68	1662.00	1664.32	1666.64	1668.96	1671.28	1673.60	1675.92	1678.24	1680.56	1682.88	1685.20	1687.52	1689.84	1692.16	1694.48	1696.80	1699.12	1701.44	1703.76	1706.08	1708.40	1710.72	1713.04	1715.36	1717.68
25	1720.00	1722.32	1724.64	1726.96	1729.28	1731.60	1733.92	1736.24	1738.56	1740.88	1743.20	1745.52	1747.84	1750.16	1752.48	1754.80	1757.12	1759.44	1761.76	1764.08	1766.40	1768.72	1771.04	1773.36	1775.68	1778.00	1780.32	1782.64	1784.96	1787.28	1789.60	1791.92	1794.24	1796.56	1798.88	1801.20	1803.52	1805.84	1808.16	1810.48	1812.80	1815.12	1817.44	1819.76	1822.08	1824.40	1826.72	1829.04	1831.36	1833.68	1836.00	1838.32	1840.64	1842.96	1845.28	1847.60	1849.92	1852.24	1854.56	1856.88	1859.20	1861.52	1863.84	1866.16	1868.48	1870.80	1873.12	1875.44	1877.76	1880.08	1882.40	1884.72	1887.04	1889.36	1891.68	1894.00	1896.32	1898.64	1900.96	1903.28	1905.60	1907.92	1910.24	1912.56	1914.88	1917.20	1919.52	1921.84	1924.16	1926.48	1928.80	1931.12	1933.44	1935.76	1938.08	1940.40	1942.72	1945.04	1947.36	1949.68
26	1952.00	1954.32	1956.64	1958.96	1961.28	1963.60	1965.92	1968.24	1970.56	1972.88	1975.20	1977.52	1979.84	1982.16	1984.48	1986.80	1989.12	1991.44	1993.76	1996.08	1998.40	2000.72	2003.04	2005.36	2007.68	2010.00	2012.32	2014.64	2016.96	2019.28	2021.60	2023.92	2026.24	2028.56	2030.88	2033.20	2035.52	2037.84	2040.16	2042.48	2044.80	2047.12	2049.44	2051.76	2054.08	2056.40	2058.72	2061.04	2063.36	2065.68	2068.00	2070.32	2072.64	2074.96	2077.28	2079.60	2081.92	2084.24	2086.56	2088.88	2091.20	2093.56	2095.92	2098.28	2100.64	2103.00	2105.36	2107.72	2110.08	2112.44	2114.80	2117.12	2119.44	2121.76	2124.08	2126.40	2128.72	2131.04	2133.36	2135.68	2138.00	2140.32	2142.64	2144.96	2147.28	2149.60	2151.92	2154.24	2156.56	2158.88	2161.20	2163.52	2165.84	2168.16	2170.48	2172.80	2175.12	2177.44	2179.76	2182.08";

        private double[] xValues;
        private Dictionary<double, double[]> table;

        public FrmMain()
        {
            InitializeComponent();
            Load += FrmMain_Load; // Đăng ký sự kiện Load của Form
            InitializeTimer();
            _stationIdToNameMap = new Dictionary<string, string>(); // Khởi tạo map

            dtpStartTime = new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "yyyy-MM-dd HH:mm:ss",
                Value = DateTime.Now.Date.AddHours(-1),
                Width = 180
            };
            dtpEndTime = new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "yyyy-MM-dd HH:mm:ss",
                Value = DateTime.Now,
                Width = 180
            };

        }
        IAhdDriverConnector driver;
        public interface ICalculatableForm
        {
            void PerformCalculations();
        }
        private async Task _refreshTimer_Tick(object sender, EventArgs e)
        {
            dtpEndTime.Value = DateTime.Now;
            // Thời gian bắt đầu là 10 phút trước thời gian hiện tại
            dtpStartTime.Value = DateTime.Now.AddMinutes(-10);
            // Tải lại dữ liệu thống kê mưa
            await LoadRainfallStatsData();
        }
        private async void FrmMain_Load(object sender, EventArgs e)
        {

            lblWelcome.Text = $"Xin chào: {Globalvariable.UserInfo.UserName} ({Globalvariable.UserInfo.PermissionScada.ToString()})";
            driver = AhdDriverConnectorProvider.GetAhdDriverConnector();

            if (!driver.IsStarted)
                // Change this line in FrmMain_Load:
                driver.Started += Driver_Started;
            else
                Driver_Started(driver, null);
            _startTime = DateTime.Now;

            timer1.Enabled = true;
            _refreshTimer.Start();
            await LoadRainfallStatsData();
            await LoadStationsData();


            InitializeDefaultValues();

            try
            {
                int cmbX = 0; // Chỉ số cột X mặc định
                table = ParseCsvToDictionary(csvInterpolationData, out xValues);

                //// Điền giá trị X vào ComboBox
                //foreach (var x in xValues)
                //{
                //    cmbX.Items.Add(x.ToString(CultureInfo.InvariantCulture));
                //}
                //if (cmbX.Items.Count > 0)
                //{
                //    cmbX.SelectedIndex = 0;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đọc bảng nội suy: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        // Parse CSV thành dictionary<double, double[]>
        private Dictionary<double, double[]> ParseCsvToDictionary(string csvData, out double[] xVals)
        {
            var result = new Dictionary<double, double[]>();
            // Sử dụng tab để phân tách các giá trị
            var lines = csvData.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            // Đọc tiêu đề cột X
            var headerParts = lines.First().Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
            xVals = headerParts.Skip(1)
                .Select(v => double.Parse(v.Trim(), CultureInfo.InvariantCulture))
                .ToArray();

            // Đọc các dòng dữ liệu
            foreach (var line in lines.Skip(1))
            {
                var parts = line.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
                if (parts.Length > 0)
                {
                    if (!double.TryParse(parts[0], NumberStyles.Float, CultureInfo.InvariantCulture, out double z))
                        throw new FormatException($"Không đọc được giá trị Z: {parts[0]}");

                    double[] values = new double[parts.Length - 1];
                    for (int i = 1; i < parts.Length; i++)
                    {
                        if (!double.TryParse(parts[i], NumberStyles.Float, CultureInfo.InvariantCulture, out double val))
                            throw new FormatException($"Không đọc được giá trị tại Z={z}, cột {i}: {parts[i]}");
                        values[i - 1] = val;
                    }

                    result[z] = values;
                }
            }

            return result;
        }
        // Thực hiện nội suy tuyến tính một giá trị duy nhất
        private double InterpolateSingleValue(Dictionary<double, double[]> table, double Z, int colIndex)
        {
            // Tìm 2 giá trị Z lân cận
            var sortedKeys = table.Keys.OrderBy(k => k).ToList();
            double z0 = sortedKeys.LastOrDefault(k => k <= Z);
            double z1 = sortedKeys.FirstOrDefault(k => k >= Z);

            // Xử lý các trường hợp ngoại lệ (giá trị Z nằm ngoài bảng)
            if (Z < sortedKeys.Min() || Z > sortedKeys.Max())
            {
                throw new ArgumentOutOfRangeException("Giá trị Z nằm ngoài phạm vi của bảng dữ liệu.");
            }
            if (Math.Abs(z0 - z1) < 1e-9) // Nếu giá trị Z trùng khớp
            {
                return table[z0][colIndex];
            }

            double[] row0 = table[z0];
            double[] row1 = table[z1];

            double t = (Z - z0) / (z1 - z0);

            return row0[colIndex] + (row1[colIndex] - row0[colIndex]) * t;
        }

        private void InitializeDefaultValues()
        {
            // Thiết lập giá trị mặc định cho các textbox
            //txtW.Text = "0";     // Bắt đầu từ giá trị W mặc định là 0
            //txtQCs1.Text = "10.66"; // Giá trị QCs1 mặc định
            //txtQCs2.Text = "10.80"; // Giá trị QCs2 mặc định
            //txtQCs3.Text = "2.00"; // Giá trị QCs3 mặc định
                                   //   txtQTr.Text = "20"; // Giá trị QTr mặc định


            // Khởi tạo txtResultOnZChange
            //if (txtResultOnZChange != null)
            //{
            //    txtResultOnZChange.Text = "";
            //}
            // Khởi tạo txtHieu
            //if (txtHieu != null)
            //{
            //    txtHieu.Text = "";
            //}
            // Khởi tạo txtTong
            //if (txtTong != null)
            //{
            //    txtTong.Text = "";
            //}
            // Khởi tạo txtQden
            //if (txtQden != null)
            //{
            //    txtQden.Text = "";
            //}
            // Khởi tạo txtQ1Denta
            //if (txtQ1Denta != null)
            //{
            //    txtQ1Denta.Text = "";
            //}
            //// Khởi tạo txtWTt
            //if (txtWTt != null)
            //{
            //    txtWTt.Text = "";
            //}
            //// Khởi tạo txtQtt
            //if (txtQTt != null)
            //{
            //    txtQTt.Text = "";
            //}
            //// Khởi tạo txtQdi
            //if (txtQdi != null)
            //{
            //    txtQdi.Text = "";
            //}
            // Khởi tạo txtResult2
            //if (txtResult2 != null)
            //{
            //    txtResult2.Text = "";
            //}
            //txtResult.Text = ""; // Khởi tạo txtResult rỗng
            //                     // _previousW_ho không còn được sử dụng trực tiếp để khởi tạo txtResultOnZChange nữa
        }

        private void InitializeTimer()
        { // Timer API dầu tiếng
            api_DTtimer = new Timer();
            api_DTtimer.Interval = 60000; // mỗi 60 giây
            api_DTtimer.Tick += async (s, ev) => await Api_DTtimer_Tick();
            api_DTtimer.Start();


            // Timer Login dữ liệu Scada
            _timer = new Timer();
            _timer.Interval = 1000; // 5 giây test, thực tế đặt 5 * 60 * 1000 = 5 phút
                                    // Fix for the CS0029 error: Replace the incorrect line with the correct event handler assignment.
            _timer.Tick += _timer_Tick;
            _timer.Start();


            // Timer API Bình Nhâm
            apiTimer = new Timer();
            apiTimer.Interval = 60000; // mỗi 60 giây
            apiTimer.Tick += async (s, ev) => await ApiTimer_Tick(s, ev);
            apiTimer.Start();
            // Timer API CDD
            api_CDDTimer = new Timer();
            api_CDDTimer.Tick += async (s, ev) => await api_CDDTimer_Tick(s, ev); // Gán đúng hàm xử lý
            api_CDDTimer.Interval = 60000; // 60 giây
            api_CDDTimer.Start();
            // Timer để lấy dữ liệu Quan trắc mưa
            _refreshTimer = new Timer();
            _refreshTimer.Tick += async (s, e) => await _refreshTimer_Tick(s, e);
            _refreshTimer.Interval = 10 * 60 * 1000; // 10 phút
            _refreshTimer.Start();

            client.DefaultRequestHeaders.Add("x-api-key", API_KEY);



        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            try
            {
                _timer.Enabled = false;

                if (Globalvariable.RealtimeDisplays == null || Globalvariable.RealtimeDisplays.Count == 0)
                    return;

                #region hien thi UI

                Globalvariable.InvokeIfRequired(this, () =>
                {
                    var location = Globalvariable.RealtimeDisplays?.FirstOrDefault(loc => loc.LocationId == 1);
                    if (Location != null)
                    {
                        foreach (var item in location.Stations)
                        {
                            if (item.Path == "Local Station/DauTieng/S71500/Station_1")
                            {
                                _labALDoor1_Station1.Text = item.Al_Door1.ToString();
                                _labALDoor2_Station1.Text = item.Al_Door2.ToString();
                             //   _labHT_Cylinder1_1.Text = item.HT_Cylinder1_1.ToString();
                            //    _labHT_Cylinder1_2.Text = item.HT_Cylinder1_2.ToString();



                            }
                            else if (item.Path == "Local Station/DauTieng/S71500/Station_2")
                            {
                                _labALDoor1_Station2.Text = item.Al_Door1.ToString();
                                _labALDoor2_Station2.Text = item.Al_Door2.ToString();
                            }
                            else if (item.Path == "Local Station/DauTieng/S71500/Station_3")
                            {
                                _labALDoor1_Station3.Text = item.Al_Door1.ToString();
                                _labALDoor2_Station3.Text = item.Al_Door2.ToString();
                            }
                        }

                        _labFllowHo.Text = location.Stations.FirstOrDefault(x => x.Path.Contains("Location_Info"))?.Fllow_Ho.ToString();

                 //       _labFlowHoFinal.Text = location.CalculatorValue.LuuLuongTong.ToString();

                       _labQi.Text = location.CalculatorValue.Q_i.ToString();
                       _labWtt.Text = location.CalculatorValue.W_tt.ToString();
                       _labQtt.Text = location.CalculatorValue.Q_tt.ToString();
                        _labW1_ho.Text = location.CalculatorValue.W1_ho.ToString();
                        _labW2_ho.Text = location.CalculatorValue.W2_ho.ToString();
                          _labQden.Text = location.CalculatorValue.Q_den.ToString();
                        _labWden.Text = location.CalculatorValue.W_den.ToString();
                        _labQtr.Text = location.CalculatorValue.Q_tr.ToString();
                        _labWtr.Text = location.CalculatorValue.W_tr.ToString();
                        _labWdi.Text = location.CalculatorValue.W_di.ToString();
                        _LabQdi.Text = location.CalculatorValue.Q_di.ToString();

                    }
                });
                #endregion

                #region Data log                
                _logTime = (int)(DateTime.Now - _startTime).TotalSeconds;

                if (_logTime >= Globalvariable.ConfigSystem.DataLogInterval)
                {
                    var dataLogs = new List<FT03>();
                    var createAt = DateTime.Now;
                    var createOperatorId = "System";

                    //datalog
                    foreach (var item in Globalvariable.RealtimeDisplays)
                    {
                        foreach (var itemStation in item.Stations)
                        {
                            var line = new FT03();

                            line.Id = Guid.NewGuid();
                            line.CreateAt = createAt;
                            line.CreateOperatorId = createOperatorId;
                            line.IsDeleted = false;
                            line.LogBaseInterval = false;
                            line.LocationId = item.LocationId;
                            line.LocationName = item.LocationName;

                            line.Fllow_DauTieng = item.CalculatorValue.Fllow_DauTieng;
                            line.Fllow_BenSuc = item.CalculatorValue.Fllow_BenSuc;
                            line.Fllow_SonDai = item.CalculatorValue.Fllow_SonDai;
                            line.Fllow_BinhNham = item.CalculatorValue.Fllow_BinhNham;
                            line.Fllow_BinhNham2 = item.CalculatorValue.Fllow_BinhNham2;
                            line.Fllow_TL_CDD = item.CalculatorValue.Fllow_TL_CDD;
                            line.Fllow_HL_TXL = item.CalculatorValue.Fllow_HL_TXL;
                            line.Total_Fllow = item.CalculatorValue.Total_Fllow;
                            line.API_DM_HoDT = item.CalculatorValue.API_DM_HoDT;
                            line.API_MinhHoa = item.CalculatorValue.API_MinhHoa;
                            line.API_MinhTam = item.CalculatorValue.API_MinhTam;
                            line.API_LocThien = item.CalculatorValue.API_LocThien;
                            line.API_LocNinh = item.CalculatorValue.API_LocNinh;
                            line.API_LocThanh = item.CalculatorValue.API_LocThanh;
                            line.API_ThanhLuong = item.CalculatorValue.API_ThanhLuong;
                            line.API_TanHoa1 = item.CalculatorValue.API_TanHoa1;
                            line.API_TanHoa2 = item.CalculatorValue.API_TanHoa2;
                            line.API_KaTum = item.CalculatorValue.API_KaTum;
                            line.API_TanThanh = item.CalculatorValue.API_TanThanh;
                            line.API_DongBan  = item.CalculatorValue.API_DongBan;
                            line.API_TanHa = item.CalculatorValue.API_TanHa;
                            line.API_Doi95 = item.CalculatorValue.API_Doi95;

                            line.Q_i = item.CalculatorValue.Q_i;
                            line.Q_den = item.CalculatorValue.Q_den;
                            line.Q_di = item.CalculatorValue.Q_di;
                            line.W1_ho = item.CalculatorValue.W1_ho;
                            line.W2_ho = item.CalculatorValue.W2_ho;
                            line.W1_ho_old = item.CalculatorValue.W1_ho_old;
                            line.W2_ho_old = item.CalculatorValue.W2_ho_old;
                            line.Q_tr = item.CalculatorValue.Q_tr;
                            line.Q_cs1 = item.CalculatorValue.Q_cs1;
                            line.Q_cs2 = item.CalculatorValue.Q_cs2;
                            line.Q_cs3 = item.CalculatorValue.Q_cs3;
                            line.Q_denta = item.CalculatorValue.Q_denta;
                            line.Q_tt = item.CalculatorValue.Q_tt;
                            line.W_tt = item.CalculatorValue.W_tt;
                            line.W_cs1 = item.CalculatorValue.W_cs1;
                            line.W_cs2 = item.CalculatorValue.W_cs2;
                            line.W_cs3 = item.CalculatorValue.W_cs3;

                            //   line.LuuLuong = item.CalculatorValue.lu;
                           //    line.LuuLuongTong = item.CalculatorValue.LuuLuongTong;

                            line.StationId = itemStation.StationId;
                            line.StationName = itemStation.StationName;
                            line.Path = itemStation.Path;

                            line.HT_Cylinder1_1 = itemStation.HT_Cylinder1_1;
                            line.HT_Cylinder1_2 = itemStation.HT_Cylinder1_2;
                            line.HT_Cylinder2_1 = itemStation.HT_Cylinder2_1;
                            line.HT_Cylinder2_2 = itemStation.HT_Cylinder2_2;
                            line.Door1_Aperture = itemStation.Door1_Aperture;
                            line.Door2_Aperture = itemStation.Door2_Aperture;
                            line.S1_Temp_Oil = itemStation.S1_Temp_Oil;
                            line.Pressure_Oil_Door1 = itemStation.Pressure_Oil_Door1;
                            line.Pressure_Oil_Door2 = itemStation.Pressure_Oil_Door2;
                            line.Fllow_Door1 = itemStation.Fllow_Door1;
                            line.Fllow_Door2 = itemStation.Fllow_Door2;
                            line.Fllow_Ho = itemStation.Fllow_Ho;

                            line.HT_Cylinder1_1_Offset = itemStation.HT_Cylinder1_1_Offset;
                            line.HT_Cylinder1_2_Offset = itemStation.HT_Cylinder1_2_Offset;
                            line.HT_Cylinder2_1_Offset = itemStation.HT_Cylinder2_1_Offset;
                            line.HT_Cylinder2_2_Offset = itemStation.HT_Cylinder2_2_Offset;
                            line.Door1_Aperture_Offset = itemStation.Door1_Aperture_Offset;
                            line.Door2_Aperture_Offset = itemStation.Door2_Aperture_Offset;
                            line.S1_Temp_Oil_Offset = itemStation.S1_Temp_Oil_Offset;
                            line.Pressure_Oil_Door1_Offset = itemStation.Pressure_Oil_Door1_Offset;
                            line.Pressure_Oil_Door2_Offset = itemStation.Pressure_Oil_Door2_Offset;
                            line.Fllow_Door1_Offset = itemStation.Fllow_Door1_Offset;
                            line.Fllow_Door2_Offset = itemStation.Fllow_Door2_Offset;
                            line.Fllow_Ho_Offset = itemStation.Fllow_Ho_Offset;

                            line.HT_Cylinder1_1_Final = itemStation.HT_Cylinder1_1_Final;
                            line.HT_Cylinder1_2_Final = itemStation.HT_Cylinder1_2_Final;
                            line.HT_Cylinder2_1_Final = itemStation.HT_Cylinder2_1_Final;
                            line.HT_Cylinder2_2_Final = itemStation.HT_Cylinder2_2_Final;
                            line.Door1_Aperture_Final = itemStation.Door1_Aperture_Final;
                            line.Door2_Aperture_Final = itemStation.Door2_Aperture_Final;
                            line.S1_Temp_Oil_Final = itemStation.S1_Temp_Oil_Final;
                            line.Pressure_Oil_Door1_Final = itemStation.Pressure_Oil_Door1_Final;
                            line.Pressure_Oil_Door2_Final = itemStation.Pressure_Oil_Door2_Final;
                            line.Fllow_Door1_Final = itemStation.Fllow_Door1_Final;
                            line.Fllow_Door2_Final = itemStation.Fllow_Door2_Final;
                            line.Fllow_Ho_Final = itemStation.Fllow_Ho_Final;

                            dataLogs.Add(line);
                        }
                    }

                    if (dataLogs.Count == 0)
                        return;

                    using var dbContext = new ApplicationDbContext();
                    dbContext.FT03s.AddRange(dataLogs);
                    dbContext.SaveChanges();//Luu thay doi vao db

                    _startTime = DateTime.Now;
                }
                #endregion
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                _timer.Enabled = true;
            }
        }

        private async Task api_CDDTimer_Tick(object sender, EventArgs ev)
        {
            string url = "https://apiv2.thuyloivietnam.vn/Api/getSoLieuQuanTrac?Key=apiktdlqtDauTieng&MaQuanTrac=7001";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        var response = await client.GetAsync(url);
                        response.EnsureSuccessStatusCode();
                        string json = await response.Content.ReadAsStringAsync();
                        var dataList = JsonConvert.DeserializeObject<List<SoLieuAPICDDModel>>(json);
                        if (dataList != null && dataList.Count > 0)
                        {
                            var latest = dataList.OrderByDescending(x => x.ThoiGian).First();

                            // Ghi async xuống PLC
                            await WriteAPI_CDDsync(latest.GiaTri);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Lỗi đọc API: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi gọi API:\n" + ex.Message);
            }
        }
        private async Task WriteAPI_CDDsync(double GT)
        {
            try
            {
                if (ahdDriverConnector1 == null)
                {
                    MessageBox.Show("Kết nối PLC chưa được khởi tạo.");
                    return;
                }
                //// Định dạng GT để chỉ lấy 2 số lẽ
                //double formattedGT = Math.Round(GT, 0);
                //await ahdDriverConnector1.WriteTagAsync(
                //    $"Local Station/DauTieng/S71500/API/Fllow_TL_CDD",
                //    GT.ToString("0.00"),
                //    WritePiority.High);
                Globalvariable.RealtimeDisplays.FirstOrDefault(loc => loc.LocationId == 1)
                .CalculatorValue.Fllow_TL_CDD = GT;


            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ghi PLC async: " + ex.Message);
            }
        }
        public class SoLieuAPICDDModel
        {
            public string ThoiGian { get; set; }
            public int MaQuanTrac { get; set; }
            public double GiaTri { get; set; }
        }


        private void Driver_Started(object sender, EventArgs e)
        {

            foreach (var item in Globalvariable.LocationsInfo)
            {
                foreach (var station in item.Stations.Where(x => x.Path.Contains("/Station_")))
                {

                    ahdDriverConnector1.GetTag($"{station.Path}/Remote").ValueChanged += Remote_ValueChanged;
                    Remote_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Remote")
                  , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Remote")
                  , "", ahdDriverConnector1.GetTag($"{station.Path}/Remote").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Local").ValueChanged += Local_ValueChanged;
                    Local_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Local")
                  , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Local")
                  , "", ahdDriverConnector1.GetTag($"{station.Path}/Local").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Auto").ValueChanged += Auto_ValueChanged;
                    Auto_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Auto")
                  , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Auto")
                  , "", ahdDriverConnector1.GetTag($"{station.Path}/Auto").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Man").ValueChanged += Man_ValueChanged;
                    Man_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Man")
                  , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Man")
                  , "", ahdDriverConnector1.GetTag($"{station.Path}/Man").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Local_Stop").ValueChanged += Local_Stop_ValueChanged;
                    Local_Stop_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Local_Stop")
                  , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Local_Stop")
                  , "", ahdDriverConnector1.GetTag($"{station.Path}/Local_Stop").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/DC1_Running").ValueChanged += DC1_Running_ValueChanged;
                    DC1_Running_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/DC1_Running")
                  , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/DC1_Running")
                  , "", ahdDriverConnector1.GetTag($"{station.Path}/DC1_Running").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/DC2_Running").ValueChanged += DC2_Running_ValueChanged;
                    DC2_Running_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/DC2_Running")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/DC2_Running")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/DC2_Running").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/DC3_Running").ValueChanged += DC3_Running_ValueChanged;
                    DC3_Running_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/DC3_Running")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/DC2_Running")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/DC3_Running").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Door1_Opening").ValueChanged += Door1_Opening_ValueChanged;
                    Door1_Opening_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Door1_Opening")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Door1_Opening")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Door1_Opening").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Door1_Closing").ValueChanged += Door1_Closing_ValueChanged;
                    Door1_Closing_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Door1_Closing")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Door1_Closing")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Door1_Closing").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Door2_Opening").ValueChanged += Door2_Opening_ValueChanged;
                    Door2_Opening_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Door2_Opening")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Door2_Opening")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Door2_Opening").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Door2_Closing").ValueChanged += Door2_Closing_ValueChanged;
                    Door2_Closing_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Door2_Closing")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Door2_Closing")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Door2_Closing").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Door1_Open").ValueChanged += Door1_Open_ValueChanged;
                    Door1_Open_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Door1_Open")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Door1_Open")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Door1_Open").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Door1_Close").ValueChanged += Door1_Close_ValueChanged;
                    Door1_Close_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Door1_Close")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Door1_Close")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Door1_Close").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Door2_Open").ValueChanged += Door2_Open_ValueChanged;
                    Door2_Open_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Door2_Open")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Door2_Open")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Door2_Open").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Door2_Close").ValueChanged += Door2_Close_ValueChanged;
                    Door2_Close_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Door2_Close")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Door2_Close")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Door2_Close").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_Opening").ValueChanged += Doorlock1_Opening_ValueChanged;
                    Doorlock1_Opening_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_Opening")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_Opening")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_Opening").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_Closing").ValueChanged += Doorlock1_Closing_ValueChanged;
                    Doorlock1_Closing_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_Closing")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_Closing")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_Closing").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_Opening").ValueChanged += Doorlock2_Opening_ValueChanged;
                    Doorlock2_Opening_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_Opening")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_Opening")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_Opening").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_Closing").ValueChanged += Doorlock2_Closing_ValueChanged;
                    Doorlock2_Closing_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_Closing")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_Closing")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_Closing").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_1Open").ValueChanged += Doorlock1_1Open_ValueChanged;
                    Doorlock1_1Open_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_1Open")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_1Open")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_1Open").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_1Close").ValueChanged += Doorlock1_1Close_ValueChanged;
                    Doorlock1_1Close_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_1Close")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_1Close")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_1Close").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_2Open").ValueChanged += Doorlock1_2Open_ValueChanged;
                    Doorlock1_2Open_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_2Open")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_1Open")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_2Open").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_2Close").ValueChanged += Doorlock1_2Close_ValueChanged;
                    Doorlock1_2Close_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_2Close")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_2Close")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Doorlock1_2Close").Value));
                    ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_1Open").ValueChanged += Doorlock2_1Open_ValueChanged;
                    Doorlock2_1Open_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_1Open")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_1Open")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_1Open").Value));
                    ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_1Close").ValueChanged += Doorlock2_1Close_ValueChanged;
                    Doorlock2_1Close_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_1Close")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_1Close")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_1Close").Value));
                    ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_2Open").ValueChanged += Doorlock2_2Open_ValueChanged;
                    Doorlock2_2Open_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_2Open")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_2Open")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_2Open").Value));
                    ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_2Close").ValueChanged += Doorlock2_2Close_ValueChanged;
                    Doorlock2_2Close_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_2Close")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_2Close")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Doorlock2_2Close").Value));
                    // Alarm
                    ahdDriverConnector1.GetTag($"{station.Path}/DC1_Over").ValueChanged += DC1_Over_ValueChanged;
                    DC1_Over_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/DC1_Over")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/DC1_Over")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/DC1_Over").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/DC2_Over").ValueChanged += DC2_Over_ValueChanged;
                    DC2_Over_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/DC2_Over")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/DC2_Over")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/DC2_Over").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/DC3_Over").ValueChanged += DC3_Over_ValueChanged;
                    DC3_Over_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/DC3_Over")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/DC3_Over")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/DC3_Over").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Door1_PressureHigh").ValueChanged += Door1_PressureHigh_ValueChanged;
                    Door1_PressureHigh_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Door1_PressureHigh")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Door1_PressureHigh")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Door1_PressureHigh").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Door1_PressureLow").ValueChanged += Door1_PressureLow_ValueChanged;
                    Door1_PressureLow_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Door1_PressureLow")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Door1_PressureLow")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Door1_PressureLow").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Door2_PressureHigh").ValueChanged += Door2_PressureHigh_ValueChanged;
                    Door2_PressureHigh_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Door2_PressureHigh")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Door2_PressureHigh")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Door2_PressureHigh").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Door2_PressureLow").ValueChanged += Door2_PressureLow_ValueChanged;
                    Door2_PressureLow_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Door2_PressureLow")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Door2_PressureLow")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Door2_PressureLow").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Al_Door1").ValueChanged += Al_Door1_ValueChanged;
                    Al_Door1_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Al_Door1")
                  , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Al_Door1")
                  , "", ahdDriverConnector1.GetTag($"{station.Path}/Al_Door1").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Al_Door2").ValueChanged += Al_Door2_ValueChanged;
                    Al_Door2_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Al_Door2")
                  , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Al_Door2")
                  , "", ahdDriverConnector1.GetTag($"{station.Path}/Al_Door2").Value));



                    ahdDriverConnector1.GetTag($"{station.Path}/HT_Cylinder1_2").ValueChanged += HT_Cylinder1_2_ValueChanged;
                    HT_Cylinder1_2_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/HT_Cylinder1_2")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/HT_Cylinder1_2")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/HT_Cylinder1_2").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/HT_Cylinder2_1").ValueChanged += HT_Cylinder2_1_ValueChanged;
                    HT_Cylinder2_1_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/HT_Cylinder2_1")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/HT_Cylinder2_1")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/HT_Cylinder2_1").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/HT_Cylinder2_2").ValueChanged += HT_Cylinder2_2_ValueChanged;
                    HT_Cylinder2_2_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/HT_Cylinder2_2")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/HT_Cylinder2_2")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/HT_Cylinder2_2").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Pressure_Oil_Door1").ValueChanged += Pressure_Oil_Door1_ValueChanged;
                    Pressure_Oil_Door1_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Pressure_Oil_Door1")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Pressure_Oil_Door1")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Pressure_Oil_Door1").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Pressure_Oil_Door2").ValueChanged += Pressure_Oil_Door2_ValueChanged;
                    Pressure_Oil_Door2_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Pressure_Oil_Door2")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Pressure_Oil_Door2")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Pressure_Oil_Door2").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/S1_Temp_Oil").ValueChanged += S1_Temp_Oil_ValueChanged;
                    S1_Temp_Oil_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/S1_Temp_Oil")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/S1_Temp_Oil")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/S1_Temp_Oil").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Door1_Aperture").ValueChanged += Door1_Aperture_ValueChanged;
                    Door1_Aperture_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Door1_Aperture")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Door1_Aperture")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Door1_Aperture").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Door2_Aperture").ValueChanged += Door2_Aperture_ValueChanged;
                    Door2_Aperture_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Door2_Aperture")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Door2_Aperture")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Door2_Aperture").Value));

                    ahdDriverConnector1.GetTag($"{station.Path}/Fllow_Door1").ValueChanged += Fllow_Door1_ValueChanged;
                    Fllow_Door1_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Fllow_Door1")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Fllow_Door1")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Fllow_Door1").Value));
                    ahdDriverConnector1.GetTag($"{station.Path}/Fllow_Door2").ValueChanged += Fllow_Door2_ValueChanged;
                    Fllow_Door2_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/Fllow_Door2")
                        , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/Fllow_Door2")
                        , "", ahdDriverConnector1.GetTag($"{station.Path}/Fllow_Door2").Value));


                    ahdDriverConnector1.GetTag($"{station.Path}/HT_Cylinder1_1").ValueChanged += HT_Cylinder1_1_ValueChanged;
                    HT_Cylinder1_1_ValueChanged(ahdDriverConnector1.GetTag($"{station.Path}/HT_Cylinder1_1")
                  , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{station.Path}/HT_Cylinder1_1")
                  , "", ahdDriverConnector1.GetTag($"{station.Path}/HT_Cylinder1_1").Value));




                }


                var stationLocation = item.Stations.FirstOrDefault(loc => loc.Path.Contains("/Location_Info"));

                if (stationLocation != null)
                {
                    // Replace this line:
                    ahdDriverConnector1.GetTag($"{stationLocation.Path}/Fllow_Ho").ValueChanged += Fllow_Ho_ValueChanged;
                    Fllow_Ho_ValueChanged(ahdDriverConnector1.GetTag($"{stationLocation.Path}/Fllow_Ho")
                  , new TagValueChangedEventArgs(ahdDriverConnector1.GetTag($"{stationLocation.Path}/Fllow_Ho")
                  , "", ahdDriverConnector1.GetTag($"{stationLocation.Path}/Fllow_Ho").Value));




                }

                //  }
            }

        }

        private async Task LoadStationsData()
        {
            _cachedStations = new List<Station>(); // Đảm bảo _cachedStations được khởi tạo
            _stationIdToNameMap.Clear(); // Xóa map cũ trước khi điền dữ liệu mới
            try
            {
                HttpResponseMessage response = await client.GetAsync(API_STATIONS_URL);
                if (!response.IsSuccessStatusCode)
                {
                    string errorMessage = $"API trả về lỗi: {(int)response.StatusCode} {response.ReasonPhrase}.";
                    string errorContent = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(errorContent))
                    {
                        errorMessage += $"\nChi tiết: {errorContent}";
                    }
                    //           MessageBox.Show(errorMessage, "Lỗi API", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                    dgvStations.DataSource = null;
                    return;
                }
                string responseBody = await response.Content.ReadAsStringAsync();
                _cachedStations = JsonConvert.DeserializeObject<List<Station>>(responseBody); // Lưu vào _cachedStations
                if (_cachedStations != null && _cachedStations.Count > 0)
                {
                    dgvStations.DataSource = _cachedStations; // Gán cho DataGridView          
                    // Điền dữ liệu vào _stationIdToNameMap, sử dụng station.Code làm khóa
                    foreach (var station in _cachedStations)
                    {
                        // Chúng ta cần đảm bảo rằng Code không null hoặc rỗng
                        if (!string.IsNullOrEmpty(station.Code) && !_stationIdToNameMap.ContainsKey(station.Code))
                        {
                            _stationIdToNameMap.Add(station.Code, station.Name);
                            // Debug: In ra các trạm được thêm vào map
                            Console.WriteLine($"Debug (LoadStationsData): Added station to map: Code={station.Code}, Name={station.Name}");
                        }
                        else if (string.IsNullOrEmpty(station.Code))
                        {
                            Console.WriteLine($"Debug (LoadStationsData): Station with Uuid={station.Uuid} has empty/null Code. Skipping for name map.");
                        }
                        else if (_stationIdToNameMap.ContainsKey(station.Code))
                        {
                            Console.WriteLine($"Debug (LoadStationsData): Duplicate Code '{station.Code}' found for station Uuid={station.Uuid}. Skipping this entry for name map.");
                        }
                    }
                    Console.WriteLine($"Debug (LoadStationsData): _stationIdToNameMap populated with {_stationIdToNameMap.Count} entries.");
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu trạm nào từ API.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvStations.DataSource = null;
                }
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"Lỗi HTTP khi tải danh sách trạm: {e.Message}\nVui lòng kiểm tra kết nối internet hoặc URL API.", "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (JsonException e)
            {
                MessageBox.Show($"Lỗi khi phân tích dữ liệu JSON cho trạm: {e.Message}\nCấu trúc dữ liệu nhận được có thể không khớp.", "Lỗi JSON", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception e)
            {
                MessageBox.Show($"Đã xảy ra lỗi không mong muốn khi tải trạm: {e.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private async Task<double> GetLastAccumulatedDepthForStation(string stationId, DateTime sevenAmCycleStart)
        {
            double lastAccumulatedDepth = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
                {
                    await connection.OpenAsync();
                    string sql = @"SELECT TOP 1 AccumulatedDepth
                                   FROM RealtimeQTM
                                   WHERE StationId = @StationId AND TimePoint >= @SevenAmCycleStart
                                   ORDER BY TimePoint DESC;"; // Lấy bản ghi mới nhất trong chu kỳ
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@StationId", stationId);
                        command.Parameters.AddWithValue("@SevenAmCycleStart", sevenAmCycleStart);
                        object result = await command.ExecuteScalarAsync();
                        if (result != null && result != DBNull.Value)
                        {
                            lastAccumulatedDepth = Convert.ToDouble(result);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Debug (GetLastAccumulatedDepth): Lỗi SQL khi lấy AccumulatedDepth cho trạm {stationId}: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Debug (GetLastAccumulatedDepth): Lỗi không mong muốn khi lấy AccumulatedDepth cho trạm {stationId}: {ex.Message}");
            }
            return lastAccumulatedDepth;
        }
        private async Task LoadRainfallStatsData()
        {
            DateTime now = DateTime.Now;
            // Xác định thời điểm 7h sáng của chu kỳ tích lũy hiện tại
            DateTime sevenAmCycleStart;
            if (now.Hour < 7) // Nếu hiện tại trước 7h sáng, chu kỳ bắt đầu từ 7h sáng ngày hôm trước
            {
                sevenAmCycleStart = now.Date.AddDays(-1).AddHours(7);
            }
            else // Nếu hiện tại từ 7h sáng trở đi, chu kỳ bắt đầu từ 7h sáng ngày hiện tại
            {
                sevenAmCycleStart = now.Date.AddHours(7);
            }

            // dtpStartTime và dtpEndTime được dùng để gọi API (lấy 10 phút gần nhất)
            // nhưng logic tích lũy sẽ dùng sevenAmCycleStart
            DateTime apiQueryStartTime = dtpStartTime.Value;
            DateTime apiQueryEndTime = dtpEndTime.Value;

            if (apiQueryStartTime >= apiQueryEndTime)
            {
                MessageBox.Show("Thời gian bắt đầu phải nhỏ hơn thời gian kết thúc.", "Lỗi tham số", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string formattedApiQueryStartTime = apiQueryStartTime.ToString("yyyy-MM-dd HH:mm:ss");
            string formattedApiQueryEndTime = apiQueryEndTime.ToString("yyyy-MM-dd HH:mm:ss");

            string statsUrl = $"{API_STATS_URL}?start_time={Uri.EscapeDataString(formattedApiQueryStartTime)}&end_time={Uri.EscapeDataString(formattedApiQueryEndTime)}&format=10m";
            dgvStats.DataSource = null;
            try
            {
                HttpResponseMessage response = await client.GetAsync(statsUrl);
                if (!response.IsSuccessStatusCode)
                {
                    string errorMessage = $"API trả về lỗi: {(int)response.StatusCode} {response.ReasonPhrase}.";
                    string errorContent = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(errorContent))
                    {
                        errorMessage += $"\nChi tiết: {errorContent}";
                    }
                    MessageBox.Show(errorMessage, "Lỗi API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dgvStats.DataSource = null;

                    return;
                }

                string responseBody = await response.Content.ReadAsStringAsync();
                RainfallStatsResponse statsResponse = JsonConvert.DeserializeObject<RainfallStatsResponse>(responseBody);

                var displayData = new List<object>();
                var latestDataPointsByStationFetched = new Dictionary<string, RealtimeRainfallData>();
                var accumulatedRainfallForDisplay = new Dictionary<string, double>(); // Dùng để hiển thị lên lblTotalRainfallSummary

                // Kiểm tra xem _stationIdToNameMap đã được điền chưa
                if (_stationIdToNameMap == null || _stationIdToNameMap.Count == 0)
                {
                    Console.WriteLine("Debug (LoadRainfallStatsData): WARNING! _stationIdToNameMap is empty or null. Attempting to re-load station data.");
                    await LoadStationsData(); // Thử tải lại dữ liệu trạm nếu map trống
                }

                if (statsResponse?.Data != null && statsResponse.Data.Count > 0)
                {
                    // Lấy AccumulatedDepth cuối cùng từ DB cho mỗi trạm trong chu kỳ 7h sáng hiện tại
                    // Chỉ lấy một lần trước khi xử lý dữ liệu mới để có cơ sở tích lũy
                    var initialAccumulatedDepths = new Dictionary<string, double>();
                    foreach (var measurement in statsResponse.Data)
                    {
                        if (!initialAccumulatedDepths.ContainsKey(measurement.StationId))
                        {
                            initialAccumulatedDepths.Add(measurement.StationId, await GetLastAccumulatedDepthForStation(measurement.StationId, sevenAmCycleStart));
                        }
                    }

                    foreach (var measurement in statsResponse.Data)
                    {
                        if (measurement.Value != null && measurement.Value.Any())
                        {
                            string stationId = measurement.StationId;
                            // Debug: In ra StationId từ dữ liệu stats
                            //          Console.WriteLine($"Debug (LoadRainfallStatsData): Processing rainfall data for StationId: {stationId}");

                            // Sử dụng _stationIdToNameMap để tra cứu tên
                            string stationName = "Không xác định";
                            if (_stationIdToNameMap.ContainsKey(stationId))
                            {
                                stationName = _stationIdToNameMap[stationId];
                                //              Console.WriteLine($"Debug (LoadRainfallStatsData): Found name for StationId '{stationId}': '{stationName}'");
                            }
                            else
                            {
                                //              Console.WriteLine($"Debug (LoadRainfallStatsData): Name NOT found in map for StationId '{stationId}'.");
                            }

                            // Lấy giá trị tích lũy ban đầu cho trạm này trong chu kỳ hiện tại
                            double currentAccumulatedDepth = initialAccumulatedDepths.ContainsKey(stationId) ? initialAccumulatedDepths[stationId] : 0;

                            foreach (var depthMeas in measurement.Value)
                            {
                                // Cộng dồn lượng mưa của điểm đo hiện tại vào tổng tích lũy
                                currentAccumulatedDepth += depthMeas.Depth;

                                // Dữ liệu cho DataGridView (có thể hiển thị depth tức thời hoặc accumulated)
                                displayData.Add(new
                                {
                                    StationId = stationId,
                                    Name = stationName, // Hiển thị tên trạm
                                    Timestamp = depthMeas.TimePoint,
                                    Depth = depthMeas.Depth,
                                    AccumulatedDepth = currentAccumulatedDepth, // Hiển thị giá trị tích lũy
                                    Unit = measurement.Unit
                                });

                                // Tạo bản ghi RealtimeRainfallData để lưu vào DB
                                RealtimeRainfallData currentRealtimeData = new RealtimeRainfallData
                                {
                                    StationId = stationId,
                                    Name = stationName, // Gán tên trạm
                                    TimePoint = depthMeas.TimePoint,
                                    Depth = depthMeas.Depth,
                                    Unit = measurement.Unit,
                                    AccumulatedDepth = currentAccumulatedDepth, // Giá trị tích lũy sẽ được lưu
                                    RecordedAt = DateTime.Now
                                };

                                // Chỉ giữ lại bản ghi mới nhất cho mỗi trạm trong dữ liệu vừa fetch
                                if (latestDataPointsByStationFetched.ContainsKey(stationId))
                                {
                                    // Cập nhật nếu bản ghi mới hơn HOẶC nếu bản ghi có AccumulatedDepth lớn hơn (để đảm bảo tính tích lũy)
                                    if (currentRealtimeData.TimePoint > latestDataPointsByStationFetched[stationId].TimePoint ||
                                        (currentRealtimeData.TimePoint == latestDataPointsByStationFetched[stationId].TimePoint &&
                                         currentRealtimeData.AccumulatedDepth > latestDataPointsByStationFetched[stationId].AccumulatedDepth))
                                    {
                                        latestDataPointsByStationFetched[stationId] = currentRealtimeData;
                                    }
                                }
                                else
                                {
                                    latestDataPointsByStationFetched.Add(stationId, currentRealtimeData);
                                }
                            }

                            // Cập nhật giá trị tích lũy cuối cùng cho mục đích hiển thị tổng
                            accumulatedRainfallForDisplay[stationId] = currentAccumulatedDepth;
                        }
                    }

                    if (displayData.Any())
                    {
                        dgvStats.DataSource = displayData;
                    }
                    else
                    {
                        dgvStats.DataSource = null;

                    }

                    // Hiển thị tổng lượng mưa từng trạm trong lblTotalRainfallSummary
                    if (accumulatedRainfallForDisplay.Any())
                    {
                        StringBuilder summaryBuilder = new StringBuilder();
                        summaryBuilder.AppendLine("Tổng lượng mưa tích lũy theo trạm (Chu kỳ 7h sáng):");
                        foreach (var entry in accumulatedRainfallForDisplay)
                        {
                            string unit = statsResponse.Data.FirstOrDefault(m => m.StationId == entry.Key)?.Unit ?? "mm";
                            // Lấy tên trạm từ _stationIdToNameMap
                            string name = _stationIdToNameMap.ContainsKey(entry.Key) ? _stationIdToNameMap[entry.Key] : entry.Key;
                            summaryBuilder.AppendLine($"- Trạm {name} ({entry.Key}): {entry.Value:F2} {unit}");
                        }

                    }
                    else
                    {

                    }
                }
                else
                {
                    MessageBox.Show(
                        "Không tìm thấy dữ liệu thống kê lượng mưa nào trong khoảng thời gian đã chọn " +
                        "hoặc API trả về dữ liệu rỗng/null cho khoảng thời gian này.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    dgvStats.DataSource = null;

                }

                // Lấy danh sách các bản ghi mới nhất từ Dictionary để lưu dữ liệu tức thời vào SQL
                List<RealtimeRainfallData> realTimeDataToSave = latestDataPointsByStationFetched.Values.ToList();
                Console.WriteLine($"Debug: Số lượng bản ghi tức thời mới nhất cần lưu vào SQL: {realTimeDataToSave.Count}");

                string saveStatusMessage = "";
                bool realtimeSaveSuccess = false;

                if (realTimeDataToSave.Any())
                {
                    try
                    {
                        await WriteQTM(latestDataPointsByStationFetched);
                        await SaveRealtimeMeasurementsToSql(realTimeDataToSave);
                        saveStatusMessage += $"Đã lưu {realTimeDataToSave.Count} bản ghi tức thời mới nhất vào SQL (bao gồm tổng lượng mưa tích lũy từ 7h sáng và Tên trạm).";
                        realtimeSaveSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        saveStatusMessage += $"Lỗi lưu tức thời vào SQL: {ex.Message}.";
                    }
                }
                else
                {
                    saveStatusMessage += "Không có dữ liệu tức thời mới nhất để lưu vào SQL.";
                }

                if (realtimeSaveSuccess)
                {

                }
                else
                {

                }


            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"Lỗi HTTP khi tải thống kê mưa: {e.Message}\nVui lòng kiểm tra kết nối internet hoặc URL API stats.", "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (JsonException e)
            {
                MessageBox.Show($"Lỗi khi phân tích dữ liệu JSON cho thống kê mưa: {e.Message}\nCấu trúc dữ liệu nhận được có thể không khớp.", "Lỗi JSON", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception e)
            {
                MessageBox.Show($"Đã xảy ra lỗi không mong muốn khi tải thống kê mưa: {e.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private async Task SaveRealtimeMeasurementsToSql(List<RealtimeRainfallData> realtimeData)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
                {
                    await connection.OpenAsync();

                    foreach (var data in realtimeData)
                    {
                        // Ghi log giá trị Unit và Name trước khi thêm vào tham số
                        Console.WriteLine($"Debug (SaveRealtimeMeasurementsToSql): Trying to save Realtime data for StationId={data.StationId}, Name='{data.Name}', TimePoint={data.TimePoint}, Depth={data.Depth}, Unit='{data.Unit}', AccumulatedDepth={data.AccumulatedDepth}, RecordedAt={data.RecordedAt}");

                        // Kiểm tra xem bản ghi cho StationId và TimePoint đã tồn tại chưa
                        string checkSql = @"SELECT COUNT(1) FROM RealtimeQTM
                                            WHERE StationId = @StationId AND TimePoint = @TimePoint;";

                        using (SqlCommand checkCommand = new SqlCommand(checkSql, connection))
                        {
                            checkCommand.Parameters.AddWithValue("@StationId", data.StationId);
                            checkCommand.Parameters.AddWithValue("@TimePoint", data.TimePoint);
                            int existingCount = (int)await checkCommand.ExecuteScalarAsync();

                            if (existingCount > 0)
                            {
                                string updateSql = @"UPDATE RealtimeQTM
                                                     SET Depth = @Depth,
                                                         Unit = @Unit,
                                                         AccumulatedDepth = @AccumulatedDepth,
                                                         Name = @Name,
                                                         RecordedAt = @RecordedAt
                                                     WHERE StationId = @StationId AND TimePoint = @TimePoint;";
                                using (SqlCommand updateCommand = new SqlCommand(updateSql, connection))
                                {
                                    updateCommand.Parameters.AddWithValue("@Depth", data.Depth);
                                    updateCommand.Parameters.AddWithValue("@Unit", (object)data.Unit ?? DBNull.Value);
                                    updateCommand.Parameters.AddWithValue("@AccumulatedDepth", (object)data.AccumulatedDepth ?? DBNull.Value);
                                    updateCommand.Parameters.AddWithValue("@Name", (object)data.Name ?? DBNull.Value);
                                    updateCommand.Parameters.AddWithValue("@RecordedAt", data.RecordedAt);
                                    updateCommand.Parameters.AddWithValue("@StationId", data.StationId);
                                    updateCommand.Parameters.AddWithValue("@TimePoint", data.TimePoint);
                                    int rowsAffected = await updateCommand.ExecuteNonQueryAsync();
                                    Console.WriteLine($"Debug (SaveRealtimeMeasurementsToSql): UPDATE affected {rowsAffected} rows for Realtime data StationId={data.StationId}, TimePoint={data.TimePoint}.");
                                }
                            }
                            else
                            {
                                string insertSql = @"INSERT INTO RealtimeQTM (StationId, TimePoint, Depth, Unit, AccumulatedDepth, Name, RecordedAt)
                                                     VALUES (@StationId, @TimePoint, @Depth, @Unit, @AccumulatedDepth, @Name, @RecordedAt);";
                                using (SqlCommand insertCommand = new SqlCommand(insertSql, connection))
                                {
                                    insertCommand.Parameters.AddWithValue("@StationId", data.StationId);
                                    insertCommand.Parameters.AddWithValue("@TimePoint", data.TimePoint);
                                    insertCommand.Parameters.AddWithValue("@Depth", data.Depth);
                                    insertCommand.Parameters.AddWithValue("@Unit", (object)data.Unit ?? DBNull.Value);
                                    insertCommand.Parameters.AddWithValue("@AccumulatedDepth", (object)data.AccumulatedDepth ?? DBNull.Value);
                                    insertCommand.Parameters.AddWithValue("@Name", (object)data.Name ?? DBNull.Value);
                                    insertCommand.Parameters.AddWithValue("@RecordedAt", data.RecordedAt);
                                    int rowsAffected = await insertCommand.ExecuteNonQueryAsync();
                                    Console.WriteLine($"Debug (SaveRealtimeMeasurementsToSql): INSERT affected {rowsAffected} rows for Realtime data StationId={data.StationId}, TimePoint={data.TimePoint}.");
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Lỗi SQL khi lưu dữ liệu tức thời: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi không mong muốn khi lưu dữ liệu tức thời vào SQL: {ex.Message}");
                throw;
            }
        }
        // Khu vực tạo Class Quan trắc mưa 
        // Định nghĩa lớp Station để ánh xạ dữ liệu JSON từ API /v1/station
        public class Station
        {
            [JsonProperty("uuid")]
            public string Uuid { get; set; }

            [JsonProperty("code")]
            public string Code { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("number")]
            public string Number { get; set; }

            [JsonProperty("latitude")]
            public double Latitude { get; set; }

            [JsonProperty("longitude")]
            public double Longitude { get; set; }

            [JsonProperty("area")]
            public string Area { get; set; }

            [JsonProperty("district")]
            public string District { get; set; }

            [JsonProperty("city")]
            public string City { get; set; }

            [JsonProperty("address")]
            public string Address { get; set; }

            [JsonProperty("altitude")]
            public object Altitude { get; set; }

            [JsonProperty("waterStationType")]
            public object WaterStationType { get; set; }
        }
        // Lớp mới để ánh xạ mỗi đối tượng bên trong mảng "value"
        public class DepthMeasurement
        {
            [JsonProperty("depth")]
            public double Depth { get; set; }

            [JsonProperty("time_point")]
            public DateTime TimePoint { get; set; }
        }
        // Định nghĩa lớp RainfallMeasurement để ánh xạ dữ liệu JSON cho mỗi bản ghi thống kê
        public class RainfallMeasurement
        {
            [JsonProperty("station_id")]
            public string StationId { get; set; }

            [JsonProperty("timestamp")]
            public DateTime? Timestamp { get; set; }

            [JsonProperty("value")]
            public List<DepthMeasurement> Value { get; set; }

            [JsonProperty("unit")]
            public string Unit { get; set; }
        }
        // Lớp mới để ánh xạ toàn bộ phản hồi từ API /v1/stations/stats
        public class RainfallStatsResponse
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("data")]
            public List<RainfallMeasurement> Data { get; set; }
        }

        // Lớp đại diện cho dữ liệu tức thời để lưu vào DB
        public class RealtimeRainfallData
        {
            public string StationId { get; set; }
            public string Name { get; set; } // Tên trạm
            public DateTime TimePoint { get; set; } // Thời điểm đo
            public double Depth { get; set; }
            public string Unit { get; set; }
            public double? AccumulatedDepth { get; set; } // Tổng lượng mưa tích lũy từ 7h sáng trong ngày
            public DateTime RecordedAt { get; set; } // Thời gian ứng dụng ghi nhận bản ghi này
        }
        // SingleOrArrayConverter không còn được sử dụng trực tiếp trong RainfallMeasurement.Value
        // nhưng được giữ lại nếu cần cho các trường hợp khác trong tương lai.
        public class SingleOrArrayConverter<T> : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return (objectType == typeof(List<T>));
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                JToken token = JToken.Load(reader);
                if (token.Type == JTokenType.Array)
                {
                    return token.ToObject<List<T>>();
                }
                else if (token.Type == JTokenType.Null)
                {
                    return null;
                }
                else
                {
                    return new List<T> { token.ToObject<T>() };
                }
            }

            public override bool CanWrite
            {
                get { return false; }
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }
        private async Task ApiTimer_Tick(object sender, EventArgs e)
        {
            string url = "https://input.dulieuthuyloivietnam.vn/latest?device_id=CR300-21411";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string json = await response.Content.ReadAsStringAsync();
                    var dataList = JsonConvert.DeserializeObject<List<SoLieuAPIBinhNhamModel>>(json);

                    if (dataList != null && dataList.Count > 0)
                    {
                        var latest = dataList.OrderByDescending(x => x.ts).First();

                        // Ghi async xuống PLC
                        await WriteToPLCAsync(latest.water_proof_1, latest.water_proof_2);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi đọc API: " + ex.Message);
                }
            }
        }
        private async Task WriteToPLCAsync(double wp1, double wp2)
        {
            try
            {
                // Định dạng số với 2 chữ số thập phân
                double formattedwp1 = Math.Round(wp1, 2);
                    double formattedwp2 = Math.Round(wp2, 2);

                //await ahdDriverConnector1.WriteTagAsync(
                //    $"Local Station/DauTieng/S71500/API/Fllow_BinhNham",
                //    wp1.ToString("0.00"),
                //    WritePiority.High);
                Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1)?
                    .CalculatorValue.Fllow_BinhNham = formattedwp1;

                //await ahdDriverConnector1.WriteTagAsync(
                //    $"Local Station/DauTieng/S71500/API/Fllow_BinhNham2",
                //    wp2.ToString("0.00"),
                //    WritePiority.High);
                Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1)?
                    .CalculatorValue.Fllow_BinhNham2 = formattedwp2;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi ghi PLC async: " + ex.Message);
            }
        }
        public class SoLieuAPIBinhNhamModel
        {
            public long ts { get; set; }
            public long c { get; set; }
            public double water_proof_1 { get; set; }
            public double water_proof_2 { get; set; }

            public DateTime Timestamp => DateTimeOffset.FromUnixTimeSeconds(ts).ToLocalTime().DateTime;
            public DateTime CreatedAt => DateTimeOffset.FromUnixTimeSeconds(c).ToLocalTime().DateTime;
        }
        public async Task Api_DTtimer_Tick()
        {
            string apiUrl = "http://dautiengphuochoa.com/api/getmn.aspx?key=dauhoaphuongtien%3b";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiData = await client.GetStringAsync(apiUrl);

                    // Danh sách các station cần parse
                    List<string> stations = new List<string> { "F01877", "F01203", "F01849" };

                    // Gọi hàm parse
                    var stationValues = ParseMultipleStationsFromAPI(apiData, stations);

                    // Hàm trợ giúp để chuyển đổi và xử lý giá trị decimal
                    Func<string, decimal> processFlowValue = (valueString) =>
                    {
                        decimal parsedValue;
                        if (decimal.TryParse(valueString, out parsedValue))
                        {
                            // Làm tròn đến 2 chữ số thập phân và sau đó chia cho 100
                            return Math.Round(parsedValue, 2) / 100m;
                        }
                        else
                        {
                            // Xử lý trường hợp không thể chuyển đổi, ví dụ: log lỗi và trả về 0
                            AppendLog($"Cảnh báo: Không thể chuyển đổi giá trị '{valueString}' từ API sang Decimal. Sử dụng giá trị 0.");
                            return 0m;
                        }
                    };

                    // Ghi từng trạm xuống PLC
                    foreach (var entry in stationValues)
                    {
                        string stationCode = entry.Key;
                        string rawValue = entry.Value; // Giá trị thô từ API

                        string tagName = "";
                        decimal processedDecimalValue = 0m; // Biến để lưu giá trị đã xử lý

                        switch (stationCode)
                        {
                            case "F01877": // Fllow_SonDai
                                tagName = "Fllow_SonDai";
                                processedDecimalValue = processFlowValue(rawValue);
                                // ghi xuống Model
                                Globalvariable.RealtimeDisplays.First(x => x.LocationId == 1)?
                               .CalculatorValue.Fllow_SonDai = (double)processedDecimalValue;
                                AppendLog($"✅ Cập nhật PLC: {tagName} = {processedDecimalValue} (Từ API: {rawValue})");
                                break;
                            case "F01203": // Fllow_BenSuc
                                tagName = "Fllow_BenSuc";
                                processedDecimalValue = processFlowValue(rawValue);
                                Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1)?
                                .CalculatorValue.Fllow_BenSuc = (double)processedDecimalValue;
                                AppendLog($"✅ Cập nhật PLC: {tagName} = {processedDecimalValue} (Từ API: {rawValue})");
                                break;
                            case "F01849": // Fllow_DauTieng
                                tagName = "Fllow_DauTieng";
                                processedDecimalValue = processFlowValue(rawValue);

                                Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1)?
                                    .CalculatorValue.Fllow_DauTieng = (double)processedDecimalValue;
                                AppendLog($"✅ Ghi PLC: {tagName} = {processedDecimalValue} (Từ API: {rawValue})");
                                break;
                            // Thêm các case khác nếu có các stationCode khác cần xử lý
                            default:
                                AppendLog($"Thông tin: Station Code '{stationCode}' không được mapping, bỏ qua xử lý.");
                                continue; // Bỏ qua entry này nếu không có mapping
                        }

                        if (!string.IsNullOrEmpty(tagName))
                        {
                            //  Ghi xuống PLC.Convert decimal thành string lại vì WriteTagAsync có lẽ mong đợi string.
                            //await ahdDriverConnector1.WriteTagAsync(
                            //    $"Local Station/DauTieng/S71500/API/{tagName}",
                            //    processedDecimalValue.ToString(), // Chuyển đổi decimal đã xử lý thành string
                            //    WritePiority.High
                            //);
                        }
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                // Bắt lỗi cụ thể khi có vấn đề về HTTP (ví dụ: không kết nối được API)
                AppendLog($"❌ Lỗi đọc API: An error occurred while sending the request. {ex.Message}");
            }
            catch (Exception ex)
            {
                AppendLog($"❌ Lỗi Timer_Tick: {ex.Message}");
            }
        }
        // Phương thức để ghi dữ liệu lượng mưa từ API xuống mô hình
        private async Task WriteQTM(Dictionary<string, RealtimeRainfallData> latestApiData)
        {
            try
            {
                // Xác định các ID trạm (hoặc các phần cuối của tag ID) mà bạn muốn ghi

                string[] stationIdsToProcess = { "610001", "610002", "610003", "610004", "610005", "610006", "610007", "610008", "610009", "610010", "610011", "610012", "610013" };

                // Lặp qua từng StationId mong muốn
                foreach (string stationId in stationIdsToProcess)
                {
                    // Tạo đường dẫn tag hoàn chỉnh
                    string tagPath = $"Local Station/DauTieng/S71500/API/{stationId}";

                    // Kiểm tra xem có dữ liệu mới nhất cho StationId này không
                    if (latestApiData.TryGetValue(stationId, out RealtimeRainfallData data))
                    {
                        // Lấy giá trị 'Depth' (lượng mưa) từ dữ liệu tức thời và định dạng
                        string valueToWrite = data.Depth.ToString("0.00");

                       Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1)?
                        .CalculatorValue.API_DM_HoDT = data.Depth;
                       


                        // Ghi log thành công
                        Console.WriteLine($"✅ Ghi PLC thành công: Tag '{tagPath}' = {valueToWrite} (Từ API: StationId={data.StationId}, TimePoint={data.TimePoint})");
                    }
                    else
                    {
                        // Xử lý trường hợp không tìm thấy dữ liệu cho một trạm cụ thể
                        // Bạn có thể ghi log, thông báo lỗi, hoặc bỏ qua nếu không cần thiết
                        Console.WriteLine($"Cảnh báo: Không tìm thấy dữ liệu tức thời cho trạm '{stationId}' để ghi.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Ghi lại lỗi nếu có bất kỳ ngoại lệ nào xảy ra trong quá trình ghi PLC
                Console.WriteLine($"Lỗi khi ghi giá trị tức thời vào PLC: {ex.Message}");
            }
        }

        // Hàm ghi log xuống TextBox
        private void AppendLog(string message)
        {
            //if (txtLog.InvokeRequired)
            //    txtLog.Invoke(new Action(() => txtLog.AppendText($"{DateTime.Now:HH:mm:ss} - {message}{Environment.NewLine}")));
            //else
            //    txtLog.AppendText($"{DateTime.Now:HH:mm:ss} - {message}{Environment.NewLine}");
        }
        private Dictionary<string, string> ParseMultipleStationsFromAPI(string apiData, List<string> stationCodes)
        {
            var results = new Dictionary<string, string>();

            // ✅ Update regex để parse số âm
            var regex = new System.Text.RegularExpressions.Regex(@"(F\d{5});\d{2}/\d{2}/\d{4};\d{2}:\d{2};value=(-?\d+);");

            var matches = regex.Matches(apiData);
            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                if (match.Success)
                {
                    string code = match.Groups[1].Value;
                    string value = match.Groups[2].Value;

                    if (stationCodes.Contains(code))
                    {
                        results[code] = value;
                    }
                }
            }

            return results;
        }


        private void Door2_PressureLow_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";
                var path = e.Tag.Parent.Path;
                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Door2_PressureLow = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                          .Where(x => x.Path == station.Path && x.TagName == "Door2_PressureLow" && x.IsDeleted != true)
                          .OrderByDescending(x => x.CreateAt)
                          .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Door2_PressureLow)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Door2_PressureLow";
                            Globalvariable.AlarmDataLog.Value = station.Door2_PressureLow;
                            Globalvariable.AlarmDataLog.Description = station.Door2_PressureLow == true ? "AS dầu cửa 2 thấp" : "AS dầu cửa 2 bình thường";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Door2_PressureHigh_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";
                var path = e.Tag.Parent.Path;
                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Door2_PressureHigh = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                          .Where(x => x.Path == station.Path && x.TagName == "Door2_PressureHigh" && x.IsDeleted != true)
                          .OrderByDescending(x => x.CreateAt)
                          .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Door2_PressureHigh)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Door2_PressureHigh";
                            Globalvariable.AlarmDataLog.Value = station.Door2_PressureHigh;
                            Globalvariable.AlarmDataLog.Description = station.Door2_PressureHigh == true ? "AS dầu cửa 2 cao" : "AS dầu cửa 2 bình thường";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }

        private void Door1_PressureLow_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Door1_PressureLow = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Door1_PressureLow" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Door1_PressureLow)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Door1_PressureLow";
                            Globalvariable.AlarmDataLog.Value = station.Door1_PressureLow;
                            Globalvariable.AlarmDataLog.Description = station.Door1_PressureLow == true ? "AS dầu cửa 1 thấp" : "AS dầu cửa 1 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Door1_PressureHigh_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Door1_PressureHigh = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Door1_PressureHigh" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Door1_PressureHigh)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Door1_PressureHigh";
                            Globalvariable.AlarmDataLog.Value = station.Door1_PressureHigh;
                            Globalvariable.AlarmDataLog.Description = station.Door1_PressureHigh == true ? "AS dầu cửa 1 cao" : "AS dầu cửa 1 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }


        }

        private void DC3_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.DC3_Over = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "DC3_Over" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.DC3_Over)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "DC3_Over";
                            Globalvariable.AlarmDataLog.Value = station.DC3_Over;
                            Globalvariable.AlarmDataLog.Description = station.DC3_Over == true ? "Quá tải bơm chốt" : "Bơm chốt bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void DC2_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.DC2_Over = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "DC2_Over" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.DC2_Over)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "DC2_Over";
                            Globalvariable.AlarmDataLog.Value = station.DC2_Over;
                            Globalvariable.AlarmDataLog.Description = station.DC2_Over == true ? "Quá tải bơm 2" : "Bơm 2 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void DC1_Over_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.DC1_Over = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "DC1_Over" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.DC1_Over)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "DC1_Over";
                            Globalvariable.AlarmDataLog.Value = station.DC1_Over;
                            Globalvariable.AlarmDataLog.Description = station.DC1_Over == true ? "Bơm 1 quá tải" : "Bơm 1 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Doorlock2_2Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Doorlock2_2Close = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Doorlock2_2Close" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Doorlock2_2Close)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Doorlock2_2Close";
                            Globalvariable.AlarmDataLog.Value = station.Doorlock2_2Close;
                            Globalvariable.AlarmDataLog.Description = station.Doorlock2_2Close == true ? "Chốt 2_2 đóng hết" : "Chốt 2_2 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Doorlock2_2Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Doorlock2_2Open = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Doorlock2_2Open" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Doorlock2_2Open)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Doorlock2_2Open";
                            Globalvariable.AlarmDataLog.Value = station.Doorlock2_2Open;
                            Globalvariable.AlarmDataLog.Description = station.Doorlock2_2Open == true ? "Chốt 2_2 mở hết" : "Chốt 2_2 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Doorlock2_1Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Doorlock2_1Close = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Doorlock2_1Close" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Doorlock2_1Close)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Doorlock2_1Close";
                            Globalvariable.AlarmDataLog.Value = station.Doorlock2_1Close;
                            Globalvariable.AlarmDataLog.Description = station.Doorlock2_1Close == true ? "Chốt 2_1 đóng hết" : "Chốt 2_1 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }
        private void Doorlock2_1Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Doorlock2_1Open = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Doorlock2_1Open" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Doorlock2_1Open)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Doorlock2_1Open";
                            Globalvariable.AlarmDataLog.Value = station.Doorlock2_1Open;
                            Globalvariable.AlarmDataLog.Description = station.Doorlock2_1Open == true ? "Chốt 2_1 mở hết" : "Chốt 2_1 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Doorlock1_2Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Doorlock1_2Close = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Doorlock1_2Close" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Doorlock1_2Close)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Doorlock1_2Close";
                            Globalvariable.AlarmDataLog.Value = station.Doorlock1_2Close;
                            Globalvariable.AlarmDataLog.Description = station.Doorlock1_2Close == true ? "Chốt 1_2 đóng hết" : "Chốt 1_2 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Doorlock1_2Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Doorlock1_2Open = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Doorlock1_2Open" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Doorlock1_2Open)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Doorlock1_2Open";
                            Globalvariable.AlarmDataLog.Value = station.Doorlock1_2Open;
                            Globalvariable.AlarmDataLog.Description = station.Doorlock1_2Open == true ? "Chốt 1_2 mở hết" : "Chốt 1_2 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Doorlock1_1Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Doorlock1_1Close = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Doorlock1_1Close" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Doorlock1_1Close)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Doorlock1_1Close";
                            Globalvariable.AlarmDataLog.Value = station.Doorlock1_1Close;
                            Globalvariable.AlarmDataLog.Description = station.Doorlock1_1Close == true ? "Chốt 1_1 đóng hết" : "Chốt 1_1 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Doorlock1_1Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Doorlock1_1Open = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Doorlock1_1Open" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Doorlock1_1Open)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Doorlock1_1Open";
                            Globalvariable.AlarmDataLog.Value = station.Doorlock1_1Open;
                            Globalvariable.AlarmDataLog.Description = station.Doorlock1_1Open == true ? "Chốt 1_1 mở hết" : "Chốt 1_1 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Doorlock2_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Doorlock2_Closing = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Doorlock2_Closing" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Doorlock2_Closing)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Doorlock2_Closing";
                            Globalvariable.AlarmDataLog.Value = station.Doorlock2_Closing;
                            Globalvariable.AlarmDataLog.Description = station.Doorlock2_Closing == true ? "Chốt 2 đang đóng" : "Chốt 2 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Doorlock2_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Doorlock2_Opening = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Doorlock2_Opening" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Doorlock2_Opening)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Doorlock2_Opening";
                            Globalvariable.AlarmDataLog.Value = station.Doorlock2_Opening;
                            Globalvariable.AlarmDataLog.Description = station.Doorlock2_Opening == true ? "Chốt 2 đang mở" : "Chốt 2 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Doorlock1_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Doorlock1_Closing = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Doorlock1_Closing" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Doorlock1_Closing)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Doorlock1_Closing";
                            Globalvariable.AlarmDataLog.Value = station.Doorlock1_Closing;
                            Globalvariable.AlarmDataLog.Description = station.Doorlock1_Closing == true ? "Chốt 1 đang đóng" : "Chốt 1 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Doorlock1_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Doorlock1_Opening = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Doorlock1_Opening" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Doorlock1_Opening)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Doorlock1_Opening";
                            Globalvariable.AlarmDataLog.Value = station.Doorlock1_Opening;
                            Globalvariable.AlarmDataLog.Description = station.Doorlock1_Opening == true ? "Chốt 1 đang mở" : "Chốt 1 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Door2_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Door2_Close = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Door2_Close" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Door2_Close)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Door2_Close";
                            Globalvariable.AlarmDataLog.Value = station.Door2_Close;
                            Globalvariable.AlarmDataLog.Description = station.Door2_Close == true ? "Cửa 2 đóng hết" : "Cửa 2 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Door2_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Door2_Open = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Door2_Open" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Door2_Open)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Door2_Open";
                            Globalvariable.AlarmDataLog.Value = station.Door2_Open;
                            Globalvariable.AlarmDataLog.Description = station.Door2_Open == true ? "Cửa 2 mở hết" : "Cửa 2 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Door1_Close_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Door1_Close = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Door1_Close" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Door1_Close)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Door1_Close";
                            Globalvariable.AlarmDataLog.Value = station.Door1_Close;
                            Globalvariable.AlarmDataLog.Description = station.Door1_Close == true ? "Cửa 1 đóng hết" : "Cửa 1 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Door1_Open_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Door1_Open = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Door1_Open" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Door1_Open)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Door1_Open";
                            Globalvariable.AlarmDataLog.Value = station.Door1_Open;
                            Globalvariable.AlarmDataLog.Description = station.Door1_Open == true ? "Cửa 1 mở hết" : "Cửa 1 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Door2_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Door2_Closing = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Door2_Closing" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Door2_Closing)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Door2_Closing";
                            Globalvariable.AlarmDataLog.Value = station.Door2_Closing;
                            Globalvariable.AlarmDataLog.Description = station.Door2_Closing == true ? "Cửa 2 đang đóng" : "Cửa 2 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Door2_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Door2_Opening = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Door2_Opening" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Door2_Opening)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Door2_Opening";
                            Globalvariable.AlarmDataLog.Value = station.Door2_Opening;
                            Globalvariable.AlarmDataLog.Description = station.Door2_Opening == true ? "Cửa 2 đang mở" : "Cửa 2 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Door1_Closing_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Door1_Closing = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Door1_Closing" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Door1_Closing)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Door1_Closing";
                            Globalvariable.AlarmDataLog.Value = station.Door1_Closing;
                            Globalvariable.AlarmDataLog.Description = station.Door1_Closing == true ? "Cửa 1 đang đóng" : "Cửa 1 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Door1_Opening_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Door1_Opening = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Door1_Opening" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Door1_Opening)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Door1_Opening";
                            Globalvariable.AlarmDataLog.Value = station.Door1_Opening;
                            Globalvariable.AlarmDataLog.Description = station.Door1_Opening == true ? "Cửa 1 đang mở" : "Cửa 1 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void DC3_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.DC3_Running = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "DC3_Running" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.DC3_Running)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "DC3_Running";
                            Globalvariable.AlarmDataLog.Value = station.DC3_Running;
                            Globalvariable.AlarmDataLog.Description = station.DC3_Running == true ? "Bơm 3 đang chạy" : "Bơm 3 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void DC2_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.DC2_Running = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "DC2_Running" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.DC2_Running)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "DC2_Running";
                            Globalvariable.AlarmDataLog.Value = station.DC2_Running;
                            Globalvariable.AlarmDataLog.Description = station.DC2_Running == true ? "Bơm 2 đang chạy" : "Bơm 2 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void DC1_Running_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.DC1_Running = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "DC1_Running" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.DC1_Running)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "DC1_Running";
                            Globalvariable.AlarmDataLog.Value = station.DC1_Running;
                            Globalvariable.AlarmDataLog.Description = station.DC1_Running == true ? "Bơm 1 đang chạy" : "Bơm 1 bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Local_Stop_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Local_Stop = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Local_Stop" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Local_Stop)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Local_Stop";
                            Globalvariable.AlarmDataLog.Value = station.Local_Stop;
                            Globalvariable.AlarmDataLog.Description = station.Local_Stop == true ? "Đang dừng khẩn" : "Bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Man_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Man = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Man" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Man)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Man";
                            Globalvariable.AlarmDataLog.Value = station.Man;
                            Globalvariable.AlarmDataLog.Description = station.Man == true ? "Đang chạy tay" : "Bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Auto_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Auto = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Auto" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Auto)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Auto";
                            Globalvariable.AlarmDataLog.Value = station.Auto;
                            Globalvariable.AlarmDataLog.Description = station.Auto == true ? "Đang chạy tự động" : "Bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Local_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Local = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Local" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Local)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Local";
                            Globalvariable.AlarmDataLog.Value = station.Local;
                            Globalvariable.AlarmDataLog.Description = station.Local == true ? "Đang chạy tại chổ" : "Bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Remote_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Remote = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Remote" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Remote)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Remote";
                            Globalvariable.AlarmDataLog.Value = station.Remote;
                            Globalvariable.AlarmDataLog.Description = station.Remote == true ? "Đang chạy từ xa" : "Bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }

        private void Al_Door2_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Al_Door2 = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Al_Door2" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Al_Door2)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Al_Door2";
                            Globalvariable.AlarmDataLog.Value = station.Al_Door2;
                            Globalvariable.AlarmDataLog.Description = station.Al_Door2 == true ? "Đang lệch cửa 2" : "Bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }
        private void Al_Door1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";

                var path = e.Tag.Parent.Path;

                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);

                if (station != null)
                {
                    station.Al_Door1 = e.NewValue == "1" ? true : false;

                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        //alarms
                        var checkExist = dbContext.FT04s
                            .Where(x => x.Path == station.Path && x.TagName == "Al_Door1" && x.IsDeleted != true)
                            .OrderByDescending(x => x.CreateAt)
                            .FirstOrDefault();

                        if (checkExist == null || checkExist.Value != station.Al_Door1)
                        {
                            Globalvariable.AlarmDataLog.Id = Guid.NewGuid();
                            Globalvariable.AlarmDataLog.CreateAt = createAt;
                            Globalvariable.AlarmDataLog.CreateOperatorId = createOperatorId;
                            Globalvariable.AlarmDataLog.IsDeleted = false;
                            Globalvariable.AlarmDataLog.LocationId = location.LocationId;
                            Globalvariable.AlarmDataLog.LocationName = location.LocationName;
                            Globalvariable.AlarmDataLog.StationId = station.StationId;
                            Globalvariable.AlarmDataLog.StationName = station.StationName;
                            Globalvariable.AlarmDataLog.Path = station.Path;
                            Globalvariable.AlarmDataLog.TagName = "Al_Door1";
                            Globalvariable.AlarmDataLog.Value = station.Al_Door1;
                            Globalvariable.AlarmDataLog.Description = station.Al_Door1 == true ? "Đang lệch cửa 1" : "Bình thường.";

                            dbContext.FT04s.Add(Globalvariable.AlarmDataLog);

                            dbContext.SaveChanges();//Luu thay doi vao db
                        }
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }

        }

        private void HT_Cylinder1_1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";
                var path = e.Tag.Parent.Path;
                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);
                if (station != null)
                {

                    station.HT_Cylinder1_1 = double.TryParse(e.NewValue.ToString(), out double newValue1) ? Math.Round(newValue1, 2) : 0;

                    //tinh toans

                    station.HT_Cylinder1_1_Final = Math.Round(station.HT_Cylinder1_1 + station.HT_Cylinder1_1_Offset ?? 0, 2);


                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        dbContext.SaveChanges();//Luu thay doi vao db
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }
        private void HT_Cylinder1_2_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";
                var path = e.Tag.Parent.Path;
                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);
                if (station != null)
                {
                    station.HT_Cylinder1_2 = double.TryParse(e.NewValue, out double newValue) ? Math.Round(newValue, 2) : 0;
                    //tinh toans
                    station.HT_Cylinder1_2_Final = Math.Round(station.HT_Cylinder1_2 + station.HT_Cylinder1_2_Offset ?? 0, 2);
                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();
                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };
                            dbContext.FT02s.Add(newLine);
                        }
                        dbContext.SaveChanges();//Luu thay doi vao db
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }
        private void HT_Cylinder2_1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";
                var path = e.Tag.Parent.Path;
                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);
                if (station != null)
                {
                    station.HT_Cylinder2_1 = double.TryParse(e.NewValue, out double newValue) ? Math.Round(newValue, 2) : 0;
                    //tinh toans
                    station.HT_Cylinder2_1_Final = Math.Round(station.HT_Cylinder2_1 + station.HT_Cylinder2_1_Offset ?? 0, 2);
                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();
                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };
                            dbContext.FT02s.Add(newLine);
                        }
                        dbContext.SaveChanges();//Luu thay doi vao db
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }
        private void HT_Cylinder2_2_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";
                var path = e.Tag.Parent.Path;
                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);
                if (station != null)
                {
                    station.HT_Cylinder2_2 = double.TryParse(e.NewValue, out double newValue) ? Math.Round(newValue, 2) : 0;
                    //tinh toans
                    station.HT_Cylinder2_2_Final = Math.Round(station.HT_Cylinder2_2 + station.HT_Cylinder2_2_Offset ?? 0, 2);
                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();
                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };
                            dbContext.FT02s.Add(newLine);
                        }
                        dbContext.SaveChanges();//Luu thay doi vao db
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }
        private void Pressure_Oil_Door1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";
                var path = e.Tag.Parent.Path;
                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);
                if (station != null)
                {
                    station.Pressure_Oil_Door1 = double.TryParse(e.NewValue, out double newValue) ? Math.Round(newValue, 2) : 0;
                    //tinh toans
                    station.Pressure_Oil_Door1_Final = Math.Round(station.Pressure_Oil_Door1 + station.Pressure_Oil_Door1_Offset ?? 0, 2);
                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();
                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };
                            dbContext.FT02s.Add(newLine);
                        }
                        dbContext.SaveChanges();//Luu thay doi vao db
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }
        private void Pressure_Oil_Door2_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";
                var path = e.Tag.Parent.Path;
                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);
                if (station != null)
                {
                    station.Pressure_Oil_Door2 = double.TryParse(e.NewValue, out double newValue) ? Math.Round(newValue, 2) : 0;
                    //tinh toans
                    station.Pressure_Oil_Door2_Final = Math.Round(station.Pressure_Oil_Door2 + station.Pressure_Oil_Door2_Offset ?? 0, 2);
                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();
                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };
                            dbContext.FT02s.Add(newLine);
                        }
                        dbContext.SaveChanges();//Luu thay doi vao db
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }
        private void S1_Temp_Oil_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";
                var path = e.Tag.Parent.Path;
                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);
                if (station != null)
                {
                    station.S1_Temp_Oil = double.TryParse(e.NewValue, out double newValue) ? Math.Round(newValue, 2) : 0;
                    //tinh toans
                    station.S1_Temp_Oil_Final = Math.Round(station.S1_Temp_Oil + station.S1_Temp_Oil_Offset ?? 0, 2);
                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();
                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };
                            dbContext.FT02s.Add(newLine);
                        }
                        dbContext.SaveChanges();//Luu thay doi vao db
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }
        // Door1_Aperture
        private void Door1_Aperture_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";
                var path = e.Tag.Parent.Path;
                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);
                if (station != null)
                {
                    station.Door1_Aperture = double.TryParse(e.NewValue, out double newValue) ? Math.Round(newValue, 2) : 0;
                    //tinh toans
                    station.Door1_Aperture_Final = Math.Round(station.Door1_Aperture + station.Door1_Aperture_Offset ?? 0, 2);
                   
                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();
                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };
                            dbContext.FT02s.Add(newLine);
                        }
                        dbContext.SaveChanges();//Luu thay doi vao db
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }
        private void Door2_Aperture_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";
                var path = e.Tag.Parent.Path;
                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);
                if (station != null)
                {
                    station.Door2_Aperture = double.TryParse(e.NewValue, out double newValue) ? Math.Round(newValue, 2) : 0;
                    //tinh toans
                    station.Door2_Aperture_Final = Math.Round(station.Door2_Aperture + station.Door2_Aperture_Offset ?? 0, 2);
                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();
                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };
                            dbContext.FT02s.Add(newLine);
                        }
                        dbContext.SaveChanges();//Luu thay doi vao db
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }

        //Fllow_Door1
        private void Fllow_Door1_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";
                var path = e.Tag.Parent.Path;
                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);
                if (station != null)
                {
                    station.Fllow_Door1 = double.TryParse(e.NewValue, out double newValue) ? Math.Round(newValue, 2) : 0;
                    //tinh toans
                    station.Fllow_Door1_Final = Math.Round(station.Fllow_Door1 + station.Fllow_Door1_Offset ?? 0, 2);
                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();
                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };
                            dbContext.FT02s.Add(newLine);
                        }
                        dbContext.SaveChanges();//Luu thay doi vao db
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }
        private void Fllow_Door2_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {
                var createAt = DateTime.Now;
                var createOperatorId = "System";
                var path = e.Tag.Parent.Path;
                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);
                if (station != null)
                {
                    station.Fllow_Door2 = double.TryParse(e.NewValue, out double newValue) ? Math.Round(newValue, 2) : 0;
                    //tinh toans
                    station.Fllow_Door2_Final = Math.Round(station.Fllow_Door2 + station.Fllow_Door2_Offset ?? 0, 2);
                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();
                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };
                            dbContext.FT02s.Add(newLine);
                        }
                        dbContext.SaveChanges();//Luu thay doi vao db
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }



        private void Fllow_Ho_ValueChanged(object sender, TagValueChangedEventArgs e)
        {
            try
            {


                var createAt = DateTime.Now;
                var createOperatorId = "System";
                var path = e.Tag.Parent.Path;
                var location = Globalvariable.RealtimeDisplays.FirstOrDefault(x => x.LocationId == 1);
                var station = location?.Stations.FirstOrDefault(x => x.Path == path);
                if (station != null)
                {
                    station.Fllow_Ho = double.TryParse(e.NewValue.ToString(), out double newValue) ? Math.Round(newValue, 2) : 0;
               //    station.Door1_Aperture = double.TryParse(e.NewValue.ToString(), out double newValueDoor1) ? Math.Round(newValueDoor1, 2) : 0;


                    //tinh toans

                    station.Fllow_Ho_Final = Math.Round(station.Fllow_Ho + station.Fllow_Ho_Offset ?? 0, 2);
                 
                    int selectedColumnIndex = 0; // mặc định chọn cột đầu tiên nếu không có combobox

                    double Z1 = ((double)station.Fllow_Ho); // Gán mực nước hồ
                    double NoisuyW1_Ho = InterpolateSingleValue(table, Z1, selectedColumnIndex);
                    // tính toán các giá trị khác dựa trên Z và bảng nội suy 
                    double Z2 = ((double)station.Fllow_Ho) + 0.01;
                    double NoisuyW2_Ho = InterpolateSingleValue(table, Z2, selectedColumnIndex);
                    // Tính Q tt và Wtt
                    double TinhWtt = Math.Round((0.024 * (Globalvariable.RealtimeDisplays.FirstOrDefault().CalculatorValue.W1_ho + Globalvariable.RealtimeDisplays.FirstOrDefault().CalculatorValue.W2_ho)/2.0)/30.0,2) ;
                    double TinhQtt = Math.Round((TinhWtt * 1000000.0) / (24.0 * 60.0 * 60.0),2); // 24*60*60 = 86400
                    // Tính lưu lượng qua tràn Qtr và tổng lưu lượng Q
                    double phi = Globalvariable.ConfigSystem.ParametterConfig.HeSoLuuToc_Phi;
                    double H0 = Math.Round(station.Fllow_Ho - Globalvariable.ConfigSystem.ParametterConfig.CaoTrinhNguongTran_Zn ?? 0, 2);
                    double aOverH = Math.Round((Globalvariable.ConfigSystem.ParametterConfig.DoMoCuaTran_h) / H0, 2);
                    double anpha = Math.Round(GetAlphaFromTable(aOverH), 2);
                    double alpha = anpha;
                    double h = Globalvariable.ConfigSystem.ParametterConfig.DoMoCuaTran_h;  
                    // h gán bằng giá trị Tag Door1_Aperture            
                //   double h = Globalvariable.RealtimeDisplays.FirstOrDefault().Stations.FirstOrDefault(x => x.Path == path).Door1_Aperture ?? 0;                
                    double SumB = Math.Round(10.0 * Globalvariable.ConfigSystem.ParametterConfig.SoCuaMo, 2);
                    int c = Globalvariable.ConfigSystem.ParametterConfig.SoCuaMo;
                    double g = Globalvariable.ConfigSystem.ParametterConfig.GiaToc_G;                 
                    double alphaTimesH = alpha * h;
                    double insideSqrt = 2 * Globalvariable.ConfigSystem.ParametterConfig.GiaToc_G * (H0 - alphaTimesH);
                    double sqrt = Math.Sqrt(insideSqrt);       
                    double Qi = Math.Round((double)Globalvariable.ConfigSystem.ParametterConfig.HeSoLuuToc_Phi * anpha * h * SumB * Math.Sqrt(insideSqrt), 1);
                    // Qi = φ × α × h × Σb × √(2 × g × (Ho - α × h)) -> là của 1 cửa( Q_i)
                    //  double Qtr = 6.0 * Qi; // Qtr = 6.0 x Qi (lưu lượng qua tràn)
                    double Qtr = 20.0;
                    double TinhWtr = Math.Round(Qtr * (86400.0 / 1000000.0),2); // *24*60*60/1000000)
                    // Tính Q đên = [(W2-W1)*1000000/86400] + Qtr + Qcs1 + Qcs2 + Qcs3 +[(W2+W1)/2*2.4%/30*1000000/86400] 
                    double TinhQCs1 = Globalvariable.ConfigSystem.ParametterConfig.Q_CongSo1;
                    double TinhQCs2 = Globalvariable.ConfigSystem.ParametterConfig.Q_CongSo2;
                    double TinhQCs3 = Globalvariable.ConfigSystem.ParametterConfig.Q_CongSo3;
                    double TinhTongW = Globalvariable.RealtimeDisplays.FirstOrDefault().CalculatorValue.W1_ho + Globalvariable.RealtimeDisplays.FirstOrDefault().CalculatorValue.W2_ho;
                    double TinhHieuW = Globalvariable.RealtimeDisplays.FirstOrDefault().CalculatorValue.W2_ho - Globalvariable.RealtimeDisplays.FirstOrDefault().CalculatorValue.W1_ho;
                    double Qden1 = Math.Round((TinhHieuW * 1000000.0) / 86400.0, 2);
                    double Qden2 = Math.Round((TinhTongW / 2.0) * (0.024 / 30.0) * (1000000.0 / 86400.0), 2);
                    double Qden = Math.Round(Qden1 + Qden2 + Qtr + TinhQCs1 + TinhQCs2 + TinhQCs3, 2);
                    double TinhWden = Math.Round(Qden * (86400.0 / 1000000.0),2); //*24*60*60/1000000)
                    // Tính Q đi Qdi =QTr + QCs1 + QCs2 +QCs3 + QTt
                    double TinhQdi = Math.Round(Qtr + TinhQCs1 + TinhQCs2 + TinhQCs3 + TinhQtt,2);
                    double TinhWdi = Math.Round(TinhQdi * (86400.0 / 1000000.0),2); // *24*60*60/1000000)
                      //    location.CalculatorValue.W1_ho = value; // W_ho = φ × H0  (lưu lượng qua hồ)
                      // ghi xuống SQL

                    //    location.CalculatorValue.LuuLuongTong = Math.Round((double)station.Fllow_Ho_Final * Globalvariable.ConfigSystem.ParametterConfig.HeSoLuuToc_Phi, 2);

                    //     location.CalculatorValue.LuuLuongTong = TinhToan((double)station.Fllow_Ho_Final);

                   location.CalculatorValue.W_di = TinhWdi;
                    location.CalculatorValue.Q_di = TinhQdi;
                    location.CalculatorValue.W_den = TinhWden;
                    location.CalculatorValue.Q_den = Qden;

                    location.CalculatorValue.Q_i = Qi;
                   location.CalculatorValue.Q_tr = Qtr;  
                    location.CalculatorValue.W_tr = TinhWtr;
                    location.CalculatorValue.W1_ho = NoisuyW1_Ho;          
                    location.CalculatorValue.W2_ho = NoisuyW2_Ho;
                    location.CalculatorValue.W_tt = TinhWtt;
                    location.CalculatorValue.Q_tt = TinhQtt;
                    location.CalculatorValue.Q_cs1 = TinhQCs1;
                    location.CalculatorValue.Q_cs2 = TinhQCs2;
                    location.CalculatorValue.Q_cs3 = TinhQCs3;


                    using (var dbContext = new ApplicationDbContext())
                    {
                        //Real time
                        var check = dbContext.FT02s.FirstOrDefault();

                        if (check != null)
                        {
                            check.C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays);
                            check.UpdateAt = createAt;
                            check.UpdateOperatorId = createOperatorId;
                        }
                        else
                        {
                            var newLine = new FT02
                            {
                                Id = Guid.NewGuid(),
                                C000 = JsonConvert.SerializeObject(Globalvariable.RealtimeDisplays),
                                IsDeleted = false,
                                CreateAt = createAt,
                                CreateOperatorId = createOperatorId,
                            };

                            dbContext.FT02s.Add(newLine);
                        }

                        dbContext.SaveChanges();//Luu thay doi vao db
                    }
                }
            }
            catch (Exception ex) { Log.Error(ex, $"From TagValueChanged {e.Tag.Path}"); }
        }
        private double TinhToan(double tagValue)
        {
            return Math.Round(tagValue * Globalvariable.ConfigSystem.ParametterConfig.HeSoLuuToc_Phi, 2);

        }





        private void button1_Click(object sender, EventArgs e)
        {
            FrmHochua mn = new FrmHochua();
            OpenFormInPanel(mn, "Hồ chứa");
            mn.UrlToLoad = "https://vrain.vn/61/overview?public_map=windy";

            mn.Show();

        }

        private async void button4_Click(object sender, EventArgs e)
        {
            await LoadRainfallStatsData();
        }


        private void bntNhaplieu_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu chưa đăng nhập
            if (string.IsNullOrEmpty(PermissionManager.CurrentUsername))
            {
                // Hiện form đăng nhập
                FrmLogin frmLogin = new FrmLogin();
                if (frmLogin.ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show("Bạn cần đăng nhập để sử dụng chức năng này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Sau khi đăng nhập xong, kiểm tra quyền
            if (!PermissionManager.CheckPermissionWithMessage("edit_data"))
                return;

            // Nếu đủ quyền, mở form nhập liệu
            FrmNhaplieu frm = new FrmNhaplieu();
            frm.ShowDialog();
        }

        




        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FrmLogin.currentLoginLogId > 0)
            {
                FrmLogin.SaveLogoutTime(FrmLogin.currentLoginLogId);
            }
        }



        private void OpenFormInPanel(Form childForm, string title)
        {
            try
            {
                // Nếu đang mở chính form đó thì không cần mở lại
                if (currentChildForm == childForm)
                    return;

                // Ẩn form cũ nếu đang có
                if (currentChildForm != null)
                {
                    currentChildForm.Hide(); // hoặc: panelDesktop.Controls.Remove(currentChildForm);
                }

                // Nếu form chưa được add thì cấu hình
                if (!panelDesktop.Controls.Contains(childForm))
                {
                    childForm.TopLevel = false;
                    childForm.FormBorderStyle = FormBorderStyle.None;
                    childForm.Dock = DockStyle.Fill;
                    panelDesktop.Controls.Add(childForm);
                }

                currentChildForm = childForm;
                childForm.BringToFront();
                childForm.Show();

                // Cập nhật tiêu đề
                if (label1 != null)
                    label1.Text = title;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở form: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bnt_Tran_Click(object sender, EventArgs e)
        {
            FrmTran data = new FrmTran();
            OpenFormInPanel(data, "Hệ thống tràn");
        }

        private void bnt_TramMN_Click(object sender, EventArgs e)
        {
            FrmMucnuoc mn = new FrmMucnuoc();
            OpenFormInPanel(mn, "Mức Nước");
        }

        private void bnt_TrangChu_Click(object sender, EventArgs e)
        {
            FrmHome H = new FrmHome();
            OpenFormInPanel(H, " GIÁM SÁT CỦA TRÀN HỒ DẦU TIẾNG");
        }

        private void bnt_CanhBao_Click(object sender, EventArgs e)
        {
            FrmCanhBao canhBao = new FrmCanhBao(this);
            OpenFormInPanel(canhBao, " Thông Tin Cảnh Báo");
        }

        private void bnt_BaoCao_Click(object sender, EventArgs e)
        {
            FrmBaoCao baocao = new FrmBaoCao();
            OpenFormInPanel(baocao, "Báo Cáo");
        }

        private void bnt_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void bnt_CaiDat_Click(object sender, EventArgs e)
        {
            FrmCaiDat caiDat = new FrmCaiDat();
            OpenFormInPanel(caiDat, " CÀI ĐẶT");
        }
        private void bnt_LogIn_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu đã đăng nhập thì không cần đăng nhập lại
            //if (!string.IsNullOrEmpty(PermissionManager.CurrentUsername))
            //{
            //    MessageBox.Show("Bạn đã đăng nhập rồi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            this.Hide();
            // Hiện form đăng nhập
            FrmLogin login = new FrmLogin();

            if (login.ShowDialog() == DialogResult.OK)
            {
                // Đăng nhập thành công, cập nhật lại thông tin người dùng
                lblWelcome.Text = $"Xin chào: {PermissionManager.CurrentUsername} ({PermissionManager.CurrentUserRole})";
                //   btnOpenRegister.Enabled = PermissionManager.CurrentUserRole == "Admin";
                bntEditdata.Enabled = PermissionManager.CurrentUserRole == "Admin";
            }

        }

        private void bnt_User_Click(object sender, EventArgs e)
        {
            FrmUserManager U = new FrmUserManager();
            OpenFormInPanel(U, "Hệ thống quản lý tài khoản");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToLongTimeString();
            //  lblDate.Text = DateTime.Now.ToLongDateString();
            lblDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //  lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            if (ahdDriverConnector1.ConnectionStatus == ConnectionStatus.Connected)
            {
                labDriverStatus.BackColor = Color.Green;
                labDriverStatus.Text = "PLC Đang Kết Nối";
            }
            else
            {
                labDriverStatus.BackColor = Color.Red;
                labDriverStatus.Text = "PLC Mất Kết Nối";
            }
        }

        private void bntThongtin_Click(object sender, EventArgs e)
        {
            FrmThongtin tt = new FrmThongtin();
            OpenFormInPanel(tt, " THÔNG TIN HỔ TRỢ VẬN HÀNH");
        }

        // khu vực nội suy công thức 
        private static double GetAlphaFromTable(double aOverH)
        {
            // Làm tròn a/H đến 2 chữ số thập phân để so sánh chính xác hơn
            aOverH = Math.Round(aOverH, 3);

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

        private void bntEditdata_Click(object sender, EventArgs e)
        {
            FrmDieukhienscada frm = new FrmDieukhienscada();
            OpenFormInPanel(frm, "ĐIỀU KHIỂN SCADA TỪ XA");
        }
    }
}