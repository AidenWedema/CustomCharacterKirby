using BaseLib.Abstracts;
using BaseLib.Utils.NodeFactories;
using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using CustomCharacterKirby.CustomCharacterKirbyCode.Extensions;
using CustomCharacterKirby.CustomCharacterKirbyCode.Relics;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Character;

public class CustomCharacterKirby : PlaceholderCharacterModel
{
    public const string CharacterId = "CustomCharacterKirby";

    public static readonly Color Color = new("fea0bb");

    public override Color NameColor => Color;
    public override Color DialogueColor => Color;
    public override Color MapDrawingColor => Color;
    public override Color EnergyLabelOutlineColor => new("eb7295");
    
    public override CharacterGender Gender => CharacterGender.Neutral;
    public override int StartingHp => 70;

    public override IEnumerable<CardModel> StartingDeck =>
    [
        ModelDb.Card<StrikeKirby>(),
        ModelDb.Card<StrikeKirby>(),
        ModelDb.Card<StrikeKirby>(),
        ModelDb.Card<Guard>(),
        ModelDb.Card<Guard>(),
        ModelDb.Card<Guard>(),
        ModelDb.Card<Hover>(),
        ModelDb.Card<Inhale>(),
        ModelDb.Card<StarSpit>(),
        ModelDb.Card<Crouch>()
    ];

    public override IReadOnlyList<RelicModel> StartingRelics =>
    [
        ModelDb.Relic<SquishySkin>()
    ];

    public override CardPoolModel CardPool => ModelDb.CardPool<CustomCharacterKirbyCardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<CustomCharacterKirbyRelicPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<CustomCharacterKirbyPotionPool>();

    /*  PlaceholderCharacterModel will utilize placeholder basegame assets for most of your character assets until you
        override all the other methods that define those assets.
        These are just some of the simplest assets, given some placeholders to differentiate your character with.
        You don't have to, but you're suggested to rename these images. */
    public override Control CustomIcon
    {
        get
        {
            var icon = NodeFactory<Control>.CreateFromResource(CustomIconTexturePath);
            icon.SetAnchorsAndOffsetsPreset(Control.LayoutPreset.FullRect);
            return icon;
        }
    }

    public override string CustomIconTexturePath => "character_icon_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectIconPath => "char_select_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectLockedIconPath => "char_select_char_name_locked.png".CharacterUiPath();
    public override string CustomMapMarkerPath => "map_marker_char_name.png".CharacterUiPath();
    public override string CustomEnergyCounterPath  => "CustomCharacterKirby/animations/energy counter/energy_counter.tscn";
    
    public override string CustomArmPointingTexturePath => ImageHelper.GetImagePath($"ui/hands/multiplayer_hand_{PlaceholderID}_arm_point.png");
    public override string CustomArmRockTexturePath => ImageHelper.GetImagePath($"ui/hands/multiplayer_hand_{PlaceholderID}_arm_rock.png");
    public override string CustomArmPaperTexturePath => ImageHelper.GetImagePath($"ui/hands/multiplayer_hand_{PlaceholderID}_arm_paper.png");
    public override string CustomArmScissorsTexturePath => ImageHelper.GetImagePath($"ui/hands/multiplayer_hand_{PlaceholderID}_arm_scissors.png");

    public override Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, Creature? dealer, CardModel? cardSource)
    {
        
        return Task.CompletedTask;
    }

    public override NCreatureVisuals? CreateCustomVisuals()
    {
        return NodeFactory<NCreatureVisuals>.CreateFromScene("CustomCharacterKirby/animations/characters/kirby/kirby.tscn");
    }
}