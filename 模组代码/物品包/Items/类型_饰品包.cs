using Terraria;
using Terraria.ModLoader;
using 物品包.玩家;
using 物品包.界面;
using 物品包.配置;

namespace 物品包.Items;



public class 类型_饰品包 : 类型_缓存包<Item> {

    public override 枚举_物品包类型 类型标识 => 枚举_物品包类型.饰品包;
    public override 类型_配置_饰品包 配置 => ModContent.GetInstance<类型_配置_饰品包>();
    public override 类型_玩家_饰品包 玩家 => Main.LocalPlayer.GetModPlayer<类型_玩家_饰品包>();
    public override 类型_包槽位_饰品 界面槽位( int 索引 ) => new( this, 索引 );

    public override bool 放入许可( Item 物品 ) => 物品.accessory && 物品.headSlot < 0 && 物品.bodySlot < 0 && 物品.legSlot < 0;

    protected override void 建立缓存() { foreach ( var 物品 in 物品矩阵 ) if ( !物品.IsAir ) 缓存列表.Add( 物品 ); }

}