using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SetupRavaArcanaGameRequest : IComparable<SetupRavaArcanaGameRequest> {
  public static readonly string NAME = "SetupRavaArcanaGameRequest";
  public class EqualityComparer : IEqualityComparer<SetupRavaArcanaGameRequest> {
    public bool Equals(SetupRavaArcanaGameRequest a, SetupRavaArcanaGameRequest b) {
      return a.Equals(b);
    }
    public int GetHashCode(SetupRavaArcanaGameRequest a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<SetupRavaArcanaGameRequest> {
    public int Compare(SetupRavaArcanaGameRequest a, SetupRavaArcanaGameRequest b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int randomSeed;
  public readonly int startLevel;
  public readonly bool squareLevelsOnly;
  public SetupRavaArcanaGameRequest(
      int randomSeed,
      int startLevel,
      bool squareLevelsOnly) {
    this.randomSeed = randomSeed;
    this.startLevel = startLevel;
    this.squareLevelsOnly = squareLevelsOnly;
    int hash = 0;
    hash = hash * 37 + randomSeed.GetDeterministicHashCode();
    hash = hash * 37 + startLevel.GetDeterministicHashCode();
    hash = hash * 37 + squareLevelsOnly.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(SetupRavaArcanaGameRequest a, SetupRavaArcanaGameRequest b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(SetupRavaArcanaGameRequest a, SetupRavaArcanaGameRequest b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is SetupRavaArcanaGameRequest)) {
      return false;
    }
    var that = obj as SetupRavaArcanaGameRequest;
    return true
               && randomSeed.Equals(that.randomSeed)
        && startLevel.Equals(that.startLevel)
        && squareLevelsOnly.Equals(that.squareLevelsOnly)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(SetupRavaArcanaGameRequest that) {
    if (randomSeed != that.randomSeed) {
      return randomSeed.CompareTo(that.randomSeed);
    }
    if (startLevel != that.startLevel) {
      return startLevel.CompareTo(that.startLevel);
    }
    if (squareLevelsOnly != that.squareLevelsOnly) {
      return squareLevelsOnly.CompareTo(that.squareLevelsOnly);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "SetupRavaArcanaGameRequest(" +
        randomSeed.DStr() + ", " +
        startLevel.DStr() + ", " +
        squareLevelsOnly.DStr()
        + ")";

    }
    public static SetupRavaArcanaGameRequest Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var randomSeed = source.ParseInt();
      source.Expect(",");
      var startLevel = source.ParseInt();
      source.Expect(",");
      var squareLevelsOnly = source.ParseBool();
      source.Expect(")");
      return new SetupRavaArcanaGameRequest(randomSeed, startLevel, squareLevelsOnly);
  }
}
       
}
