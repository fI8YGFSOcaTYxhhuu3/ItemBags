using Terraria;
using 物品包.Items;

namespace 物品包.玩家;



public abstract class 类型_玩家_脏标记包<类型_脏标记包> : 类型_玩家_物品包 where 类型_脏标记包 : 类型_物品包 {
    public bool 脏标记 = true;
    private Item 上一个鼠标物品 = new();

    public override void OnEnterWorld() { 脏标记 = true; }

    public override bool OnPickup( Item 拾取物品 ) {
        if ( 拾取物品.ModItem is 类型_脏标记包 ) 脏标记 = true;
        return base.OnPickup( 拾取物品 );
    }

    public override bool ShiftClickSlot( Item[] 容器, int 容器索引, int 物品索引 ) {
        if ( 容器[ 物品索引 ].ModItem is 类型_脏标记包 ) 脏标记 = true;
        return false;
    }

    public override void PostUpdate() {
        bool 当前是包 = Main.mouseItem.ModItem is 类型_脏标记包;
        bool 上次是包 = 上一个鼠标物品.ModItem is 类型_脏标记包;
        if ( ( 当前是包 || 上次是包 ) && Main.mouseItem != 上一个鼠标物品 ) 脏标记 = true;
        上一个鼠标物品 = Main.mouseItem;
    }
}