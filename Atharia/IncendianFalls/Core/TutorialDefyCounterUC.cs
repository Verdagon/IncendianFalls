using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class TutorialDefyCounterUC {
  public readonly Root root;
  public readonly int id;
  public TutorialDefyCounterUC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public TutorialDefyCounterUCIncarnation incarnation { get { return root.GetTutorialDefyCounterUCIncarnation(id); } }
  public void AddObserver(ITutorialDefyCounterUCEffectObserver observer) {
    root.AddTutorialDefyCounterUCObserver(id, observer);
  }
  public void RemoveObserver(ITutorialDefyCounterUCEffectObserver observer) {
    root.RemoveTutorialDefyCounterUCObserver(id, observer);
  }
  public void Delete() {
    root.EffectTutorialDefyCounterUCDelete(id);
  }
  public static TutorialDefyCounterUC Null = new TutorialDefyCounterUC(null, 0);
  public bool Exists() { return root != null && root.TutorialDefyCounterUCExists(id); }
  public bool NullableIs(TutorialDefyCounterUC that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public void CheckForNullViolations(List<string> violations) {
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
  }
  public bool Is(TutorialDefyCounterUC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public int numDefiesRemaining {
    get { return incarnation.numDefiesRemaining; }
    set { root.EffectTutorialDefyCounterUCSetNumDefiesRemaining(id, value); }
  }
  public string onChangeTriggerName {
    get { return incarnation.onChangeTriggerName; }
  }
}
}
