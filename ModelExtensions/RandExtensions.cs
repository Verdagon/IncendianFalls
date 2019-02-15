using System;
using Atharia.Model;

namespace IncendianFalls {
  public static class RandExtensions {
    public static int Next(this Rand rand) {
      int result = new Random(rand.rand).Next();
      rand.rand = result;
      return result;
    }
    public static int Next(this Rand rand, int min, int max) {
      int result = new Random(rand.rand).Next(min, max);
      rand.rand = new Random(rand.rand).Next();
      return result;
    }
  }
}
