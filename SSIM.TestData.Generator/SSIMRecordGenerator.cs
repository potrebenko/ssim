namespace SSIM.TestData.Generator;

public class SSIMRecordGenerator : ISSIMRecordGenerator
{
    public string GenerateHeaderRecord()
    {
        return
            "1AIRLINE STANDARD SCHEDULE DATA SET                                                                                                                                                               000001";
    }

    public string GenerateCarrierRecord()
    {
        return "2LXX          09JUN0509JUN0511JAN24                                    P                                                                                                                      0914000002";
    }

    public string GenerateFlightRecord(int recordSerialNumber)
    {
        return $"3 XX   410101F09JUN0509JUN05   4    RUN21302130+0400  DZA22402240+0300  733                                                              XX   42                            W18Y112VVCGO          {recordSerialNumber:000000}";
    }

    public string GenerateSegmentRecord(int recordSerialNumber)
    {
        return $"4XAF 43310101J              XX020CDGALCAF TEST                                                                                                                                                    {recordSerialNumber:000000}";
    }

    public string GenerateTrailerRecord(int recordSerialNumber)
    {
        return $"5 XX                                                                                                                                                                                       000517E{recordSerialNumber:000000}";
    }

    public string Blank()
    {
        return
            "00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
    }
}