using MultipleArmorSetsFramework;
using Terraria;
using Terraria.ModLoader;
using 物品包.玩家;
using 物品包.界面;
using 物品包.配置;

namespace 物品包.Items;



// 简单重写成员
public partial class 类型_护甲包 : 类型_缓存包_列表<类型_护甲组合> {
    public override 枚举_物品包类型 类型标识 => 枚举_物品包类型.护甲包;
    public override 类型_配置_护甲包 配置 => ModContent.GetInstance<类型_配置_护甲包>();
    public override 类型_玩家_护甲包 玩家 => Main.LocalPlayer.GetModPlayer<类型_玩家_护甲包>();
    public override 类型_包槽位_护甲 界面槽位( int 索引 ) => new( this, 索引 );
    public override bool 放入许可( Item 物品 ) => 物品.headSlot >= 0 || 物品.bodySlot >= 0 || 物品.legSlot >= 0;
}

// 特征重写函数
public partial class 类型_护甲包 : 类型_缓存包_列表<类型_护甲组合> {
    public override void 建立缓存() {
        for ( int i = 0; i + 2 < 物品矩阵.Length; i += 3 ) {
            Item 头盔 = 物品矩阵[ i ]; Item 胸甲 = 物品矩阵[ i + 1 ]; Item 护腿 = 物品矩阵[ i + 2 ];
            if ( !头盔.IsAir || !胸甲.IsAir || !护腿.IsAir ) 缓存数据.Add( new 类型_护甲组合( 头盔, 胸甲, 护腿 ) );
        }
    }
}