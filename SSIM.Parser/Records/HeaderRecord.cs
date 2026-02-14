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
    public static ReadOnlySpan<byte> RecordType => "RecordType"u8;
    
    /// <summary>
    /// The application of the data set in plain language
    /// </summary>
    public static ReadOnlySpan<byte> TitleOfContents => "TitleOfContents"u8;
    public static ReadOnlySpan<byte> Spare => "Spare"u8;
    
    /// <summary>
    /// The number of Seasons that have been included in the data set
    /// </summary>
    public static ReadOnlySpan<byte> NumberOfSeasons => "NumberOfSeasons"u8;
    public static ReadOnlySpan<byte> Spare2 => "Spare2"u8;
    
    /// <summary>
    /// Indication of the position of the physical data set within the logical data set in which it occurs 
    /// </summary>
    public static ReadOnlySpan<byte> DataSetSerialNumber => "DataSetSerialNumber"u8;
    
    /// <summary>
    /// The number of the record in computerized schedule formats
    /// </summary>
    public static ReadOnlySpan<byte> RecordSerialNumber => "RecordSerialNumber"u8;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ParseHeaderRecord(this byte[] record, IDataComposer composer)
    {
        composer.CheckBuffer(153);
        composer.AppendOpenCurlyBrace();

        composer.AppendField(RecordType, ConstantValues.RecordType1);
        composer.AppendField(TitleOfContents, ConstantValues.TitleOfContents);
        composer.AppendField(NumberOfSeasons, record, 40, 1);
        composer.AppendField(DataSetSerialNumber, record, 191, 3);
        composer.AppendField(RecordSerialNumber, ConstantValues.RecordSerialNumber, false);
        composer.AppendCloseCurlyBrace();
    }
}