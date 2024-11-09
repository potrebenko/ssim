using FluentAssertions;

namespace SSIM.Parser.Tests;

public class FlightLegRecordTests
{
    [Fact]
    public void ParseFlightLeg_ValidRecord_ShouldReturnExpectedJson()
    {
        // Arrange
        var dataComposer = new JsonComposer(10000);
        var input = 
"3AXX F 410102F09JUN0510JUN051 3 5 72RUN21302130+04003 DZA22402240+03003 D92F008C038BQV145       N   LSL L     AB BC DE DIS     2AB AB AB KL 01232Z 2X  A  Z     S           F014Y119VVT3M33     UL000003"
    .ToBytes();
        
        var expected =
            ",{\"RecordType\":\"3\",\"OperationalSuffix\":\"A\",\"FlightDesignator\":\"XX F 41\"," +
            "\"AirlineDesignator\":\"XX\",\"FlightNumber\":\"F 41\",\"ItineraryVariationIdentifier\":\"01\"," +
            "\"LegSequenceNumber\":\"02\",\"ServiceType\":\"F\",\"PeriodOfOperationFrom\":\"09JUN05\"," +
            "\"PeriodOfOperationTo\":\"10JUN05\",\"DaysOfOperation\":\"1 3 5 7\",\"FrequencyRate\":\"2\"," +
            "\"DepartureStation\":\"RUN\",\"ScheduledTimeOfPassengerDeparture\":\"2130\",\"ScheduledTimeOfAircraftDeparture\":\"2130\"," +
            "\"TimeVariationDeparture\":\"+0400\",\"PassengerTerminalDeparture\":\"3\",\"ArrivalStation\":\"DZA\"," +
            "\"ScheduledTimeOfAircraftArrival\":\"2240\",\"ScheduledTimeOfPassengerArrival\":\"2240\"," +
            "\"TimeVariationArrival\":\"+0300\",\"PassengerTerminalArrival\":\"3\",\"AircraftType\":\"D92\"," +
            "\"PassengerReservationsBookingDesignator\":\"F008C038BQV145\",\"PassengerReservationsBookingModifier\":\" N   \"," +
            "\"MealServiceNote\":\"LSL L     \",\"JointOperationAirlineDesignators\":\"AB BC DE \"," +
            "\"MinimumConnectingTimeStatusDeparture\":\"D\",\"MinimumConnectingTimeStatusArrival\":\"I\"," +
            "\"SecureFlightIndicator\":\"S\",\"ItineraryVariationIdentifierOverflow\":\"2\"," +
            "\"AircraftOwner\":\"AB\",\"CockpitCrewEmployer\":\"AB\",\"CabinCrewEmployer\":\"AB\"," +
            "\"OnwardFlight\":\"KL 01232Z\",\"AirlineDesignator2\":\"KL\",\"FlightNumber2\":\"0123\"," +
            "\"AircraftRotationLayover\":\"2\",\"OperationalSuffix2\":\"Z\"," +
            "\"FlightTransitLayover\":\"2\",\"OperatingAirlineDisclosure\":\"X\"," +
            "\"TrafficRestrictionCode\":\"  A  Z     \",\"TrafficRestrictionCodeLegOverflowIndicator\":\"S\"," +
            "\"AircraftConfiguration\":\"F014Y119VVT3M33     \",\"DateVariation\":\"UL\"," +
            "\"RecordSerialNumber\":\"000003\"}";
        
        
        // Act
        input.ParseFlightLegRecord(0, dataComposer);

        // Assert
        var str = dataComposer.ToString();
        str.Should().Be(expected);
    }
}