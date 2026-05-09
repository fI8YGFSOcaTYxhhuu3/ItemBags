using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.UI;
using 物品包.Items;
using 物品包.玩家;

namespace 物品包.界面;



public class 类型_包槽位_护甲( 类型_护甲包 所属包, int 槽位索引 ) : 类型_包槽位_物品( 所属包, 槽位索引 ) {

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

    internal override void LeftClick_交换物品() {
        base.LeftClick_交换物品();
        所属包.脏标记 = true;
        Main.LocalPlayer.GetModPlayer<类型_玩家_护甲包>().脏标记_额外缓存 = true;
    }

}