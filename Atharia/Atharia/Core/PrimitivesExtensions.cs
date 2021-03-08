using System;
using System.Collections;

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
  public static int GetDeterministicHashCode(this int a) {
    return a.GetHashCode();
  }
  public static int GetDeterministicHashCode(this bool a) {
    return a.GetHashCode();
  }
  public static int GetDeterministicHashCode(this float a) {
    return a.GetHashCode();
  }
  public static int GetDeterministicHashCode(this string s) {
    int hash = 0;
    hash = 37 * hash + s.Length;
    foreach (var c in s) {
      hash = 37 * hash + c;
    }
    return hash;
  }
  public static string DStr(this int a) {
    return a.ToString();
  }
  public static string DStr(this bool a) {
    return a ? "true" : "false";
  }
  public static string DStr(this float a) {
    return a.ToString();
  }
  public static string DStr(this string s) {
    return '"' + s + '"';
  }
}
    
}
