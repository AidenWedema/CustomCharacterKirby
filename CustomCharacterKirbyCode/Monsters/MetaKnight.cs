using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.ValueProps;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Monsters;

public class MetaKnight : CustomMonsterModel
{
    public override int MinInitialHp => 93;     // HP is reference to the release year of meta knights fist appearance: Kirby's adventure released in 1993
    public override int MaxInitialHp => 93;
    
    private int BackhandSlashDamage => AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 12, 11);
    private int QuadrupleSlashDamage => AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 5, 4);
    private int HyperRushDamage => AscensionHelper.GetValueIfAscension(AscensionLevel.DeadlyEnemies, 4, 3);
    
    protected override string VisualsPath => SceneHelper.GetScenePath("creature_visuals/" + "Osty".ToLowerInvariant());

    public override async Task AfterAddedToRoom()
    {
        await PowerCmd.Apply<MercifulPower>(Creature, 1M, null, null);
    }
    
    protected override MonsterMoveStateMachine GenerateMoveStateMachine()
    {
        List<MonsterState> states = new List<MonsterState>();

        MoveState initialState = new("TAUNT", Taunt, []);
        MoveState state1 = new("BACKHAND_SLASH", BackhandSlash, [new SingleAttackIntent(BackhandSlashDamage), new DefendIntent()]);
        MoveState state2 = new("META_QUADRUPLE_SLASH", MetaQuadrupleSlash, [new MultiAttackIntent(QuadrupleSlashDamage, 4), new DefendIntent()]);
        MoveState state3 = new("HYPER_RUSH", HyperRush, [new MultiAttackIntent(HyperRushDamage, 8)]);
        
        initialState.FollowUpState = state1;
        state1.FollowUpState = state2;
        state2.FollowUpState = state3;
        state3.FollowUpState = state1;
        
        states.Add(initialState);
        states.Add(state1);
        states.Add(state2);
        states.Add(state3);
        
        return new MonsterMoveStateMachine(states, initialState);
    }

    private Task Taunt(IReadOnlyList<Creature> targets)
    {
        var line = L10NMonsterLookup($"{Id.Entry}.moves.TAUNT.speakLine1");
        TalkCmd.Play(line, Creature, VfxColor.White, VfxDuration.Standard);
        return Task.CompletedTask;
    }

    private async Task BackhandSlash(IReadOnlyList<Creature> targets)
    {
        MetaKnight monster = this;
        await DamageCmd.Attack(monster.BackhandSlashDamage).FromMonster(monster).WithAttackerFx(sfx: "event:/sfx/enemy/enemy_attacks/mechaknight/mechaknight_heavy_attack").Execute(null);
        await CreatureCmd.GainBlock(monster.Creature, 9, ValueProp.Move, null);
    }

    private async Task MetaQuadrupleSlash(IReadOnlyList<Creature> targets)
    {
        MetaKnight monster = this;
        await DamageCmd.Attack(monster.QuadrupleSlashDamage).WithHitCount(4).FromMonster(monster).WithAttackerFx(sfx: "event:/sfx/enemy/enemy_attacks/mechaknight/mechaknight_heavy_attack").Execute(null);
        await CreatureCmd.GainBlock(monster.Creature, 5, ValueProp.Move, null);
    }
    
    private async Task HyperRush(IReadOnlyList<Creature> targets)
    {
        MetaKnight monster = this;
        await DamageCmd.Attack(monster.HyperRushDamage).WithHitCount(8).FromMonster(monster).WithAttackerFx(sfx: "event:/sfx/enemy/enemy_attacks/mechaknight/mechaknight_heavy_attack").Execute(null);
    }
    
    public override CreatureAnimator GenerateAnimator(MegaSprite controller)
    {
        AnimState animState1 = new AnimState("idle_loop", true);
        AnimState state1 = new AnimState("hurt");
        AnimState state2 = new AnimState("die");
        AnimState animState2 = new AnimState("die_loop", true);
        state1.NextState = animState1;
        state2.NextState = animState2;
        CreatureAnimator animator = new CreatureAnimator(animState1, controller);
        animator.AddAnyState("Idle", animState1);
        animator.AddAnyState("Dead", state2);
        animator.AddAnyState("Hit", state1);
        return animator;
    }
}