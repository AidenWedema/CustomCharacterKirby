using BaseLib.Abstracts;
using CustomCharacterKirby.CustomCharacterKirbyCode.Extensions;
using Godot;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Character;

public class CustomCharacterKirbyPotionPool : CustomPotionPoolModel
{
    public override Color LabOutlineColor => CustomCharacterKirby.Color;


    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}