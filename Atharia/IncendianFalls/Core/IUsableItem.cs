using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IUsableItem {
  IUsableItem AsIUsableItem();
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  void FindReachableObjects(SortedSet<int> foundIds);
  bool Is(IUsableItem that);
  bool NullableIs(IUsableItem that);
  IDestructible AsIDestructible();
  IUnitComponent AsIUnitComponent();
  IItem AsIItem();
  Void Use(Game game, Superstate superstate, Unit unit);
  IItem ClonifyAndReturnNewReal(Root newRoot);
  Void Destruct();
}
}
