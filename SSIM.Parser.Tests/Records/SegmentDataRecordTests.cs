using FluentAssertions;

namespace SSIM.Parser.Tests;

public class SegmentDataRecordTests
{
    [Fact]
    public void ParseSegmentData_ValidRecord_ShouldReturnExpectedJson()
    {
        // Arrange
        var dataComposer = new JsonComposer(2024);
        var input = 
"4XAF 43310101J             AXC 20FRALHRAF TEST                                                                                                                                                    000004"
                .ToBytes();

        var expected = ",{\"RecordType\":\"4\",\"OperationalSuffix\":\"X\"," +
                       "\"FlightDesignator\":\"AF 4331\",\"AirlineDesignator\":\"AF\"," +
                       "\"FlightNumber\":\"4331\",\"ItineraryVariationIdentifier\":\"01\"," +
                       "\"LegSequenceNumber\":\"01\",\"ServiceType\":\"J\"," +
                       "\"ItineraryVariationIdentifierOverflow\":\"A\",\"BoardPointIndicator\":\"X\"," +
                       "\"OffPointIndicator\":\"C\",\"DataElementIdentifier\":\"20\"," +
                       "\"Segment\":\"FRALHR\",\"BoardPoint\":\"FRA\",\"OffPoint\":\"LHR\",\"Data\":\"AF TEST                                                                                                                                                    \"," +
                       "\"RecordSerialNumber\":\"000004\"}";

        // Act
        input.ParseSegmentDataRecord(0, dataComposer);

        // Assert
        var str = dataComposer.ToString();
        str.Should().Be(expected);
    }
}