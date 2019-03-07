using System;
using System.Collections.Generic;
using Atharia.Model;

public class SetUtils {
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
  public static List<T> GetRandomN<T>(Rand rand, SortedSet<T> set, int x) {
    if (set.Count < x) {
      throw new Exception("wat");
    }

    List<T> shuffled = new List<T>(set);

    // Shuffle them
    int n = shuffled.Count;
    while (n > 1) {
      n--;
      int k = rand.Next() % (shuffled.Count - 1);
      var value = shuffled[k];
      shuffled[k] = shuffled[n];
      shuffled[n] = value;
    }

    shuffled.RemoveRange(x, shuffled.Count - x);
    return shuffled;
  }

  public static void RemoveAll<T>(
      SortedSet<T> removeFrom,
      SortedSet<T> removeThese) {
    foreach (var loc in removeThese) {
      removeFrom.Remove(loc);
    }
  }

  public static void AddAll<T>(
      SortedSet<T> addTo,
      SortedSet<T> addThese) {
    foreach (var loc in addThese) {
      addTo.Add(loc);
    }
  }
}
