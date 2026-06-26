using Terraria;
using Terraria.ModLoader;

namespace 物品包.玩家;



public interface 接口_玩家 {
    ModPlayer 模组玩家 => this as ModPlayer;
    Player 玩家 => 模组玩家.Player;
}