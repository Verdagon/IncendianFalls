using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class FindPathRequest : IComparable<FindPathRequest> {
  public static readonly string NAME = "FindPathRequest";
  public class EqualityComparer : IEqualityComparer<FindPathRequest> {
    public bool Equals(FindPathRequest a, FindPathRequest b) {
      return a.Equals(b);
    }
    public int GetHashCode(FindPathRequest a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<FindPathRequest> {
    public int Compare(FindPathRequest a, FindPathRequest b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int gameId;
  public readonly int unitId;
  public readonly Location destination;
  public FindPathRequest(
      int gameId,
      int unitId,
      Location destination) {
    this.gameId = gameId;
    this.unitId = unitId;
    this.destination = destination;
    int hash = 0;
    hash = hash * 37 + gameId.GetDeterministicHashCode();
    hash = hash * 37 + unitId.GetDeterministicHashCode();
    hash = hash * 37 + destination.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(FindPathRequest a, FindPathRequest b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(FindPathRequest a, FindPathRequest b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is FindPathRequest)) {
      return false;
    }
    var that = obj as FindPathRequest;
    return true
               && gameId.Equals(that.gameId)
        && unitId.Equals(that.unitId)
        && destination.Equals(that.destination)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(FindPathRequest that) {
    if (gameId != that.gameId) {
      return gameId.CompareTo(that.gameId);
    }
    if (unitId != that.unitId) {
      return unitId.CompareTo(that.unitId);
    }
    if (destination != that.destination) {
      return destination.CompareTo(that.destination);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "FindPathRequest(" +
        gameId.DStr() + ", " +
        unitId.DStr() + ", " +
        destination.DStr()
        + ")";

    }
    public static FindPathRequest Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var gameId = source.ParseInt();
      source.Expect(",");
      var unitId = source.ParseInt();
      source.Expect(",");
      var destination = Location.Parse(source);
      source.Expect(")");
      return new FindPathRequest(gameId, unitId, destination);
  }
}
       
}
