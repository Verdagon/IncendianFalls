using System.Collections.Generic;
using Atharia.Model;

namespace IncendianFalls {
  public class Bridge {
    // See BridgeMaking0.png for what these are
    public readonly Location aLoc;
    public readonly Location bLoc;
    public readonly Location cLoc;
    public readonly Location dLoc;
    public readonly Location eLoc;
    public readonly Location fLoc;
    public readonly Location gLoc;
    public readonly Location hLoc;
    public readonly Location iLoc;
    public readonly Location jLoc;
    public readonly Location kLoc;
    public readonly Location lLoc;
    public readonly Location mLoc;
    public readonly Location nLoc;
    public readonly Location oLoc;
    public readonly Location pLoc;

    public Bridge(
        Location aLoc,
        Location bLoc,
        Location cLoc,
        Location dLoc,
        Location eLoc,
        Location fLoc,
        Location gLoc,
        Location hLoc,
        Location iLoc,
        Location jLoc,
        Location kLoc,
        Location lLoc,
        Location mLoc,
        Location nLoc,
        Location oLoc,
        Location pLoc) {
      this.aLoc = aLoc;
      this.bLoc = bLoc;
      this.cLoc = cLoc;
      this.dLoc = dLoc;
      this.eLoc = eLoc;
      this.fLoc = fLoc;
      this.gLoc = gLoc;
      this.hLoc = hLoc;
      this.iLoc = iLoc;
      this.jLoc = jLoc;
      this.kLoc = kLoc;
      this.lLoc = lLoc;
      this.mLoc = mLoc;
      this.nLoc = nLoc;
      this.oLoc = oLoc;
      this.pLoc = pLoc;
    }

    public List<Location> getAllLocations() {
      return new List<Location>() {
          aLoc, bLoc, cLoc, dLoc, eLoc, fLoc, gLoc, hLoc,
          iLoc, jLoc, kLoc, lLoc, mLoc, nLoc, oLoc, pLoc
      };
    }
    public List<Location> getEmptyLocations() {
      return new List<Location>() { aLoc, dLoc, eLoc, hLoc, iLoc, jLoc, kLoc, lLoc };
    }
    public List<Location> getEndLocations() {
      return new List<Location>() { iLoc, jLoc, kLoc, lLoc };
    }
    public List<Location> getPathLocations() {
      return new List<Location>() { mLoc, nLoc, oLoc, pLoc };
    }
  }
  
  public class BridgeMaker {
    readonly Pattern pattern;
    readonly SortedSet<Location> circleLocs;
    readonly SortedSet<Location> mainLocs;
    readonly SortedSet<Location> sideLocs;
    readonly SortedSet<Location> nonEmptyLocs;

    BridgeMaker(
        Pattern pattern,
        SortedSet<Location> circleLocs,
        SortedSet<Location> mainLocs,
        SortedSet<Location> sideLocs) {
      this.pattern = pattern;
      this.circleLocs = circleLocs;
      this.mainLocs = mainLocs;
      this.sideLocs = sideLocs;
      this.nonEmptyLocs = new SortedSet<Location>(mainLocs);
      SetUtils.AddAll(this.nonEmptyLocs, sideLocs);
    }
    
    // See BridgeMaking.png for what points A-H are 
    public static List<Bridge> getBridges(
        Pattern pattern,
        SortedSet<Location> circleLocs,
        SortedSet<Location> mainLocs,
        SortedSet<Location> sideLocs,
        Location startLoc) {
      var aLoc = startLoc;
      var results = new List<Bridge>();
      new BridgeMaker(pattern, circleLocs, mainLocs, sideLocs).FindB(results, aLoc);
      return results;
    }

    void FindB(List<Bridge> results, Location aLoc) {
      // B is a main loc next to A.
      var bLocs = new SortedSet<Location>(pattern.GetAdjacentLocations(aLoc, false));
      SetUtils.RetainAll(bLocs, circleLocs);
      SetUtils.RetainAll(bLocs, mainLocs);
      foreach (var bLoc in bLocs) {
        FindC(results, aLoc, bLoc);
      }
    }
    
