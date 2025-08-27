namespace Domain
{
    public class TagsModel
    {
        public bool? Remote { get; set; } = false;
        public bool? Local { get; set; } = false;
        public bool? Auto { get; set; } = false;
        public bool? Man { get; set; } = false;
        public bool? Local_Stop { get; set; } = false;
        public bool? DC1_Running { get; set; } = false;
        public bool? DC2_Running { get; set; } = false;
        public bool? DC3_Running { get; set; } = false;
        public bool? Door1_Opening { get; set; } = false;
        public bool? Door1_Closing { get; set; } = false;
        public bool? Door2_Opening { get; set; } = false;
        public bool? Door2_Closing { get; set; } = false;
        public bool? Doorlock1_Opening { get; set; } = false;
        public bool? Doorlock1_Closing { get; set; } = false;
        public bool? Doorlock2_Opening { get; set; } = false;
        public bool? Doorlock2_Closing { get; set; } = false;
        public bool? Door1_Open { get; set; } = false;
        public bool? Door1_Close { get; set; } = false;
        public bool? Door2_Open { get; set; } = false;
        public bool? Door2_Close { get; set; } = false;
        public bool? Doorlock1_1Open { get; set; } = false;
        public bool? Doorlock1_1Close { get; set; } = false;
        public bool? Doorlock1_2Open { get; set; } = false;
        public bool? Doorlock1_2Close { get; set; } = false;
        public bool? Doorlock2_1Open { get; set; } = false;
        public bool? Doorlock2_1Close { get; set; } = false;
        public bool? Doorlock2_2Open { get; set; } = false;
        public bool? Doorlock2_2Close { get; set; } = false;
        public bool? DC1_Over { get; set; } = false;
        public bool? DC2_Over { get; set; } = false;
        public bool? DC3_Over { get; set; } = false;
        public bool? Door1_PressureHigh { get; set; } = false;
        public bool? Door1_PressureLow { get; set; } = false;
        public bool? Door2_PressureHigh { get; set; } = false;
        public bool? Door2_PressureLow { get; set; } = false;
        public bool? HT_Cylinder1_1 { get; set; } = false;
        public bool? HT_Cylinder1_2 { get; set; } = false;
        public bool? HT_Cylinder2_1 { get; set; } = false;
        public bool? HT_Cylinder2_2 { get; set; } = false;
        public bool? Door1_Aperture { get; set; } = false;
        public bool? Door2_Aperture { get; set; } = false;
        public int? S1_Temp_Oil { get; set; } = 0;
        public int? Pressure_Oil_Door1 { get; set; } = 0;
        public int? Pressure_Oil_Door2 { get; set; } = 0;
        public bool? Al_Door1 { get; set; } = false;
        public bool? Al_Door2 { get; set; } = false;
        public int? Fllow_Door1 { get; set; } = 0;
        public int? Fllow_Door2 { get; set; } = 0;
        public int? Total_Fllow { get; set; } = 0;
        public int? Fllow_Ho { get; set; } = 0;
    }

    public class TagGroupAPI
    {
        public string? Flow_DauTieng { get; set; } = "0";
        public string? Flow_BenSuc { get; set; } = "0";
        public string? Fllow_SonDai { get; set; } = "0";
        public string? Fllow_BinhNham { get; set; } = "0";
        public string? Fllow_BinhNham2 { get; set; } = "0";
        public string? Fllow_TL_CDD { get; set; } = "0";
        public string? Fllow_HL_TXL { get; set; } = "0";
    }

  public class TagGroupSpecial
    {
        public int? Fllow_Ho { get; set; }
    }
}