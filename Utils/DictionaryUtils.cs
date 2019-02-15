using System;
using System.Collections.Generic;
using Atharia.Model;

public class DictionaryUtils {
  public static T GetFirstKey<T>(SortedDictionary<T, object> dict) {
    foreach (var key in dict.Keys) {
      return key;
    }
    throw new Exception("Dict is empty!");
  }

  public static T GetAndRemoveFirstKey<T>(SortedDictionary<T, object> dict) {
    var key = GetFirstKey(dict);
    dict.Remove(key);
    return key;
  }

  public static T GetRandomKey<T>(int randomInt, SortedDictionary<T, object> dict) {
    if (dict.Count == 0) {
      throw new Exception("wat");
    }
    int randomUnusedLocationIndex = randomInt % dict.Count;
    foreach (var key in dict.Keys) {
      if (randomUnusedLocationIndex == 0) {
        return key;
      }
      randomUnusedLocationIndex--;
    }
    throw new Exception("unreachable");
  }

  public static void RemoveAll<T>(
      SortedDictionary<T, object> removeFrom,
      SortedDictionary<T, object> removeThese) {
    foreach (var loc in removeThese.Keys) {
      removeFrom.Remove(loc);
    }
  }
}