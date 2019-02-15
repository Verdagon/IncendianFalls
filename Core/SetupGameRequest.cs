using System;
using System.Collections.Generic;

namespace Atharia.Model {
public class SetupGameRequest : IComparable<SetupGameRequest> {
  public static readonly string NAME = "SetupGameRequest";
  public class EqualityComparer : IEqualityComparer<SetupGameRequest> {
    public bool Equals(SetupGameRequest a, SetupGameRequest b) {
      return a.Equals(b);
    }
    public int GetHashCode(SetupGameRequest a) {
      return a.GetHashCode();
    }
  }
  public class Comparer : IComparer<SetupGameRequest> {
    public int Compare(SetupGameRequest a, SetupGameRequest b) {
      return a.CompareTo(b);
    }
  }
       public readonly int randomSeed;
  public readonly bool squareLevelsOnly;
  public SetupGameRequest(
      int randomSeed,
      bool squareLevelsOnly) {
    this.randomSeed = randomSeed;
    this.squareLevelsOnly = squareLevelsOnly;

  }
  public static bool operator==(SetupGameRequest a, SetupGameRequest b) {
    return a.Equals(b);
  }
  public static bool operator!=(SetupGameRequest a, SetupGameRequest b) {
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is SetupGameRequest)) {
      return false;
    }
    var that = obj as SetupGameRequest;
    return true
             && randomSeed.Equals(that.randomSeed)
        && squareLevelsOnly.Equals(that.squareLevelsOnly)
        ;
  }
  public override int GetHashCode() {
    int hash = 0;
    hash = hash * 1337;
    hash = hash + randomSeed.GetHashCode();
    hash = hash * 1337;
    hash = hash + squareLevelsOnly.GetHashCode();
    return hash;
  }
  public int CompareTo(SetupGameRequest that) {
    if (randomSeed != that.randomSeed) {
      return randomSeed.CompareTo(that.randomSeed);
    }
    if (squareLevelsOnly != that.squareLevelsOnly) {
      return squareLevelsOnly.CompareTo(that.squareLevelsOnly);
    }
    return 0;
  }
  public string DStr() {
    return "SetupGameRequest(" +
        randomSeed + ", " +
        (squareLevelsOnly ? "true" : "false")
        + ")";

    }
    public static SetupGameRequest Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var randomSeed = source.ParseInt();
      source.Expect(",");
      var squareLevelsOnly = source.ParseBool();
      source.Expect(")");
      return new SetupGameRequest(randomSeed, squareLevelsOnly);
  }
}
     
}
