using System.IO;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using 物品包.Items;
using 物品包.配置;

namespace 物品包.玩家;



public class 类型_玩家_饰品包 : 类型_玩家_缓存包_额外缓存<类型_饰品包, Item> {

    public override 枚举_物品包类型 缓存包标识 => 枚举_物品包类型.饰品包;

    public 类型_配置_饰品包 配置 = new();
    public override void OnEnterWorld() { base.OnEnterWorld(); 配置 = ( 类型_配置_饰品包 ) ModContent.GetInstance<类型_配置_饰品包>().Clone(); }

    public override bool 存在重复( Item 查询饰品 ) {
        foreach ( var 装备 in Player.armor ) if ( 装备.type == 查询饰品.type ) return true;
        return base.存在重复( 查询饰品 );
    }

    public override void UpdateEquips() {
        base.UpdateEquips();
        UpdateEquips_应用效果();
    }

    private void UpdateEquips_应用效果() {
        foreach ( var 饰品 in CollectionsMarshal.AsSpan( 缓存列表_额外缓存 ) ) {
            Player.ApplyEquipFunctional( 饰品, true );
            Player.GrantArmorBenefits( 饰品 );
            if ( 配置.应用视觉效果 ) Player.ApplyEquipVanity( 饰品 );
            if ( 配置.应用前缀效果 ) Player.GrantPrefixBenefits( 饰品 );
            饰品.ModItem?.UpdateEquip( Player );
        }
    }

    public override void UpdateVisibleAccessories() { if ( 配置.应用视觉效果 ) foreach ( var 饰品 in CollectionsMarshal.AsSpan( 缓存列表_额外缓存 ) ) Player.UpdateVisibleAccessory( 13, 饰品, true ); }

    public override void SyncPlayer( int toWho, int fromWho, bool newPlayer ) { 网络发送( toWho, fromWho ); 配置.网络发送( toWho, fromWho ); }

    public override void 网络发送( int 接收玩家ID, int 发送玩家ID ) {
        ModPacket 网络数据 = Mod.GetPacket();
        网络数据.Write( ( byte ) 类型_物品包模组.枚举_同步操作类型.玩家同步 );
        网络数据.Write( ( byte ) 类型_物品包模组.枚举_同步对象类型.饰品包 );
        网络数据.Write( ( byte ) Player.whoAmI );
        网络数据.Write( 缓存列表_额外缓存.Count );
        foreach ( var 饰品 in 缓存列表_额外缓存 ) ItemIO.Send( 饰品, 网络数据, writeStack: false, writeFavorite: false );
        网络数据.Send( 接收玩家ID, 发送玩家ID );
    }

    public override void 网络接收( BinaryReader 网络流 ) {
        缓存列表_额外缓存.Clear();
        int 数量 = 网络流.ReadInt32();
        for ( int i = 0; i < 数量; i++ ) 缓存列表_额外缓存.Add( ItemIO.Receive( 网络流, readStack: false, readFavorite: false ) );
    }

}