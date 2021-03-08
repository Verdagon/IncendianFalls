using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SetupGauntletGameRequest : IComparable<SetupGauntletGameRequest> {
  public static readonly string NAME = "SetupGauntletGameRequest";
  public class EqualityComparer : IEqualityComparer<SetupGauntletGameRequest> {
    public bool Equals(SetupGauntletGameRequest a, SetupGauntletGameRequest b) {
      return a.Equals(b);
    }
    public int GetHashCode(SetupGauntletGameRequest a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<SetupGauntletGameRequest> {
    public int Compare(SetupGauntletGameRequest a, SetupGauntletGameRequest b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int randomSeed;
  public readonly bool squareLevelsOnly;
  public SetupGauntletGameRequest(
      int randomSeed,
      bool squareLevelsOnly) {
    this.randomSeed = randomSeed;
    this.squareLevelsOnly = squareLevelsOnly;
    int hash = 0;
    hash = hash * 37 + randomSeed.GetDeterministicHashCode();
    hash = hash * 37 + squareLevelsOnly.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(SetupGauntletGameRequest a, SetupGauntletGameRequest b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(SetupGauntletGameRequest a, SetupGauntletGameRequest b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is SetupGauntletGameRequest)) {
      return false;
    }
    var that = obj as SetupGauntletGameRequest;
    return true
               && randomSeed.Equals(that.randomSeed)
        && squareLevelsOnly.Equals(that.squareLevelsOnly)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(SetupGauntletGameRequest that) {
    if (randomSeed != that.randomSeed) {
      return randomSeed.CompareTo(that.randomSeed);
    }
    if (squareLevelsOnly != that.squareLevelsOnly) {
      return squareLevelsOnly.CompareTo(that.squareLevelsOnly);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "SetupGauntletGameRequest(" +
        randomSeed.DStr() + ", " +
        squareLevelsOnly.DStr()
        + ")";

    }
    public static SetupGauntletGameRequest Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var randomSeed = source.ParseInt();
      source.Expect(",");
      var squareLevelsOnly = source.ParseBool();
      source.Expect(")");
      return new SetupGauntletGameRequest(randomSeed, squareLevelsOnly);
  }
}
       
}
