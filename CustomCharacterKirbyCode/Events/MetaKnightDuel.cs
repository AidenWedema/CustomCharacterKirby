using BaseLib.Abstracts;
using CustomCharacterKirby.CustomCharacterKirbyCode.Encounters;
using CustomCharacterKirby.CustomCharacterKirbyCode.Relics;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Acts;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Encounters;
using MegaCrit.Sts2.Core.Models.Events;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Events;

public class MetaKnightDuel() : CustomEventModel()
{
    public override EncounterModel CanonicalEncounter => ModelDb.Encounter<MetaKnightEventEncounter>();

    public override bool IsShared => true;

    public override ActModel[] Acts => [ModelDb.Act<Hive>()];

    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override IReadOnlyList<EventOption> GenerateInitialOptions() =>
    [
        new(this, Accept, $"{Id.Entry}.pages.INITIAL.options.ACCEPT"),
        new(this, Decline, $"{Id.Entry}.pages.INITIAL.options.DECLINE")
    ];

    private Task Accept()
    {
        MetaKnightDuel metaKnightDuel = this;
        metaKnightDuel.SetEventState(metaKnightDuel.L10NLookup($"{Id.Entry}.pages.ACCEPT.description"), [new EventOption(metaKnightDuel, metaKnightDuel.Fight, $"{Id.Entry}.pages.ACCEPT.options.FIGHT")]);
        return Task.CompletedTask;
    }

    private async Task Decline()
    {
        MetaKnightDuel metaKnightDuel = this;
        metaKnightDuel.SetEventFinished(metaKnightDuel.L10NLookup($"{Id.Entry}.pages.DONE.options.DECLINE.description"));
    }

    private Task Fight()
    {
        MetaKnightDuel metaKnightDuel = this;
        metaKnightDuel.EnterCombatWithoutExitingEvent<MetaKnightEventEncounter>([], true);
        return Task.CompletedTask;
    }
    
    public override async Task Resume(AbstractRoom room)
    {
        MetaKnightDuel metaKnightDuel = this;
        MetaKnightEventEncounter encounter = (MetaKnightEventEncounter) ((CombatRoom) room).Encounter;
        if (encounter.LostDuel)
            metaKnightDuel.SetEventFinished(metaKnightDuel.L10NLookup($"{Id.Entry}.pages.DEFEAT.description"));
        else
        {
            metaKnightDuel.SetEventFinished(metaKnightDuel.L10NLookup($"{Id.Entry}.pages.VICTORY.description"));
            await RelicCmd.Obtain(ModelDb.Relic<Galaxia>().ToMutable(), metaKnightDuel.Owner);
        }
    }


    public override string CustomInitialPortraitPath => ImageHelper.GetImagePath($"events/{ModelDb.Event<BattlewornDummy>().Id.Entry.ToLowerInvariant()}.png");
    public override string CustomBackgroundScenePath => SceneHelper.GetScenePath("events/background_scenes/" + ModelDb.Event<BattlewornDummy>().Id.Entry.ToLowerInvariant());
}
