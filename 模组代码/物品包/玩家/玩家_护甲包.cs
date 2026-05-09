using MultipleArmorSetsFramework;
using Terraria;
using 物品包.Items;

namespace 物品包.玩家;



public class 类型_玩家_护甲包 : 类型_玩家_缓存包_额外缓存<类型_护甲包, 类型_护甲组合> {

    internal override void 脏标记更新_额外缓存() { base.脏标记更新_额外缓存(); 注册护甲(); }

    private void 注册护甲() { Player.GetModPlayer<护甲玩家>().护甲管理器.注册( "物品包", 缓存列表_额外缓存 ); }

}