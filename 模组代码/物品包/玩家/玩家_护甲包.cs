using MultipleArmorSetsFramework;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using 物品包.Items;
using 物品包.配置;
using static 物品包.Items.类型_物品包;

namespace 物品包.玩家;



public class 类型_玩家_护甲包 : 类型_玩家_缓存包_额外缓存<类型_护甲包, 类型_护甲组合> {

    public override 枚举_物品包类型 缓存包标识 => 枚举_物品包类型.护甲包;

    public 类型_配置_护甲包 配置 = new();

    internal override void 脏标记更新_额外缓存() { base.脏标记更新_额外缓存(); 注册护甲(); }

    public void 注册护甲() { Player.GetModPlayer<护甲玩家>().护甲管理器.注册( "物品包", 缓存列表_额外缓存, new( 配置.启用单件加成, 配置.启用套装加成 ) ); }

    public override void SyncPlayer( int toWho, int fromWho, bool newPlayer ) { 网络发送( toWho, fromWho ); 配置.网络发送( toWho, fromWho ); }

    public override void 网络发送( int 接收玩家ID, int 发送玩家ID ) {
        ModPacket 网络数据 = Mod.GetPacket();
        网络数据.Write( ( byte ) 类型_物品包模组.枚举_同步操作类型.玩家同步 );
        网络数据.Write( ( byte ) 类型_物品包模组.枚举_同步对象类型.护甲包 );
        网络数据.Write( ( byte ) Player.whoAmI );
        网络数据.Write( 缓存列表_额外缓存.Count );
        foreach ( var 组合 in 缓存列表_额外缓存 ) {
            ItemIO.Send( 组合.头盔, 网络数据, writeStack: false, writeFavorite: false );
            ItemIO.Send( 组合.胸甲, 网络数据, writeStack: false, writeFavorite: false );
            ItemIO.Send( 组合.护腿, 网络数据, writeStack: false, writeFavorite: false );
        }
        网络数据.Send( 接收玩家ID, 发送玩家ID );
    }

    public override void 网络接收( BinaryReader 网络流 ) {
        缓存列表_额外缓存.Clear();
        int 数量 = 网络流.ReadInt32();
        for ( int i = 0; i < 数量; i++ ) {
            Item 头盔 = ItemIO.Receive( 网络流, readStack: false, readFavorite: false );
            Item 胸甲 = ItemIO.Receive( 网络流, readStack: false, readFavorite: false );
            Item 护腿 = ItemIO.Receive( 网络流, readStack: false, readFavorite: false );
            缓存列表_额外缓存.Add( new 类型_护甲组合( 头盔, 胸甲, 护腿 ) );
        }
        注册护甲();
    }
}