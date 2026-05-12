using Terraria;
using 物品包.Items;
using 物品包.玩家;

namespace 物品包.界面;



public class 类型_包槽位_饰品( 类型_饰品包 所属包, int 槽位索引 ) : 类型_包槽位_物品( 所属包, 槽位索引 ) {

    internal override void LeftClick_交换物品() {
        base.LeftClick_交换物品();
        所属包.脏标记 = true;
        Main.LocalPlayer.GetModPlayer<类型_玩家_饰品包>().脏标记_额外缓存 = true;
    }

}