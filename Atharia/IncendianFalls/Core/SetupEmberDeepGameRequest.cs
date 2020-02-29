using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class SetupEmberDeepGameRequest : IComparable<SetupEmberDeepGameRequest> {
  public static readonly string NAME = "SetupEmberDeepGameRequest";
  public class EqualityComparer : IEqualityComparer<SetupEmberDeepGameRequest> {
    public bool Equals(SetupEmberDeepGameRequest a, SetupEmberDeepGameRequest b) {
      return a.Equals(b);
    }
    public int GetHashCode(SetupEmberDeepGameRequest a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<SetupEmberDeepGameRequest> {
    public int Compare(SetupEmberDeepGameRequest a, SetupEmberDeepGameRequest b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int randomSeed;
  public readonly bool squareLevelsOnly;
  public SetupEmberDeepGameRequest(
      int randomSeed,
      bool squareLevelsOnly) {
    this.randomSeed = randomSeed;
    this.squareLevelsOnly = squareLevelsOnly;
    int hash = 0;
    hash = hash * 37 + randomSeed.GetDeterministicHashCode();
    hash = hash * 37 + squareLevelsOnly.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(SetupEmberDeepGameRequest a, SetupEmberDeepGameRequest b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(SetupEmberDeepGameRequest a, SetupEmberDeepGameRequest b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is SetupEmberDeepGameRequest)) {
      return false;
    }
    var that = obj as SetupEmberDeepGameRequest;
    return true
               && randomSeed.Equals(that.randomSeed)
        && squareLevelsOnly.Equals(that.squareLevelsOnly)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(SetupEmberDeepGameRequest that) {
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
    return "SetupEmberDeepGameRequest(" +
        randomSeed.DStr() + ", " +
        squareLevelsOnly.DStr()
        + ")";

    }
    public static SetupEmberDeepGameRequest Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var randomSeed = source.ParseInt();
      source.Expect(",");
      var squareLevelsOnly = source.ParseBool();
      source.Expect(")");
      return new SetupEmberDeepGameRequest(randomSeed, squareLevelsOnly);
  }
}
       
}
