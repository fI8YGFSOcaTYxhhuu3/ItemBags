using Terraria;
using Terraria.ModLoader;

namespace 物品包.玩家;



public class 类型_玩家_物品包 : ModPlayer {

    public Item[][] 玩家容器列表 => [ Player.inventory, Player.bank.item, Player.bank2.item, Player.bank3.item, Player.bank4.item ];

    public static Item[] 当前容器数组() {
        Player 玩家 = Main.LocalPlayer;
        return 玩家.chest switch {
            -1 => null,
            -2 => 玩家.bank.item,
            -3 => 玩家.bank2.item,
            -4 => 玩家.bank3.item,
            -5 => 玩家.bank4.item,
            >= 0 => Main.chest[ 玩家.chest ].item,
            _ => null
        };
    }

    public virtual bool 条件符合( Item 查询物品 ) => true;

}