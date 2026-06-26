using Terraria.ModLoader;

namespace 物品包.玩家;



public interface 接口_玩家_物品包 : 接口_玩家;

public class 类型_玩家_物品包 : ModPlayer, 接口_玩家_物品包 {
    public 接口_玩家_物品包 接口 => this;
}