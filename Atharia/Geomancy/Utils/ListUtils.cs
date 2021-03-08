using System;
using System.Collections.Generic;
using Geomancer.Model;

namespace Geomancer.Model {
  public static class ListUtils {
    public static List<T> GetRandomN<T>(List<T> list, Rand rand, int numShuffles, int numToGet) {
      if (list.Count < numToGet) {
        throw new Exception("wat");
      }
      List<T> shuffled = ListUtils.Shuffled(list, rand, numShuffles);
      shuffled.RemoveRange(numToGet, shuffled.Count - numToGet);
      return shuffled;
    }

    public static List<T> Shuffled<T>(List<T> original, Rand rand, int numShuffles) {
      List<T> shuffled = new List<T>(original);

      for (int i = 0; i < numShuffles; i++) {
        int n = shuffled.Count;
        while (n > 1) {
          n--;
          int k = rand.Next() % (shuffled.Count - 1);
          var value = shuffled[k];
          shuffled[k] = shuffled[n];
          shuffled[n] = value;
        }
      }

      return shuffled;
    }
  }
}