namespace SSIM.Parser;

public static class FlightLegRecord
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
    
    /// <summary>
    /// The date limits for the first and last operation of a flight.
    /// The Period of Operation relates to each leg of the flight
    /// </summary>
    public static ReadOnlySpan<byte> PeriodOfOperationFrom => "PeriodOfOperationFrom"u8;
    public static ReadOnlySpan<byte> PeriodOfOperationTo => "PeriodOfOperationTo"u8;
    
    /// <summary>
    /// The day(s) of the week when a flight is operated
    /// </summary>
    public static ReadOnlySpan<byte> DaysOfOperation => "DaysOfOperation"u8;
    
    /// <summary>
    /// An indication that a flight operates at fortnightly intervals (every 2 weeks) on the day(s) of the
    /// week stated under Day(s) of Operation
    /// </summary>
    public static ReadOnlySpan<byte> FrequencyRate => "FrequencyRate"u8;
    
    /// <summary>
    /// Identification of an airport for airline purposes. 3-character IATA code
    /// </summary>
    public static ReadOnlySpan<byte> DepartureStation => "DepartureStation"u8;
    
    /// <summary>
    /// The Scheduled Time of Departure of the passenger at the terminal or departure gate at an
    /// airport
    /// </summary>
    public static ReadOnlySpan<byte> ScheduledTimeOfPassengerDeparture => "ScheduledTimeOfPassengerDeparture"u8;
    
    /// <summary>
    /// The scheduled departure time of an aircraft from the terminal or departure gate/position at an
    /// airport
    /// </summary>
    public static ReadOnlySpan<byte> ScheduledTimeOfAircraftDeparture => "ScheduledTimeOfAircraftDeparture"u8;
    
    /// <summary>
    /// Indication of the difference in hours and minutes between UTC and local time
    /// </summary>
    public static ReadOnlySpan<byte> TimeVariationDeparture => "TimeVariationDeparture"u8;

    /// <summary>
    /// The physical terminal used by a passenger at any airport where more than one terminal exists
    /// </summary>
    public static ReadOnlySpan<byte> PassengerTerminalDeparture => "PassengerTerminalDeparture"u8;
    
    /// <summary>
    /// Identification of an airport for airline purposes. 3-character IATA code
    /// </summary>
    public static ReadOnlySpan<byte> ArrivalStation => "ArrivalStation"u8;
    
    /// <summary>
    /// The scheduled arrival time of an aircraft at the terminal or arrival gate/position at an airport
    /// </summary>
    public static ReadOnlySpan<byte> ScheduledTimeOfAircraftArrival => "ScheduledTimeOfAircraftArrival"u8;
    
    /// <summary>
    /// The Scheduled Time of Arrival of the passenger at the terminal or arrival gate at an airport
    /// </summary>
    public static ReadOnlySpan<byte> ScheduledTimeOfPassengerArrival => "ScheduledTimeOfPassengerArrival"u8;
    
    /// <summary>
    /// Indication of the difference in hours and minutes between UTC and local time
    /// </summary>
    public static ReadOnlySpan<byte> TimeVariationArrival => "TimeVariationArrival"u8;
    
    /// <summary>
    /// The physical terminal used by a passenger at any airport where more than one terminal exists
    /// </summary>
    public static ReadOnlySpan<byte> PassengerTerminalArrival => "PassengerTerminalArrival"u8;
    
    /// <summary>
    /// The ATA/IATA standard 3-character code that normally covers the manufacturer and main
    /// model of a commercial aircraft
    /// </summary>
    public static ReadOnlySpan<byte> AircraftType => "AircraftType"u8;
    
    /// <summary>
    /// The Passenger Reservations Booking Designator is a leg oriented (see Note 4) data element
    /// specifying the codes to describe the reservations classes provided, and optionally the number of
    /// seats allocated for each class or group of classes
    /// </summary>
    public static ReadOnlySpan<byte> PassengerReservationsBookingDesignator => "PassengerReservationsBookingDesignator"u8;
    
    /// <summary>
    /// A modifying code applicable to the appropriate Passenger Reservations Booking Designator Code
    /// </summary>
    public static ReadOnlySpan<byte> PassengerReservationsBookingModifier => "PassengerReservationsBookingModifier"u8;
    
    /// <summary>
    /// Indicates the meal service provided on a leg
    /// </summary>
    public static ReadOnlySpan<byte> MealServiceNote => "MealServiceNote"u8;
    
    /// <summary>
    /// Identification of flights or legs of flights jointly operated by two or more carriers
    /// </summary>
    public static ReadOnlySpan<byte> JointOperationAirlineDesignators => "JointOperationAirlineDesignators"u8;
    
    /// <summary>
    /// Identification of the international/domestic status on each flight leg to control the correct
    /// generation of flight connections between two flights
    /// </summary>
    public static ReadOnlySpan<byte> MinimumConnectingTimeStatusDeparture => "MinimumConnectingTimeStatusDeparture"u8;
    
    /// <summary>
    /// Identification of the international/domestic status on each flight leg to control the correct
    /// generation of flight connections between two flights
    /// </summary>
    public static ReadOnlySpan<byte> MinimumConnectingTimeStatusArrival => "MinimumConnectingTimeStatusArrival"u8;
    
    /// <summary>
    /// Indication that flight is subject to requirements for Secure Flight
    /// </summary>
    public static ReadOnlySpan<byte> SecureFlightIndicator => "SecureFlightIndicator"u8;
    public static ReadOnlySpan<byte> Spare => "Spare"u8;
    
    /// <summary>
    /// The number of hundreds to be added to the number in the IVI field to give the true IVI
    /// </summary>
    public static ReadOnlySpan<byte> ItineraryVariationIdentifierOverflow => "ItineraryVariationIdentifierOverflow"u8;
    
    /// <summary>
    /// Information provided to whomever it may concern that the flight(s) will be operated with an
    /// aircraft not belonging to the fleet of the Administrating Carrier
    /// </summary>
    public static ReadOnlySpan<byte> AircraftOwner => "AircraftOwner"u8;
    
    /// <summary>
    /// Information provided to whomever it may concern that the flight(s) will be operated with a
    /// cockpit crew not employed by the Aircraft Owner
    /// </summary>
    public static ReadOnlySpan<byte> CockpitCrewEmployer => "CockpitCrewEmployer"u8;
    
    /// <summary>
    /// Information provided to whomever it may concern that the flight(s) will be operated with cabin
    /// crew not employed by the Aircraft Owner
    /// </summary>
    public static ReadOnlySpan<byte> CabinCrewEmployer => "CabinCrewEmployer"u8;
    
    /// <summary>
    /// The Flight Designator for the next leg operated by the same aircraft
    /// </summary>
    public static ReadOnlySpan<byte> OnwardFlight => "OnwardFlight"u8;
    
    /// <summary>
    /// The 2-character code assigned to a carrier by IATA and published in the IATA Airline Coding
    /// Directory or the 3-alphabetic codes assigned to a carrier by ICAO
    /// </summary>
    public static ReadOnlySpan<byte> AirlineDesignator2 => "AirlineDesignator2"u8;
    
    /// <summary>
    /// A multi-purpose reference assigned by a carrier in connection with the planning and control of
    /// the operation of flights
    /// </summary>
    public static ReadOnlySpan<byte> FlightNumber2 => "FlightNumber2"u8;
    
    /// <summary>
    /// A single numeric value to denote that the layover of the aircraft at the leg arrival station is 24 or
    /// more hours
    /// </summary>
    public static ReadOnlySpan<byte> AircraftRotationLayover => "AircraftRotationLayover"u8;
    
    /// <summary>
    /// A code assigned by the administrating carrier for operational purposes
    /// </summary>
    public static ReadOnlySpan<byte> OperationalSuffix2 => "OperationalSuffix2"u8;
    public static ReadOnlySpan<byte> Spare2 => "Spare2"u8;
    
    /// <summary>
    /// Indication that there is a layover of the flight at the leg arrival station of 24 hours or more
    /// between the arrival and the departure of the next leg of the same flight
    /// </summary>
    public static ReadOnlySpan<byte> FlightTransitLayover => "FlightTransitLayover"u8;
    
    /// <summary>
    /// To state the operator of the flight in a code share, shared airline designation or wet lease
    /// situation
    /// </summary>
    public static ReadOnlySpan<byte> OperatingAirlineDisclosure => "OperatingAirlineDisclosure"u8;
    
    /// <summary>
    /// Information provided by a carrier to specify restrictions to carry traffic or specify limitations on
    /// the carriage of traffic
    /// </summary>
    public static ReadOnlySpan<byte> TrafficRestrictionCode => "TrafficRestrictionCode"u8;
    
    /// <summary>
    /// Indication of a Traffic Restriction Code overflow situation
    /// </summary>
    public static ReadOnlySpan<byte> TrafficRestrictionCodeLegOverflowIndicator => "TrafficRestrictionCodeLegOverflowIndicator"u8;
    public static ReadOnlySpan<byte> Spare3 => "Spare3"u8;
    
    /// <summary>
    /// Identification of the physical cabin layout of an aircraft
    /// </summary>
    public static ReadOnlySpan<byte> AircraftConfiguration => "AircraftConfiguration"u8;
    public static ReadOnlySpan<byte> DateVariation => "DateVariation"u8;
    
    /// <summary>
    /// The number of the record in computerized schedule formats
    /// </summary>
    public static ReadOnlySpan<byte> RecordSerialNumber => "RecordSerialNumber"u8;

    public static void ParseFlightLegRecord(this byte[] record, int offset, IDataComposer composer)
    {
        composer.CheckBuffer(1476);
        composer.AppendComma();
        composer.AppendOpenCurlyBrace();

        composer.AppendField(RecordType, ConstantValues.RecordType3);
        composer.AppendField(OperationalSuffix, record, offset + 1, 1);
        composer.AppendField(FlightDesignator, record, offset + 2, 7);
        composer.AppendFieldLeftJustified(AirlineDesignator, record, offset + 2, 3);
        composer.AppendFieldRightJustified(FlightNumber, record, offset + 5, 4);
        composer.AppendField(ItineraryVariationIdentifier, record, offset + 9, 2);
        composer.AppendField(LegSequenceNumber, record, offset + 11, 2);
        composer.AppendField(ServiceType, record, offset + 13, 1);
        composer.AppendField(PeriodOfOperationFrom, record, offset + 14, 7);
        composer.AppendField(PeriodOfOperationTo, record, offset + 21, 7);
        composer.AppendField(DaysOfOperation, record, offset + 28, 7);
        composer.AppendField(FrequencyRate, record, offset + 35, 1);
        composer.AppendField(DepartureStation, record, offset + 36, 3);
        composer.AppendField(ScheduledTimeOfPassengerDeparture, record, offset + 39, 4);
        composer.AppendField(ScheduledTimeOfAircraftDeparture, record, offset + 43, 4);
        composer.AppendField(TimeVariationDeparture, record, offset + 47, 5);
        composer.AppendFieldLeftJustified(PassengerTerminalDeparture, record, offset + 52, 2);
        composer.AppendField(ArrivalStation, record, offset + 54, 3);
        composer.AppendField(ScheduledTimeOfAircraftArrival, record, offset + 57, 4);
        composer.AppendField(ScheduledTimeOfPassengerArrival, record, offset + 61, 4);
        composer.AppendField(TimeVariationArrival, record, offset + 65, 5);
        composer.AppendFieldLeftJustified(PassengerTerminalArrival, record, offset + 70, 2);
        composer.AppendField(AircraftType, record, offset + 72, 3);
        composer.AppendFieldLeftJustified(PassengerReservationsBookingDesignator, record, offset + 75, 20);
        composer.AppendField(PassengerReservationsBookingModifier, record, offset + 95, 5);
        composer.AppendField(MealServiceNote, record, offset + 100, 10);
        composer.AppendField(JointOperationAirlineDesignators, record, offset + 110, 9); //TODO: Could be an array
        composer.AppendField(MinimumConnectingTimeStatusDeparture, record, offset + 119, 1);
        composer.AppendField(MinimumConnectingTimeStatusArrival, record, offset + 120, 1);
        composer.AppendField(SecureFlightIndicator, record, offset + 121, 1);
        composer.AppendField(ItineraryVariationIdentifierOverflow, record, offset + 127, 1);
        composer.AppendFieldLeftJustified(AircraftOwner, record, offset + 128, 3);
        composer.AppendFieldLeftJustified(CockpitCrewEmployer, record, offset + 131, 3);
        composer.AppendFieldLeftJustified(CabinCrewEmployer, record, offset + 134, 3);
        composer.AppendField(OnwardFlight, record, offset + 137, 9);
        composer.AppendFieldLeftJustified(AirlineDesignator2, record, offset + 137, 3);
        composer.AppendFieldRightJustified(FlightNumber2, record, offset + 140, 4);
        composer.AppendField(AircraftRotationLayover, record, offset + 144, 1);
        composer.AppendField(OperationalSuffix2, record, offset + 145, 1);
        composer.AppendField(FlightTransitLayover, record, offset + 147, 1);
        composer.AppendField(OperatingAirlineDisclosure, record, offset + 148, 1);
        composer.AppendField(TrafficRestrictionCode, record, offset + 149, 11);
        composer.AppendField(TrafficRestrictionCodeLegOverflowIndicator, record, offset + 160, 1);
        composer.AppendField(AircraftConfiguration, record, offset + 172, 20);
        composer.AppendField(DateVariation, record, offset + 192, 2);
        composer.AppendFieldRightJustified(RecordSerialNumber, record, offset + 194, 6, false);

        composer.AppendCloseCurlyBrace();
    }
}