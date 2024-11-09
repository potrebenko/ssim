using FluentAssertions;

namespace SSIM.Parser.Tests;

public class CarrierRecordTests
{
    [Fact]
    public void ParseCarrier_ValidRecord_ShouldReturnExpectedJson()
    {
        // Arrange
        var dataComposer = new JsonComposer(1024);
        var input = 
"2LXX      S02 09JUN0510JUN0511JAN24SAS IATA DRAFT S90           09JUN05PABC1234/05APR                      XLAST SSM REFLECTED 02145001                                    1/  7/ 12/  6/  7ET0914000002"
                .ToBytes();
        var expected =
            ",{\"RecordType\":\"2\",\"TimeMode\":\"L\",\"AirlineDesignator\":\"XX\",\"Season\":\"S02\"," +
            "\"PeriodOfScheduleValidityFrom\":\"09JUN05\",\"PeriodOfScheduleValidityTo\":\"10JUN05\"," +
            "\"CreationDate\":\"11JAN24\",\"TitleOfData\":\"SAS IATA DRAFT S90           \"," +
            "\"ReleaseSellDate\":\"09JUN05\"," +
            "\"ScheduleStatus\":\"P\",\"CreatorReference\":\"ABC1234/05APR                      \"," +
            "\"DuplicateAirlineDesignatorMarker\":\"X\"," +
            "\"GeneralInformation\":\"LAST SSM REFLECTED 02145001                                  \"," +
            "\"InFlightServiceInformation\":\"1/  7/ 12/  6/  7\"," +
            "\"ElectronicTicketingInformation\":\"ET\",\"CreationTime\":\"0914\"," +
            "\"RecordSerialNumber\":\"000002\"}";

        // Act
        input.ParseCarrierRecord(dataComposer);

        // Assert
        var str = dataComposer.ToString();
        str.Should().Be(expected);
    }
}