using System;
using Atharia.Model;

namespace Atharia.Model {
  public static class RandExtensions {
    public static int Next(this Rand rand) {
      rand.root.GetDeterministicHashCode();
      int result = new Random(rand.rand).Next();
      rand.rand = result;
      rand.root.GetDeterministicHashCode();
      return result;
    }
    public static int Next(this Rand rand, int min, int max) {
      rand.root.GetDeterministicHashCode();
      int randomThing = new Random(rand.rand).Next();
      rand.root.GetDeterministicHashCode();
      rand.rand = randomThing;
      rand.root.GetDeterministicHashCode();
      return randomThing % (max + 1 - min) + min;
    }
  }
}
