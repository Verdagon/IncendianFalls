using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class TimeScriptDirectiveUC {
  public readonly Root root;
  public readonly int id;
  public TimeScriptDirectiveUC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public TimeScriptDirectiveUCIncarnation incarnation { get { return root.GetTimeScriptDirectiveUCIncarnation(id); } }
  public void AddObserver(ITimeScriptDirectiveUCEffectObserver observer) {
    root.AddTimeScriptDirectiveUCObserver(id, observer);
  }
  public void RemoveObserver(ITimeScriptDirectiveUCEffectObserver observer) {
    root.RemoveTimeScriptDirectiveUCObserver(id, observer);
  }
  public void Delete() {
    root.EffectTimeScriptDirectiveUCDelete(id);
  }
  public static TimeScriptDirectiveUC Null = new TimeScriptDirectiveUC(null, 0);
  public bool Exists() { return root != null && root.TimeScriptDirectiveUCExists(id); }
  public bool NullableIs(TimeScriptDirectiveUC that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public void CheckForNullViolations(List<string> violations) {

    if (!root.IRequestMutListExists(script.id)) {
      violations.Add("Null constraint violated! TimeScriptDirectiveUC#" + id + ".script");
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    if (root.IRequestMutListExists(script.id)) {
      script.FindReachableObjects(foundIds);
    }
  }
  public bool Is(TimeScriptDirectiveUC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public IRequestMutList script {

    get {
      if (root == null) {
        throw new Exception("Tried to get member script of null!");
      }
      return new IRequestMutList(root, incarnation.script);
    }
                       }
}
}
