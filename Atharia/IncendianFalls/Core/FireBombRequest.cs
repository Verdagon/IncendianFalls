using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class FireBombRequest : IComparable<FireBombRequest> {
  public static readonly string NAME = "FireBombRequest";
  public class EqualityComparer : IEqualityComparer<FireBombRequest> {
    public bool Equals(FireBombRequest a, FireBombRequest b) {
      return a.Equals(b);
    }
    public int GetHashCode(FireBombRequest a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<FireBombRequest> {
    public int Compare(FireBombRequest a, FireBombRequest b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int gameId;
  public readonly Location location;
  public FireBombRequest(
      int gameId,
      Location location) {
    this.gameId = gameId;
    this.location = location;
    int hash = 0;
    hash = hash * 37 + gameId.GetDeterministicHashCode();
    hash = hash * 37 + location.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(FireBombRequest a, FireBombRequest b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(FireBombRequest a, FireBombRequest b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is FireBombRequest)) {
      return false;
    }
    var that = obj as FireBombRequest;
    return true
               && gameId.Equals(that.gameId)
        && location.Equals(that.location)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(FireBombRequest that) {
    if (gameId != that.gameId) {
      return gameId.CompareTo(that.gameId);
    }
    if (location != that.location) {
      return location.CompareTo(that.location);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "FireBombRequest(" +
        gameId.DStr() + ", " +
        location.DStr()
        + ")";

    }
    public static FireBombRequest Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var gameId = source.ParseInt();
      source.Expect(",");
      var location = Location.Parse(source);
      source.Expect(")");
      return new FireBombRequest(gameId, location);
  }
}
       
}
