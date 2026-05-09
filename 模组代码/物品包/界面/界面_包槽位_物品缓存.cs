using Terraria;
using 物品包.Items;

namespace 物品包.界面;



public class 类型_包槽位_物品缓存( 类型_缓存包_物品 所属包, int 槽位索引 ) : 类型_包槽位_物品( 所属包, 槽位索引 ) {

    internal override void LeftClick_交换物品() {
        base.LeftClick_交换物品();
        if ( 所属包.缓存许可( 所属物品 ) || 所属包.缓存许可( Main.mouseItem ) ) 所属包.脏标记 = true;
    }

}