using System;
using System.Collections.Generic;

namespace Atharia.Model {
public class TimeShiftRequest : IComparable<TimeShiftRequest> {
  public static readonly string NAME = "TimeShiftRequest";
  public class EqualityComparer : IEqualityComparer<TimeShiftRequest> {
    public bool Equals(TimeShiftRequest a, TimeShiftRequest b) {
      return a.Equals(b);
    }
    public int GetHashCode(TimeShiftRequest a) {
      return a.GetHashCode();
    }
  }
  public class Comparer : IComparer<TimeShiftRequest> {
    public int Compare(TimeShiftRequest a, TimeShiftRequest b) {
      return a.CompareTo(b);
    }
  }
       public readonly int gameId;
  public readonly int version;
  public readonly int futuremostTime;
  public TimeShiftRequest(
      int gameId,
      int version,
      int futuremostTime) {
    this.gameId = gameId;
    this.version = version;
    this.futuremostTime = futuremostTime;

  }
  public static bool operator==(TimeShiftRequest a, TimeShiftRequest b) {
    return a.Equals(b);
  }
  public static bool operator!=(TimeShiftRequest a, TimeShiftRequest b) {
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
        && version.Equals(that.version)
        && futuremostTime.Equals(that.futuremostTime)
        ;
  }
  public override int GetHashCode() {
    int hash = 0;
    hash = hash * 1337;
    hash = hash + gameId.GetHashCode();
    hash = hash * 1337;
    hash = hash + version.GetHashCode();
    hash = hash * 1337;
    hash = hash + futuremostTime.GetHashCode();
    return hash;
  }
  public int CompareTo(TimeShiftRequest that) {
    if (gameId != that.gameId) {
      return gameId.CompareTo(that.gameId);
    }
    if (version != that.version) {
      return version.CompareTo(that.version);
    }
    if (futuremostTime != that.futuremostTime) {
      return futuremostTime.CompareTo(that.futuremostTime);
    }
    return 0;
  }
  public string DStr() {
    return "TimeShiftRequest(" +
        gameId + ", " +
        version + ", " +
        futuremostTime
        + ")";

    }
    public static TimeShiftRequest Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var gameId = source.ParseInt();
      source.Expect(",");
      var version = source.ParseInt();
      source.Expect(",");
      var futuremostTime = source.ParseInt();
      source.Expect(")");
      return new TimeShiftRequest(gameId, version, futuremostTime);
  }
}
     
}
