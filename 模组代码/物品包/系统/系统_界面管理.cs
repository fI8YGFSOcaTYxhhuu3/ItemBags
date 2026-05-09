using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using 物品包.玩家;
using 物品包.界面;
using 物品包.Items;

namespace 物品包.系统;



public class 类型_系统_界面管理 : ModSystem {

    public static UserInterface 界面接口;
    public static 类型_包界面管理器 界面管理器;

    public override void Load() {
        界面管理器 = new 类型_包界面管理器(); 界面管理器.Activate();
        界面接口 = new UserInterface(); 界面接口.SetState( 界面管理器 );
    }

    public override void Unload() {
        界面管理器 = null;
        界面接口 = null;
    }

    public override void ModifyInterfaceLayers( List<GameInterfaceLayer> 界面层级 ) {
        int 鼠标提示层索引 = 界面层级.FindIndex( 界面 => 界面.Name == "Vanilla: Mouse Text" );
        界面层级.Insert( 鼠标提示层索引, new LegacyGameInterfaceLayer(
            "物品包: 物品包界面",
            delegate {
                界面接口.Update( Main._drawInterfaceGameTime );
                界面接口.Draw( Main.spriteBatch, Main._drawInterfaceGameTime );
                return true;
            },
            InterfaceScaleType.UI
        ) );
    }

    public override void PostUpdateInput() {
        PostUpdateInput_包窗口关闭检测();
        PostUpdateInput_容器内右键检测();
    }

    private void PostUpdateInput_包窗口关闭检测() { if ( !Main.playerInventory ) 界面管理器.关闭全部(); }

    private void PostUpdateInput_容器内右键检测() {
        if ( !Main.playerInventory ) return;
        if ( !Main.mouseRight || !Main.mouseRightRelease ) return;
        if ( Main.HoverItem.ModItem is not 类型_物品包 点击饰品包 ) return;

        Item[] 当前容器 = 类型_玩家_物品包.当前容器数组(); if ( 当前容器 == null ) return;

        foreach ( var 物品 in 当前容器 ) if ( 物品.ModItem is 类型_物品包 容器物品包 && 容器物品包.ID == 点击饰品包.ID ) { 界面管理器.切换窗口( 容器物品包 ); return; }
    }

}