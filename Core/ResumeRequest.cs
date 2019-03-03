using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ResumeRequest : IComparable<ResumeRequest> {
  public static readonly string NAME = "ResumeRequest";
  public class EqualityComparer : IEqualityComparer<ResumeRequest> {
    public bool Equals(ResumeRequest a, ResumeRequest b) {
      return a.Equals(b);
    }
    public int GetHashCode(ResumeRequest a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<ResumeRequest> {
    public int Compare(ResumeRequest a, ResumeRequest b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int gameId;
  public ResumeRequest(
      int gameId) {
    this.gameId = gameId;
    int hash = 0;
    hash = hash * 37 + gameId.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(ResumeRequest a, ResumeRequest b) {
    return a.Equals(b);
  }
  public static bool operator!=(ResumeRequest a, ResumeRequest b) {
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is ResumeRequest)) {
      return false;
    }
    var that = obj as ResumeRequest;
    return true
               && gameId.Equals(that.gameId)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(ResumeRequest that) {
    if (gameId != that.gameId) {
      return gameId.CompareTo(that.gameId);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "ResumeRequest(" +
        gameId.DStr()
        + ")";

    }
    public static ResumeRequest Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var gameId = source.ParseInt();
      source.Expect(")");
      return new ResumeRequest(gameId);
  }
}
       
}
