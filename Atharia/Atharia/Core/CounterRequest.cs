using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class CounterRequest : IComparable<CounterRequest> {
  public static readonly string NAME = "CounterRequest";
  public class EqualityComparer : IEqualityComparer<CounterRequest> {
    public bool Equals(CounterRequest a, CounterRequest b) {
      return a.Equals(b);
    }
    public int GetHashCode(CounterRequest a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<CounterRequest> {
    public int Compare(CounterRequest a, CounterRequest b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int gameId;
  public CounterRequest(
      int gameId) {
    this.gameId = gameId;
    int hash = 0;
    hash = hash * 37 + gameId.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(CounterRequest a, CounterRequest b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(CounterRequest a, CounterRequest b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is CounterRequest)) {
      return false;
    }
    var that = obj as CounterRequest;
    return true
               && gameId.Equals(that.gameId)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(CounterRequest that) {
    if (gameId != that.gameId) {
      return gameId.CompareTo(that.gameId);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "CounterRequest(" +
        gameId.DStr()
        + ")";

    }
    public static CounterRequest Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var gameId = source.ParseInt();
      source.Expect(")");
      return new CounterRequest(gameId);
  }
}
       
}
