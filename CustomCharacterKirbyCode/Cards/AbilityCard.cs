using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.ValueProps;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

public abstract class AbilityCard(int cost, CardType type, CardRarity rarity, TargetType target) : CustomCharacterKirbyCard(cost, type, rarity, target)
{
    protected virtual IEnumerable<DynamicVar> DefaultCanonicalVars =>
    [
        new DamageVar(0M, ValueProp.Move),
        new ExtraDamageVar(0M),
        new BlockVar(0M, ValueProp.Move),
        new RepeatVar(0),
        new EnergyVar(0),
        new CardsVar(0),
        new HpLossVar(0M),
        new HealVar(0M),
        new CalculationBaseVar(0M),
        new CalculationExtraVar(1M),
        new CalculatedDamageVar(ValueProp.Move).WithMultiplier((_, _) => 1),
        new CalculatedBlockVar(ValueProp.Move).WithMultiplier((_, _) => 1)
    ];

    protected virtual IEnumerable<DynamicVar> OverrideCanonicalVars => [];

    protected virtual IEnumerable<DynamicVar> ExtraCanonicalVars => [];

    // Seal CanonicalVars so AbilityCards have to use this enchantment-proof system
    protected sealed override IEnumerable<DynamicVar> CanonicalVars
    {
        get
        {
            // Merge defaults + overrides + extras
            var dict = DefaultCanonicalVars.ToDictionary(v => v.Name);

            // Replace defaults with overrides
            foreach (var v in OverrideCanonicalVars)
                dict[v.Name] = v;

            // Add extras
            foreach (var v in ExtraCanonicalVars)
                dict[v.Name] = v;

            return dict.Values;
        }
    }
    
    
    protected enum AbilityType
    {
        BasicAttack,
        BasicSkill,
        Forward,
        Up,
        Down
    }

    protected abstract AbilityType abilityType { get; }

    public async Task OnAbilityChanged(CombatState combatState, Player owner, CopyAbility ability)
    {
        AbilityCard newCard = abilityType switch
        {
            AbilityType.BasicAttack => ability.BasicAttackCard,
            AbilityType.BasicSkill => ability.BasicSkillCard,
            AbilityType.Forward => ability.ForwardCard,
            AbilityType.Up => ability.UpCard,
            AbilityType.Down => ability.DownCard,
            _ => throw new ArgumentOutOfRangeException(nameof(abilityType), abilityType, null)
        };
        newCard = (AbilityCard)combatState.CreateCard(newCard, owner);
        
        // If this card is upgraded, the replacement should be as well
        if (IsUpgraded)
            CardCmd.Upgrade(newCard);
        
        // Cary over affliction
        var affliction = Affliction;
        if (affliction != null)
        {
            var newAffliction = (AfflictionModel)affliction.MutableClone();
            await CardCmd.Afflict(newAffliction, newCard, affliction.Amount);
        }
        
        // Cary over enchantment
        var enchantment = Enchantment;
        if (enchantment != null)
        {
            var newEnchantment = (EnchantmentModel)enchantment.MutableClone();
            CardCmd.Enchant(newEnchantment, newCard, enchantment.Amount);
        }
        
        // Transform the card
        await CardCmd.Transform(this, newCard, CardPreviewStyle.GridLayout);
    }
}