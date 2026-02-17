using SailClubLibrary.Models;

public class BoatCompareBoatType : IComparer<Boat>
{
    public int Compare(Boat? x, Boat? y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;

        return x.TheBoatType.CompareTo(y.TheBoatType);
    }
}

