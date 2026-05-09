using MultipleArmorSetsFramework;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Terraria;
using 物品包.Items;

namespace 物品包.玩家;



public class 类型_玩家_护甲包 : 类型_玩家_缓存包<类型_护甲包> {
    public override void UpdateEquips() {
        if ( !脏标记_缓存包 ) return;
        重构缓存_缓存包();
        注册护甲();
        脏标记_缓存包 = false;
    }

    private void 注册护甲() {
        List<类型_护甲组合> 护甲组合列表 = [];
        foreach ( var 护甲包 in CollectionsMarshal.AsSpan( 缓存列表_缓存包 ) ) { 护甲包.更新缓存(); 护甲组合列表.AddRange( 护甲包.缓存列表 ); }
        Player.GetModPlayer<护甲玩家>().护甲管理器.注册( "物品包:护甲包", 护甲组合列表 );
    }
}