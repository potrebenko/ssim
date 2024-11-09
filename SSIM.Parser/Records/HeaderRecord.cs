using System.Runtime.CompilerServices;

namespace SSIM.Parser;

/// <summary>
/// Record type 1
/// </summary>
public static class HeaderRecord
{
    /// <summary>
    /// The type of records in the computerized schedules formats for Chapter 7
    /// </summary>
    public static byte[] RecordType = "RecordType".ToBytes();
    
    /// <summary>
    /// The application of the data set in plain language
    /// </summary>
    public static byte[] TitleOfContents = "TitleOfContents".ToBytes();
    public static byte[] Spare = "Spare".ToBytes();
    
    /// <summary>
    /// The number of Seasons that have been included in the data set
    /// </summary>
    public static byte[] NumberOfSeasons = "NumberOfSeasons".ToBytes();
    public static byte[] Spare2 = "Spare2".ToBytes();
    
    /// <summary>
    /// Indication of the position of the physical data set within the logical data set in which it occurs 
    /// </summary>
    public static byte[] DataSetSerialNumber = "DataSetSerialNumber".ToBytes();
    
    /// <summary>
    /// The number of the record in computerized schedule formats
    /// </summary>
    public static byte[] RecordSerialNumber = "RecordSerialNumber".ToBytes();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ParseHeaderRecord(this byte[] record, IDataComposer composer)
    {
        composer.AppendOpenCurlyBrace();

        composer.AppendField(RecordType, ConstantValues.RecordType1);
        composer.AppendField(TitleOfContents, ConstantValues.TitleOfContents);
        composer.AppendField(NumberOfSeasons, record, 40, 1);
        composer.AppendField(DataSetSerialNumber, record, 191, 3);
        composer.AppendField(RecordSerialNumber, ConstantValues.RecordSerialNumber, false);
        composer.AppendCloseCurlyBrace();
    }
}