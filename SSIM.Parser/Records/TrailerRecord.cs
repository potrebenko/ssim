using System.Runtime.CompilerServices;

namespace SSIM.Parser;

public static class TrailerRecord
{
    /// <summary>
    /// The type of records in the computerized schedules formats for Chapter 7
    /// </summary>
    public static ReadOnlySpan<byte> RecordType => "RecordType"u8;
    public static ReadOnlySpan<byte> Spare => "Spare"u8;
    
    /// <summary>
    /// The 2-character code assigned to a carrier by IATA and published in the IATA Airline Coding
    /// Directory or the 3-alphabetic codes assigned to a carrier by ICAO
    /// </summary>
    public static ReadOnlySpan<byte> AirlineDesignator => "AirlineDesignator"u8;
    
    /// <summary>
    /// The Release (Sell) Date is intended to show the first date when a specified schedule can be
    /// opened for sale
    /// </summary>
    public static ReadOnlySpan<byte> ReleaseSellDate => "ReleaseSellDate"u8;
    public static ReadOnlySpan<byte> Spare2 => "Spare2"u8;
    
    /// <summary>
    /// A check number to ensure that data set records are processed in the correct sequence
    /// </summary>
    public static ReadOnlySpan<byte> SerialNumberCheckReference => "SerialNumberCheckReference"u8;
    
    /// <summary>
    /// Indication that this is either the last message/data set in a data transfer or that further
    /// are to be expected
    /// </summary>
    public static ReadOnlySpan<byte> ContinuationEndCode => "ContinuationEndCode"u8;
    
    /// <summary>
    /// The number of the record in computerized schedule formats
    /// </summary>
    public static ReadOnlySpan<byte> RecordSerialNumber => "RecordSerialNumber"u8;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ParseTrailerRecord(this byte[] record, int offset, IDataComposer composer)
    {
        composer.CheckBuffer(167);
        composer.AppendComma();
        composer.AppendOpenCurlyBrace();

        composer.AppendField(RecordType, ConstantValues.RecordType5);
        composer.AppendFieldLeftJustified(AirlineDesignator, record, offset + 2, 3);
        composer.AppendField(ReleaseSellDate, record, offset + 5, 7);
        composer.AppendField(SerialNumberCheckReference, record, offset + 187, 6);
        composer.AppendField(ContinuationEndCode, record, offset + 193, 1);
        composer.AppendFieldRightJustified(RecordSerialNumber, record, offset + 194, 6, false);

        composer.AppendCloseCurlyBrace();
    }
}