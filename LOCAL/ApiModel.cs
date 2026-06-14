using System.Collections.Generic;

public class ApiResponse
{
    public int code { get; set; }
    public List<ApiItem> data { get; set; }
}

public class ApiItem
{
    public string Ts { get; set; }
    public string A1 { get; set; }
    public string A2 { get; set; }
    public string Ap1 { get; set; }
    public string Ap2 { get; set; }
    public string Z2 { get; set; }
    public string Qc { get; set; }
}