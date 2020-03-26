package net.verdagon.chronobasecs.generator.chronobase.struct

import net.verdagon.chronobasecs.compiled.{BunchS, MutableS}
import net.verdagon.chronobasecs.generator.chronobase.Generator.toCS

object Bunch {
//  def getImplementingInterfaces(bunch: BunchS): List[String] = {
//    (bunch.structSetByStruct.values.map(_.name + "EffectObserver") ++
//    bunch.structSetByStruct.values.map(_.name + "EffectVisitor")).toList
//  }

  def generateClasses(bunch: BunchS): Map[String, String] = {
    val BunchS(
        struct,
        MutableS,
        interface,
        structs,
        structSetByStruct,
        structByInterface)
        = bunch

    val interfaceCSType = toCS(interface)
    val bunchCSType = toCS(struct)

    val observerName = s"I${bunchCSType}Observer"
    val broadcasterName = s"${bunchCSType}Broadcaster"

    val observerDefinition =
      s"""public interface ${observerName} {
         |  void On${bunchCSType}Add(int id);
         |  void On${bunchCSType}Remove(int id);
         |}
         |""".stripMargin

    val broadcasterDefinition =
      s"public class ${broadcasterName}" +
      (if (structs.nonEmpty) ":" else "") +
      structs
        .map(structSetByStruct)
        .flatMap(structSet => {
          List(s"I${toCS(structSet)}EffectObserver", s"I${toCS(structSet)}EffectVisitor")
        })
        .mkString(", ") +
      s""" {
         |  EffectBroadcaster broadcaster;
         |  ${bunchCSType} bunch;
         |  private List<${observerName}> observers;
         |
         |  public ${broadcasterName}(EffectBroadcaster broadcaster, ${bunchCSType} bunch) {
         |    this.broadcaster = broadcaster;
         |    this.bunch = bunch;
         |    this.observers = new List<${observerName}>();
         |""".stripMargin +
      structs
        .map(structSetByStruct)
        .map(structSet => {
          val structSetCSType = toCS(structSet)
          s"    bunch.members${structSetCSType}.AddObserver(broadcaster, this);\n"
        })
        .mkString("") +
      s"""
         |  }
         |  public void Stop() {
         |""".stripMargin +
      structs
        .map(structSetByStruct)
        .map(structSet => {
          val structSetCSType = toCS(structSet)
          s"    bunch.members${structSetCSType}.RemoveObserver(broadcaster, this);\n"
        })
        .mkString("") +
      s"""
         |  }
         |  public void AddObserver(${observerName} observer) {
         |    this.observers.Add(observer);
         |  }
         |  public void RemoveObserver(${observerName} observer) {
         |    this.observers.Remove(observer);
         |  }
         |  private void BroadcastAdd(int id) {
         |    foreach (var observer in observers) {
         |      observer.On${bunchCSType}Add(id);
         |    }
         |  }
         |  private void BroadcastRemove(int id) {
         |    foreach (var observer in observers) {
         |      observer.On${bunchCSType}Remove(id);
         |    }
         |  }
         |""".stripMargin +
      structs
        .map(structSetByStruct)
        .map(structSet => {
          val structSetCSType = toCS(structSet)
          s"""  public void On${structSetCSType}Effect(I${structSetCSType}Effect effect) {
             |    effect.visitI${structSetCSType}Effect(this);
             |  }
             |  public void visit${structSetCSType}AddEffect(${structSetCSType}AddEffect effect) {
             |    BroadcastAdd(effect.element);
             |  }
             |  public void visit${structSetCSType}RemoveEffect(${structSetCSType}RemoveEffect effect) {
             |    BroadcastRemove(effect.element);
             |  }
             |  public void visit${structSetCSType}CreateEffect(${structSetCSType}CreateEffect effect) { }
             |  public void visit${structSetCSType}DeleteEffect(${structSetCSType}DeleteEffect effect) { }
             |""".stripMargin
        })
        .mkString("") +
      s"""
         |}
       """.stripMargin

    Map(
      observerName -> observerDefinition,
      broadcasterName -> broadcasterDefinition)
  }

