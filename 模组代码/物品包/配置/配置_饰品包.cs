using System.ComponentModel;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using 物品包.玩家;

namespace 物品包.配置;



public class 类型_配置_饰品包 : 类型_配置_物品包 {

    [Header( "功能设置" )]

    [DefaultValue( false )]
    public bool 允许饰品重复 { get; set; } = false;

    [DefaultValue( true )]
    public bool 应用前缀效果 { get; set; } = true;

    [DefaultValue( true )]
    public bool 应用视觉效果 { get; set; } = true;

    public override void OnChanged() {
        if ( Main.LocalPlayer == null || !Main.LocalPlayer.active ) return;

        var 玩家 = Main.LocalPlayer.GetModPlayer<类型_玩家_饰品包>();
        ( var 先前配置, 玩家.配置) = (玩家.配置, ( 类型_配置_饰品包 ) Clone());

        if ( 先前配置.应用前缀效果 != 应用前缀效果 || 先前配置.应用视觉效果 != 应用视觉效果 ) {
            if ( Main.netMode == Terraria.ID.NetmodeID.MultiplayerClient ) 网络发送( -1, Main.myPlayer );
        }
    }

    public override void 网络发送( int 接收玩家ID, int 发送玩家ID ) {
        ModPacket 网络数据 = Mod.GetPacket();
        网络数据.Write( ( byte ) 类型_物品包模组.枚举_同步操作类型.配置同步 );
        网络数据.Write( ( byte ) 类型_物品包模组.枚举_同步对象类型.饰品包 );
        网络数据.Write( ( byte ) 发送玩家ID );
        网络数据.Write( 应用前缀效果 );
        网络数据.Write( 应用视觉效果 );
        网络数据.Send( 接收玩家ID, 发送玩家ID );
    }

    public override void 网络接收( BinaryReader 网络流, int 配置玩家ID ) {
        var 玩家 = Main.player[ 配置玩家ID ].GetModPlayer<类型_玩家_饰品包>();
        玩家.配置 = ( 类型_配置_饰品包 ) Clone();
        玩家.配置.应用前缀效果 = 网络流.ReadBoolean();
        玩家.配置.应用视觉效果 = 网络流.ReadBoolean();
    }

}