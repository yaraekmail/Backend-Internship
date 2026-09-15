namespace CardiacPatientMonitoring.Api.Services;

// Provides simple calculations related to vital signs.
public class VitalSignService
{
    // Calculates the average heart rate from a collection of heart rate values.
    public double CalculateAverageHeartRate(List<int> heartRates)
    {
        // Returns 0 when the list is empty.
        if (heartRates.Count == 0)
        {
            return 0;
        }

        // Adds all heart rate values together.
        int total = heartRates.Sum();

        // Divides the total by the number of readings to calculate the average.
        return (double)total / heartRates.Count;
    }
}