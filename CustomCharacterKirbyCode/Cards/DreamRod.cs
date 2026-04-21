using BaseLib.Utils;
using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using CustomCharacterKirby.CustomCharacterKirbyCode.Character;
using CustomCharacterKirby.CustomCharacterKirbyCode.DreamFriends;
using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.ValueProps;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

public class DreamRod() : CustomCharacterKirbyCard(3, CardType.Power, CardRarity.Rare, TargetType.Self)
{
    private static readonly List<DreamFriend> DreamFriends = [
        ModelDb.Monster<BandanaWaddleDee>(),
        ModelDb.Monster<KingDedede>(),
        ModelDb.Monster<MetaKnight>()
    ];

    private static readonly Vector2[] FriendPositions = [
        new Vector2(250f, -75f),
        new Vector2(275f, -50f),
        new Vector2(250f, -25f)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        DreamRod card = this;

        await PowerCmd.Apply<DreamRodPower>(card.Owner.Creature, 1, card.Owner.Creature, card);

        DreamFriend? friend;
        bool newFriend = true;

        var existingFriends = DreamFriendCmd.GetAllPets<DreamFriend>(card.Owner.Creature);
        
        // Check if max is friends reached
        if (existingFriends.Count == FriendPositions.Length)
        {
            var r = card.Owner.RunState.Rng.Niche.NextInt(0, existingFriends.Count);
            friend = existingFriends[r];
            newFriend = false;
        }
        else
        {
            // Summon new friend
            List<DreamFriend> possibleFriends = [];
            foreach (var f in DreamFriends)
            {
                if (existingFriends.Any((d => d.GetType() == f.GetType()))) continue;
                possibleFriends.Add(f);
            }
            
            // If, somehow, no friends are possible, just use existingFriends
            if (possibleFriends.Count == 0)
            {
                possibleFriends = existingFriends.ToList();
                newFriend = false;
            }
        
            var r = card.Owner.RunState.Rng.Niche.NextInt(0, possibleFriends.Count);
            friend = possibleFriends[r];
        }
        var creature = await DreamFriendCmd.Summon(friend, choiceContext, card.Owner, friend.MaxInitialHp, card, true, false);
        
        // Set the new friend to the correct position
        if (newFriend)
            DreamFriendCmd.SetPositionRelativeToOwner(creature, card.Owner, FriendPositions[existingFriends.Count]);
    }

    protected override void OnUpgrade() => EnergyCost.UpgradeBy(-1);
}