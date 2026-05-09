using System.ComponentModel;

namespace 物品包.配置;



public class 类型_配置_护甲包 : 类型_配置_物品包 {

    [DefaultValue( 150 )]
    public override int 容量 { get; set; }

    [DefaultValue( 15 )]
    public override int 列数 { get; set; }

    [DefaultValue( 10 )]
    public override int 行数 { get; set; }

}