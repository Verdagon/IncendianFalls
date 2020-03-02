using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class FlyCameraEvent : IComparable<FlyCameraEvent> {
  public static readonly string NAME = "FlyCameraEvent";
  public class EqualityComparer : IEqualityComparer<FlyCameraEvent> {
    public bool Equals(FlyCameraEvent a, FlyCameraEvent b) {
      return a.Equals(b);
    }
    public int GetHashCode(FlyCameraEvent a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<FlyCameraEvent> {
    public int Compare(FlyCameraEvent a, FlyCameraEvent b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly Location lookAt;
  public readonly Vec3 relativeCameraPosition;
  public readonly int transitionTimeMs;
  public readonly string endTriggerName;
  public FlyCameraEvent(
      Location lookAt,
      Vec3 relativeCameraPosition,
      int transitionTimeMs,
      string endTriggerName) {
    this.lookAt = lookAt;
    this.relativeCameraPosition = relativeCameraPosition;
    this.transitionTimeMs = transitionTimeMs;
    this.endTriggerName = endTriggerName;
    int hash = 0;
    hash = hash * 37 + lookAt.GetDeterministicHashCode();
    hash = hash * 37 + relativeCameraPosition.GetDeterministicHashCode();
    hash = hash * 37 + transitionTimeMs.GetDeterministicHashCode();
    hash = hash * 37 + endTriggerName.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(FlyCameraEvent a, FlyCameraEvent b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(FlyCameraEvent a, FlyCameraEvent b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is FlyCameraEvent)) {
      return false;
    }
    var that = obj as FlyCameraEvent;
    return true
               && lookAt.Equals(that.lookAt)
        && relativeCameraPosition.Equals(that.relativeCameraPosition)
        && transitionTimeMs.Equals(that.transitionTimeMs)
        && endTriggerName.Equals(that.endTriggerName)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(FlyCameraEvent that) {
    if (lookAt != that.lookAt) {
      return lookAt.CompareTo(that.lookAt);
    }
    if (relativeCameraPosition != that.relativeCameraPosition) {
      return relativeCameraPosition.CompareTo(that.relativeCameraPosition);
    }
    if (transitionTimeMs != that.transitionTimeMs) {
      return transitionTimeMs.CompareTo(that.transitionTimeMs);
    }
    if (endTriggerName != that.endTriggerName) {
      return endTriggerName.CompareTo(that.endTriggerName);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "FlyCameraEvent(" +
        lookAt.DStr() + ", " +
        relativeCameraPosition.DStr() + ", " +
        transitionTimeMs.DStr() + ", " +
        endTriggerName.DStr()
        + ")";

    }
    public static FlyCameraEvent Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var lookAt = Location.Parse(source);
      source.Expect(",");
      var relativeCameraPosition = Vec3.Parse(source);
      source.Expect(",");
      var transitionTimeMs = source.ParseInt();
      source.Expect(",");
      var endTriggerName = source.ParseStr();
      source.Expect(")");
      return new FlyCameraEvent(lookAt, relativeCameraPosition, transitionTimeMs, endTriggerName);
  }
}
       
}
