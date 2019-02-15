using System;
using System.Collections.Generic;

namespace Atharia.Model {

public struct Void { }

public static class PrimitivesExtensions {
  public static int Compare(this int a, int b) {
    return a - b;
  }
  public static int Compare(this float a, float b) {
    return Math.Sign(a - b);
  }
  public static int Compare(this bool a, bool b) {
    return (a ? 1 : 0).Compare(b ? 1 : 0);
  }
  public static int Compare(this string a, string b) {
    return a.CompareTo(b);
  }
}
    
}
