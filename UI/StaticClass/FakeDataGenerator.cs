using Domain;

namespace UI.StaticClass;

public static class FakeDataGenerator
{
    private static readonly Random rnd = new Random();

    public static RealtimeDisplayModel GenerateLocation()
    {
        return new RealtimeDisplayModel
        {
            LocationId = 1,
            LocationName = "Hồ Dầu Tiếng",
            Stations = new List<TagsStation>
        {
            GenerateStation(1, "Station_1"),
            GenerateStation(2, "Station_2"),
            GenerateStation(3, "Station_3"),
            GenerateStation(4, "Location_Info")
        },
            CalculatorValue = new CalculatorValueModel
            {
                API_Fllow_DauTieng = RandomDouble(0, 100),
                API_Fllow_BenSuc = RandomDouble(0, 100),
                API_Fllow_SonDai = RandomDouble(0, 100),
                API_Fllow_BinhNham = RandomDouble(0, 100),
                API_Fllow_BinhNham2 = RandomDouble(0, 100),
                API_Fllow_TL_CDD = RandomDouble(0, 100),
                API_Fllow_HL_TXL = RandomDouble(0, 100),
                
                Q_i_total = RandomDouble(0, 200),
                Q_tt = RandomDouble(200, 1000),
                Q_den = RandomDouble(0, 200),
                Q_di = RandomDouble(0, 200),

                Q_tr = RandomDouble(0, 10),
                Q_cs1 = RandomDouble(1000, 5000),
                Q_cs2 = RandomDouble(1000, 5000),
                Q_cs3 = RandomDouble(1000, 5000),

                //mapping API field
                API_D_DM_HoDT = RandomDouble(0, 100),
                API_D_MinhHoa = RandomDouble(0, 100),
                API_D_MinhTam = RandomDouble(0, 100),
                API_D_LocThien = RandomDouble(0, 100),
                API_D_LocNinh = RandomDouble(0, 100),
                API_D_LocThanh = RandomDouble(0, 100),
                API_D_ThanhLuong = RandomDouble(0, 100),
                API_D_TanHoa1 = RandomDouble(0, 100),
                API_D_TanHoa2 = RandomDouble(0, 100),
                API_D_KaTum = RandomDouble(0, 100),
                API_D_TanThanh = RandomDouble(0, 100),
                API_D_DongBan = RandomDouble(0, 100),
                API_D_TanHa = RandomDouble(0, 100),
                API_D_Doi95 = RandomDouble(0, 100),
            }
        };
    }

    private static TagsStation GenerateStation(int id, string name)
    {
        // Generate door lock states first
        var doorlock1_1IsClosed = RandomBoolean();
        var doorlock1_2IsClosed = RandomBoolean();
        var doorlock2_1IsClosed = RandomBoolean();
        var doorlock2_2IsClosed = RandomBoolean();
        var doorlock1IsClosing = RandomBoolean();
        var doorlock2IsClosing = RandomBoolean();
        
        var station = new TagsStation
        {
            Path = $"Local Station/DauTieng/S71500/{name}",
            StationId = id,
            StationName = name,

            Pressure_Oil_Door1 = RandomDouble(0, 200),
            Pressure_Oil_Door2 = RandomDouble(0, 200),

            HT_Cylinder1_1 = RandomDouble(0, 200),
            HT_Cylinder1_2 = RandomDouble(0, 200),
            HT_Cylinder2_1 = RandomDouble(0, 200),
            HT_Cylinder2_2 = RandomDouble(0, 200),

            Door1_Aperture = RandomDouble(0, 100),
            Door2_Aperture = RandomDouble(0, 100),
            Fllow_Door1 = RandomDouble(0, 510),
            Fllow_Door2 = RandomDouble(0, 510),
            Fllow_Ho = RandomDouble(0, 500),

            S1_Temp_Oil = RandomDouble(0, 100),



            // Door lock states - using local variables to ensure proper initialization
            Doorlock1_1Close = doorlock1_1IsClosed,
            Doorlock1_1Open = !doorlock1_1IsClosed,
            Doorlock1_2Close = doorlock1_2IsClosed,
            Doorlock1_2Open = !doorlock1_2IsClosed,

            Doorlock2_1Close = doorlock2_1IsClosed,
            Doorlock2_1Open = !doorlock2_1IsClosed,
            Doorlock2_2Close = doorlock2_2IsClosed,
            Doorlock2_2Open = !doorlock2_2IsClosed,

            // Door lock movement states - using local variables to ensure proper initialization
            Doorlock1_Closing = doorlock1IsClosing,
            Doorlock1_Opening = !doorlock1IsClosing,
            Doorlock2_Closing = doorlock2IsClosing,
            Doorlock2_Opening = !doorlock2IsClosing,


            // Pump and motor status
            DC1_Running = rnd.Next(2) == 1,
            DC2_Running = rnd.Next(2) == 1,
            DC3_Running = rnd.Next(2) == 1,

            Lock1 = (rnd.Next(2) == 1),
            Lock2 = !(rnd.Next(2) == 1),

            // System status
            Local_Stop = (rnd.Next(2) == 1),
            Remote = rnd.Next(2) == 1,
            Local = !(rnd.Next(2) == 1),
            Auto = rnd.Next(2) == 1,
            Man = !(rnd.Next(2) == 1),
            
            // Alarms and warnings
            Al_Door1 = rnd.Next(10) < 2,      // 20% chance of door 1 alarm
            Al_Door2 = rnd.Next(10) < 2,      // 20% chance of door 2 alarm
            Temp_Oil_High = rnd.Next(10) < 1,  // 10% chance of high oil temp
            Temp_Oil_Low = rnd.Next(10) < 1,   // 10% chance of low oil temp
            Door1_PressureHigh = rnd.Next(10) < 1,
            Door1_PressureLow = rnd.Next(10) < 1,
            Door2_PressureHigh = rnd.Next(10) < 1,
            Door2_PressureLow = rnd.Next(10) < 1
        };
        
        // Set random door states after station is created
        SetRandomDoorStates(station);
        return station;
    }

    private static double RandomDouble(double min, double max)
    {
        return Math.Round(min + rnd.NextDouble() * (max - min), 2);
    }
    
    //function random bool
    private static bool RandomBoolean()
    {
        return rnd.Next(2) == 1;
    }
    
    private static void SetRandomDoorStates(TagsStation station)
    {
        // For Door 1
        var door1States = new[] { "Open", "Close", "Opening", "Closing", "Open_Door" };
        var door1State = door1States[rnd.Next(door1States.Length)];
        
        // For Door 2
        var door2States = new[] { "Open", "Close", "Opening", "Closing", "Open_Door" };
        var door2State = door2States[rnd.Next(door2States.Length)];
        
        // Set door 1 state
        station.Door1_Open = door1State == "Open";
        station.Door1_Opening = door1State == "Opening";
        station.Door1_Open_Door = door1State == "Open_Door";

        station.Door1_Close = door1State == "Close";
        station.Door1_Closing = door1State == "Closing";

        // Set door 2 state
        station.Door2_Open = door2State == "Open";
        station.Door2_Opening = door2State == "Opening";
        station.Door2_Open_Door = door2State == "Open_Door";

        station.Door2_Close = door2State == "Close";
        station.Door2_Closing = door2State == "Closing";
    }
    
}
