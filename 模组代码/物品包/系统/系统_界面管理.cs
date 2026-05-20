using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
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
        PostUpdateInput_容器内中键检测();
    }

    private static void PostUpdateInput_包窗口关闭检测() { if ( !Main.playerInventory ) 界面管理器.关闭全部(); }
    private static void PostUpdateInput_容器内右键检测() {
        if ( !Main.playerInventory ) return;
        if ( !Main.mouseRight || !Main.mouseRightRelease ) return;
        if ( Main.HoverItem.ModItem is not 接口_物品包 点击包 ) return;
        foreach ( var 容器 in 打开容器列表() ) foreach ( var 物品 in 容器 ) if ( 物品.ModItem is 接口_物品包 容器包 && 容器包.ID == 点击包.ID ) { 界面管理器.切换窗口( 容器包 ); return; }
    }
    private static void PostUpdateInput_容器内中键检测() {
        if ( !Main.playerInventory ) return;
        if ( !Main.mouseMiddle || !Main.mouseMiddleRelease ) return;
        if ( Main.HoverItem.ModItem is not 接口_缓存包 点击包 ) return;
        foreach ( var 容器 in 打开容器列表() ) foreach ( var 物品 in 容器 ) if ( 物品.ModItem is 接口_缓存包 容器包 && 容器包.ID == 点击包.ID ) { 容器包.切换启用状态(); return; }
    }

    private static IEnumerable<Item[]> 打开容器列表() {
        var 玩家 = Main.LocalPlayer;
        yield return 玩家.inventory;
        switch ( 玩家.chest ) {
            case -2: yield return 玩家.bank.item; break;
            case -3: yield return 玩家.bank2.item; break;
            case -4: yield return 玩家.bank3.item; break;
            case -5: yield return 玩家.bank4.item; break;
            case >= 0: yield return Main.chest[ 玩家.chest ].item; break;
        }
    }

}