using System;
using System.Collections.Generic;
using Geomancer.Model;

public class Asserts {
  public static void Assert(bool condition) {
    if (!condition) {
      throw new Exception("Assertion failed");
    }
  }
  public static void Assert(bool condition, String message) {
    if (!condition) {
      throw new Exception(message);
    }
  }
}