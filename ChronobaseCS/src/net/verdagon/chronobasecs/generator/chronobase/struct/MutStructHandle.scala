package net.verdagon.chronobasecs.generator.chronobase.struct

import net.verdagon.chronobasecs.compiled._
import net.verdagon.chronobasecs.generator.chronobase.Generator.toCS
import net.verdagon.chronobasecs.generator.chronobase.ChronobaseOptions

object MutStructHandle {

  def generateInstance(
                        opt: ChronobaseOptions,
                        struct: StructS,
                        bunches: List[BunchS]
  ): Map[String, String] = {
    val StructS(structName, _, MutableS, members, _, _, _) = struct
    val definition =
      s"""
         |public class ${structName} {
         |  public readonly Root root;
         |  public readonly int id;
         |  public ${structName}(Root root, int id) {
         |    this.root = root;
         |    this.id = id;
         |  }
         |  public ${structName}Incarnation incarnation { get { return root.Get${structName}Incarnation(id); } }
         |  public void AddObserver(EffectBroadcaster broadcaster, I${structName}EffectObserver observer) {
         |    broadcaster.Add${structName}Observer(id, observer);
         |  }
         |  public void RemoveObserver(EffectBroadcaster broadcaster, I${structName}EffectObserver observer) {
         |    broadcaster.Remove${structName}Observer(id, observer);
         |  }
         |  public void Delete() {
         |    root.Effect${structName}Delete(id);
         |  }
         |  public static ${structName} Null = new ${structName}(null, 0);
         |  public bool Exists() { return root != null && root.${structName}Exists(id); }
         |  public bool NullableIs(${structName} that) {
         |    if (!this.Exists() && !that.Exists()) {
         |      return true;
         |    }
         |    if (!this.Exists() || !that.Exists()) {
         |      return false;
         |    }
         |    return this.Is(that);
         |  }
         |  public void CheckForNullViolations(List<string> violations) {
         |""".stripMargin +
      members
        .filter(!_.tyype.nullable)
        .filter(_.tyype.kind.mutability == MutableS)
        .filter(member => List(OwnS, StrongS).contains(member.tyype.ownership))
        .map({ case StructMemberS(memberName, variability, memberType) =>
          val memberCSType = toCS(memberType)
          s"""
             |    if (!root.${memberCSType}Exists(${memberName}.id)) {
             |      violations.Add("Null constraint violated! ${structName}#" + id + ".${memberName}");
             |    }
             |""".stripMargin
        })
        .mkString("") +
      s"""  }
         |  public void FindReachableObjects(SortedSet<int> foundIds) {
         |    if (foundIds.Contains(id)) {
         |      return;
         |    }
         |    foundIds.Add(id);
         |""".stripMargin +
      members
        .map({ case StructMemberS(memberName, variability, memberType) =>
          (memberType.kind.mutability match {
            case ImmutableS => ""
            case MutableS => {
              val memberCSType = toCS(memberType)
              s"""    if (root.${memberCSType}Exists(${memberName}.id)) {
                 |      ${memberName}.FindReachableObjects(foundIds);
                 |    }
                 |""".stripMargin
            }
          })
        })
        .mkString("") +
      s"""  }
         |  public bool Is(${structName} that) {
         |    if (!this.Exists()) {
         |      throw new Exception("Called Is on a null!");
         |    }
         |    if (!that.Exists()) {
         |      throw new Exception("Called Is on a null!");
         |    }
         |    return this.root == that.root && id == that.id;
         |  }
       """.stripMargin +
        members.map({ case StructMemberS(memberName, variability, memberType) =>
          s"  public ${toCS(memberType)} ${memberName} {\n" +
            (memberType.kind.mutability match {
              case ImmutableS => s"    get { return incarnation.${memberName}; }\n"
              case MutableS => {
                val memberCSType = toCS(memberType)
                memberType match {
                  case TypeS(_, _, InterfaceKindS(_, _)) => {
                    s"    get { return root.Get${memberCSType}(incarnation.${memberName}); }\n"
                  }
                  case _ => {
                    s"""
                       |    get {
                       |      if (root == null) {
                       |        throw new Exception("Tried to get member ${memberName} of null!");
                       |      }
                       |      return new ${memberCSType}(root, incarnation.${memberName});
                       |    }
                     """.stripMargin
                  }
                }
              }
            }) +
            (variability match {
              case FinalS => ""
              case VaryingS => {
                s"    set { root.Effect${structName}Set${memberName.capitalize}(id, value); }\n"
              }
            }) +
            s"  }\n"
        }).mkString("") +
        makeCodeForBunches(opt, struct, bunches) +
        "}"
    Map(structName -> definition) ++
      bunches.flatMap(Bunch.generateClasses)
  }

  def makeCodeForBunches(opt: ChronobaseOptions, struct: StructS, bunches: List[BunchS]): String = {
    bunches match {
      case List() => ""
      // We only support one extension/bunch for now
      case List(bunch) => {
        Bunch.generateInstanceMethods(bunch)
      }
    }
  }
}
