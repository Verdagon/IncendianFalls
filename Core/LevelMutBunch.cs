using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class LevelMutBunch {// : IEnumerable<Level> {
  public readonly Root root;
  public readonly int id;

  public LevelMutBunch(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public LevelMutBunchIncarnation incarnation {
    get { return root.GetLevelMutBunchIncarnation(id); }
  }
  public void AddObserver(ILevelMutBunchEffectObserver observer) {
    root.AddLevelMutBunchObserver(id, observer);
  }
  public void RemoveObserver(ILevelMutBunchEffectObserver observer) {
    root.RemoveLevelMutBunchObserver(id, observer);
  }
  public void Add(Level element) {
    root.EffectLevelMutBunchAdd(id, element.id);
  }
  public void Remove(Level element) {
    root.EffectLevelMutBunchRemove(id, element.id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectLevelMutBunchRemove(id, elementId);
    }
  }
  public int Count { get { return incarnation.set.Count; } }
  public int GetDeterministicHashCode() {
    return incarnation.GetDeterministicHashCode();
  }
  public IEnumerator<Level> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetLevel(element);
    }
  }
}
         
}
