using CardiacPatientMonitoring.Api.Services;
using Xunit;

// Contains unit tests for the VitalSignService class.
public class VitalSignServiceTests
{
    // Tests that the average of three heart rate values is calculated correctly.
    [Fact]
    public void CalculateAverageHeartRate_ShouldReturnCorrectAverage()
    {
        // Arrange: create the service and provide test data.
        var service = new VitalSignService();
        var heartRates = new List<int> { 60, 70, 80 };

        // Act: call the method that we want to test.
        var result = service.CalculateAverageHeartRate(heartRates);

        // Assert: verify that the result is what we expected.
        Assert.Equal(70, result);
    }


    // Tests that an empty list returns 0.
[Fact]
public void CalculateAverageHeartRate_ShouldReturnZero_WhenListIsEmpty()
{
    // Arrange: create the service and an empty list.
    var service = new VitalSignService();
    var heartRates = new List<int>();

    // Act: calculate the average.
    var result = service.CalculateAverageHeartRate(heartRates);

    // Assert: an empty list should return 0.
    Assert.Equal(0, result);
}

// Tests that a single heart rate value is returned as the average.
[Fact]
public void CalculateAverageHeartRate_ShouldReturnSameValue_WhenOnlyOneReadingExists()
{
    // Arrange: create the service and provide one heart rate reading.
    var service = new VitalSignService();
    var heartRates = new List<int> { 75 };

    // Act: calculate the average.
    var result = service.CalculateAverageHeartRate(heartRates);

    // Assert: the average of one value should be that same value.
    Assert.Equal(75, result);
}


// Tests the average calculation using multiple sets of input values.
[Theory]
[InlineData(60, 70, 80, 70)]
[InlineData(50, 60, 70, 60)]
[InlineData(80, 90, 100, 90)]
public void CalculateAverageHeartRate_ShouldReturnExpectedAverage(
    int heartRate1,
    int heartRate2,
    int heartRate3,
    double expectedAverage)
{
    // Arrange: create the service and provide three heart rate values.
    var service = new VitalSignService();
    var heartRates = new List<int>
    {
        heartRate1,
        heartRate2,
        heartRate3
    };

    // Act: calculate the average.
    var result = service.CalculateAverageHeartRate(heartRates);

    // Assert: verify that the calculated average matches the expected value.
    Assert.Equal(expectedAverage, result);
}
}

