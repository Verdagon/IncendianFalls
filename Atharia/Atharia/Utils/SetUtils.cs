using System;
using System.Collections.Generic;
using Atharia.Model;

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
  // Removes any from the removeFrom set that aren't in the keepThese set.
  public static void RetainAll<T>(
      SortedSet<T> removeFrom,
      SortedSet<T> keepThese) {
    foreach (var loc in new SortedSet<T>(removeFrom)) {
      if (!keepThese.Contains(loc)) {
        removeFrom.Remove(loc);
      }
    }
  }

  public static void AddAll<T>(
      SortedSet<T> addTo,
      IEnumerable<T> addThese) {
    foreach (var loc in addThese) {
      addTo.Add(loc);
    }
  }
}