    void FindC(List<Bridge> results, Location aLoc, Location bLoc) {
      // C is a side loc next to B, and not touching A (even via corner).
      var cLocs = new SortedSet<Location>(pattern.GetAdjacentLocations(bLoc, false));
      SetUtils.RetainAll(cLocs, circleLocs);
      SetUtils.RetainAll(cLocs, sideLocs);
      SetUtils.RemoveAll(cLocs, pattern.GetAdjacentLocations(aLoc, true));
      SetUtils.RemoveAll(cLocs, new List<Location>{aLoc, bLoc}); // Just in case
      foreach (var cLoc in cLocs) {
        FindD(results, aLoc, bLoc, cLoc);
      }
    }
    void FindD(List<Bridge> results, Location aLoc, Location bLoc, Location cLoc) {
      // D is an empty loc next to C, and not touching B or A (even via corner).
      var dLocs = new SortedSet<Location>(pattern.GetAdjacentLocations(cLoc, false));
      SetUtils.RetainAll(dLocs, circleLocs);
      SetUtils.RemoveAll(dLocs, mainLocs);
      SetUtils.RemoveAll(dLocs, sideLocs);
      SetUtils.RemoveAll(dLocs, pattern.GetAdjacentLocations(aLoc, true));
      SetUtils.RemoveAll(dLocs, pattern.GetAdjacentLocations(bLoc, true));
      SetUtils.RemoveAll(dLocs, new List<Location>{aLoc, bLoc, cLoc}); // Just in case
      foreach (var dLoc in dLocs) {
        FindE(results, aLoc, bLoc, cLoc, dLoc);
      }
    }
    void FindE(List<Bridge> results, Location aLoc, Location bLoc, Location cLoc, Location dLoc) {
      // E is a new empty loc next to D, which touches another side loc (F) that's next to C but isn't D.
      var eLocs = new SortedSet<Location>(pattern.GetAdjacentLocations(dLoc, false));
      SetUtils.RetainAll(eLocs, circleLocs);
      SetUtils.RemoveAll(eLocs, mainLocs);
      SetUtils.RemoveAll(eLocs, sideLocs);
      SetUtils.RemoveAll(eLocs, new List<Location>{aLoc, bLoc, cLoc, dLoc}); // Just in case
      foreach (var eLoc in eLocs) {
        // We'll check the above conditions as part of FindF.
        FindF(results, aLoc, bLoc, cLoc, dLoc, eLoc);
      }
    }
    void FindF(List<Bridge> results, Location aLoc, Location bLoc, Location cLoc, Location dLoc, Location eLoc) {
      // F is a new side loc next to E, which is next to C but isn't D.
      var fLocs = new SortedSet<Location>(pattern.GetAdjacentLocations(eLoc, false));
      SetUtils.RetainAll(fLocs, circleLocs);
      SetUtils.RetainAll(fLocs, sideLocs);
      SetUtils.RetainAll(fLocs, new SortedSet<Location>(pattern.GetAdjacentLocations(cLoc, false)));
      SetUtils.RemoveAll(fLocs, new List<Location>{aLoc, bLoc, cLoc, dLoc, eLoc});
      foreach (var fLoc in fLocs) {
        FindG(results, aLoc, bLoc, cLoc, dLoc, eLoc, fLoc);
      }
    }
    void FindG(List<Bridge> results, Location aLoc, Location bLoc, Location cLoc, Location dLoc, Location eLoc, Location fLoc) {
      // G is a new main loc next to F, which is next to B but isn't C.
      var gLocs = new SortedSet<Location>(pattern.GetAdjacentLocations(fLoc, false));
      SetUtils.RetainAll(gLocs, circleLocs);
      SetUtils.RetainAll(gLocs, mainLocs);
      SetUtils.RetainAll(gLocs, new SortedSet<Location>(pattern.GetAdjacentLocations(bLoc, false)));
      SetUtils.RemoveAll(gLocs, new List<Location>{aLoc, bLoc, cLoc, dLoc, eLoc, fLoc});
      foreach (var gLoc in gLocs) {
        FindH(results, aLoc, bLoc, cLoc, dLoc, eLoc, fLoc, gLoc);
      }
    }
    void FindH(List<Bridge> results, Location aLoc, Location bLoc, Location cLoc, Location dLoc, Location eLoc, Location fLoc, Location gLoc) {
      // H is an new empty loc next to G, which is next to A but isn't B.
      var hLocs = new SortedSet<Location>(pattern.GetAdjacentLocations(gLoc, false));
      SetUtils.RetainAll(hLocs, circleLocs);
      SetUtils.RemoveAll(hLocs, mainLocs);
      SetUtils.RemoveAll(hLocs, sideLocs);
      SetUtils.RetainAll(hLocs, new SortedSet<Location>(pattern.GetAdjacentLocations(aLoc, false)));
      SetUtils.RemoveAll(hLocs, new List<Location>{aLoc, bLoc, cLoc, dLoc, eLoc, fLoc, gLoc});
      foreach (var hLoc in hLocs) {
        FindI(results, aLoc, bLoc, cLoc, dLoc, eLoc, fLoc, gLoc, hLoc);
      }
    }
    void FindI(List<Bridge> results, Location aLoc, Location bLoc, Location cLoc, Location dLoc, Location eLoc, Location fLoc, Location gLoc, Location hLoc) {
      // I is an new empty loc next to A, which isn't H, and surrounded by only other empty tiles.
      var iLocs = new SortedSet<Location>(pattern.GetAdjacentLocations(aLoc, false));
      SetUtils.RetainAll(iLocs, circleLocs);
      SetUtils.RemoveAll(iLocs, mainLocs);
      SetUtils.RemoveAll(iLocs, sideLocs);
      SetUtils.RemoveAll(iLocs, new List<Location>{aLoc, bLoc, cLoc, dLoc, eLoc, fLoc, gLoc, hLoc});
      foreach (var iLoc in iLocs) {
        var nearbyNonEmpty = new SortedSet<Location>(pattern.GetAdjacentLocations(iLoc, false));
        SetUtils.RetainAll(nearbyNonEmpty, nonEmptyLocs);
        if (nearbyNonEmpty.Count > 0) {
          continue;
        }
        FindJ(results, aLoc, bLoc, cLoc, dLoc, eLoc, fLoc, gLoc, hLoc, iLoc);
      }
    }
    void FindJ(List<Bridge> results, Location aLoc, Location bLoc, Location cLoc, Location dLoc, Location eLoc, Location fLoc, Location gLoc, Location hLoc, Location iLoc) {
      // J is an new empty loc next to I and next to H, and surrounded by only other empty tiles.
      var jLocs = new SortedSet<Location>(pattern.GetAdjacentLocations(iLoc, false));
      SetUtils.RetainAll(jLocs, circleLocs);
      SetUtils.RemoveAll(jLocs, mainLocs);
      SetUtils.RemoveAll(jLocs, sideLocs);
      SetUtils.RetainAll(jLocs, new SortedSet<Location>(pattern.GetAdjacentLocations(hLoc, false)));
      SetUtils.RemoveAll(jLocs, new List<Location>{aLoc, bLoc, cLoc, dLoc, eLoc, fLoc, gLoc, hLoc, iLoc});
      foreach (var jLoc in jLocs) {
        var nearbyNonEmpty = new SortedSet<Location>(pattern.GetAdjacentLocations(jLoc, false));
        SetUtils.RetainAll(nearbyNonEmpty, nonEmptyLocs);
        if (nearbyNonEmpty.Count > 0) {
          continue;
        }
        FindK(results, aLoc, bLoc, cLoc, dLoc, eLoc, fLoc, gLoc, hLoc, iLoc, jLoc);
      }
    }
    void FindK(List<Bridge> results, Location aLoc, Location bLoc, Location cLoc, Location dLoc, Location eLoc, Location fLoc, Location gLoc, Location hLoc, Location iLoc, Location jLoc) {
      // I is an new empty loc next to D, and surrounded by only other empty tiles.
      var kLocs = new SortedSet<Location>(pattern.GetAdjacentLocations(dLoc, false));
      SetUtils.RetainAll(kLocs, circleLocs);
      SetUtils.RemoveAll(kLocs, mainLocs);
      SetUtils.RemoveAll(kLocs, sideLocs);
      SetUtils.RemoveAll(kLocs, new List<Location>{aLoc, bLoc, cLoc, dLoc, eLoc, fLoc, gLoc, hLoc, iLoc, jLoc});
      foreach (var kLoc in kLocs) {
        var nearbyNonEmpty = new SortedSet<Location>(pattern.GetAdjacentLocations(kLoc, false));
        SetUtils.RetainAll(nearbyNonEmpty, nonEmptyLocs);
        if (nearbyNonEmpty.Count > 0) {
          continue;
        }
        FindL(results, aLoc, bLoc, cLoc, dLoc, eLoc, fLoc, gLoc, hLoc, iLoc, jLoc, kLoc);
      }
    }
    void FindL(List<Bridge> results, Location aLoc, Location bLoc, Location cLoc, Location dLoc, Location eLoc, Location fLoc, Location gLoc, Location hLoc, Location iLoc, Location jLoc, Location kLoc) {
      // L is an new empty loc next to K and next to E, and surrounded by only other empty tiles.
      var lLocs = new SortedSet<Location>(pattern.GetAdjacentLocations(kLoc, false));
      SetUtils.RetainAll(lLocs, circleLocs);
      SetUtils.RemoveAll(lLocs, mainLocs);
      SetUtils.RemoveAll(lLocs, sideLocs);
      SetUtils.RetainAll(lLocs, new SortedSet<Location>(pattern.GetAdjacentLocations(eLoc, false)));
      SetUtils.RemoveAll(lLocs, new List<Location>{aLoc, bLoc, cLoc, dLoc, eLoc, fLoc, gLoc, hLoc, iLoc, jLoc, kLoc});
      foreach (var lLoc in lLocs) {
        var nearbyNonEmpty = new SortedSet<Location>(pattern.GetAdjacentLocations(lLoc, false));
        SetUtils.RetainAll(nearbyNonEmpty, nonEmptyLocs);
        if (nearbyNonEmpty.Count > 0) {
          continue;
        }
        FindM(results, aLoc, bLoc, cLoc, dLoc, eLoc, fLoc, gLoc, hLoc, iLoc, jLoc, kLoc, lLoc);
      }
    }
    void FindM(List<Bridge> results, Location aLoc, Location bLoc, Location cLoc, Location dLoc, Location eLoc, Location fLoc, Location gLoc, Location hLoc, Location iLoc, Location jLoc, Location kLoc, Location lLoc) {
      // M is a new main loc next to B.
      var mLocs = new SortedSet<Location>(pattern.GetAdjacentLocations(bLoc, false));
      SetUtils.RetainAll(mLocs, circleLocs);
      SetUtils.RetainAll(mLocs, mainLocs);
      SetUtils.RemoveAll(mLocs, new List<Location>{aLoc, bLoc, cLoc, dLoc, eLoc, fLoc, gLoc, hLoc, iLoc, jLoc, kLoc, lLoc});
      foreach (var mLoc in mLocs) {
        FindN(results, aLoc, bLoc, cLoc, dLoc, eLoc, fLoc, gLoc, hLoc, iLoc, jLoc, kLoc, lLoc, mLoc);
      }
    }
    void FindN(List<Bridge> results, Location aLoc, Location bLoc, Location cLoc, Location dLoc, Location eLoc, Location fLoc, Location gLoc, Location hLoc, Location iLoc, Location jLoc, Location kLoc, Location lLoc, Location mLoc) {
      // N is a new main loc next to G.
      var nLocs = new SortedSet<Location>(pattern.GetAdjacentLocations(gLoc, false));
      SetUtils.RetainAll(nLocs, circleLocs);
      SetUtils.RetainAll(nLocs, mainLocs);
      SetUtils.RemoveAll(nLocs, new List<Location>{aLoc, bLoc, cLoc, dLoc, eLoc, fLoc, gLoc, hLoc, iLoc, jLoc, kLoc, lLoc, mLoc});
      foreach (var nLoc in nLocs) {
        FindO(results, aLoc, bLoc, cLoc, dLoc, eLoc, fLoc, gLoc, hLoc, iLoc, jLoc, kLoc, lLoc, mLoc, nLoc);
      }
    }
    void FindO(List<Bridge> results, Location aLoc, Location bLoc, Location cLoc, Location dLoc, Location eLoc, Location fLoc, Location gLoc, Location hLoc, Location iLoc, Location jLoc, Location kLoc, Location lLoc, Location mLoc, Location nLoc) {
      // O is a new side loc next to C but not necessarily next to M (see BridgeMaking1.png).
      var oLocs = new SortedSet<Location>(pattern.GetAdjacentLocations(cLoc, false));
      SetUtils.RetainAll(oLocs, circleLocs);
      SetUtils.RetainAll(oLocs, sideLocs);
      SetUtils.RemoveAll(oLocs, new List<Location>{aLoc, bLoc, cLoc, dLoc, eLoc, fLoc, gLoc, hLoc, iLoc, jLoc, kLoc, lLoc, mLoc, nLoc});
      foreach (var oLoc in oLocs) {
        FindP(results, aLoc, bLoc, cLoc, dLoc, eLoc, fLoc, gLoc, hLoc, iLoc, jLoc, kLoc, lLoc, mLoc, nLoc, oLoc);
      }
    }
    void FindP(List<Bridge> results, Location aLoc, Location bLoc, Location cLoc, Location dLoc, Location eLoc, Location fLoc, Location gLoc, Location hLoc, Location iLoc, Location jLoc, Location kLoc, Location lLoc, Location mLoc, Location nLoc, Location oLoc) {
      // P is a new side loc next to F but not necessarily next to N (see BridgeMaking1.png).
      var pLocs = new SortedSet<Location>(pattern.GetAdjacentLocations(fLoc, false));
      SetUtils.RetainAll(pLocs, circleLocs);
      SetUtils.RetainAll(pLocs, sideLocs);
      SetUtils.RemoveAll(pLocs, new List<Location>{aLoc, bLoc, cLoc, dLoc, eLoc, fLoc, gLoc, hLoc, iLoc, jLoc, kLoc, lLoc, mLoc, nLoc, oLoc});
      foreach (var pLoc in pLocs) {
        var result =
            new Bridge(aLoc, bLoc, cLoc, dLoc, eLoc, fLoc, gLoc, hLoc, iLoc, jLoc, kLoc, lLoc, mLoc, nLoc, oLoc, pLoc);
        results.Add(result);
      }
    }
  }
}
