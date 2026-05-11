using MultipleArmorSetsFramework;
using System.ComponentModel;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.IO;
using 物品包.玩家;

namespace 物品包.配置;



public class 类型_配置_护甲包 : 类型_配置_物品包 {

    [DefaultValue( 150 )]
    public override int 容量 { get; set; }

    [DefaultValue( 15 )]
    public override int 列数 { get; set; }

    [DefaultValue( 10 )]
    public override int 行数 { get; set; }

    [Header( "功能设置" )]

    [DefaultValue( true )]
    public bool 启用单件加成 { get; set; }

    [DefaultValue( true )]
    public bool 启用套装加成 { get; set; }

    public override void OnChanged() {
        if ( Main.LocalPlayer == null || !Main.LocalPlayer.active ) return;

        var 玩家 = Main.LocalPlayer.GetModPlayer<类型_玩家_护甲包>();
        (var 先前配置, 玩家.配置) = (玩家.配置, ( 类型_配置_护甲包 ) Clone());

        if ( 先前配置.启用单件加成 != 启用单件加成 || 先前配置.启用套装加成 != 启用套装加成 ) {
            Main.LocalPlayer.GetModPlayer<类型_玩家_护甲包>().注册护甲();
            if ( Main.netMode == Terraria.ID.NetmodeID.MultiplayerClient ) 网络发送( -1, Main.myPlayer );
        }
    }

    public override void 网络发送( int 接收玩家ID, int 发送玩家ID ) {
        ModPacket 网络数据 = Mod.GetPacket();
        网络数据.Write( ( byte ) 类型_物品包模组.枚举_同步操作类型.配置同步 );
        网络数据.Write( ( byte ) 类型_物品包模组.枚举_同步对象类型.护甲包 );
        网络数据.Write( ( byte ) 发送玩家ID );
        网络数据.Write( 启用单件加成 );
        网络数据.Write( 启用套装加成 );
        网络数据.Send( 接收玩家ID, 发送玩家ID );
    }

    public override void 网络接收( BinaryReader 网络流, int 配置玩家ID ) {
        var 玩家 = Main.player[ 配置玩家ID ].GetModPlayer<类型_玩家_护甲包>();
        玩家.配置 = ( 类型_配置_护甲包 ) Clone();
        玩家.配置.启用单件加成 = 网络流.ReadBoolean();
        玩家.配置.启用套装加成 = 网络流.ReadBoolean();
        玩家.注册护甲();
    }

}