using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class KamikazeTargetTTCStrongByLocationMutMap {
  public readonly Root root;
  public readonly int id;


  public KamikazeTargetTTCStrongByLocationMutMap(Root root, int id) {
    this.root = root;
    this.id = id;
  }

  public KamikazeTargetTTCStrongByLocationMutMapIncarnation incarnation {
    get { return root.GetKamikazeTargetTTCStrongByLocationMutMapIncarnation(id); }
  }

  public bool Exists() { return root != null && root.KamikazeTargetTTCStrongByLocationMutMapExists(id); }

  public void AddObserver(EffectBroadcaster broadcaster, IKamikazeTargetTTCStrongByLocationMutMapEffectObserver observer) {
    broadcaster.AddKamikazeTargetTTCStrongByLocationMutMapObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IKamikazeTargetTTCStrongByLocationMutMapEffectObserver observer) {
    broadcaster.RemoveKamikazeTargetTTCStrongByLocationMutMapObserver(id, observer);
  }

  public void Add(Location key, KamikazeTargetTTC value) {
      root.EffectKamikazeTargetTTCStrongByLocationMutMapAdd(id, key, value.id);
  }

  public void Remove(Location key) {
    root.EffectKamikazeTargetTTCStrongByLocationMutMapRemove(id, key);
  }

  public int Count { get { return incarnation.map.Count; } }

  public void CheckForNullViolations(List<string> violations) {
    foreach (var entry in this) {
      var element = entry.Value;
      if (!root.KamikazeTargetTTCExists(element.id)) {
        violations.Add("Null constraint violated! KamikazeTargetTTCStrongByLocationMutMap#" + id + "." + element.id);
      }
    }
  }

  public void Delete() {
    root.EffectKamikazeTargetTTCStrongByLocationMutMapDelete(id);
  }
  public void Destruct() {
    var elements = new List<KamikazeTargetTTC>();
    foreach (var entry in this) {
      elements.Add(entry.Value);
    }
    this.Delete();
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    foreach (var entry in this) {
      var element = entry.Value;
      if (root.KamikazeTargetTTCExists(element.id)) {
       element.FindReachableObjects(foundIds);
      }
    }
  }
  public bool ContainsKey(Location key) {
    return incarnation.map.ContainsKey(key);
  }

  public List<Location> Keys {
    // Could be optimized, I think SortedDictionary uses an inner class instead of making a List
    // like this.
    get { return new List<Location>(incarnation.map.Keys); }
  }

  public KamikazeTargetTTC this[Location key] {
    get { return new KamikazeTargetTTC(root, incarnation.map[key]); }
  }

  public IEnumerator<KeyValuePair<Location, KamikazeTargetTTC>> GetEnumerator() {
    foreach (var keyAndValue in incarnation.map) {
      yield return new KeyValuePair<Location, KamikazeTargetTTC>(
          keyAndValue.Key,
          new KamikazeTargetTTC(root, keyAndValue.Value));
    }
  }
}
         
}