  def generateInstanceMethods(bunch: BunchS): String = {
    val BunchS(struct, MutableS, interface, structs, structSetByStruct, structByInterface) = bunch
    val interfaceCSType = toCS(interface)
    val bunchCSType = toCS(struct)

    s"""
       |  public static ${bunchCSType} New(Root root) {
       |    return root.Effect${bunchCSType}Create(
       |""".stripMargin +
      structs
        .map(struct => {
          val structCSType = toCS(struct)
          val structSet = structSetByStruct(struct);
          val structSetCSType = toCS(structSet);
          s"""      root.Effect${structSetCSType}Create()
             |""".stripMargin
        })
        .mkString(",\n") +
      s"""        );
         |  }
         |  public void Add(${interfaceCSType} elementI) {
         |""".stripMargin +
      structs
        .map(struct => {
          val structCSType = toCS(struct)
          val structSet = structSetByStruct(struct);
          val structSetCSType = toCS(structSet);
          s"""
             |    // Can optimize, check the type of element directly somehow
             |    if (root.${structCSType}Exists(elementI.id)) {
             |      this.members${structSetCSType}.Add(root.Get${structCSType}(elementI.id));
             |      return;
             |    }
             |""".stripMargin
        })
        .mkString("") +
      s"""    throw new Exception("Unknown type " + elementI);
         |  }
         |  public void Remove(${interfaceCSType} elementI) {
         |""".stripMargin +
      structs
        .map(struct => {
          val structCSType = toCS(struct)
          val structSet = structSetByStruct(struct);
          val structSetCSType = toCS(structSet);
          s"""
             |    // Can optimize, check the type of element directly somehow
             |    if (root.${structCSType}Exists(elementI.id)) {
             |      this.members${structSetCSType}.Remove(root.Get${structCSType}(elementI.id));
             |      return;
             |    }
             |""".stripMargin
        })
        .mkString("") +
      s"""    throw new Exception("Unknown type " + elementI);
         |  }
         |  public void Clear() {
         |""".stripMargin +
      structs
        .map(struct => {
          val structSet = structSetByStruct(struct);
          val structSetCSType = toCS(structSet);
          s"""    this.members${structSetCSType}.Clear();
             |""".stripMargin
        })
        .mkString("") +
      s"""  }
         |  public int Count {
         |    get {
         |      return
         |""".stripMargin +
      (if (structs.isEmpty) "0" else {
        structs
          .map(struct => {
            val structCSType = toCS(struct)
            val structSet = structSetByStruct(struct);
            val structSetCSType = toCS(structSet);
            s"        this.members${structSetCSType}.Count"
          })
          .mkString(" +\n")
      }) +
      s"""
         |        ;
         |    }
         |  }
         |  public ${interfaceCSType} GetArbitrary() {
         |    foreach (var element in this) {
         |      return element;
         |    }
         |    throw new Exception("Can't get element from empty bunch!");
         |  }
         |
           |  public void Destruct() {
         |""".stripMargin +
      structs
        .map(struct => {
          val structSet = structSetByStruct(struct);
          val structSetCSType = toCS(structSet);
          s"    var tempMembers${structSetCSType} = this.members${structSetCSType};\n"
        })
        .mkString("") +
      s"""
         |    this.Delete();
         |""".stripMargin +
      structs
        .map(struct => {
          val structSet = structSetByStruct(struct);
          val structSetCSType = toCS(structSet);
          s"    tempMembers${structSetCSType}.Destruct();\n"
        })
        .mkString("") +
      s"""  }
         |  public IEnumerator<${interfaceCSType}> GetEnumerator() {
         |""".stripMargin +
      (if (structs.nonEmpty) {
        structs
          .map(struct => {
            val structCSType = toCS(struct)
            val structSet = structSetByStruct(struct);
            val structSetCSType = toCS(structSet);
            s"""    foreach (var element in this.members${structSetCSType}) {
               |      yield return new ${structCSType}As${interfaceCSType}(element);
               |    }
               |""".stripMargin
          })
          .mkString("")
      } else {
        s"""    // Do nothing.
           |    foreach (var element in new ${interfaceCSType}[0]) {
           |      yield return element;
           |    }
           |""".stripMargin
      }) +
      s"""  }
         |""".stripMargin +
      structs
        .map(struct => {
          val structCSType = toCS(struct)
          val structSet = structSetByStruct(struct);
          val structSetCSType = toCS(structSet);
          s"""    public List<${structCSType}> GetAll${structCSType}() {
             |      var result = new List<${structCSType}>();
             |      foreach (var thing in this.members${structSetCSType}) {
             |        result.Add(thing);
             |      }
             |      return result;
             |    }
             |    public List<${structCSType}> ClearAll${structCSType}() {
             |      var result = new List<${structCSType}>();
             |      this.members${structSetCSType}.Clear();
             |      return result;
             |    }
             |    public ${structCSType} GetOnly${structCSType}OrNull() {
             |      var result = GetAll${structCSType}();
             |      Asserts.Assert(result.Count <= 1);
             |      if (result.Count > 0) {
             |        return result[0];
             |      } else {
             |        return ${structCSType}.Null;
             |      }
             |    }
             |""".stripMargin
        })
        .mkString("") +
      structByInterface
        .map({ case (subInterface, structsForThisSubInterface) =>
          val subInterfaceCSType = toCS(subInterface)
          s"""    public List<${subInterfaceCSType}> GetAll${subInterfaceCSType}() {
             |      var result = new List<${subInterfaceCSType}>();
             |""".stripMargin +
            structsForThisSubInterface
              .map(subStruct => {
                val subStructCSType = toCS(subStruct)
                val subStructSet = structSetByStruct(subStruct);
                val subStructSetCSType = toCS(subStructSet);
                s"""      foreach (var obj in this.members${subStructSetCSType}) {
                   |        result.Add(
                   |            new ${subStructCSType}As${subInterfaceCSType}(obj));
                   |      }
                   |""".stripMargin
              })
              .mkString("") +
            s"""      return result;
               |    }
               |    public List<${subInterfaceCSType}> ClearAll${subInterfaceCSType}() {
               |      var result = new List<${subInterfaceCSType}>();
               |""".stripMargin +
            structsForThisSubInterface
              .map(subStruct => {
                val subStructSetCSType = toCS(structSetByStruct(subStruct));
                s"""      this.members${subStructSetCSType}.Clear();
                   |""".stripMargin
              })
              .mkString("") +
            s"""      return result;
               |    }
               |    public ${subInterfaceCSType} GetOnly${subInterfaceCSType}OrNull() {
               |      var result = GetAll${subInterfaceCSType}();
               |      Asserts.Assert(result.Count <= 1);
               |      if (result.Count > 0) {
               |        return result[0];
               |      } else {
               |        return Null${subInterfaceCSType}.Null;
               |      }
               |    }
             """.stripMargin
        })
        .mkString("")
  }
}
