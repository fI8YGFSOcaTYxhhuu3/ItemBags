using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using 物品包.界面;
using 物品包.配置;

namespace 物品包.Items;



public class 类型_嵌套包 : 类型_缓存包<类型_物品包> {
    private static Dictionary<枚举_物品包类型, List<类型_物品包>> 缓存字典初始化() {
        var 枚举数组 = Enum.GetValues<枚举_物品包类型>();
        var 返回字典 = new Dictionary<枚举_物品包类型, List<类型_物品包>>( 枚举数组.Length );
        foreach ( var 枚举 in 枚举数组 ) 返回字典[ 枚举 ] = [];
        return 返回字典;
    }
    public Dictionary<枚举_物品包类型, List<类型_物品包>> 缓存字典 = 缓存字典初始化();

    public override ModItem Clone( Item 克隆目标 ) { var 克隆实例 = ( 类型_嵌套包 ) base.Clone( 克隆目标 ); 克隆实例.缓存字典 = 缓存字典初始化(); return 克隆实例; }

    public override 枚举_物品包类型 类型标识 => 枚举_物品包类型.嵌套包;
    public override 类型_配置_嵌套包 配置 => ModContent.GetInstance<类型_配置_嵌套包>();
    public override 类型_包槽位_嵌套 界面槽位( int 索引 ) => new( this, 索引 );
    public override bool 放入许可( Item 物品 ) => 物品.ModItem is 类型_物品包 物品包 && !存在自我嵌套( 物品包 );
    private bool 存在自我嵌套( 类型_物品包 目标包 ) {
        if ( 目标包 is not 类型_嵌套包 目标嵌套包 ) return false;
        if ( 目标嵌套包.ID == ID ) return true;
        目标嵌套包.更新缓存(); foreach ( var 子嵌套包 in 目标嵌套包.缓存字典[ 枚举_物品包类型.嵌套包 ] ) if ( 存在自我嵌套( 子嵌套包 ) ) return true;
        return false;
    }

    protected override void 清空缓存() { base.清空缓存(); foreach ( var 列表 in 缓存字典.Values ) 列表.Clear(); }
    protected override void 建立缓存() { foreach ( var 物品 in 物品矩阵 ) if ( 物品.ModItem is 类型_物品包 物品包 ) 缓存字典[ 物品包.类型标识 ].Add( 物品包 ); }
}