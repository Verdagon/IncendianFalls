using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TimeAnchorMoveRequest : IComparable<TimeAnchorMoveRequest> {
  public static readonly string NAME = "TimeAnchorMoveRequest";
  public class EqualityComparer : IEqualityComparer<TimeAnchorMoveRequest> {
    public bool Equals(TimeAnchorMoveRequest a, TimeAnchorMoveRequest b) {
      return a.Equals(b);
    }
    public int GetHashCode(TimeAnchorMoveRequest a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<TimeAnchorMoveRequest> {
    public int Compare(TimeAnchorMoveRequest a, TimeAnchorMoveRequest b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int gameId;
  public readonly Location destination;
  public TimeAnchorMoveRequest(
      int gameId,
      Location destination) {
    this.gameId = gameId;
    this.destination = destination;
    int hash = 0;
    hash = hash * 37 + gameId.GetDeterministicHashCode();
    hash = hash * 37 + destination.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(TimeAnchorMoveRequest a, TimeAnchorMoveRequest b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(TimeAnchorMoveRequest a, TimeAnchorMoveRequest b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is TimeAnchorMoveRequest)) {
      return false;
    }
    var that = obj as TimeAnchorMoveRequest;
    return true
               && gameId.Equals(that.gameId)
        && destination.Equals(that.destination)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(TimeAnchorMoveRequest that) {
    if (gameId != that.gameId) {
      return gameId.CompareTo(that.gameId);
    }
    if (destination != that.destination) {
      return destination.CompareTo(that.destination);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "TimeAnchorMoveRequest(" +
        gameId.DStr() + ", " +
        destination.DStr()
        + ")";

    }
    public static TimeAnchorMoveRequest Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var gameId = source.ParseInt();
      source.Expect(",");
      var destination = Location.Parse(source);
      source.Expect(")");
      return new TimeAnchorMoveRequest(gameId, destination);
  }
}
       
}
