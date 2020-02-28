using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IImmediatelyUseItem {
  IImmediatelyUseItem AsIImmediatelyUseItem();
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  void FindReachableObjects(SortedSet<int> foundIds);
  bool Is(IImmediatelyUseItem that);
  bool NullableIs(IImmediatelyUseItem that);
  IDestructible AsIDestructible();
  IUnitComponent AsIUnitComponent();
  IItem AsIItem();
  IUsableItem AsIUsableItem();
  Void Use(Game game, Superstate superstate, Unit unit);
  IItem ClonifyAndReturnNewReal(Root newRoot);
  Void Destruct();
}
}
