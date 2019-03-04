using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TimeShiftRequest : IComparable<TimeShiftRequest> {
  public static readonly string NAME = "TimeShiftRequest";
  public class EqualityComparer : IEqualityComparer<TimeShiftRequest> {
    public bool Equals(TimeShiftRequest a, TimeShiftRequest b) {
      return a.Equals(b);
    }
    public int GetHashCode(TimeShiftRequest a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<TimeShiftRequest> {
    public int Compare(TimeShiftRequest a, TimeShiftRequest b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int gameId;
  public TimeShiftRequest(
      int gameId) {
    this.gameId = gameId;
    int hash = 0;
    hash = hash * 37 + gameId.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(TimeShiftRequest a, TimeShiftRequest b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(TimeShiftRequest a, TimeShiftRequest b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is TimeShiftRequest)) {
      return false;
    }
    var that = obj as TimeShiftRequest;
    return true
               && gameId.Equals(that.gameId)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(TimeShiftRequest that) {
    if (gameId != that.gameId) {
      return gameId.CompareTo(that.gameId);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "TimeShiftRequest(" +
        gameId.DStr()
        + ")";

    }
    public static TimeShiftRequest Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var gameId = source.ParseInt();
      source.Expect(")");
      return new TimeShiftRequest(gameId);
  }
}
       
}
