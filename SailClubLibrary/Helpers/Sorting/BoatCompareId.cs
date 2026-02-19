using SailClubLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class BoatCompareId : IComparer<Boat>
{
    public int Compare(Boat? x, Boat? y)
    {
        if (x == null && y == null) return 0;
        if (x == null) return -1;
        if (y == null) return 1;

        return x.Id.CompareTo(y.Id);
    }
}

