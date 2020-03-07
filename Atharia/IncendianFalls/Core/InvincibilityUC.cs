using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class InvincibilityUC {
  public readonly Root root;
  public readonly int id;
  public InvincibilityUC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public InvincibilityUCIncarnation incarnation { get { return root.GetInvincibilityUCIncarnation(id); } }
  public void AddObserver(IInvincibilityUCEffectObserver observer) {
    root.AddInvincibilityUCObserver(id, observer);
  }
  public void RemoveObserver(IInvincibilityUCEffectObserver observer) {
    root.RemoveInvincibilityUCObserver(id, observer);
  }
  public void Delete() {
    root.EffectInvincibilityUCDelete(id);
  }
  public static InvincibilityUC Null = new InvincibilityUC(null, 0);
  public bool Exists() { return root != null && root.InvincibilityUCExists(id); }
  public bool NullableIs(InvincibilityUC that) {
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
  public bool Is(InvincibilityUC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
       }
}
