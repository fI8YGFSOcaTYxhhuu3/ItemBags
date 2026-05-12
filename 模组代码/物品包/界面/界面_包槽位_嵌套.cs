using Terraria;
using Terraria.UI;
using 物品包.Items;
using 物品包.玩家;
using 物品包.系统;

namespace 物品包.界面;



public class 类型_包槽位_嵌套( 类型_嵌套包 所属包, int 槽位索引 ) : 类型_包槽位_物品( 所属包, 槽位索引 ) {

    internal override void LeftClick_交换物品() {
        base.LeftClick_交换物品();
        所属包.脏标记 = true;

        var 饰品包玩家 = Main.LocalPlayer.GetModPlayer<类型_玩家_饰品包>(); if ( 饰品包玩家.缓存变更检测( 所属物品.ModItem ) || 饰品包玩家.缓存变更检测( Main.mouseItem.ModItem ) ) 饰品包玩家.脏标记_缓存包 = true;
        var 护甲包玩家 = Main.LocalPlayer.GetModPlayer<类型_玩家_护甲包>(); if ( 护甲包玩家.缓存变更检测( 所属物品.ModItem ) || 护甲包玩家.缓存变更检测( Main.mouseItem.ModItem ) ) 护甲包玩家.脏标记_缓存包 = true;
    }

    public override void RightClick( UIMouseEvent 鼠标事件 ) { if ( !所属物品.IsAir ) 类型_系统_界面管理.界面管理器.切换窗口( ( 类型_物品包 ) 所属物品.ModItem ); }

}