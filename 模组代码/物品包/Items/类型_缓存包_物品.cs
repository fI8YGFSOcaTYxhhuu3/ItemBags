using Terraria;
using 物品包.界面;

namespace 物品包.Items;



public abstract class 类型_缓存包_物品 : 类型_缓存包<Item> {

    public override 类型_包槽位_物品缓存 界面槽位( int 索引 ) => new( this, 索引 );

    public virtual bool 缓存许可( Item 物品 ) => 放入许可( 物品 );

    protected override void 扫描矩阵() { foreach ( var 物品 in 物品矩阵 ) if( 缓存许可( 物品 ) ) 缓存列表.Add( 物品 ); }

}