using Godot;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Random;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.DreamFriends;

public abstract class DreamFriend : MonsterModel
{
  public const float attackerAnimDelay = 0.3f;
  public const string pokeAnim = "attack_poke";
  public const string ostyAttackSfx = "event:/sfx/characters/osty/osty_attack";

  public static Vector2 MinOffset => new Vector2(150f, -75f);

  public static Vector2 MaxOffset => new Vector2(250f, -75f);

  public static Vector2 ScaleRange => new Vector2(1f, 2f);

  public override int MinInitialHp => 1;

  public override int MaxInitialHp => 1;

  public override string DeathSfx => "event:/sfx/characters/osty/osty_die";

  public override bool HasDeathSfx => true;

  public override bool IsHealthBarVisible => this.Creature.IsAlive;
  
  // FIXTHIS: Use Osty visuals as placeholder to prevent NullReferenceException when creating the creature
  protected override string VisualsPath => SceneHelper.GetScenePath("creature_visuals/" + "Osty".ToLowerInvariant());

  protected override MonsterMoveStateMachine GenerateMoveStateMachine()
  {
    MoveState initialState = new MoveState("NOTHING_MOVE", (Func<IReadOnlyList<Creature>, Task>) (_ => Task.CompletedTask), Array.Empty<AbstractIntent>());
    initialState.FollowUpState = (MonsterState) initialState;
    // ISSUE: object of a compiler-generated type is created
    return new MonsterMoveStateMachine([initialState], initialState);
  }

  public override CreatureAnimator GenerateAnimator(MegaSprite controller)
  {
    AnimState initialState = new AnimState("idle_loop", true);
    AnimState state1 = new AnimState("cast");
    AnimState state2 = new AnimState("attack");
    AnimState state3 = new AnimState("attack_poke");
    AnimState state4 = new AnimState("hurt");
    AnimState state5 = new AnimState("die");
    AnimState animState = new AnimState("dead_loop", true);
    AnimState state6 = new AnimState("revive");
    initialState.AddBranch("Hit", state4);
    state1.NextState = initialState;
    state1.AddBranch("Hit", state4);
    state2.NextState = initialState;
    state2.AddBranch("Hit", state4);
    state3.NextState = initialState;
    state3.AddBranch("Hit", state4);
    state4.NextState = initialState;
    state4.AddBranch("Hit", state4);
    state5.NextState = animState;
    state6.NextState = initialState;
    CreatureAnimator animator = new CreatureAnimator(initialState, controller);
    animator.AddAnyState("Attack", state2);
    animator.AddAnyState("Cast", state1);
    animator.AddAnyState("Dead", state5);
    animator.AddAnyState("attack_poke", state3);
    animator.AddAnyState("Revive", state6);
    return animator;
  }

  public static bool CheckMissingWithAnim(Player owner)
  {
    NCombatRoom.Instance?.ShakeOstyIfDead(owner);
    return owner.IsOstyMissing;
  }
  
  
    
  protected Creature? RandomEnemy 
  {
    get
    {
      var combatState = this.CombatState;
      if (combatState.HittableEnemies.Count == 0) return null;
      var r = Rng.NextInt(0, combatState.HittableEnemies.Count);
      return combatState.HittableEnemies[r];
    }
  }
}
