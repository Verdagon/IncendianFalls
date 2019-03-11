using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SetupGameRequest : IComparable<SetupGameRequest> {
  public static readonly string NAME = "SetupGameRequest";
  public class EqualityComparer : IEqualityComparer<SetupGameRequest> {
    public bool Equals(SetupGameRequest a, SetupGameRequest b) {
      return a.Equals(b);
    }
    public int GetHashCode(SetupGameRequest a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<SetupGameRequest> {
    public int Compare(SetupGameRequest a, SetupGameRequest b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int randomSeed;
  public readonly bool squareLevelsOnly;
  public readonly bool gauntletMode;
  public SetupGameRequest(
      int randomSeed,
      bool squareLevelsOnly,
      bool gauntletMode) {
    this.randomSeed = randomSeed;
    this.squareLevelsOnly = squareLevelsOnly;
    this.gauntletMode = gauntletMode;
    int hash = 0;
    hash = hash * 37 + randomSeed.GetDeterministicHashCode();
    hash = hash * 37 + squareLevelsOnly.GetDeterministicHashCode();
    hash = hash * 37 + gauntletMode.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(SetupGameRequest a, SetupGameRequest b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(SetupGameRequest a, SetupGameRequest b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
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
        && gauntletMode.Equals(that.gauntletMode)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(SetupGameRequest that) {
    if (randomSeed != that.randomSeed) {
      return randomSeed.CompareTo(that.randomSeed);
    }
    if (squareLevelsOnly != that.squareLevelsOnly) {
      return squareLevelsOnly.CompareTo(that.squareLevelsOnly);
    }
    if (gauntletMode != that.gauntletMode) {
      return gauntletMode.CompareTo(that.gauntletMode);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "SetupGameRequest(" +
        randomSeed.DStr() + ", " +
        squareLevelsOnly.DStr() + ", " +
        gauntletMode.DStr()
        + ")";

    }
    public static SetupGameRequest Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var randomSeed = source.ParseInt();
      source.Expect(",");
      var squareLevelsOnly = source.ParseBool();
      source.Expect(",");
      var gauntletMode = source.ParseBool();
      source.Expect(")");
      return new SetupGameRequest(randomSeed, squareLevelsOnly, gauntletMode);
  }
}
       
}
