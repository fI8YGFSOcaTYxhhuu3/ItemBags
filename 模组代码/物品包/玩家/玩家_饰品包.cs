using System.IO;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using 物品包.Items;
using 物品包.配置;

namespace 物品包.玩家;



public class 类型_玩家_饰品包 : 类型_玩家_缓存包_额外缓存<类型_饰品包, Item> {

    public override bool 条件符合( Item 查询物品 ) => ModContent.GetInstance<类型_配置_饰品包>().允许饰品重复 || !存在重复( 查询物品 );

    public override bool 存在重复( Item 查询饰品 ) {
        foreach ( var 装备 in Player.armor ) if ( 装备.type == 查询饰品.type ) return true;
        return base.存在重复( 查询饰品 );
    }

    public override void UpdateEquips() {
        base.UpdateEquips();
        UpdateEquips_应用效果();
    }

    private void UpdateEquips_应用效果() {
        类型_配置_饰品包 配置 = ModContent.GetInstance<类型_配置_饰品包>();

        foreach ( var 饰品 in CollectionsMarshal.AsSpan( 缓存列表_额外缓存 ) ) {
            Player.ApplyEquipFunctional( 饰品, true );
            Player.GrantArmorBenefits( 饰品 );
            if ( 配置.应用视觉效果 ) Player.ApplyEquipVanity( 饰品 );
            if ( 配置.应用前缀效果 ) Player.GrantPrefixBenefits( 饰品 );
            饰品.ModItem?.UpdateEquip( Player );
        }
    }

    public override void UpdateVisibleAccessories() {
        if ( 缓存列表_缓存包.Count == 0 ) return;

        类型_配置_饰品包 配置 = ModContent.GetInstance<类型_配置_饰品包>();
        if ( !配置.应用视觉效果 ) return;

        foreach ( var 饰品包 in CollectionsMarshal.AsSpan( 缓存列表_缓存包 ) ) foreach ( var 饰品 in CollectionsMarshal.AsSpan( 饰品包.缓存列表 ) ) Player.UpdateVisibleAccessory( 13, 饰品, true );
    }

    public override void 网络发送( int 接收玩家ID, int 发送玩家ID ) {
        ModPacket 网络数据 = Mod.GetPacket();
        网络数据.Write( ( byte ) 类型_物品包模组.枚举_网络信息类型.饰品包同步 );
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