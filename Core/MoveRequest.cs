using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class MoveRequest : IComparable<MoveRequest> {
  public static readonly string NAME = "MoveRequest";
  public class EqualityComparer : IEqualityComparer<MoveRequest> {
    public bool Equals(MoveRequest a, MoveRequest b) {
      return a.Equals(b);
    }
    public int GetHashCode(MoveRequest a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<MoveRequest> {
    public int Compare(MoveRequest a, MoveRequest b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int gameId;
  public readonly Location destination;
  public MoveRequest(
      int gameId,
      Location destination) {
    this.gameId = gameId;
    this.destination = destination;
    int hash = 0;
    hash = hash * 37 + gameId.GetDeterministicHashCode();
    hash = hash * 37 + destination.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(MoveRequest a, MoveRequest b) {
    return a.Equals(b);
  }
  public static bool operator!=(MoveRequest a, MoveRequest b) {
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is MoveRequest)) {
      return false;
    }
    var that = obj as MoveRequest;
    return true
               && gameId.Equals(that.gameId)
        && destination.Equals(that.destination)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(MoveRequest that) {
    if (gameId != that.gameId) {
      return gameId.CompareTo(that.gameId);
    }
    if (destination != that.destination) {
      return destination.CompareTo(that.destination);
    }
    return 0;
  }
  public string DStr() {
    return "MoveRequest(" +
        gameId.DStr() + ", " +
        destination.DStr()
        + ")";

    }
    public static MoveRequest Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var gameId = source.ParseInt();
      source.Expect(",");
      var destination = Location.Parse(source);
      source.Expect(")");
      return new MoveRequest(gameId, destination);
  }
}
       
}
