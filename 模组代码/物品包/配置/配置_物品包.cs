using System.ComponentModel;
using System.IO;
using Terraria.ModLoader.Config;

namespace 物品包.配置; 



public class 类型_配置_物品包 : ModConfig {

    public override ConfigScope Mode => ConfigScope.ClientSide;

    [Header( "容量设置" )]

    [Range( 1, 1000 )]
    [DefaultValue( 200 )]
    public virtual int 容量 { get; set; }

    [Range( 1, 50 )]
    [DefaultValue( 20 )]
    public virtual int 列数 { get; set; }


    [Range( 1, 50 )]
    [DefaultValue( 10 )]
    public virtual int 行数 { get; set; }

    public virtual void 网络发送( int 接收玩家ID, int 发送玩家ID ) { }

    public virtual void 网络接收( BinaryReader 网络流, int 配置玩家ID ) { }

}