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
  public static List<T> GetRandomN<T>(Rand rand, SortedSet<T> set, int n) {
    if (set.Count < n) {
      throw new Exception("wat");
    }


    SortedSet<T> possibleValues = new SortedSet<T>(set);

    List<T> result = new List<T>();

    for (int i = 0; i < n; i++) {
      int randomUnusedLocationIndex = rand.Next() % set.Count;
      foreach (var key in set) {
        if (randomUnusedLocationIndex == 0) {
          result.Add(key);
          possibleValues.Remove(key);
          break;
        }
        randomUnusedLocationIndex--;
      }
    }

    return result;
  }

  public static void RemoveAll<T>(
      SortedSet<T> removeFrom,
      SortedSet<T> removeThese) {
    foreach (var loc in removeThese) {
      removeFrom.Remove(loc);
    }
  }
}
