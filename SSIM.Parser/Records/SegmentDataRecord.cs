namespace SSIM.Parser;

public static class SegmentDataRecord
{
    /// <summary>
    /// The type of records in the computerized schedules formats for Chapter 7
    /// </summary>
    public static ReadOnlySpan<byte> RecordType => "RecordType"u8;
    
    /// <summary>
    /// A code assigned by the administrating carrier for operational purposes
    /// </summary>
    public static ReadOnlySpan<byte> OperationalSuffix => "OperationalSuffix"u8;
    
    /// <summary>
    /// Identification of the flight or a series of similar flights operated by a carrier
    /// </summary>
    public static ReadOnlySpan<byte> FlightDesignator => "FlightDesignator"u8;
    
    /// <summary>
    /// The 2-character code assigned to a carrier by IATA and published in the IATA Airline Coding
    /// Directory or the 3-alphabetic codes assigned to a carrier by ICAO
    /// </summary>
    public static ReadOnlySpan<byte> AirlineDesignator => "AirlineDesignator"u8;
    
    /// <summary>
    /// A multi-purpose reference assigned by a carrier in connection with the planning and control of
    /// the operation of flights
    /// </summary>
    public static ReadOnlySpan<byte> FlightNumber => "FlightNumber"u8;
    
    /// <summary>
    /// A number used to differentiate between itineraries having the same Flight Designator (without
    /// regard to Operational Suffixes, if any).
    /// An Itinerary is a single flight or a series of identical flights defined by a continuous Period and
    /// Day(s) of Operation (and Frequency Rate if applicable), each of which consists of one or more
    /// contiguous legs which, taken together, describe a complete routing of that flight
    /// </summary>
    public static ReadOnlySpan<byte> ItineraryVariationIdentifier => "ItineraryVariationIdentifier"u8;
    
    /// <summary>
    /// The sequence number of the leg for the flight and itinerary variation being specified within each
    /// Itinerary Variation Identifier
    /// </summary>
    public static ReadOnlySpan<byte> LegSequenceNumber => "LegSequenceNumber"u8;
    
    /// <summary>
    /// Classification of flight or flight leg as well as the type of service provided
    /// </summary>
    public static ReadOnlySpan<byte> ServiceType => "ServiceType"u8;
    public static ReadOnlySpan<byte> Spare => "Spare"u8;
    
    /// <summary>
    /// The number of hundreds to be added to the number in the IVI field to give the true IVI
    /// </summary>
    public static ReadOnlySpan<byte> ItineraryVariationIdentifierOverflow => "ItineraryVariationIdentifierOverflow"u8;
    
    /// <summary>
    /// A single alpha character to indicate the departure station of a segment (Board Point) to which a
    /// data element associated with a Data Element Identifier applies
    /// </summary>
    public static ReadOnlySpan<byte> BoardPointIndicator => "BoardPointIndicator"u8;
    
    /// <summary>
    /// A single alpha character to indicate the arrival station of a segment (Off Point) to which a data
    /// element associated with a Data Element Identifier applies
    /// </summary>
    public static ReadOnlySpan<byte> OffPointIndicator => "OffPointIndicator"u8;
    
    /// <summary>
    /// Identification of a specific data element in SSIM
    /// </summary>
    public static ReadOnlySpan<byte> DataElementIdentifier => "DataElementIdentifier"u8;
    
    /// <summary>
    /// The Board Point followed by the Off Point
    /// </summary>
    public static ReadOnlySpan<byte> Segment => "Segment"u8;
    
    /// <summary>
    /// 3-character IATA Code
    /// </summary>
    public static ReadOnlySpan<byte> BoardPoint => "BoardPoint"u8;
    
    /// <summary>
    /// 3-character IATA Code
    /// </summary>
    public static ReadOnlySpan<byte> OffPoint => "OffPoint"u8;
    
    /// <summary>
    /// A free format text field assigned by the individual carrier for bilateral purposes
    /// </summary>
    public static ReadOnlySpan<byte> Data => "Data"u8;
    
    /// <summary>
    /// The number of the record in computerized schedule formats
    /// </summary>
    public static ReadOnlySpan<byte> RecordSerialNumber => "RecordSerialNumber"u8;

    public static void ParseSegmentDataRecord(this byte[] record, int offset, IDataComposer composer)
    {
        composer.CheckBuffer(572);
        composer.AppendComma();
        composer.AppendOpenCurlyBrace();

        composer.AppendField(RecordType, ConstantValues.RecordType4);
        composer.AppendField(OperationalSuffix, record, offset + 1, 1);
        composer.AppendField(FlightDesignator, record, offset + 2, 7);
        composer.AppendFieldLeftJustified(AirlineDesignator, record, offset + 2, 3);
        composer.AppendFieldRightJustified(FlightNumber, record, offset + 5, 4);
        composer.AppendField(ItineraryVariationIdentifier, record, offset + 9, 2);
        composer.AppendField(LegSequenceNumber, record, offset + 11, 2);
        composer.AppendField(ServiceType, record, offset + 13, 1);
        composer.AppendField(ItineraryVariationIdentifierOverflow, record, offset + 27, 1);
        composer.AppendField(BoardPointIndicator, record, offset + 28, 1);
        composer.AppendField(OffPointIndicator, record, offset + 29, 1);
        composer.AppendFieldRightJustified(DataElementIdentifier, record, offset + 30, 3);
        composer.AppendField(Segment, record, offset + 33, 6);
        composer.AppendField(BoardPoint, record, offset + 33, 3);
        composer.AppendField(OffPoint, record, offset + 36, 3);
        composer.AppendField(Data, record, offset + 39, 155);
        composer.AppendFieldRightJustified(RecordSerialNumber, record, offset + 194, 6, false);
 
        composer.AppendCloseCurlyBrace();
    }
}