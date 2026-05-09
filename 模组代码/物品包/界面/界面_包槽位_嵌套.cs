using Terraria.UI;
using 物品包.Items;
using 物品包.系统;

namespace 物品包.界面;



public class 类型_包槽位_嵌套( 类型_缓存包_物品 所属包, int 槽位索引 ) : 类型_包槽位_物品缓存( 所属包, 槽位索引 ) {

    public override void RightClick( UIMouseEvent 鼠标事件 ) {
        if ( 所属物品.ModItem is 类型_物品包 目标包 ) { 类型_系统_界面管理.界面管理器.切换窗口( 目标包 ); return; }
        base.RightClick( 鼠标事件 );
    }

}