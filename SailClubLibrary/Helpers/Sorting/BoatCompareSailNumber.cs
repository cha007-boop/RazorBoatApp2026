using SailClubLibrary.Models;

public class BoatCompareSailNumber : IComparer<Boat>
{
    public int Compare(Boat? x, Boat? y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;

        return string.Compare(x.SailNumber, y.SailNumber, StringComparison.Ordinal);
    }
}

