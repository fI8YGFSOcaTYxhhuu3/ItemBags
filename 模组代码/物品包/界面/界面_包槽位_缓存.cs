using 物品包.Items;

namespace 物品包.界面;



public class 类型_包槽位_缓存( 类型_缓存包_非模板基类 所属包, int 槽位索引 ) : 类型_包槽位_物品( 所属包, 槽位索引 ) {
    protected override void LeftClick_交换物品() {
        base.LeftClick_交换物品();
        所属包.脏标记 = true;
    }
}