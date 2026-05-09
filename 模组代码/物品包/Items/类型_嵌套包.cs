using Terraria;
using Terraria.ModLoader;
using 物品包.界面;
using 物品包.配置;

namespace 物品包.Items;



public class 类型_嵌套包 : 类型_缓存包_物品 {

    public override 类型_配置_嵌套包 配置 => ModContent.GetInstance<类型_配置_嵌套包>();
    
    public override 类型_包槽位_嵌套 界面槽位( int 索引 ) => new( this, 索引 );

    public override bool 放入许可( Item 物品 ) {
        if ( 物品.ModItem is not 类型_物品包 待放入包 ) return false;
        if ( 存在循环引用( 待放入包 ) ) return false;
        return true;
    }

    public override bool 缓存许可( Item 物品 ) => 物品.ModItem is 类型_嵌套包;

    private bool 存在循环引用( 类型_物品包 目标包 ) {
        if ( 目标包 is not 类型_缓存包_物品 目标缓存包 ) return false;
        if ( 目标缓存包.ID == ID ) return true;

        目标缓存包.更新缓存();
        foreach ( var 子物品 in 目标缓存包.缓存列表 ) {
            if ( 子物品.ModItem is not 类型_物品包 子包 ) continue;
            if ( 存在循环引用( 子包 ) ) return true;
        }

        return false;
    }

}