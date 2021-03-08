using System;
using System.Collections.Generic;
using Geomancer.Model;

namespace Geomancer.Model {
  public class SetUtils {
    public static List<T> GetRandomN<T>(SortedSet<T> set, Rand rand, int numShuffles, int numToGet) {
      return ListUtils.GetRandomN(new List<T>(set), rand, numShuffles, numToGet);
    }
    public static T GetFirst<T>(SortedSet<T> set) {
      foreach (var key in set) {
        return key;
      }
      throw new Exception("Set is empty!");
    }
    public static List<T> GetFirstN<T>(SortedSet<T> set, int n) {
      List<T> items = new List<T>();
      foreach (var item in set) {
        if (items.Count >= n) {
          return items;
        }
        items.Add(item);
      }
      if (items.Count >= n) {
        return items;
      }
      throw new Exception("Set not big enough!");
    }
    public static T GetRandom<T>(int randomInt, SortedSet<T> set) {
      if (set.Count == 0) {
        throw new Exception("wat");
      }
      int randomUnusedLocationIndex = randomInt % set.Count;
      foreach (var key in set) {
        if (randomUnusedLocationIndex == 0) {
          return key;
        }
        randomUnusedLocationIndex--;
      }
      throw new Exception("unreachable");
    }
    public static void RemoveAll<T>(
        SortedSet<T> removeFrom,
        IEnumerable<T> removeThese) {
      foreach (var loc in removeThese) {
        removeFrom.Remove(loc);
      }
    }

    public static void AddAll<T>(
        SortedSet<T> addTo,
        IEnumerable<T> addThese) {
      foreach (var loc in addThese) {
        addTo.Add(loc);
      }
    }

    public static (SortedSet<T>, SortedSet<T>) Diff<T>(SortedSet<T> a, SortedSet<T> b) {
      var addToA = new SortedSet<T>();
      var removeFromA = new SortedSet<T>();

      foreach (var x in a) {
        if (!b.Contains(x)) {
          removeFromA.Add(x);
        }
      }
      foreach (var x in b) {
        if (!a.Contains(x)) {
          addToA.Add(x);
        }
      }
      return (addToA, removeFromA);
    }
  }
}