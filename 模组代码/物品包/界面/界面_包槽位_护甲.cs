using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.UI;
using 物品包.Items;

namespace 物品包.界面;



public class 类型_包槽位_护甲( 接口_护甲包 所属包, int 槽位索引 ) : 类型_包槽位_缓存( 所属包, 槽位索引 ) {
    public override void LeftClick( UIMouseEvent 鼠标事件 ) {
        if ( !Main.mouseItem.IsAir ) {
            switch (索引 % 3) {
                case 0 when Main.mouseItem.headSlot < 0: { SoundEngine.PlaySound( SoundID.MenuClose ); return; }
                case 1 when Main.mouseItem.bodySlot < 0: { SoundEngine.PlaySound( SoundID.MenuClose ); return; }
                case 2 when Main.mouseItem.legSlot < 0: { SoundEngine.PlaySound( SoundID.MenuClose ); return; }
            }
        }
        base.LeftClick( 鼠标事件 );
    }
    protected override void 物品存取处理( Item 先前物品, Item 当前物品 ) { base.物品存取处理( 先前物品, 当前物品 ); 所属包.类型玩家.脏标记_同步缓存 = true; }
}