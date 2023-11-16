namespace Garage.Logic;
public class ParkingSpot
{
    public ParkingSpot(string licensePlate, DateTime entryDate)
    {
        LicensePlate = licensePlate;
        EntryDate = entryDate;
    }

    public string LicensePlate { get; set; } = "";
    public DateTime EntryDate { get; set; }
}

public class Garage
{
    public ParkingSpot[] parkingSpots { get; } = new ParkingSpot[50];

    public bool IsOccupied(int parkingSpotNumber)
    {
        if (parkingSpots[parkingSpotNumber - 1] != null)
        {
            return true;
        }
        return false;
    }

    public bool TryOccupy(int parkingSpotNumber, string licensePlate, DateTime entryTime)
    {
        if (IsOccupied(parkingSpotNumber) == true)
        {
            return false;
        }
        parkingSpots[parkingSpotNumber - 1] = new ParkingSpot(licensePlate, entryTime);
        return true;
    }

    public bool TryExit(int parkingSpotNumber, DateTime exitTime, out decimal costs)
    {
        if (IsOccupied(parkingSpotNumber))
        {
            decimal startedHalfHour = (decimal)Math.Ceiling(((exitTime - parkingSpots[parkingSpotNumber - 1].EntryDate).TotalMinutes) / 30);
            costs = startedHalfHour * 3;
            Console.WriteLine($"Costs are {costs}$");
            parkingSpots[parkingSpotNumber - 1] = null!;
            return true;
        }
        costs = 0;
        return false;
    }

    public void GenerateReport()
    {
        Console.WriteLine("| Spot | License Plate |");
        Console.WriteLine("|------|---------------|");

        for (int i = 0; i < parkingSpots.Length; i++)
        {
            ParkingSpot spot = parkingSpots[i];

            if (spot != null)
            {
                Console.WriteLine($"| {i + 1,-4} | {spot.LicensePlate,-13} |");
            }
            else
            {
                Console.WriteLine($"| {i + 1,-4} |               |");
            }
        }
    }
}