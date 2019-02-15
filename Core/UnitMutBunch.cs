using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class UnitMutBunch {// : IEnumerable<Unit> {
  public readonly Root root;
  public readonly int id;

  public UnitMutBunch(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public UnitMutBunchIncarnation incarnation {
    get { return root.GetUnitMutBunchIncarnation(id); }
  }
  public void AddObserver(IUnitMutBunchEffectObserver observer) {
    root.AddUnitMutBunchObserver(id, observer);
  }
  public void RemoveObserver(IUnitMutBunchEffectObserver observer) {
    root.RemoveUnitMutBunchObserver(id, observer);
  }
  public void Add(Unit element) {
    root.EffectUnitMutBunchAdd(id, element.id);
  }
  public void Remove(Unit element) {
    root.EffectUnitMutBunchRemove(id, element.id);
  }
  public void Clear() {
    foreach (var elementId in new List<int>(incarnation.set)) {
      root.EffectUnitMutBunchRemove(id, elementId);
    }
  }
  public int Count { get { return incarnation.set.Count; } }
  public IEnumerator<Unit> GetEnumerator() {
    foreach (var element in incarnation.set) {
      yield return root.GetUnit(element);
    }
  }
}
         
}
