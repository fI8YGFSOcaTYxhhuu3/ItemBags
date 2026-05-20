using MultipleArmorSetsFramework;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using 物品包.Items;

namespace 物品包.玩家;



// 特征成员
public partial class 类型_玩家_护甲包 : 类型_玩家_同步缓存包<接口_护甲包, 结构_同步数据_护甲包> {
    private readonly List<string> 注册备份列表 = [];

    public void 注册护甲() {
        var 护甲框架 = Player.GetModPlayer<护甲玩家>().护甲管理器;

        foreach ( var 注册ID in CollectionsMarshal.AsSpan( 注册备份列表 ) ) 护甲框架.注销( 注册ID );
        注册备份列表.Clear();

        foreach ( var 数据 in CollectionsMarshal.AsSpan( 缓存列表_同步缓存 ) ) {
            string 标识ID = 数据.ID.ToString();
            护甲框架.注册( 标识ID, 数据.数据列表, new( 数据.功能配置.单件加成模式, 数据.功能配置.套装加成模式 ) );
            注册备份列表.Add( 标识ID );
        }
    }
}

// 简单重写成员
public partial class 类型_玩家_护甲包 {
    public override 枚举_物品包类型 缓存包标识 => 枚举_物品包类型.护甲包;
}

// 特征重写函数
public partial class 类型_玩家_护甲包 {
    protected override void 脏标记更新_同步缓存() { base.脏标记更新_同步缓存(); 注册护甲(); }
}

// 网络同步
public partial class 类型_玩家_护甲包 {
    public override void 网络发送( int 接收玩家ID, int 发送玩家ID ) {
        ModPacket 网络数据 = Mod.GetPacket();
        网络数据.Write( ( byte ) 类型_物品包模组.枚举_同步对象类型.护甲包 );
        网络数据.Write( ( byte ) Player.whoAmI );

        网络数据.Write( 缓存列表_同步缓存.Count );
        foreach ( var 数据 in CollectionsMarshal.AsSpan( 缓存列表_同步缓存 ) ) {
            网络数据.Write( 数据.ID.ToByteArray() );
            网络数据.Write( ( byte ) 数据.功能配置.单件加成模式 );
            网络数据.Write( ( byte ) 数据.功能配置.套装加成模式 );

            网络数据.Write( 数据.数据列表.Count );
            foreach ( var 组合 in CollectionsMarshal.AsSpan( 数据.数据列表 ) ) {
                ItemIO.Send( 组合.头盔, 网络数据, writeStack: false, writeFavorite: false );
                ItemIO.Send( 组合.胸甲, 网络数据, writeStack: false, writeFavorite: false );
                ItemIO.Send( 组合.护腿, 网络数据, writeStack: false, writeFavorite: false );
            }
        }
        网络数据.Send( 接收玩家ID, 发送玩家ID );
    }
    public override void 网络接收( BinaryReader 网络流 ) {
        缓存列表_同步缓存.Clear();
        int 包数量 = 网络流.ReadInt32();
        for ( int i = 0; i < 包数量; i++ ) {
            结构_同步数据_护甲包 数据 = new() {
                ID = new System.Guid( 网络流.ReadBytes( 16 ) ),
                功能配置 = new() { 单件加成模式 = ( 枚举_加成模式 ) 网络流.ReadByte(), 套装加成模式 = ( 枚举_加成模式 ) 网络流.ReadByte() },
                数据列表 = []
            };

            int 组合数量 = 网络流.ReadInt32();
            for ( int j = 0; j < 组合数量; j++ ) 数据.数据列表.Add( new( ItemIO.Receive( 网络流, readStack: false, readFavorite: false ), ItemIO.Receive( 网络流, readStack: false, readFavorite: false ), ItemIO.Receive( 网络流, readStack: false, readFavorite: false ) ) );
            缓存列表_同步缓存.Add( 数据 );
        }
        注册护甲();
    }
}