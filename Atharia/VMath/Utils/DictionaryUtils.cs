using System;
using System.Collections.Generic;
using Geomancer.Model;

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

  public static K GetFirstKeyFromSetMultimap<K, V>(
      SortedDictionary<K, SortedSet<V>> map) {
    foreach (var thing in map) {
      return thing.Key;
    }
    throw new Exception("No key to get!");
  }

  public static K GetLastKeyFromSetMultimap<K, V>(
      SortedDictionary<K, SortedSet<V>> map) {
    var keys = new List<K>(map.Keys);
    return keys[keys.Count - 1];
  }

  public static void AddToSetMultimap<K, V>(SortedDictionary<K, SortedSet<V>> map, K key, V value) {
    SortedSet<V> locs;
    if (!map.TryGetValue(key, out locs)) {
      locs = new SortedSet<V>();
      map.Add(key, locs);
    }
    locs.Add(value);
  }

}