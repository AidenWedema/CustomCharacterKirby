using BaseLib.Abstracts;
using BaseLib.Utils;
using CustomCharacterKirby.CustomCharacterKirbyCode.Character;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Potions;

[Pool(typeof(CustomCharacterKirbyPotionPool))]
public abstract class CustomCharacterKirbyPotion : CustomPotionModel;