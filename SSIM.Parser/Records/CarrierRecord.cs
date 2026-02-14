using System.Runtime.CompilerServices;

namespace SSIM.Parser;

/// <summary>
/// Record type 2
/// </summary>
public static class CarrierRecord
{
    /// <summary>
    /// The type of records in the computerized schedules formats for Chapter 7
    /// </summary>
    public static ReadOnlySpan<byte> RecordType => "RecordType"u8;
    
    /// <summary>
    /// Indication of whether Local Time or UTC (Universal Time Coordinated) is being used
    /// </summary>
    public static ReadOnlySpan<byte> TimeMode => "TimeMode"u8;
    
    /// <summary>
    /// The 2-character code assigned to a carrier by IATA and published in the IATA Airline Coding
    /// Directory or the 3-alphabetic codes assigned to a carrier by ICAO
    /// </summary>
    public static ReadOnlySpan<byte> AirlineDesignator => "AirlineDesignator"u8;
    public static ReadOnlySpan<byte> Spare => "Spare"u8;
    
    /// <summary>
    /// A set of schedules that is valid within a specified IATA Season
    /// </summary>
    public static ReadOnlySpan<byte> Season => "Season"u8;
    public static ReadOnlySpan<byte> Spare2 => "Spare2"u8;
    
    /// <summary>
    /// First date
    /// The limits of the Period of Operation of the first leg of each itinerary variation
    /// </summary>
    public static ReadOnlySpan<byte> PeriodOfScheduleValidityFrom => "PeriodOfScheduleValidityFrom"u8;
    
    /// <summary>
    /// Last date
    /// The last date can be specified as “00XXX00” to indicate that the specified schedule is valid
    /// indefinitely
    /// </summary>
    public static ReadOnlySpan<byte> PeriodOfScheduleValidityTo => "PeriodOfScheduleValidityTo"u8;
    
    /// <summary>
    /// The computer-generated date of data set creation
    /// </summary>
    public static ReadOnlySpan<byte> CreationDate => "CreationDate"u8;
    
    /// <summary>
    /// The title of the information included in the data set in plain language
    /// </summary>
    public static ReadOnlySpan<byte> TitleOfData => "TitleOfData"u8;
    
    /// <summary>
    /// The Release (Sell) Date is intended to show the first date when a specified schedule can be
    /// opened for sale
    /// </summary>
    public static ReadOnlySpan<byte> ReleaseSellDate => "ReleaseSellDate"u8;
    
    /// <summary>
    /// The status of the specified schedule provided to a recipient
    /// </summary>
    public static ReadOnlySpan<byte> ScheduleStatus => "ScheduleStatus"u8;
    
    /// <summary>
    /// Unique identification assigned by the originator of the data and referenced by the recipient
    /// whenever appropriate
    /// </summary>
    public static ReadOnlySpan<byte> CreatorReference => "CreatorReference"u8;
    
    /// <summary>
    /// Identification of a duplicate airline designation
    /// </summary>
    public static ReadOnlySpan<byte> DuplicateAirlineDesignatorMarker => "DuplicateAirlineDesignatorMarker"u8;
    
    /// <summary>
    /// Optional free text that does not directly relate to the data lines in the message
    /// </summary>
    public static ReadOnlySpan<byte> GeneralInformation => "GeneralInformation"u8;
    
    /// <summary>
    /// In-flight service information provided on individual flight legs
    /// For Chapter 7, by using bytes 170 to 188 of Record Type 2 to specify up to five defaults
    /// </summary>
    public static ReadOnlySpan<byte> InFlightServiceInformation => "InFlightServiceInformation"u8;
    
    /// <summary>
    /// Identification of a flight leg as an Electronic Ticketing Candidate
    /// </summary>
    public static ReadOnlySpan<byte> ElectronicTicketingInformation => "ElectronicTicketingInformation"u8;
    
    /// <summary>
    /// The computer-generated time of data set creation
    /// This is a mandatory field and is expressed by four digits indicating the 24 hours clock timing in the
    /// range 0000 through 2400
    /// </summary>
    public static ReadOnlySpan<byte> CreationTime => "CreationTime"u8;
    
    /// <summary>
    /// The number of the record in computerized schedule formats
    /// </summary>
    public static ReadOnlySpan<byte> RecordSerialNumber => "RecordSerialNumber"u8;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ParseCarrierRecord(this byte[] record, IDataComposer composer)
    {
        composer.CheckBuffer(596);
        composer.AppendComma();
        composer.AppendOpenCurlyBrace();

        composer.AppendField(RecordType, ConstantValues.RecordType2);
        composer.AppendField(TimeMode, record, 1, 1);
        composer.AppendFieldLeftJustified(AirlineDesignator, record, 2, 3);
        composer.AppendField(Season, record, 10, 3);
        composer.AppendField(PeriodOfScheduleValidityFrom, record, 14, 7);
        composer.AppendField(PeriodOfScheduleValidityTo, record, 21, 7);
        composer.AppendField(CreationDate, record, 28, 7);
        composer.AppendField(TitleOfData, record, 35, 29);
        composer.AppendField(ReleaseSellDate, record, 64, 7);
        composer.AppendField(ScheduleStatus, record, 71, 1);
        composer.AppendField(CreatorReference, record, 72, 35);
        composer.AppendField(DuplicateAirlineDesignatorMarker, record, 107, 1);
        composer.AppendField(GeneralInformation, record, 108, 61);
        composer.AppendFieldRightJustified(InFlightServiceInformation, record, 169, 19);
        composer.AppendField(ElectronicTicketingInformation, record, 188, 2);
        composer.AppendField(CreationTime, record, 190, 4);
        composer.AppendFieldRightJustified(RecordSerialNumber, record, 194, 6, false);
        
        composer.AppendCloseCurlyBrace();
    }
}