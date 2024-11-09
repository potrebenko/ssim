namespace SSIM.Parser;

public static class SegmentDataRecord
{
    /// <summary>
    /// The type of records in the computerized schedules formats for Chapter 7
    /// </summary>
    public static byte[] RecordType = "RecordType".ToBytes();
    
    /// <summary>
    /// A code assigned by the administrating carrier for operational purposes
    /// </summary>
    public static byte[] OperationalSuffix = "OperationalSuffix".ToBytes();
    
    /// <summary>
    /// Identification of the flight or a series of similar flights operated by a carrier
    /// </summary>
    public static byte[] FlightDesignator = "FlightDesignator".ToBytes();
    
    /// <summary>
    /// The 2-character code assigned to a carrier by IATA and published in the IATA Airline Coding
    /// Directory or the 3-alphabetic codes assigned to a carrier by ICAO
    /// </summary>
    public static byte[] AirlineDesignator = "AirlineDesignator".ToBytes();
    
    /// <summary>
    /// A multi-purpose reference assigned by a carrier in connection with the planning and control of
    /// the operation of flights
    /// </summary>
    public static byte[] FlightNumber = "FlightNumber".ToBytes();
    
    /// <summary>
    /// A number used to differentiate between itineraries having the same Flight Designator (without
    /// regard to Operational Suffixes, if any).
    /// An Itinerary is a single flight or a series of identical flights defined by a continuous Period and
    /// Day(s) of Operation (and Frequency Rate if applicable), each of which consists of one or more
    /// contiguous legs which, taken together, describe a complete routing of that flight
    /// </summary>
    public static byte[] ItineraryVariationIdentifier = "ItineraryVariationIdentifier".ToBytes();
    
    /// <summary>
    /// The sequence number of the leg for the flight and itinerary variation being specified within each
    /// Itinerary Variation Identifier
    /// </summary>
    public static byte[] LegSequenceNumber = "LegSequenceNumber".ToBytes();
    
    /// <summary>
    /// Classification of flight or flight leg as well as the type of service provided
    /// </summary>
    public static byte[] ServiceType = "ServiceType".ToBytes();
    public static byte[] Spare = "Spare".ToBytes();
    
    /// <summary>
    /// The number of hundreds to be added to the number in the IVI field to give the true IVI
    /// </summary>
    public static byte[] ItineraryVariationIdentifierOverflow = "ItineraryVariationIdentifierOverflow".ToBytes();
    
    /// <summary>
    /// A single alpha character to indicate the departure station of a segment (Board Point) to which a
    /// data element associated with a Data Element Identifier applies
    /// </summary>
    public static byte[] BoardPointIndicator = "BoardPointIndicator".ToBytes();
    
    /// <summary>
    /// A single alpha character to indicate the arrival station of a segment (Off Point) to which a data
    /// element associated with a Data Element Identifier applies
    /// </summary>
    public static byte[] OffPointIndicator = "OffPointIndicator".ToBytes();
    
    /// <summary>
    /// Identification of a specific data element in SSIM
    /// </summary>
    public static byte[] DataElementIdentifier = "DataElementIdentifier".ToBytes();
    
    /// <summary>
    /// The Board Point followed by the Off Point
    /// </summary>
    public static byte[] Segment = "Segment".ToBytes();
    
    /// <summary>
    /// 3-character IATA Code
    /// </summary>
    public static byte[] BoardPoint = "BoardPoint".ToBytes();
    
    /// <summary>
    /// 3-character IATA Code
    /// </summary>
    public static byte[] OffPoint = "OffPoint".ToBytes();
    
    /// <summary>
    /// A free format text field assigned by the individual carrier for bilateral purposes
    /// </summary>
    public static byte[] Data = "Data".ToBytes();
    
    /// <summary>
    /// The number of the record in computerized schedule formats
    /// </summary>
    public static byte[] RecordSerialNumber = "RecordSerialNumber".ToBytes();

    public static void ParseSegmentDataRecord(this byte[] record, int offset, IDataComposer composer)
    {
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