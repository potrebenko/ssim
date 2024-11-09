namespace SSIM.TestData.Generator;

public interface ISSIMRecordGenerator
{
    string GenerateHeaderRecord();
    string GenerateCarrierRecord();
    string GenerateFlightRecord(int recordSerialNumber);
    string GenerateSegmentRecord(int recordSerialNumber);
    string GenerateTrailerRecord(int recordSerialNumber);
    string Blank();
}