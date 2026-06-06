using CustomCharacterKirby.CustomCharacterKirbyCode.Monsters;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Rooms;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Encounters;

public sealed class MetaKnightEventEncounter : EncounterModel
{
    public override RoomType RoomType => RoomType.Monster;

    public override IEnumerable<MonsterModel> AllPossibleMonsters => [ModelDb.Monster<MetaKnight>()];

    private bool _lostDuel;
    
    public bool LostDuel
    {
        get => _lostDuel;
        set
        {
            AssertMutable();
            _lostDuel = value;
        }
    }

    protected override IReadOnlyList<(MonsterModel, string?)> GenerateMonsters()
    {
        return [(ModelDb.Monster<MetaKnight>().ToMutable(), null)];
    }
}