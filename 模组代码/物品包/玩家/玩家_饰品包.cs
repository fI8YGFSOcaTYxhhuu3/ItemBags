using System.Runtime.InteropServices;
using Terraria;
using Terraria.ModLoader;
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

}